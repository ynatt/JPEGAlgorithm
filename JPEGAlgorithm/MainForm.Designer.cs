﻿using System;

namespace JPEGAlgorithm
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.openPictureButton = new System.Windows.Forms.Button();
			this.CompressButton = new System.Windows.Forms.Button();
			this.quantCoeffCbCrAC = new System.Windows.Forms.TextBox();
			this.quantCoeffCbCrDC = new System.Windows.Forms.TextBox();
			this.resultPictureBox = new System.Windows.Forms.PictureBox();
			this.sourceImageBox = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.quantCoeffsGroupBox = new System.Windows.Forms.GroupBox();
			this.enableQuantOfYCheckBox = new System.Windows.Forms.CheckBox();
			this.label9 = new System.Windows.Forms.Label();
			this.quantCoeffYDC = new System.Windows.Forms.TextBox();
			this.quantCoeffYAC = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.cbcrLabel = new System.Windows.Forms.Label();
			this.infoGroupBox = new System.Windows.Forms.GroupBox();
			this.blockSize_label = new System.Windows.Forms.Label();
			this.resolutionValue = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.resolutionlabel = new System.Windows.Forms.Label();
			this.compressTimeValue = new System.Windows.Forms.Label();
			this.compressTimeLabel = new System.Windows.Forms.Label();
			this.compareGroupBox = new System.Windows.Forms.GroupBox();
			this.compareInfoBox = new System.Windows.Forms.GroupBox();
			this.comparePSNRValue = new System.Windows.Forms.Label();
			this.compareMSElabel = new System.Windows.Forms.Label();
			this.comparePSNRLabel = new System.Windows.Forms.Label();
			this.compareMSEValue = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.compareButton = new System.Windows.Forms.Button();
			this.compareHeightBox = new System.Windows.Forms.TextBox();
			this.compareWidthBox = new System.Windows.Forms.TextBox();
			this.compareYValueBox = new System.Windows.Forms.TextBox();
			this.compareXValueBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.compareBoxBefore = new System.Windows.Forms.PictureBox();
			this.compareBoxAfter = new System.Windows.Forms.PictureBox();
			this.errorInfoGroupBox = new System.Windows.Forms.GroupBox();
			this.psnrValueLabel = new System.Windows.Forms.Label();
			this.psnrLabel = new System.Windows.Forms.Label();
			this.mseValueLabel = new System.Windows.Forms.Label();
			this.label_zeroedCoeffsPercent = new System.Windows.Forms.Label();
			this.mseLabel = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.enableQuantForYToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.label12 = new System.Windows.Forms.Label();
			this.zeroPercentYBox = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.N_textBox = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.p_textBox = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label20 = new System.Windows.Forms.Label();
			this.zeroPercentCr_textBox = new System.Windows.Forms.TextBox();
			this.zeroPercentCb_textBox = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.checkBox_EnableAveraging = new System.Windows.Forms.CheckBox();
			this.checkBox_EnableYCbCr = new System.Windows.Forms.CheckBox();
			this.comboBox_Transform = new System.Windows.Forms.ComboBox();
			this.label21 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sourceImageBox)).BeginInit();
			this.quantCoeffsGroupBox.SuspendLayout();
			this.infoGroupBox.SuspendLayout();
			this.compareGroupBox.SuspendLayout();
			this.compareInfoBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.compareBoxBefore)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.compareBoxAfter)).BeginInit();
			this.errorInfoGroupBox.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// openPictureButton
			// 
			this.openPictureButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.openPictureButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.openPictureButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.openPictureButton.Location = new System.Drawing.Point(3, 3);
			this.openPictureButton.Name = "openPictureButton";
			this.openPictureButton.Size = new System.Drawing.Size(85, 30);
			this.openPictureButton.TabIndex = 0;
			this.openPictureButton.Text = "Open";
			this.openPictureButton.UseVisualStyleBackColor = false;
			this.openPictureButton.Click += new System.EventHandler(this.openPictureButton_Click);
			// 
			// CompressButton
			// 
			this.CompressButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.CompressButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CompressButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.CompressButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.CompressButton.Location = new System.Drawing.Point(3, 38);
			this.CompressButton.Name = "CompressButton";
			this.CompressButton.Size = new System.Drawing.Size(85, 30);
			this.CompressButton.TabIndex = 11;
			this.CompressButton.Text = "Compress";
			this.CompressButton.UseVisualStyleBackColor = false;
			this.CompressButton.Click += new System.EventHandler(this.CompressButton_Click);
			// 
			// quantCoeffCbCrAC
			// 
			this.quantCoeffCbCrAC.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.quantCoeffCbCrAC.Font = new System.Drawing.Font("Calibri", 8.25F);
			this.quantCoeffCbCrAC.Location = new System.Drawing.Point(116, 18);
			this.quantCoeffCbCrAC.Name = "quantCoeffCbCrAC";
			this.quantCoeffCbCrAC.Size = new System.Drawing.Size(25, 14);
			this.quantCoeffCbCrAC.TabIndex = 13;
			this.quantCoeffCbCrAC.Text = "1";
			// 
			// quantCoeffCbCrDC
			// 
			this.quantCoeffCbCrDC.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.quantCoeffCbCrDC.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.quantCoeffCbCrDC.Location = new System.Drawing.Point(65, 18);
			this.quantCoeffCbCrDC.Name = "quantCoeffCbCrDC";
			this.quantCoeffCbCrDC.Size = new System.Drawing.Size(25, 14);
			this.quantCoeffCbCrDC.TabIndex = 17;
			this.quantCoeffCbCrDC.Text = "1";
			// 
			// resultPictureBox
			// 
			this.resultPictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
			this.resultPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.resultPictureBox.Image = global::JPEGAlgorithm.Properties.Resources.Fallout_3_Test_Pattern_52_www_FullHDWpp_com_;
			this.resultPictureBox.InitialImage = null;
			this.resultPictureBox.Location = new System.Drawing.Point(483, 72);
			this.resultPictureBox.Name = "resultPictureBox";
			this.resultPictureBox.Size = new System.Drawing.Size(768, 432);
			this.resultPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.resultPictureBox.TabIndex = 3;
			this.resultPictureBox.TabStop = false;
			// 
			// sourceImageBox
			// 
			this.sourceImageBox.BackColor = System.Drawing.SystemColors.ControlDark;
			this.sourceImageBox.Image = global::JPEGAlgorithm.Properties.Resources.plyazh_tropiki_more_pesok_leto_84726_1920x1080;
			this.sourceImageBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("sourceImageBox.InitialImage")));
			this.sourceImageBox.Location = new System.Drawing.Point(2, 72);
			this.sourceImageBox.Name = "sourceImageBox";
			this.sourceImageBox.Size = new System.Drawing.Size(480, 270);
			this.sourceImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.sourceImageBox.TabIndex = 1;
			this.sourceImageBox.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(38, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(28, 15);
			this.label1.TabIndex = 18;
			this.label1.Text = "DC :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(90, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(27, 15);
			this.label2.TabIndex = 19;
			this.label2.Text = "AC :";
			// 
			// quantCoeffsGroupBox
			// 
			this.quantCoeffsGroupBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.quantCoeffsGroupBox.Controls.Add(this.enableQuantOfYCheckBox);
			this.quantCoeffsGroupBox.Controls.Add(this.label9);
			this.quantCoeffsGroupBox.Controls.Add(this.quantCoeffYDC);
			this.quantCoeffsGroupBox.Controls.Add(this.quantCoeffYAC);
			this.quantCoeffsGroupBox.Controls.Add(this.label10);
			this.quantCoeffsGroupBox.Controls.Add(this.label11);
			this.quantCoeffsGroupBox.Controls.Add(this.cbcrLabel);
			this.quantCoeffsGroupBox.Controls.Add(this.quantCoeffCbCrDC);
			this.quantCoeffsGroupBox.Controls.Add(this.quantCoeffCbCrAC);
			this.quantCoeffsGroupBox.Controls.Add(this.label2);
			this.quantCoeffsGroupBox.Controls.Add(this.label1);
			this.quantCoeffsGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.quantCoeffsGroupBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.quantCoeffsGroupBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.quantCoeffsGroupBox.Location = new System.Drawing.Point(1099, 3);
			this.quantCoeffsGroupBox.Name = "quantCoeffsGroupBox";
			this.quantCoeffsGroupBox.Size = new System.Drawing.Size(152, 65);
			this.quantCoeffsGroupBox.TabIndex = 21;
			this.quantCoeffsGroupBox.TabStop = false;
			this.quantCoeffsGroupBox.Text = "Quant coeffs";
			this.quantCoeffsGroupBox.Visible = false;
			// 
			// enableQuantOfYCheckBox
			// 
			this.enableQuantOfYCheckBox.AutoSize = true;
			this.enableQuantOfYCheckBox.Checked = true;
			this.enableQuantOfYCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.enableQuantOfYCheckBox.Location = new System.Drawing.Point(130, 38);
			this.enableQuantOfYCheckBox.Name = "enableQuantOfYCheckBox";
			this.enableQuantOfYCheckBox.Size = new System.Drawing.Size(15, 14);
			this.enableQuantOfYCheckBox.TabIndex = 26;
			this.enableQuantForYToolTip.SetToolTip(this.enableQuantOfYCheckBox, "Enable quantization for Y component");
			this.enableQuantOfYCheckBox.UseVisualStyleBackColor = true;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.CausesValidation = false;
			this.label9.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label9.Location = new System.Drawing.Point(6, 36);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(19, 14);
			this.label9.TabIndex = 25;
			this.label9.Text = "Y :";
			// 
			// quantCoeffYDC
			// 
			this.quantCoeffYDC.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.quantCoeffYDC.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.quantCoeffYDC.Location = new System.Drawing.Point(48, 37);
			this.quantCoeffYDC.Name = "quantCoeffYDC";
			this.quantCoeffYDC.Size = new System.Drawing.Size(25, 14);
			this.quantCoeffYDC.TabIndex = 22;
			this.quantCoeffYDC.Text = "1";
			// 
			// quantCoeffYAC
			// 
			this.quantCoeffYAC.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.quantCoeffYAC.Font = new System.Drawing.Font("Calibri", 8.25F);
			this.quantCoeffYAC.Location = new System.Drawing.Point(99, 37);
			this.quantCoeffYAC.Name = "quantCoeffYAC";
			this.quantCoeffYAC.Size = new System.Drawing.Size(25, 14);
			this.quantCoeffYAC.TabIndex = 21;
			this.quantCoeffYAC.Text = "1";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label10.Location = new System.Drawing.Point(73, 35);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(27, 15);
			this.label10.TabIndex = 24;
			this.label10.Text = "AC :";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label11.Location = new System.Drawing.Point(21, 35);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(28, 15);
			this.label11.TabIndex = 23;
			this.label11.Text = "DC :";
			// 
			// cbcrLabel
			// 
			this.cbcrLabel.AutoSize = true;
			this.cbcrLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.cbcrLabel.Location = new System.Drawing.Point(6, 17);
			this.cbcrLabel.Name = "cbcrLabel";
			this.cbcrLabel.Size = new System.Drawing.Size(36, 14);
			this.cbcrLabel.TabIndex = 20;
			this.cbcrLabel.Text = "CbCr :";
			// 
			// infoGroupBox
			// 
			this.infoGroupBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.infoGroupBox.Controls.Add(this.blockSize_label);
			this.infoGroupBox.Controls.Add(this.resolutionValue);
			this.infoGroupBox.Controls.Add(this.label16);
			this.infoGroupBox.Controls.Add(this.resolutionlabel);
			this.infoGroupBox.Controls.Add(this.compressTimeValue);
			this.infoGroupBox.Controls.Add(this.compressTimeLabel);
			this.infoGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.infoGroupBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.infoGroupBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.infoGroupBox.Location = new System.Drawing.Point(96, 3);
			this.infoGroupBox.Name = "infoGroupBox";
			this.infoGroupBox.Size = new System.Drawing.Size(183, 65);
			this.infoGroupBox.TabIndex = 22;
			this.infoGroupBox.TabStop = false;
			this.infoGroupBox.Text = "Info";
			// 
			// blockSize_label
			// 
			this.blockSize_label.AutoSize = true;
			this.blockSize_label.Location = new System.Drawing.Point(77, 45);
			this.blockSize_label.Name = "blockSize_label";
			this.blockSize_label.Size = new System.Drawing.Size(11, 15);
			this.blockSize_label.TabIndex = 33;
			this.blockSize_label.Text = "-";
			// 
			// resolutionValue
			// 
			this.resolutionValue.AutoSize = true;
			this.resolutionValue.Location = new System.Drawing.Point(77, 30);
			this.resolutionValue.Name = "resolutionValue";
			this.resolutionValue.Size = new System.Drawing.Size(11, 15);
			this.resolutionValue.TabIndex = 21;
			this.resolutionValue.Text = "-";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(6, 45);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(67, 15);
			this.label16.TabIndex = 32;
			this.label16.Text = "Block Size :";
			// 
			// resolutionlabel
			// 
			this.resolutionlabel.AutoSize = true;
			this.resolutionlabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.resolutionlabel.Location = new System.Drawing.Point(6, 30);
			this.resolutionlabel.Name = "resolutionlabel";
			this.resolutionlabel.Size = new System.Drawing.Size(71, 15);
			this.resolutionlabel.TabIndex = 20;
			this.resolutionlabel.Text = "Resolution :";
			// 
			// compressTimeValue
			// 
			this.compressTimeValue.AutoSize = true;
			this.compressTimeValue.Location = new System.Drawing.Point(98, 15);
			this.compressTimeValue.Name = "compressTimeValue";
			this.compressTimeValue.Size = new System.Drawing.Size(11, 15);
			this.compressTimeValue.TabIndex = 19;
			this.compressTimeValue.Text = "-";
			// 
			// compressTimeLabel
			// 
			this.compressTimeLabel.AutoSize = true;
			this.compressTimeLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.compressTimeLabel.Location = new System.Drawing.Point(6, 15);
			this.compressTimeLabel.Name = "compressTimeLabel";
			this.compressTimeLabel.Size = new System.Drawing.Size(93, 15);
			this.compressTimeLabel.TabIndex = 18;
			this.compressTimeLabel.Text = "Compress time :";
			// 
			// compareGroupBox
			// 
			this.compareGroupBox.Controls.Add(this.compareInfoBox);
			this.compareGroupBox.Controls.Add(this.label8);
			this.compareGroupBox.Controls.Add(this.label7);
			this.compareGroupBox.Controls.Add(this.label6);
			this.compareGroupBox.Controls.Add(this.label5);
			this.compareGroupBox.Controls.Add(this.compareButton);
			this.compareGroupBox.Controls.Add(this.compareHeightBox);
			this.compareGroupBox.Controls.Add(this.compareWidthBox);
			this.compareGroupBox.Controls.Add(this.compareYValueBox);
			this.compareGroupBox.Controls.Add(this.compareXValueBox);
			this.compareGroupBox.Controls.Add(this.label4);
			this.compareGroupBox.Controls.Add(this.label3);
			this.compareGroupBox.Controls.Add(this.compareBoxBefore);
			this.compareGroupBox.Controls.Add(this.compareBoxAfter);
			this.compareGroupBox.Location = new System.Drawing.Point(1, 343);
			this.compareGroupBox.Name = "compareGroupBox";
			this.compareGroupBox.Size = new System.Drawing.Size(480, 160);
			this.compareGroupBox.TabIndex = 23;
			this.compareGroupBox.TabStop = false;
			this.compareGroupBox.Text = "Compare";
			// 
			// compareInfoBox
			// 
			this.compareInfoBox.Controls.Add(this.comparePSNRValue);
			this.compareInfoBox.Controls.Add(this.compareMSElabel);
			this.compareInfoBox.Controls.Add(this.comparePSNRLabel);
			this.compareInfoBox.Controls.Add(this.compareMSEValue);
			this.compareInfoBox.Location = new System.Drawing.Point(11, 88);
			this.compareInfoBox.Name = "compareInfoBox";
			this.compareInfoBox.Size = new System.Drawing.Size(181, 66);
			this.compareInfoBox.TabIndex = 13;
			this.compareInfoBox.TabStop = false;
			this.compareInfoBox.Text = "Info";
			// 
			// comparePSNRValue
			// 
			this.comparePSNRValue.AutoSize = true;
			this.comparePSNRValue.Location = new System.Drawing.Point(48, 31);
			this.comparePSNRValue.Name = "comparePSNRValue";
			this.comparePSNRValue.Size = new System.Drawing.Size(10, 13);
			this.comparePSNRValue.TabIndex = 27;
			this.comparePSNRValue.Text = "-";
			// 
			// compareMSElabel
			// 
			this.compareMSElabel.AutoSize = true;
			this.compareMSElabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.compareMSElabel.Location = new System.Drawing.Point(6, 16);
			this.compareMSElabel.Name = "compareMSElabel";
			this.compareMSElabel.Size = new System.Drawing.Size(36, 15);
			this.compareMSElabel.TabIndex = 26;
			this.compareMSElabel.Text = "MSE :";
			// 
			// comparePSNRLabel
			// 
			this.comparePSNRLabel.AutoSize = true;
			this.comparePSNRLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comparePSNRLabel.Location = new System.Drawing.Point(6, 30);
			this.comparePSNRLabel.Name = "comparePSNRLabel";
			this.comparePSNRLabel.Size = new System.Drawing.Size(41, 15);
			this.comparePSNRLabel.TabIndex = 28;
			this.comparePSNRLabel.Text = "PSNR :";
			// 
			// compareMSEValue
			// 
			this.compareMSEValue.AutoSize = true;
			this.compareMSEValue.Location = new System.Drawing.Point(42, 17);
			this.compareMSEValue.Name = "compareMSEValue";
			this.compareMSEValue.Size = new System.Drawing.Size(10, 13);
			this.compareMSEValue.TabIndex = 25;
			this.compareMSEValue.Text = "-";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(137, 11);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(13, 13);
			this.label8.TabIndex = 12;
			this.label8.Text = "h";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(92, 11);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(15, 13);
			this.label7.TabIndex = 11;
			this.label7.Text = "w";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(49, 11);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(12, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "y";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 11);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(12, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "x";
			// 
			// compareButton
			// 
			this.compareButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.compareButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.compareButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.compareButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.compareButton.Location = new System.Drawing.Point(9, 52);
			this.compareButton.Name = "compareButton";
			this.compareButton.Size = new System.Drawing.Size(85, 30);
			this.compareButton.TabIndex = 8;
			this.compareButton.Text = "Compare";
			this.compareButton.UseVisualStyleBackColor = false;
			this.compareButton.Click += new System.EventHandler(this.compareButton_Click);
			// 
			// compareHeightBox
			// 
			this.compareHeightBox.Location = new System.Drawing.Point(138, 27);
			this.compareHeightBox.Name = "compareHeightBox";
			this.compareHeightBox.Size = new System.Drawing.Size(40, 20);
			this.compareHeightBox.TabIndex = 7;
			this.compareHeightBox.Text = "100";
			// 
			// compareWidthBox
			// 
			this.compareWidthBox.Location = new System.Drawing.Point(95, 27);
			this.compareWidthBox.Name = "compareWidthBox";
			this.compareWidthBox.Size = new System.Drawing.Size(40, 20);
			this.compareWidthBox.TabIndex = 6;
			this.compareWidthBox.Text = "100";
			// 
			// compareYValueBox
			// 
			this.compareYValueBox.Location = new System.Drawing.Point(52, 27);
			this.compareYValueBox.Name = "compareYValueBox";
			this.compareYValueBox.Size = new System.Drawing.Size(40, 20);
			this.compareYValueBox.TabIndex = 5;
			this.compareYValueBox.Text = "0";
			// 
			// compareXValueBox
			// 
			this.compareXValueBox.Location = new System.Drawing.Point(9, 27);
			this.compareXValueBox.Name = "compareXValueBox";
			this.compareXValueBox.Size = new System.Drawing.Size(40, 20);
			this.compareXValueBox.TabIndex = 4;
			this.compareXValueBox.Text = "0";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(343, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 14);
			this.label4.TabIndex = 3;
			this.label4.Text = "After";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(207, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 14);
			this.label3.TabIndex = 2;
			this.label3.Text = "Before";
			// 
			// compareBoxBefore
			// 
			this.compareBoxBefore.Location = new System.Drawing.Point(210, 24);
			this.compareBoxBefore.Name = "compareBoxBefore";
			this.compareBoxBefore.Size = new System.Drawing.Size(130, 130);
			this.compareBoxBefore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.compareBoxBefore.TabIndex = 1;
			this.compareBoxBefore.TabStop = false;
			// 
			// compareBoxAfter
			// 
			this.compareBoxAfter.Location = new System.Drawing.Point(346, 24);
			this.compareBoxAfter.Name = "compareBoxAfter";
			this.compareBoxAfter.Size = new System.Drawing.Size(130, 130);
			this.compareBoxAfter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.compareBoxAfter.TabIndex = 0;
			this.compareBoxAfter.TabStop = false;
			// 
			// errorInfoGroupBox
			// 
			this.errorInfoGroupBox.Controls.Add(this.psnrValueLabel);
			this.errorInfoGroupBox.Controls.Add(this.psnrLabel);
			this.errorInfoGroupBox.Controls.Add(this.mseValueLabel);
			this.errorInfoGroupBox.Controls.Add(this.label_zeroedCoeffsPercent);
			this.errorInfoGroupBox.Controls.Add(this.mseLabel);
			this.errorInfoGroupBox.Controls.Add(this.label13);
			this.errorInfoGroupBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.errorInfoGroupBox.Location = new System.Drawing.Point(285, 3);
			this.errorInfoGroupBox.Name = "errorInfoGroupBox";
			this.errorInfoGroupBox.Size = new System.Drawing.Size(203, 65);
			this.errorInfoGroupBox.TabIndex = 24;
			this.errorInfoGroupBox.TabStop = false;
			this.errorInfoGroupBox.Text = "Error Info";
			// 
			// psnrValueLabel
			// 
			this.psnrValueLabel.AutoSize = true;
			this.psnrValueLabel.Location = new System.Drawing.Point(48, 30);
			this.psnrValueLabel.Name = "psnrValueLabel";
			this.psnrValueLabel.Size = new System.Drawing.Size(11, 14);
			this.psnrValueLabel.TabIndex = 23;
			this.psnrValueLabel.Text = "-";
			// 
			// psnrLabel
			// 
			this.psnrLabel.AutoSize = true;
			this.psnrLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.psnrLabel.Location = new System.Drawing.Point(6, 29);
			this.psnrLabel.Name = "psnrLabel";
			this.psnrLabel.Size = new System.Drawing.Size(41, 15);
			this.psnrLabel.TabIndex = 24;
			this.psnrLabel.Text = "PSNR :";
			// 
			// mseValueLabel
			// 
			this.mseValueLabel.AutoSize = true;
			this.mseValueLabel.Location = new System.Drawing.Point(42, 16);
			this.mseValueLabel.Name = "mseValueLabel";
			this.mseValueLabel.Size = new System.Drawing.Size(11, 14);
			this.mseValueLabel.TabIndex = 22;
			this.mseValueLabel.Text = "-";
			// 
			// label_zeroedCoeffsPercent
			// 
			this.label_zeroedCoeffsPercent.AutoSize = true;
			this.label_zeroedCoeffsPercent.Location = new System.Drawing.Point(110, 47);
			this.label_zeroedCoeffsPercent.Name = "label_zeroedCoeffsPercent";
			this.label_zeroedCoeffsPercent.Size = new System.Drawing.Size(11, 14);
			this.label_zeroedCoeffsPercent.TabIndex = 30;
			this.label_zeroedCoeffsPercent.Text = "-";
			// 
			// mseLabel
			// 
			this.mseLabel.AutoSize = true;
			this.mseLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.mseLabel.Location = new System.Drawing.Point(6, 15);
			this.mseLabel.Name = "mseLabel";
			this.mseLabel.Size = new System.Drawing.Size(36, 15);
			this.mseLabel.TabIndex = 22;
			this.mseLabel.Text = "MSE :";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(6, 46);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(98, 14);
			this.label13.TabIndex = 29;
			this.label13.Text = "Zeroed coeffs % :";
			// 
			// enableQuantForYToolTip
			// 
			this.enableQuantForYToolTip.AutomaticDelay = 100;
			this.enableQuantForYToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.enableQuantForYToolTip.ToolTipTitle = "Info";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(967, 36);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(105, 30);
			this.button1.TabIndex = 25;
			this.button1.Text = "Vilenkin Compress";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(6, 16);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(68, 13);
			this.label12.TabIndex = 26;
			this.label12.Text = "Zero percent";
			// 
			// zeroPercentYBox
			// 
			this.zeroPercentYBox.Location = new System.Drawing.Point(91, 13);
			this.zeroPercentYBox.Name = "zeroPercentYBox";
			this.zeroPercentYBox.Size = new System.Drawing.Size(20, 20);
			this.zeroPercentYBox.TabIndex = 27;
			this.zeroPercentYBox.Text = "0";
			this.zeroPercentYBox.TextChanged += new System.EventHandler(this.zeroPercentYBox_TextChanged);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Location = new System.Drawing.Point(967, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(90, 30);
			this.button2.TabIndex = 28;
			this.button2.Text = "Haar Compress";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Visible = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// N_textBox
			// 
			this.N_textBox.Location = new System.Drawing.Point(136, 37);
			this.N_textBox.Name = "N_textBox";
			this.N_textBox.Size = new System.Drawing.Size(20, 20);
			this.N_textBox.TabIndex = 29;
			this.N_textBox.Text = "2";
			this.N_textBox.TextChanged += new System.EventHandler(this.N_textBox_TextChanged);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(117, 40);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(15, 13);
			this.label15.TabIndex = 28;
			this.label15.Text = "N";
			// 
			// p_textBox
			// 
			this.p_textBox.Location = new System.Drawing.Point(91, 37);
			this.p_textBox.Name = "p_textBox";
			this.p_textBox.Size = new System.Drawing.Size(20, 20);
			this.p_textBox.TabIndex = 27;
			this.p_textBox.Text = "3";
			this.p_textBox.TextChanged += new System.EventHandler(this.p_textBox_TextChanged);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(76, 40);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(14, 13);
			this.label14.TabIndex = 26;
			this.label14.Text = "P";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label21);
			this.groupBox2.Controls.Add(this.comboBox_Transform);
			this.groupBox2.Controls.Add(this.checkBox_EnableYCbCr);
			this.groupBox2.Controls.Add(this.N_textBox);
			this.groupBox2.Controls.Add(this.checkBox_EnableAveraging);
			this.groupBox2.Controls.Add(this.label20);
			this.groupBox2.Controls.Add(this.label15);
			this.groupBox2.Controls.Add(this.zeroPercentCr_textBox);
			this.groupBox2.Controls.Add(this.p_textBox);
			this.groupBox2.Controls.Add(this.zeroPercentCb_textBox);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.label19);
			this.groupBox2.Controls.Add(this.label18);
			this.groupBox2.Controls.Add(this.label17);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.zeroPercentYBox);
			this.groupBox2.Location = new System.Drawing.Point(494, 4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(376, 64);
			this.groupBox2.TabIndex = 32;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Settings";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(6, 40);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(61, 13);
			this.label20.TabIndex = 38;
			this.label20.Text = "Block size :";
			// 
			// zeroPercentCr_textBox
			// 
			this.zeroPercentCr_textBox.Location = new System.Drawing.Point(184, 13);
			this.zeroPercentCr_textBox.Name = "zeroPercentCr_textBox";
			this.zeroPercentCr_textBox.Size = new System.Drawing.Size(20, 20);
			this.zeroPercentCr_textBox.TabIndex = 37;
			this.zeroPercentCr_textBox.Text = "0";
			this.zeroPercentCr_textBox.TextChanged += new System.EventHandler(this.zeroPercentCr_textBox_TextChanged);
			// 
			// zeroPercentCb_textBox
			// 
			this.zeroPercentCb_textBox.Location = new System.Drawing.Point(136, 13);
			this.zeroPercentCb_textBox.Name = "zeroPercentCb_textBox";
			this.zeroPercentCb_textBox.Size = new System.Drawing.Size(20, 20);
			this.zeroPercentCb_textBox.TabIndex = 36;
			this.zeroPercentCb_textBox.Text = "0";
			this.zeroPercentCb_textBox.TextChanged += new System.EventHandler(this.zeroPercentCb_textBox_TextChanged);
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(161, 16);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(17, 13);
			this.label19.TabIndex = 35;
			this.label19.Text = "Cr";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(115, 16);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(20, 13);
			this.label18.TabIndex = 34;
			this.label18.Text = "Cb";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(76, 16);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(14, 13);
			this.label17.TabIndex = 33;
			this.label17.Text = "Y";
			// 
			// checkBox_EnableAveraging
			// 
			this.checkBox_EnableAveraging.AutoSize = true;
			this.checkBox_EnableAveraging.Checked = true;
			this.checkBox_EnableAveraging.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_EnableAveraging.Location = new System.Drawing.Point(164, 40);
			this.checkBox_EnableAveraging.Name = "checkBox_EnableAveraging";
			this.checkBox_EnableAveraging.Size = new System.Drawing.Size(109, 17);
			this.checkBox_EnableAveraging.TabIndex = 33;
			this.checkBox_EnableAveraging.Text = "Enable averaging";
			this.checkBox_EnableAveraging.UseVisualStyleBackColor = true;
			this.checkBox_EnableAveraging.CheckedChanged += new System.EventHandler(this.checkBox_EnableAveraging_CheckedChanged);
			// 
			// checkBox_EnableYCbCr
			// 
			this.checkBox_EnableYCbCr.AutoSize = true;
			this.checkBox_EnableYCbCr.Checked = true;
			this.checkBox_EnableYCbCr.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_EnableYCbCr.Location = new System.Drawing.Point(278, 40);
			this.checkBox_EnableYCbCr.Name = "checkBox_EnableYCbCr";
			this.checkBox_EnableYCbCr.Size = new System.Drawing.Size(92, 17);
			this.checkBox_EnableYCbCr.TabIndex = 34;
			this.checkBox_EnableYCbCr.Text = "Enable YCbCr";
			this.checkBox_EnableYCbCr.UseVisualStyleBackColor = true;
			this.checkBox_EnableYCbCr.CheckedChanged += new System.EventHandler(this.checkBox_EnableYCbCr_CheckedChanged);
			// 
			// comboBox_Transform
			// 
			this.comboBox_Transform.DisplayMember = "DCT";
			this.comboBox_Transform.FormattingEnabled = true;
			this.comboBox_Transform.Items.AddRange(new object[] {
            "DCT",
            "Haar",
            "Vilenkin"});
			this.comboBox_Transform.Location = new System.Drawing.Point(278, 12);
			this.comboBox_Transform.Name = "comboBox_Transform";
			this.comboBox_Transform.Size = new System.Drawing.Size(92, 21);
			this.comboBox_Transform.TabIndex = 39;
			this.comboBox_Transform.Text = "DCT";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(218, 15);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(54, 13);
			this.label21.TabIndex = 40;
			this.label21.Text = "Transform";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(1254, 506);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.errorInfoGroupBox);
			this.Controls.Add(this.compareGroupBox);
			this.Controls.Add(this.infoGroupBox);
			this.Controls.Add(this.quantCoeffsGroupBox);
			this.Controls.Add(this.CompressButton);
			this.Controls.Add(this.resultPictureBox);
			this.Controls.Add(this.sourceImageBox);
			this.Controls.Add(this.openPictureButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "MainForm";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Text = "CompressAlgorithm";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sourceImageBox)).EndInit();
			this.quantCoeffsGroupBox.ResumeLayout(false);
			this.quantCoeffsGroupBox.PerformLayout();
			this.infoGroupBox.ResumeLayout(false);
			this.infoGroupBox.PerformLayout();
			this.compareGroupBox.ResumeLayout(false);
			this.compareGroupBox.PerformLayout();
			this.compareInfoBox.ResumeLayout(false);
			this.compareInfoBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.compareBoxBefore)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.compareBoxAfter)).EndInit();
			this.errorInfoGroupBox.ResumeLayout(false);
			this.errorInfoGroupBox.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button openPictureButton;
        private System.Windows.Forms.PictureBox sourceImageBox;
        private System.Windows.Forms.PictureBox resultPictureBox;
        private System.Windows.Forms.Button CompressButton;
        private System.Windows.Forms.TextBox quantCoeffCbCrAC;
        private System.Windows.Forms.TextBox quantCoeffCbCrDC;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox quantCoeffsGroupBox;
		private System.Windows.Forms.GroupBox infoGroupBox;
		private System.Windows.Forms.Label compressTimeValue;
		private System.Windows.Forms.Label compressTimeLabel;
		private System.Windows.Forms.GroupBox compareGroupBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox compareBoxBefore;
		private System.Windows.Forms.PictureBox compareBoxAfter;
		private System.Windows.Forms.TextBox compareHeightBox;
		private System.Windows.Forms.TextBox compareWidthBox;
		private System.Windows.Forms.TextBox compareYValueBox;
		private System.Windows.Forms.TextBox compareXValueBox;
		private System.Windows.Forms.Button compareButton;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label resolutionValue;
		private System.Windows.Forms.Label resolutionlabel;
		private System.Windows.Forms.GroupBox errorInfoGroupBox;
		private System.Windows.Forms.Label mseValueLabel;
		private System.Windows.Forms.Label mseLabel;
		private System.Windows.Forms.Label psnrValueLabel;
		private System.Windows.Forms.Label psnrLabel;
		private System.Windows.Forms.GroupBox compareInfoBox;
		private System.Windows.Forms.Label comparePSNRValue;
		private System.Windows.Forms.Label compareMSElabel;
		private System.Windows.Forms.Label comparePSNRLabel;
		private System.Windows.Forms.Label compareMSEValue;
		private System.Windows.Forms.CheckBox enableQuantOfYCheckBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox quantCoeffYDC;
		private System.Windows.Forms.TextBox quantCoeffYAC;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label cbcrLabel;
		private System.Windows.Forms.ToolTip enableQuantForYToolTip;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox zeroPercentYBox;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label_zeroedCoeffsPercent;
		private System.Windows.Forms.TextBox N_textBox;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox p_textBox;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label blockSize_label;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox zeroPercentCr_textBox;
		private System.Windows.Forms.TextBox zeroPercentCb_textBox;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.CheckBox checkBox_EnableAveraging;
		private System.Windows.Forms.CheckBox checkBox_EnableYCbCr;
		private System.Windows.Forms.ComboBox comboBox_Transform;
		private System.Windows.Forms.Label label21;
	}
}

