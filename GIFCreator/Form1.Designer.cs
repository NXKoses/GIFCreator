namespace GIFCreator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            convert_button = new Button();
            setting_groupbox = new GroupBox();
            HalfFrame_checkBox = new CheckBox();
            nowframe_label = new Label();
            groupBox3 = new GroupBox();
            preview_button = new Button();
            endframe_textBox = new TextBox();
            startframe_textBox = new TextBox();
            endframesetting_button = new Button();
            startframesetting_button = new Button();
            trackBar1 = new TrackBar();
            groupBox2 = new GroupBox();
            delayTime_textbox = new TextBox();
            previewPictureBox = new PictureBox();
            resolution_groupbox = new GroupBox();
            label1 = new Label();
            resolutionHeihgt_textbox = new TextBox();
            resolutionWidth_textbox = new TextBox();
            input_groupbox = new GroupBox();
            inputpath_textbox = new TextBox();
            inputselect_button = new Button();
            outputselect_button = new Button();
            outputpath_textbox = new TextBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripProgressBar1 = new ToolStripProgressBar();
            groupBox1 = new GroupBox();
            setting_groupbox.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).BeginInit();
            resolution_groupbox.SuspendLayout();
            input_groupbox.SuspendLayout();
            statusStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // convert_button
            // 
            convert_button.Location = new Point(593, 12);
            convert_button.Name = "convert_button";
            convert_button.Size = new Size(96, 464);
            convert_button.TabIndex = 0;
            convert_button.Text = "変換";
            convert_button.UseVisualStyleBackColor = true;
            convert_button.Click += convert_button_Click;
            // 
            // setting_groupbox
            // 
            setting_groupbox.Controls.Add(HalfFrame_checkBox);
            setting_groupbox.Controls.Add(nowframe_label);
            setting_groupbox.Controls.Add(groupBox3);
            setting_groupbox.Controls.Add(groupBox2);
            setting_groupbox.Controls.Add(previewPictureBox);
            setting_groupbox.Controls.Add(resolution_groupbox);
            setting_groupbox.Location = new Point(12, 12);
            setting_groupbox.Name = "setting_groupbox";
            setting_groupbox.Size = new Size(575, 332);
            setting_groupbox.TabIndex = 1;
            setting_groupbox.TabStop = false;
            setting_groupbox.Text = "設定";
            // 
            // HalfFrame_checkBox
            // 
            HalfFrame_checkBox.AutoSize = true;
            HalfFrame_checkBox.Checked = true;
            HalfFrame_checkBox.CheckState = CheckState.Checked;
            HalfFrame_checkBox.Location = new Point(6, 153);
            HalfFrame_checkBox.Name = "HalfFrame_checkBox";
            HalfFrame_checkBox.Size = new Size(154, 19);
            HalfFrame_checkBox.TabIndex = 5;
            HalfFrame_checkBox.Text = "フレームを半分に間引きする";
            HalfFrame_checkBox.UseVisualStyleBackColor = true;
            HalfFrame_checkBox.CheckedChanged += HalfFps_checkBox_CheckedChanged;
            // 
            // nowframe_label
            // 
            nowframe_label.AutoSize = true;
            nowframe_label.Location = new Point(251, 175);
            nowframe_label.Name = "nowframe_label";
            nowframe_label.Size = new Size(0, 15);
            nowframe_label.TabIndex = 4;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(preview_button);
            groupBox3.Controls.Add(endframe_textBox);
            groupBox3.Controls.Add(startframe_textBox);
            groupBox3.Controls.Add(endframesetting_button);
            groupBox3.Controls.Add(startframesetting_button);
            groupBox3.Controls.Add(trackBar1);
            groupBox3.Location = new Point(6, 196);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(563, 130);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "開始フレーム・終了フレーム";
            // 
            // preview_button
            // 
            preview_button.Location = new Point(472, 12);
            preview_button.Name = "preview_button";
            preview_button.Size = new Size(85, 23);
            preview_button.TabIndex = 6;
            preview_button.Text = "プレビューする";
            preview_button.UseVisualStyleBackColor = true;
            preview_button.Click += preview_button_Click;
            // 
            // endframe_textBox
            // 
            endframe_textBox.Location = new Point(338, 50);
            endframe_textBox.Name = "endframe_textBox";
            endframe_textBox.Size = new Size(112, 23);
            endframe_textBox.TabIndex = 5;
            endframe_textBox.Text = "0";
            endframe_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // startframe_textBox
            // 
            startframe_textBox.Location = new Point(108, 50);
            startframe_textBox.Name = "startframe_textBox";
            startframe_textBox.Size = new Size(116, 23);
            startframe_textBox.TabIndex = 4;
            startframe_textBox.Text = "0";
            startframe_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // endframesetting_button
            // 
            endframesetting_button.Location = new Point(338, 25);
            endframesetting_button.Name = "endframesetting_button";
            endframesetting_button.Size = new Size(112, 23);
            endframesetting_button.TabIndex = 2;
            endframesetting_button.Text = "終了フレームを設定";
            endframesetting_button.UseVisualStyleBackColor = true;
            endframesetting_button.Click += endframesetting_button_Click;
            // 
            // startframesetting_button
            // 
            startframesetting_button.Location = new Point(108, 25);
            startframesetting_button.Name = "startframesetting_button";
            startframesetting_button.Size = new Size(116, 23);
            startframesetting_button.TabIndex = 1;
            startframesetting_button.Text = "開始フレームを設定";
            startframesetting_button.UseVisualStyleBackColor = true;
            startframesetting_button.Click += startframesetting_button_Click;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(6, 79);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(551, 45);
            trackBar1.TabIndex = 0;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(delayTime_textbox);
            groupBox2.Location = new Point(6, 90);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(215, 57);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "1コマの遅延(2~8)";
            // 
            // delayTime_textbox
            // 
            delayTime_textbox.Location = new Point(60, 22);
            delayTime_textbox.Name = "delayTime_textbox";
            delayTime_textbox.Size = new Size(90, 23);
            delayTime_textbox.TabIndex = 0;
            delayTime_textbox.Text = "5";
            delayTime_textbox.TextAlign = HorizontalAlignment.Center;
            // 
            // previewPictureBox
            // 
            previewPictureBox.BackColor = SystemColors.ActiveBorder;
            previewPictureBox.Location = new Point(292, 12);
            previewPictureBox.Name = "previewPictureBox";
            previewPictureBox.Size = new Size(277, 178);
            previewPictureBox.TabIndex = 1;
            previewPictureBox.TabStop = false;
            // 
            // resolution_groupbox
            // 
            resolution_groupbox.Controls.Add(label1);
            resolution_groupbox.Controls.Add(resolutionHeihgt_textbox);
            resolution_groupbox.Controls.Add(resolutionWidth_textbox);
            resolution_groupbox.Location = new Point(6, 22);
            resolution_groupbox.Name = "resolution_groupbox";
            resolution_groupbox.Size = new Size(215, 62);
            resolution_groupbox.TabIndex = 0;
            resolution_groupbox.TabStop = false;
            resolution_groupbox.Text = "解像度";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(76, 25);
            label1.Name = "label1";
            label1.Size = new Size(14, 15);
            label1.TabIndex = 2;
            label1.Text = "X";
            // 
            // resolutionHeihgt_textbox
            // 
            resolutionHeihgt_textbox.Location = new Point(96, 22);
            resolutionHeihgt_textbox.Name = "resolutionHeihgt_textbox";
            resolutionHeihgt_textbox.ReadOnly = true;
            resolutionHeihgt_textbox.Size = new Size(113, 23);
            resolutionHeihgt_textbox.TabIndex = 1;
            resolutionHeihgt_textbox.Text = "(Widthに合わせます)";
            resolutionHeihgt_textbox.TextAlign = HorizontalAlignment.Center;
            // 
            // resolutionWidth_textbox
            // 
            resolutionWidth_textbox.Location = new Point(6, 22);
            resolutionWidth_textbox.Name = "resolutionWidth_textbox";
            resolutionWidth_textbox.Size = new Size(64, 23);
            resolutionWidth_textbox.TabIndex = 0;
            resolutionWidth_textbox.Text = "1280";
            resolutionWidth_textbox.TextAlign = HorizontalAlignment.Center;
            // 
            // input_groupbox
            // 
            input_groupbox.Controls.Add(inputpath_textbox);
            input_groupbox.Controls.Add(inputselect_button);
            input_groupbox.Location = new Point(12, 350);
            input_groupbox.Name = "input_groupbox";
            input_groupbox.Size = new Size(575, 58);
            input_groupbox.TabIndex = 2;
            input_groupbox.TabStop = false;
            input_groupbox.Text = "入力";
            // 
            // inputpath_textbox
            // 
            inputpath_textbox.Location = new Point(114, 22);
            inputpath_textbox.Name = "inputpath_textbox";
            inputpath_textbox.ReadOnly = true;
            inputpath_textbox.Size = new Size(455, 23);
            inputpath_textbox.TabIndex = 1;
            // 
            // inputselect_button
            // 
            inputselect_button.Location = new Point(6, 21);
            inputselect_button.Name = "inputselect_button";
            inputselect_button.Size = new Size(102, 23);
            inputselect_button.TabIndex = 0;
            inputselect_button.Text = "選択";
            inputselect_button.UseVisualStyleBackColor = true;
            inputselect_button.Click += inputselect_button_Click;
            // 
            // outputselect_button
            // 
            outputselect_button.Location = new Point(6, 22);
            outputselect_button.Name = "outputselect_button";
            outputselect_button.Size = new Size(102, 23);
            outputselect_button.TabIndex = 3;
            outputselect_button.Text = "選択";
            outputselect_button.UseVisualStyleBackColor = true;
            outputselect_button.Click += outputselect_button_Click;
            // 
            // outputpath_textbox
            // 
            outputpath_textbox.Location = new Point(114, 23);
            outputpath_textbox.Name = "outputpath_textbox";
            outputpath_textbox.ReadOnly = true;
            outputpath_textbox.Size = new Size(455, 23);
            outputpath_textbox.TabIndex = 4;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripProgressBar1 });
            statusStrip1.Location = new Point(0, 479);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(701, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(0, 17);
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(100, 16);
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(outputselect_button);
            groupBox1.Controls.Add(outputpath_textbox);
            groupBox1.Location = new Point(12, 414);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(575, 62);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "出力";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(701, 501);
            Controls.Add(groupBox1);
            Controls.Add(statusStrip1);
            Controls.Add(input_groupbox);
            Controls.Add(setting_groupbox);
            Controls.Add(convert_button);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "超GIFCreator0.2";
            setting_groupbox.ResumeLayout(false);
            setting_groupbox.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).EndInit();
            resolution_groupbox.ResumeLayout(false);
            resolution_groupbox.PerformLayout();
            input_groupbox.ResumeLayout(false);
            input_groupbox.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button convert_button;
        private GroupBox setting_groupbox;
        private GroupBox input_groupbox;
        private TextBox inputpath_textbox;
        private Button inputselect_button;
        private Button outputselect_button;
        private TextBox outputpath_textbox;
        private GroupBox resolution_groupbox;
        private Label label1;
        private TextBox resolutionHeihgt_textbox;
        private TextBox resolutionWidth_textbox;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripProgressBar toolStripProgressBar1;
        private PictureBox previewPictureBox;
        private GroupBox groupBox1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private GroupBox groupBox2;
        private TextBox delayTime_textbox;
        private GroupBox groupBox3;
        private Button endframesetting_button;
        private Button startframesetting_button;
        private TextBox endframe_textBox;
        private TextBox startframe_textBox;
        private Button preview_button;
        private Label nowframe_label;
        private CheckBox HalfFrame_checkBox;
        public TrackBar trackBar1;
    }
}