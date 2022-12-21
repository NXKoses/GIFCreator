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
            this.convert_button = new System.Windows.Forms.Button();
            this.setting_groupbox = new System.Windows.Forms.GroupBox();
            this.HalfFps_checkBox = new System.Windows.Forms.CheckBox();
            this.nowframe_label = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.preview_button = new System.Windows.Forms.Button();
            this.endframe_textBox = new System.Windows.Forms.TextBox();
            this.startframe_textBox = new System.Windows.Forms.TextBox();
            this.endframesetting_button = new System.Windows.Forms.Button();
            this.startframesetting_button = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.delayTime_textbox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.resolution_groupbox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.resolutionHeihgt_textbox = new System.Windows.Forms.TextBox();
            this.resolutionWidth_textbox = new System.Windows.Forms.TextBox();
            this.input_groupbox = new System.Windows.Forms.GroupBox();
            this.inputpath_textbox = new System.Windows.Forms.TextBox();
            this.inputselect_button = new System.Windows.Forms.Button();
            this.outputselect_button = new System.Windows.Forms.Button();
            this.outputpath_textbox = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.setting_groupbox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.resolution_groupbox.SuspendLayout();
            this.input_groupbox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // convert_button
            // 
            this.convert_button.Location = new System.Drawing.Point(593, 12);
            this.convert_button.Name = "convert_button";
            this.convert_button.Size = new System.Drawing.Size(96, 464);
            this.convert_button.TabIndex = 0;
            this.convert_button.Text = "変換";
            this.convert_button.UseVisualStyleBackColor = true;
            this.convert_button.Click += new System.EventHandler(this.convert_button_Click);
            // 
            // setting_groupbox
            // 
            this.setting_groupbox.Controls.Add(this.HalfFps_checkBox);
            this.setting_groupbox.Controls.Add(this.nowframe_label);
            this.setting_groupbox.Controls.Add(this.groupBox3);
            this.setting_groupbox.Controls.Add(this.groupBox2);
            this.setting_groupbox.Controls.Add(this.pictureBox1);
            this.setting_groupbox.Controls.Add(this.resolution_groupbox);
            this.setting_groupbox.Location = new System.Drawing.Point(12, 12);
            this.setting_groupbox.Name = "setting_groupbox";
            this.setting_groupbox.Size = new System.Drawing.Size(575, 332);
            this.setting_groupbox.TabIndex = 1;
            this.setting_groupbox.TabStop = false;
            this.setting_groupbox.Text = "設定";
            // 
            // HalfFps_checkBox
            // 
            this.HalfFps_checkBox.AutoSize = true;
            this.HalfFps_checkBox.Checked = true;
            this.HalfFps_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HalfFps_checkBox.Location = new System.Drawing.Point(6, 153);
            this.HalfFps_checkBox.Name = "HalfFps_checkBox";
            this.HalfFps_checkBox.Size = new System.Drawing.Size(154, 19);
            this.HalfFps_checkBox.TabIndex = 5;
            this.HalfFps_checkBox.Text = "フレームを半分に間引きする";
            this.HalfFps_checkBox.UseVisualStyleBackColor = true;
            this.HalfFps_checkBox.CheckedChanged += new System.EventHandler(this.HalfFps_checkBox_CheckedChanged);
            // 
            // nowframe_label
            // 
            this.nowframe_label.AutoSize = true;
            this.nowframe_label.Location = new System.Drawing.Point(263, 175);
            this.nowframe_label.Name = "nowframe_label";
            this.nowframe_label.Size = new System.Drawing.Size(0, 15);
            this.nowframe_label.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.preview_button);
            this.groupBox3.Controls.Add(this.endframe_textBox);
            this.groupBox3.Controls.Add(this.startframe_textBox);
            this.groupBox3.Controls.Add(this.endframesetting_button);
            this.groupBox3.Controls.Add(this.startframesetting_button);
            this.groupBox3.Controls.Add(this.trackBar1);
            this.groupBox3.Location = new System.Drawing.Point(6, 196);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(563, 130);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "開始フレーム・終了フレーム";
            // 
            // preview_button
            // 
            this.preview_button.Location = new System.Drawing.Point(472, 12);
            this.preview_button.Name = "preview_button";
            this.preview_button.Size = new System.Drawing.Size(85, 23);
            this.preview_button.TabIndex = 6;
            this.preview_button.Text = "プレビューする";
            this.preview_button.UseVisualStyleBackColor = true;
            this.preview_button.Click += new System.EventHandler(this.preview_button_Click);
            // 
            // endframe_textBox
            // 
            this.endframe_textBox.Location = new System.Drawing.Point(338, 50);
            this.endframe_textBox.Name = "endframe_textBox";
            this.endframe_textBox.Size = new System.Drawing.Size(112, 23);
            this.endframe_textBox.TabIndex = 5;
            this.endframe_textBox.Text = "0";
            this.endframe_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // startframe_textBox
            // 
            this.startframe_textBox.Location = new System.Drawing.Point(108, 50);
            this.startframe_textBox.Name = "startframe_textBox";
            this.startframe_textBox.Size = new System.Drawing.Size(116, 23);
            this.startframe_textBox.TabIndex = 4;
            this.startframe_textBox.Text = "0";
            this.startframe_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // endframesetting_button
            // 
            this.endframesetting_button.Location = new System.Drawing.Point(338, 25);
            this.endframesetting_button.Name = "endframesetting_button";
            this.endframesetting_button.Size = new System.Drawing.Size(112, 23);
            this.endframesetting_button.TabIndex = 2;
            this.endframesetting_button.Text = "終了フレームを設定";
            this.endframesetting_button.UseVisualStyleBackColor = true;
            this.endframesetting_button.Click += new System.EventHandler(this.endframesetting_button_Click);
            // 
            // startframesetting_button
            // 
            this.startframesetting_button.Location = new System.Drawing.Point(108, 25);
            this.startframesetting_button.Name = "startframesetting_button";
            this.startframesetting_button.Size = new System.Drawing.Size(116, 23);
            this.startframesetting_button.TabIndex = 1;
            this.startframesetting_button.Text = "開始フレームを設定";
            this.startframesetting_button.UseVisualStyleBackColor = true;
            this.startframesetting_button.Click += new System.EventHandler(this.startframesetting_button_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(6, 79);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(551, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.delayTime_textbox);
            this.groupBox2.Location = new System.Drawing.Point(6, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 57);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "1コマの遅延(2~8)";
            // 
            // delayTime_textbox
            // 
            this.delayTime_textbox.Location = new System.Drawing.Point(60, 22);
            this.delayTime_textbox.Name = "delayTime_textbox";
            this.delayTime_textbox.Size = new System.Drawing.Size(90, 23);
            this.delayTime_textbox.TabIndex = 0;
            this.delayTime_textbox.Text = "5";
            this.delayTime_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pictureBox1.Location = new System.Drawing.Point(292, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(277, 178);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // resolution_groupbox
            // 
            this.resolution_groupbox.Controls.Add(this.label1);
            this.resolution_groupbox.Controls.Add(this.resolutionHeihgt_textbox);
            this.resolution_groupbox.Controls.Add(this.resolutionWidth_textbox);
            this.resolution_groupbox.Location = new System.Drawing.Point(6, 22);
            this.resolution_groupbox.Name = "resolution_groupbox";
            this.resolution_groupbox.Size = new System.Drawing.Size(215, 62);
            this.resolution_groupbox.TabIndex = 0;
            this.resolution_groupbox.TabStop = false;
            this.resolution_groupbox.Text = "解像度";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "X";
            // 
            // resolutionHeihgt_textbox
            // 
            this.resolutionHeihgt_textbox.Location = new System.Drawing.Point(96, 22);
            this.resolutionHeihgt_textbox.Name = "resolutionHeihgt_textbox";
            this.resolutionHeihgt_textbox.ReadOnly = true;
            this.resolutionHeihgt_textbox.Size = new System.Drawing.Size(113, 23);
            this.resolutionHeihgt_textbox.TabIndex = 1;
            this.resolutionHeihgt_textbox.Text = "(Widthに合わせます)";
            this.resolutionHeihgt_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // resolutionWidth_textbox
            // 
            this.resolutionWidth_textbox.Location = new System.Drawing.Point(6, 22);
            this.resolutionWidth_textbox.Name = "resolutionWidth_textbox";
            this.resolutionWidth_textbox.Size = new System.Drawing.Size(64, 23);
            this.resolutionWidth_textbox.TabIndex = 0;
            this.resolutionWidth_textbox.Text = "1280";
            this.resolutionWidth_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // input_groupbox
            // 
            this.input_groupbox.Controls.Add(this.inputpath_textbox);
            this.input_groupbox.Controls.Add(this.inputselect_button);
            this.input_groupbox.Location = new System.Drawing.Point(12, 350);
            this.input_groupbox.Name = "input_groupbox";
            this.input_groupbox.Size = new System.Drawing.Size(575, 58);
            this.input_groupbox.TabIndex = 2;
            this.input_groupbox.TabStop = false;
            this.input_groupbox.Text = "入力";
            // 
            // inputpath_textbox
            // 
            this.inputpath_textbox.Location = new System.Drawing.Point(114, 22);
            this.inputpath_textbox.Name = "inputpath_textbox";
            this.inputpath_textbox.ReadOnly = true;
            this.inputpath_textbox.Size = new System.Drawing.Size(455, 23);
            this.inputpath_textbox.TabIndex = 1;
            // 
            // inputselect_button
            // 
            this.inputselect_button.Location = new System.Drawing.Point(6, 21);
            this.inputselect_button.Name = "inputselect_button";
            this.inputselect_button.Size = new System.Drawing.Size(102, 23);
            this.inputselect_button.TabIndex = 0;
            this.inputselect_button.Text = "選択";
            this.inputselect_button.UseVisualStyleBackColor = true;
            this.inputselect_button.Click += new System.EventHandler(this.inputselect_button_Click);
            // 
            // outputselect_button
            // 
            this.outputselect_button.Location = new System.Drawing.Point(6, 22);
            this.outputselect_button.Name = "outputselect_button";
            this.outputselect_button.Size = new System.Drawing.Size(102, 23);
            this.outputselect_button.TabIndex = 3;
            this.outputselect_button.Text = "選択";
            this.outputselect_button.UseVisualStyleBackColor = true;
            this.outputselect_button.Click += new System.EventHandler(this.outputselect_button_Click);
            // 
            // outputpath_textbox
            // 
            this.outputpath_textbox.Location = new System.Drawing.Point(114, 23);
            this.outputpath_textbox.Name = "outputpath_textbox";
            this.outputpath_textbox.ReadOnly = true;
            this.outputpath_textbox.Size = new System.Drawing.Size(455, 23);
            this.outputpath_textbox.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 479);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(701, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.outputselect_button);
            this.groupBox1.Controls.Add(this.outputpath_textbox);
            this.groupBox1.Location = new System.Drawing.Point(12, 414);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 62);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "出力";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 501);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.input_groupbox);
            this.Controls.Add(this.setting_groupbox);
            this.Controls.Add(this.convert_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "超GIFCreator0.2";
            this.setting_groupbox.ResumeLayout(false);
            this.setting_groupbox.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.resolution_groupbox.ResumeLayout(false);
            this.resolution_groupbox.PerformLayout();
            this.input_groupbox.ResumeLayout(false);
            this.input_groupbox.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private PictureBox pictureBox1;
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
        private CheckBox HalfFps_checkBox;
        public TrackBar trackBar1;
    }
}