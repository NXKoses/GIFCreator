using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing.Imaging;
using static GIFCreator.SelectMenu;

namespace GIFCreator
{
    public partial class Form1 : Form
    {
        public static List<Image>? images;
        public string? inputpath;
        public string? outputpath;
        private int convertframecount;
        private bool ispreviewbuffer = false;
        private bool HalfFps = true;

        public Form1()
        {
            InitializeComponent();
            ConvertingMode(false);
            previewEnable(false);
            previewPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        /// <summary>
        /// �t���[���̏W�܂��GIF�ɂ����
        /// </summary>
        /// <param name="fileName">�ۑ���Ɩ��O</param>
        /// <param name="baseImages">�t���[�����Ƃ�List</param>
        /// <param name="delayTime">�P�R�}�P�R�}�̑���</param>
        /// <param name="loopCount"></param>
        /// <returns>1�����s 0������</returns>
        public async Task<int> CreateAnimationGIF(string fileName, List<Image> baseImages, UInt16 delayTime, UInt16 loopCount)
        {
            //�������ݐ�̃t�@�C�����J��
            using FileStream writerFs = new(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            //BinaryWriter�ŏ�������
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
                        //�摜��GIF�ɕϊ����āAMemoryStream�ɓ����
                        image.Save(ms, ImageFormat.Gif);
                        ms.Position = 0;

                        if (foreachloopsCount == 0)
                        {
                            //�w�b�_����������
                            //Header
                            writer.Write(ReadBytes(ms, 6));

                            //Logical Screen Descriptor
                            byte[] screenDescriptor = ReadBytes(ms, 7);
                            //Global Color Table�����邩�m�F
                            if ((screenDescriptor[4] & 0x80) != 0)
                            {
                                //Color Table�̃T�C�Y���擾
                                colorTableSize = screenDescriptor[4] & 0x07;
                                hasGlobalColorTable = true;
                            }
                            else
                            {
                                hasGlobalColorTable = false;
                            }
                            //Global Color Table���g��Ȃ�
                            //�L��z�F�\�t���O�ƍL��z�F�\�̐��@������
                            screenDescriptor[4] = (byte)(screenDescriptor[4] & 0x78);
                            writer.Write(screenDescriptor);

                            //Application Extension
                            writer.Write(GetApplicationExtension(loopCount));
                        }
                        else
                        {
                            //Header��Logical Screen Descriptor���X�L�b�v
                            ms.Position += 6 + 7;
                        }

                        byte[]? colorTable = null;
                        if (hasGlobalColorTable)
                        {
                            //Color Table���擾
                            colorTable = ReadBytes(ms, (int)Math.Pow(2, colorTableSize + 1) * 3);
                        }

                        //Graphics Control Extension
                        writer.Write(GetGraphicControlExtension(delayTime));
                        //���Graphics Control Extension���X�L�b�v
                        if (ms.GetBuffer()[ms.Position] == 0x21)
                        {
                            ms.Position += 8;
                        }

                        //Image Descriptor
                        byte[] imageDescriptor = ReadBytes(ms, 10);
                        if (!hasGlobalColorTable)
                        {
                            //Local Color Table�������Ă��邩�m�F
                            if ((imageDescriptor[9] & 0x80) == 0)
                                throw new Exception("Not found color table.");
                            //Color Table�̃T�C�Y���擾
                            colorTableSize = imageDescriptor[9] & 7;
                            //Color Table���擾
                            colorTable = ReadBytes(ms, (int)Math.Pow(2, colorTableSize + 1) * 3);
                        }
                        //����z�F�\�t���O (Local Color Table Flag) �Ƌ���z�F�\�̐��@��ǉ�
                        imageDescriptor[9] = (byte)(imageDescriptor[9] | 0x80 | colorTableSize);
                        writer.Write(imageDescriptor);

                        //Local Color Table����������
                        writer.Write(colorTable);

                        //Image Data���������� (�I�����͏������܂Ȃ�)
                        writer.Write(ReadBytes(ms, (int)(ms.Length - ms.Position - 1)));

                        if (foreachloopsCount == imagesCount - 1)
                        {
                            //�I���� (Trailer)
                            writer.Write((byte)0x3B);
                        }

                        //MemoryStream�����Z�b�g
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

            //��n��
            ms.Close();
            writer.Close();
            writerFs.Close();
            _ = updateaction;
            _ = images;

            GC.Collect();
            return 0;
        }
        /// <summary>
        /// ������P�t���[�����Ƃɉ𑜓x��ϊ����ēW�J�����
        /// </summary>
        /// <param name="videopath">����̃p�X</param>
        /// <param name="width"></param>
        /// <param name="halfframe"></param>
        /// <param name="start_frame">0�͖��w��</param>
        /// <param name="end_frame">0�͖��w��</param>
        /// <returns>1�����s 0������</returns>
        public async Task<int> ConvertMovieFrames(string videoPath, int resizeWidth, bool halfFrame, int startFrame = 0, int endFrame = 0)
        {
            try
            {
                using var videoCapture = new VideoCapture(videoPath);
                if (!videoCapture.IsOpened())
                {
                    throw new Exception("�r�f�I�̓ǂݍ��݂Ɏ��s���܂����B");
                }

                // �摜���X�g�A�i���o�[�A�A�N�V������������
                Action? updateprogressaction = new(UpdateProgressBarAction);
                images?.Clear();
                images = new List<Image>
                {
                    Capacity = halfFrame ? videoCapture.FrameCount : videoCapture.FrameCount / 2
                };
                toolStripProgressBar1.Maximum = videoCapture.FrameCount;

                // �A�X�y�N�g����ێ����ă��T�C�Y���邽�߂̃T�C�Y���v�Z
                int resizeHeight = (int)(videoCapture.FrameHeight * (resizeWidth / (double)videoCapture.FrameWidth));
                resolutionHeihgt_textbox.Text = resizeHeight.ToString();
                var resizeSize = new OpenCvSharp.Size(resizeWidth, resizeHeight);

                // �t���[����ϊ����ă��X�g�ɒǉ�����
                convertframecount = 0;
                await Task.Run(() =>
                {
                    while (videoCapture.IsOpened())
                    {
                        using var frame = new Mat();
                        using var resizedFrame = new Mat();
                        if (videoCapture.Read(frame) & frame.IsContinuous())
                        {
                            if (startFrame > convertframecount & startFrame > 0)
                            {
                                convertframecount++;
                                this.Invoke(updateprogressaction);
                                continue;
                            }

                            if (endFrame == convertframecount & endFrame > 0)
                            {
                                break;
                            }

                            if (convertframecount % 2 == 1 & halfFrame)
                            {
                                convertframecount++;
                                this.Invoke(updateprogressaction);
                                continue;
                            }

                            // �t���[�������T�C�Y
                            Cv2.Resize(frame, resizedFrame, resizeSize);

                            // �r�b�g�}�b�v�ɕϊ�
                            using var bitmapFrame = BitmapConverter.ToBitmap(resizedFrame);

                            // �v���r���[��ʂɕ\��
                            this.Invoke(new Action(() =>
                            {
                                previewPictureBox.Image?.Dispose();
                                previewPictureBox.Image = (Image)bitmapFrame.Clone();
                            }));

                            // �摜�����X�g�ɒǉ�
                            images.Add((Image)bitmapFrame.Clone());

                            // �����������
                            resizedFrame.Release();
                            frame.Release();
                            bitmapFrame.Dispose();

                            this.Invoke(updateprogressaction);
                            convertframecount++;
                        }
                        else
                        {
                            break;
                        }
                    }
                });
                videoCapture.Release();
                GC.Collect();
                return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GC.Collect();
                return 1;
            }
        }
        private void UpdateProgressBarAction()
        {
            toolStripProgressBar1.Value = convertframecount;
        }
        public void SetStatusStrip1(string text)
        {
            toolStripStatusLabel1.Text = text + "  ";
        }
        /// <summary>
        /// �����ǂݍ��ޑO�ɕύX�����Ȃ��Ƃ���𐧌䂷��
        /// false = ��\���ɂ���
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
        /// �ϊ��{�^���Ȃǂ��N���b�N�ł��Ȃ��悤�ɂ���
        /// progressbar�̕\���A��\�������˂Ă܂�
        /// true = ��\��
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
                HalfFrame_checkBox.Enabled = false;
                resolution_groupbox.Enabled = false;
                groupBox2.Enabled = false;

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
                HalfFrame_checkBox.Enabled = true;
                resolution_groupbox.Enabled = true;
                groupBox2.Enabled = true;
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (!ispreviewbuffer) return;

            using var g = previewPictureBox.CreateGraphics();
            g.DrawImage(images[trackBar1.Value], 0, 0, previewPictureBox.Width, previewPictureBox.Height);
            nowframe_label.Text = trackBar1.Value.ToString();
        }
        private async void convert_button_Click(object sender, EventArgs e)//�ϊ����s�{�^��
        {
            if (inputpath_textbox.Text.Length == 0)
            {
                MessageBox.Show("���͂̓����I�����Ă�������", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (outputpath_textbox.Text.Length == 0)
            {
                MessageBox.Show("�o�͂̃t�@�C����ݒ肵�Ă�������", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (resolutionWidth_textbox.Text.Length == 0)
            {
                MessageBox.Show("�𑜓x��ݒ肵�Ă�������", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (delayTime_textbox.Text.Length == 0)
            {
                MessageBox.Show("�x�����Ԃ�ݒ肵�Ă�������", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (int.Parse(startframe_textBox.Text) >= int.Parse(endframe_textBox.Text) & int.Parse(startframe_textBox.Text) + int.Parse(endframe_textBox.Text) != 0)
            {
                MessageBox.Show("�X�^�[�g�̒l�̓G���h�����傫���ł��܂���", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ConvertingMode(true);
            SetStatusStrip1("�����W�J���Ă��܂�");
            //������P�t���[�����ɓW�J
            if (HalfFps)
            {
                //HalfFPS�̎��̓t���[����{�ɂ��Ȃ��Ƃ��܂��؂���Ȃ��̂œ�{�ɂ��Ď��s
                using Task<int> task = ConvertMovieFrames(inputpath_textbox.Text, int.Parse(resolutionWidth_textbox.Text), HalfFps, int.Parse(startframe_textBox.Text) * 2, int.Parse(endframe_textBox.Text) * 2);
                if (await task == 1)
                {
                    ConvertingMode(false);
                    previewEnable(false);
                    SetStatusStrip1("����W�J�Ɏ��s���܂���");
                    return;
                }
            }
            else
            {
                using Task<int> task = ConvertMovieFrames(inputpath_textbox.Text, int.Parse(resolutionWidth_textbox.Text), HalfFps, int.Parse(startframe_textBox.Text), int.Parse(endframe_textBox.Text));
                if (await task == 1)
                {
                    ConvertingMode(false);
                    previewEnable(false);
                    SetStatusStrip1("����W�J�Ɏ��s���܂���");
                    return;
                }
            }

            //GIF�쐬
            SetStatusStrip1("GIF���쐬���Ă��܂�");
            using Task<int> task2 = CreateAnimationGIF(outputpath_textbox.Text, images, ushort.Parse(delayTime_textbox.Text), 0);
            if (await task2 == 1)
            {
                ConvertingMode(false);
                previewEnable(false);
                SetStatusStrip1("GIF�̍쐬�Ɏ��s���܂���");
                return;
            }

            ConvertingMode(false);
            previewEnable(false);
            SetStatusStrip1("����");
            _ = images;
            ispreviewbuffer = false;
        }
        private async void preview_button_Click(object sender, EventArgs e)//�v���r���[�{�^��
        {
            if (inputpath_textbox.Text.Length == 0)
            {
                MessageBox.Show("���͂̓����I�����Ă�������", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ConvertingMode(true);
            SetStatusStrip1("�����W�J���Ă��܂�");

            using Task<int> task = ConvertMovieFrames(inputpath_textbox.Text, 200, HalfFps, 0, 0);
            if (await task == 1)
            {
                ConvertingMode(false);
                SetStatusStrip1("����W�J�Ɏ��s���܂���");
                return;
            }
            trackBar1.Maximum = images.Count - 1;
            trackBar1.Value = trackBar1.Minimum;
            ispreviewbuffer = true;
            SetStatusStrip1("�����W�J���܂���");
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
        /// MemoryStream�̌��݂̈ʒu����w�肳�ꂽ�T�C�Y�̃o�C�g�z���ǂݎ��
        /// </summary>
        /// <param name="ms">�ǂݎ��MemoryStream</param>
        /// <param name="count">�ǂݎ��o�C�g�̃T�C�Y</param>
        /// <returns>�ǂݎ�ꂽ�o�C�g�z��</returns>
        private static byte[] ReadBytes(MemoryStream ms, int count)
        {
            byte[] bs = new byte[count];
            ms.Read(bs, 0, count);
            return bs;
        }
        /// <summary>
        /// Netscape Application Extension�u���b�N��Ԃ��B
        /// </summary>
        /// <param name="loopCount">�J��Ԃ��񐔁B0�Ŗ����B</param>
        /// <returns>Netscape Application Extension�u���b�N��byte�z��B</returns>
        private static byte[] GetApplicationExtension(UInt16 loopCount)
        {
            byte[] bs = new byte[19];

            //�g�������� (Extension Introducer)
            bs[0] = 0x21;
            //�A�v���P�[�V�����g�����x�� (Application Extension Label)
            bs[1] = 0xFF;
            //�u���b�N���@ (Block Size)
            bs[2] = 0x0B;
            //�A�v���P�[�V�������ʖ� (Application Identifier)
            bs[3] = (byte)'N';
            bs[4] = (byte)'E';
            bs[5] = (byte)'T';
            bs[6] = (byte)'S';
            bs[7] = (byte)'C';
            bs[8] = (byte)'A';
            bs[9] = (byte)'P';
            bs[10] = (byte)'E';
            //�A�v���P�[�V�����m�ؕ��� (Application Authentication Code)
            bs[11] = (byte)'2';
            bs[12] = (byte)'.';
            bs[13] = (byte)'0';
            //�f�[�^���u���b�N���@ (Data Sub-block Size)
            bs[14] = 0x03;
            //�l�ߍ��ݗ� [�l�b�g�X�P�[�v�g���R�[�h (Netscape Extension Code)]
            bs[15] = 0x01;
            //�J��Ԃ��� (Loop Count)
            byte[] loopCountBytes = BitConverter.GetBytes(loopCount);
            bs[16] = loopCountBytes[0];
            bs[17] = loopCountBytes[1];
            //�u���b�N�I���� (Block Terminator)
            bs[18] = 0x00;

            return bs;
        }
        /// <summary>
        /// Graphic Control Extension�u���b�N��Ԃ��B
        /// </summary>
        /// <param name="delayTime">�x�����ԁi100����1�b�P�ʁj�B</param>
        /// <returns>Graphic Control Extension�u���b�N��byte�z��B</returns>
        private static byte[] GetGraphicControlExtension(UInt16 delayTime)
        {
            byte[] bs = new byte[8];

            //�g�������� (Extension Introducer)
            bs[0] = 0x21;
            //�O���t�B�b�N���䃉�x�� (Graphic Control Label)
            bs[1] = 0xF9;
            //�u���b�N���@ (Block Size, Byte Size)
            bs[2] = 0x04;
            //�l�ߍ��ݗ� (Packed Field)
            //���ߐF�w�W���g������+1
            //�������@:���̂܂܎c��+4�A�w�i�F�łԂ�+8�A���O�̉摜�ɖ߂�+12
            bs[3] = 0x00;
            //�x������ (Delay Time)
            byte[] delayTimeBytes = BitConverter.GetBytes(delayTime);
            bs[4] = delayTimeBytes[0];
            bs[5] = delayTimeBytes[1];
            //���ߐF�w�W (Transparency Index, Transparent Color Index)
            bs[6] = 0x00;
            //�u���b�N�I���� (Block Terminator)
            bs[7] = 0x00;

            return bs;
        }
        private void HalfFps_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            HalfFps = HalfFrame_checkBox.Checked;
        }
    }
}