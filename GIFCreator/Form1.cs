using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing.Imaging;
using static GIFCreator.SelectMenu;

namespace GIFCreator
{
    public partial class Form1 : Form
    {
        public List<Image>? images;
        public string? inputpath;
        public string? outputpath;
        private Bitmap previewbitmap;
        private int convertframecount;
        private bool ispreviewbuffer = false;
        private bool HalfFps = true;

        public Form1()
        {
            InitializeComponent();
            ConvertingMode(false);
            previewEnable(false);
        }
        /// <summary>
        /// フレームの集まりをGIFにするよ
        /// </summary>
        /// <param name="fileName">保存先と名前</param>
        /// <param name="baseImages">フレームごとのList</param>
        /// <param name="delayTime">１コマ１コマの速さ</param>
        /// <param name="loopCount"></param>
        /// <returns>1が失敗 0が成功</returns>
        public async Task<int> CreateAnimationGIF(string fileName, List<Image> baseImages, UInt16 delayTime, UInt16 loopCount)
        {
            //書き込み先のファイルを開く
            using FileStream writerFs = new(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            //BinaryWriterで書き込む
            using BinaryWriter writer = new(writerFs);

            using MemoryStream ms = new();
            bool hasGlobalColorTable = false;
            int colorTableSize = 0;

            int imagesCount = baseImages.Count;
            int foreachloopsCount = 0;
            toolStripProgressBar1.Maximum = imagesCount;
            Action? updateaction = new(UpdateProgressBarAction);
            try
            {
                await Task.Run(() =>
                {
                    foreach (Bitmap image in baseImages.Cast<Bitmap>())
                    {
                        //画像をGIFに変換して、MemoryStreamに入れる
                        image.Save(ms, ImageFormat.Gif);
                        ms.Position = 0;

                        if (foreachloopsCount == 0)
                        {
                            //ヘッダを書き込む
                            //Header
                            writer.Write(ReadBytes(ms, 6));

                            //Logical Screen Descriptor
                            byte[] screenDescriptor = ReadBytes(ms, 7);
                            //Global Color Tableがあるか確認
                            if ((screenDescriptor[4] & 0x80) != 0)
                            {
                                //Color Tableのサイズを取得
                                colorTableSize = screenDescriptor[4] & 0x07;
                                hasGlobalColorTable = true;
                            }
                            else
                            {
                                hasGlobalColorTable = false;
                            }
                            //Global Color Tableを使わない
                            //広域配色表フラグと広域配色表の寸法を消す
                            screenDescriptor[4] = (byte)(screenDescriptor[4] & 0x78);
                            writer.Write(screenDescriptor);

                            //Application Extension
                            writer.Write(GetApplicationExtension(loopCount));
                        }
                        else
                        {
                            //HeaderとLogical Screen Descriptorをスキップ
                            ms.Position += 6 + 7;
                        }

                        byte[]? colorTable = null;
                        if (hasGlobalColorTable)
                        {
                            //Color Tableを取得
                            colorTable = ReadBytes(ms, (int)Math.Pow(2, colorTableSize + 1) * 3);
                        }

                        //Graphics Control Extension
                        writer.Write(GetGraphicControlExtension(delayTime));
                        //基のGraphics Control Extensionをスキップ
                        if (ms.GetBuffer()[ms.Position] == 0x21)
                        {
                            ms.Position += 8;
                        }

                        //Image Descriptor
                        byte[] imageDescriptor = ReadBytes(ms, 10);
                        if (!hasGlobalColorTable)
                        {
                            //Local Color Tableを持っているか確認
                            if ((imageDescriptor[9] & 0x80) == 0)
                                throw new Exception("Not found color table.");
                            //Color Tableのサイズを取得
                            colorTableSize = imageDescriptor[9] & 7;
                            //Color Tableを取得
                            colorTable = ReadBytes(ms, (int)Math.Pow(2, colorTableSize + 1) * 3);
                        }
                        //狭域配色表フラグ (Local Color Table Flag) と狭域配色表の寸法を追加
                        imageDescriptor[9] = (byte)(imageDescriptor[9] | 0x80 | colorTableSize);
                        writer.Write(imageDescriptor);

                        //Local Color Tableを書き込む
                        writer.Write(colorTable);

                        //Image Dataを書き込む (終了部は書き込まない)
                        writer.Write(ReadBytes(ms, (int)(ms.Length - ms.Position - 1)));

                        if (foreachloopsCount == imagesCount - 1)
                        {
                            //終了部 (Trailer)
                            writer.Write((byte)0x3B);
                        }

                        //MemoryStreamをリセット
                        ms.SetLength(0);

                        convertframecount = foreachloopsCount;
                        this.Invoke(updateaction);
                        foreachloopsCount++;
                    }
                });
            }
            catch
            {
                return 1;
            }

            //後始末
            ms.Close();
            writer.Close();
            writerFs.Close();
            _ = updateaction;
            _ = images;

            GC.Collect();
            return 0;
        }
        /// <summary>
        /// 動画を１フレームごとに解像度を変換して展開するよ
        /// </summary>
        /// <param name="videopath">動画のパス</param>
        /// <param name="width"></param>
        /// <param name="halfframe"></param>
        /// <param name="start_frame">0は無指定</param>
        /// <param name="end_frame">0は無指定</param>
        /// <returns>1が失敗 0が成功</returns>
        public async Task<int> ConvertMovieFrame(string videopath, int width, bool halfframe, int start_frame = 0, int end_frame = 0)
        {
            images?.Clear();

            using VideoCapture vcap = new(videopath);

            //アスペクト比を維持してHeightを設定
            int resizeHeight = (int)(vcap.FrameHeight * (width / (double)vcap.FrameWidth));
            resolutionHeihgt_textbox.Text = resizeHeight.ToString();
            OpenCvSharp.Size cv2_size = new OpenCvSharp.Size(width, resizeHeight);

            //フレームカウントの準備、imagesの準備
            int maxframe = vcap.FrameCount;
            toolStripProgressBar1.Maximum = maxframe;

            if (halfframe) maxframe = vcap.FrameCount / 2;
            images = new List<Image>(maxframe); // フレーム分作成
            convertframecount = 0;

            Action? previewaction = new(BitmapPreviewAction);
            Action? updateaction = new(UpdateProgressBarAction);
            try
            {
                await Task.Run(() =>
                {
                    while (vcap.IsOpened())
                    {
                        using Mat mat = new();
                        using Mat resizematframe = new();
                        if (vcap.Read(mat) && mat.IsContinuous())
                        {
                            if (start_frame > convertframecount && 0 != start_frame)
                            {
                                convertframecount++;
                                this.Invoke(updateaction);
                                continue;
                            }

                            if (end_frame == convertframecount && 0 != end_frame)
                            {
                                break;
                            }

                            if (convertframecount % 2 == 1 && halfframe)
                            {
                                convertframecount++;
                                this.Invoke(updateaction);
                                continue;
                            }

                            //画像変換
                            Cv2.Resize(mat, resizematframe, cv2_size);

                            //Cv から　bitmapに変換   *こいつをDisposeするとダメ
                            Bitmap resizebitmapframe = new(BitmapConverter.ToBitmap(resizematframe));

                            //Formに変換中のフレームの画像を表示する
                            previewbitmap = resizebitmapframe;
                            this.Invoke(previewaction);

                            //追加
                            images.Add(resizebitmapframe);

                            resizematframe.Release();
                            mat.Release();

                            this.Invoke(updateaction);
                        }
                        else
                        {
                            break;
                        }
                        convertframecount++;
                    }
                    vcap.Release();
                    GC.Collect();
                });

                return 0;
            }
            catch
            {
                return 1;
            }
        }
        private void UpdateProgressBarAction()
        {
            toolStripProgressBar1.Value = convertframecount;
        }
        private void BitmapPreviewAction()
        {
            using var g = pictureBox1.CreateGraphics();
            g.DrawImage(previewbitmap, 0, 0, pictureBox1.Width, pictureBox1.Height);
        }
        public void SetStatusStrip1(string text)
        {
            toolStripStatusLabel1.Text = text + "  ";
        }
        /// <summary>
        /// 動画を読み込む前に変更いけないところを制御する
        /// false = 非表示にする
        /// </summary>
        /// <param name="enable"></param>
        public void previewEnable(bool enable)
        {
            if (!enable)
            {
                startframesetting_button.Enabled = false;
                endframesetting_button.Enabled = false;
                trackBar1.Enabled = false;
                startframe_textBox.Enabled = false;
                endframe_textBox.Enabled = false;
            }
        }
        /// <summary>
        /// 変換ボタンなどをクリックできないようにする
        /// progressbarの表示、非表示も兼ねてます
        /// true = 非表示
        /// </summary>
        /// <param name="converting"></param>
        public void ConvertingMode(bool converting)
        {
            if (converting)
            {
                convert_button.Enabled = false;
                inputselect_button.Enabled = false;
                outputselect_button.Enabled = false;
                trackBar1.Enabled = false;
                startframe_textBox.Enabled = false;
                endframe_textBox.Enabled = false;
                startframesetting_button.Enabled = false;
                endframesetting_button.Enabled = false;
                preview_button.Enabled = false;
                toolStripProgressBar1.Visible = true;

            }
            else
            {
                convert_button.Enabled = true;
                inputselect_button.Enabled = true;
                outputselect_button.Enabled = true;
                trackBar1.Enabled = true;
                startframe_textBox.Enabled = true;
                endframe_textBox.Enabled = true;
                startframesetting_button.Enabled = true;
                endframesetting_button.Enabled = true;
                preview_button.Enabled = true;
                toolStripProgressBar1.Visible = true;
                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Visible = false;
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (!ispreviewbuffer) return;

            using var g = pictureBox1.CreateGraphics();
            g.DrawImage(images[trackBar1.Value], 0, 0, pictureBox1.Width, pictureBox1.Height);
            nowframe_label.Text = trackBar1.Value.ToString();
        }
        private async void convert_button_Click(object sender, EventArgs e)//変換実行ボタン
        {
            if (inputpath_textbox.Text.Length == 0)
            {
                MessageBox.Show("入力の動画を選択してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (outputpath_textbox.Text.Length == 0)
            {
                MessageBox.Show("出力のファイルを設定してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (resolutionWidth_textbox.Text.Length == 0)
            {
                MessageBox.Show("解像度を設定してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (delayTime_textbox.Text.Length == 0)
            {
                MessageBox.Show("遅延時間を設定してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (int.Parse(startframe_textBox.Text) >= int.Parse(endframe_textBox.Text) & int.Parse(startframe_textBox.Text) + int.Parse(endframe_textBox.Text) != 0)
            {
                MessageBox.Show("スタートの値はエンドよりも大きくできません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ConvertingMode(true);
            SetStatusStrip1("動画を展開しています");
            //動画を１フレームずつに展開
            if (HalfFps)
            {
                //HalfFPSの時はフレームを倍にしないとうまく切り取れないので二倍にして実行
                using Task<int> task = ConvertMovieFrame(inputpath_textbox.Text, int.Parse(resolutionWidth_textbox.Text), HalfFps, int.Parse(startframe_textBox.Text) * 2, int.Parse(endframe_textBox.Text) * 2);
                if (await task == 1)
                {
                    ConvertingMode(false);
                    SetStatusStrip1("動画展開に失敗しました");
                    return;
                }
            }
            else
            {
                using Task<int> task = ConvertMovieFrame(inputpath_textbox.Text, int.Parse(resolutionWidth_textbox.Text), HalfFps, int.Parse(startframe_textBox.Text), int.Parse(endframe_textBox.Text));
                if (await task == 1)
                {
                    ConvertingMode(false);
                    SetStatusStrip1("動画展開に失敗しました");
                    return;
                }
            }

            //GIF作成
            SetStatusStrip1("GIFを作成しています");
            using Task<int> task2 = CreateAnimationGIF(outputpath_textbox.Text, images, ushort.Parse(delayTime_textbox.Text), 0);
            if (await task2 == 1)
            {
                ConvertingMode(false);
                SetStatusStrip1("GIFの作成に失敗しました");
                return;
            }

            ConvertingMode(false);
            SetStatusStrip1("完了");
            images = null;
            ispreviewbuffer = false;
        }
        private async void preview_button_Click(object sender, EventArgs e)//プレビューボタン
        {
            if (inputpath_textbox.Text.Length == 0)
            {
                MessageBox.Show("入力の動画を選択してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ConvertingMode(true);
            SetStatusStrip1("動画を展開しています");

            using Task<int> task = ConvertMovieFrame(inputpath_textbox.Text, 200, HalfFps, 0, 0);
            if (await task == 1)
            {
                ConvertingMode(false);
                SetStatusStrip1("動画展開に失敗しました");
                return;
            }
            trackBar1.Maximum = images.Count - 1;
            trackBar1.Value = trackBar1.Minimum;
            ispreviewbuffer = true;
            SetStatusStrip1("動画を展開しました");
            ConvertingMode(false);
        }
        private void startframesetting_button_Click(object sender, EventArgs e)
        {
            startframe_textBox.Text = trackBar1.Value.ToString();
        }
        private void endframesetting_button_Click(object sender, EventArgs e)
        {
            endframe_textBox.Text = trackBar1.Value.ToString();
        }
        private void inputselect_button_Click(object sender, EventArgs e)
        {
            inputpath = FileOpenDialog();
            inputpath_textbox.Text = inputpath;
        }
        private void outputselect_button_Click(object sender, EventArgs e)
        {
            outputpath = FileOpenDialog(false);
            outputpath_textbox.Text = outputpath + ".gif";
        }
        /// <summary>
        /// MemoryStreamの現在の位置から指定されたサイズのバイト配列を読み取る
        /// </summary>
        /// <param name="ms">読み取るMemoryStream</param>
        /// <param name="count">読み取るバイトのサイズ</param>
        /// <returns>読み取れたバイト配列</returns>
        private static byte[] ReadBytes(MemoryStream ms, int count)
        {
            byte[] bs = new byte[count];
            ms.Read(bs, 0, count);
            return bs;
        }
        /// <summary>
        /// Netscape Application Extensionブロックを返す。
        /// </summary>
        /// <param name="loopCount">繰り返す回数。0で無限。</param>
        /// <returns>Netscape Application Extensionブロックのbyte配列。</returns>
        private static byte[] GetApplicationExtension(UInt16 loopCount)
        {
            byte[] bs = new byte[19];

            //拡張導入符 (Extension Introducer)
            bs[0] = 0x21;
            //アプリケーション拡張ラベル (Application Extension Label)
            bs[1] = 0xFF;
            //ブロック寸法 (Block Size)
            bs[2] = 0x0B;
            //アプリケーション識別名 (Application Identifier)
            bs[3] = (byte)'N';
            bs[4] = (byte)'E';
            bs[5] = (byte)'T';
            bs[6] = (byte)'S';
            bs[7] = (byte)'C';
            bs[8] = (byte)'A';
            bs[9] = (byte)'P';
            bs[10] = (byte)'E';
            //アプリケーション確証符号 (Application Authentication Code)
            bs[11] = (byte)'2';
            bs[12] = (byte)'.';
            bs[13] = (byte)'0';
            //データ副ブロック寸法 (Data Sub-block Size)
            bs[14] = 0x03;
            //詰め込み欄 [ネットスケープ拡張コード (Netscape Extension Code)]
            bs[15] = 0x01;
            //繰り返し回数 (Loop Count)
            byte[] loopCountBytes = BitConverter.GetBytes(loopCount);
            bs[16] = loopCountBytes[0];
            bs[17] = loopCountBytes[1];
            //ブロック終了符 (Block Terminator)
            bs[18] = 0x00;

            return bs;
        }
        /// <summary>
        /// Graphic Control Extensionブロックを返す。
        /// </summary>
        /// <param name="delayTime">遅延時間（100分の1秒単位）。</param>
        /// <returns>Graphic Control Extensionブロックのbyte配列。</returns>
        private static byte[] GetGraphicControlExtension(UInt16 delayTime)
        {
            byte[] bs = new byte[8];

            //拡張導入符 (Extension Introducer)
            bs[0] = 0x21;
            //グラフィック制御ラベル (Graphic Control Label)
            bs[1] = 0xF9;
            //ブロック寸法 (Block Size, Byte Size)
            bs[2] = 0x04;
            //詰め込み欄 (Packed Field)
            //透過色指標を使う時は+1
            //消去方法:そのまま残す+4、背景色でつぶす+8、直前の画像に戻す+12
            bs[3] = 0x00;
            //遅延時間 (Delay Time)
            byte[] delayTimeBytes = BitConverter.GetBytes(delayTime);
            bs[4] = delayTimeBytes[0];
            bs[5] = delayTimeBytes[1];
            //透過色指標 (Transparency Index, Transparent Color Index)
            bs[6] = 0x00;
            //ブロック終了符 (Block Terminator)
            bs[7] = 0x00;

            return bs;
        }
        private void HalfFps_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            HalfFps = HalfFrame_checkBox.Checked;
        }
    }
}