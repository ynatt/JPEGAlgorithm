using System;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.openPictureButton = new System.Windows.Forms.Button();
			this.sourceImageBox = new System.Windows.Forms.PictureBox();
			this.buttonToRGBMatrix = new System.Windows.Forms.Button();
			this.resultPictureBox = new System.Windows.Forms.PictureBox();
			this.dimentionTextBox = new System.Windows.Forms.TextBox();
			this.xTextBox = new System.Windows.Forms.TextBox();
			this.yTextBox = new System.Windows.Forms.TextBox();
			this.channelComboBox = new System.Windows.Forms.ComboBox();
			this.colorSchemeComboBox = new System.Windows.Forms.ComboBox();
			this.isFullImage = new System.Windows.Forms.CheckBox();
			this.averageResultButton = new System.Windows.Forms.Button();
			this.CompressButton = new System.Windows.Forms.Button();
			this.doAverageChannel = new System.Windows.Forms.CheckBox();
			this.quantCoeff = new System.Windows.Forms.TextBox();
			this.quantCoeffLabel = new System.Windows.Forms.Label();
			this.fitToBlockButton = new System.Windows.Forms.Button();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.sourceImageBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			this.SuspendLayout();
			// 
			// openPictureButton
			// 
			this.openPictureButton.Location = new System.Drawing.Point(8, 2);
			this.openPictureButton.Name = "openPictureButton";
			this.openPictureButton.Size = new System.Drawing.Size(80, 25);
			this.openPictureButton.TabIndex = 0;
			this.openPictureButton.Text = "Open picture";
			this.openPictureButton.UseVisualStyleBackColor = true;
			this.openPictureButton.Click += new System.EventHandler(this.openPictureButton_Click);
			// 
			// sourceImageBox
			// 
			this.sourceImageBox.BackColor = System.Drawing.SystemColors.ControlDark;
			this.sourceImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.sourceImageBox.Image = global::JPEGAlgorithm.Properties.Resources.TestPicture;
			this.sourceImageBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("sourceImageBox.InitialImage")));
			this.sourceImageBox.Location = new System.Drawing.Point(3, 85);
			this.sourceImageBox.MaximumSize = new System.Drawing.Size(512, 512);
			this.sourceImageBox.MinimumSize = new System.Drawing.Size(512, 512);
			this.sourceImageBox.Name = "sourceImageBox";
			this.sourceImageBox.Size = new System.Drawing.Size(512, 512);
			this.sourceImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.sourceImageBox.TabIndex = 1;
			this.sourceImageBox.TabStop = false;
			// 
			// buttonToRGBMatrix
			// 
			this.buttonToRGBMatrix.Location = new System.Drawing.Point(282, 3);
			this.buttonToRGBMatrix.Name = "buttonToRGBMatrix";
			this.buttonToRGBMatrix.Size = new System.Drawing.Size(100, 25);
			this.buttonToRGBMatrix.TabIndex = 2;
			this.buttonToRGBMatrix.Text = "Show sub image";
			this.buttonToRGBMatrix.UseVisualStyleBackColor = true;
			this.buttonToRGBMatrix.Click += new System.EventHandler(this.buttonToRGBMatrix_Click);
			// 
			// resultPictureBox
			// 
			this.resultPictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
			this.resultPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.resultPictureBox.Location = new System.Drawing.Point(518, 85);
			this.resultPictureBox.MaximumSize = new System.Drawing.Size(512, 512);
			this.resultPictureBox.MinimumSize = new System.Drawing.Size(512, 512);
			this.resultPictureBox.Name = "resultPictureBox";
			this.resultPictureBox.Size = new System.Drawing.Size(512, 512);
			this.resultPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.resultPictureBox.TabIndex = 3;
			this.resultPictureBox.TabStop = false;
			// 
			// dimentionTextBox
			// 
			this.dimentionTextBox.Location = new System.Drawing.Point(109, 6);
			this.dimentionTextBox.Name = "dimentionTextBox";
			this.dimentionTextBox.Size = new System.Drawing.Size(25, 20);
			this.dimentionTextBox.TabIndex = 4;
			this.dimentionTextBox.Text = "8";
			// 
			// xTextBox
			// 
			this.xTextBox.Location = new System.Drawing.Point(109, 32);
			this.xTextBox.Name = "xTextBox";
			this.xTextBox.Size = new System.Drawing.Size(40, 20);
			this.xTextBox.TabIndex = 5;
			this.xTextBox.Text = "0";
			// 
			// yTextBox
			// 
			this.yTextBox.Location = new System.Drawing.Point(109, 58);
			this.yTextBox.Name = "yTextBox";
			this.yTextBox.Size = new System.Drawing.Size(40, 20);
			this.yTextBox.TabIndex = 6;
			this.yTextBox.Text = "0";
			// 
			// channelComboBox
			// 
			this.channelComboBox.FormattingEnabled = true;
			this.channelComboBox.Location = new System.Drawing.Point(155, 6);
			this.channelComboBox.Name = "channelComboBox";
			this.channelComboBox.Size = new System.Drawing.Size(100, 21);
			this.channelComboBox.TabIndex = 7;
			// 
			// colorSchemeComboBox
			// 
			this.colorSchemeComboBox.FormattingEnabled = true;
			this.colorSchemeComboBox.Location = new System.Drawing.Point(155, 33);
			this.colorSchemeComboBox.Name = "colorSchemeComboBox";
			this.colorSchemeComboBox.Size = new System.Drawing.Size(100, 21);
			this.colorSchemeComboBox.TabIndex = 8;
			this.colorSchemeComboBox.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
			// 
			// isFullImage
			// 
			this.isFullImage.AutoSize = true;
			this.isFullImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.isFullImage.Location = new System.Drawing.Point(12, 33);
			this.isFullImage.Name = "isFullImage";
			this.isFullImage.Size = new System.Drawing.Size(74, 17);
			this.isFullImage.TabIndex = 9;
			this.isFullImage.Text = "Full Image";
			this.isFullImage.UseVisualStyleBackColor = false;
			this.isFullImage.CheckedChanged += new System.EventHandler(this.isFullImage_CheckedChanged);
			// 
			// averageResultButton
			// 
			this.averageResultButton.Location = new System.Drawing.Point(388, 3);
			this.averageResultButton.Name = "averageResultButton";
			this.averageResultButton.Size = new System.Drawing.Size(100, 25);
			this.averageResultButton.TabIndex = 10;
			this.averageResultButton.Text = "Average result";
			this.averageResultButton.UseVisualStyleBackColor = true;
			this.averageResultButton.Click += new System.EventHandler(this.averageResultButton_Click);
			// 
			// CompressButton
			// 
			this.CompressButton.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.CompressButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CompressButton.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.CompressButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.CompressButton.Location = new System.Drawing.Point(507, 3);
			this.CompressButton.Name = "CompressButton";
			this.CompressButton.Size = new System.Drawing.Size(83, 30);
			this.CompressButton.TabIndex = 11;
			this.CompressButton.Text = "Compress";
			this.CompressButton.UseVisualStyleBackColor = false;
			this.CompressButton.Click += new System.EventHandler(this.CompressButton_Click);
			// 
			// doAverageChannel
			// 
			this.doAverageChannel.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.doAverageChannel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.doAverageChannel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.doAverageChannel.Location = new System.Drawing.Point(507, 39);
			this.doAverageChannel.Name = "doAverageChannel";
			this.doAverageChannel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.doAverageChannel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.doAverageChannel.Size = new System.Drawing.Size(142, 22);
			this.doAverageChannel.TabIndex = 12;
			this.doAverageChannel.Text = "Do Average Channel";
			this.doAverageChannel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.doAverageChannel.UseVisualStyleBackColor = false;
			// 
			// quantCoeff
			// 
			this.quantCoeff.Location = new System.Drawing.Point(347, 38);
			this.quantCoeff.Name = "quantCoeff";
			this.quantCoeff.Size = new System.Drawing.Size(35, 20);
			this.quantCoeff.TabIndex = 13;
			this.quantCoeff.Text = "1";
			// 
			// quantCoeffLabel
			// 
			this.quantCoeffLabel.AutoSize = true;
			this.quantCoeffLabel.Location = new System.Drawing.Point(279, 41);
			this.quantCoeffLabel.Name = "quantCoeffLabel";
			this.quantCoeffLabel.Size = new System.Drawing.Size(63, 13);
			this.quantCoeffLabel.TabIndex = 14;
			this.quantCoeffLabel.Text = "Quant coeff";
			// 
			// fitToBlockButton
			// 
			this.fitToBlockButton.Location = new System.Drawing.Point(412, 36);
			this.fitToBlockButton.Name = "fitToBlockButton";
			this.fitToBlockButton.Size = new System.Drawing.Size(76, 23);
			this.fitToBlockButton.TabIndex = 15;
			this.fitToBlockButton.Text = "Fit To Block";
			this.fitToBlockButton.UseVisualStyleBackColor = true;
			this.fitToBlockButton.Click += new System.EventHandler(this.fitToBlockButton_Click);
			// 
			// chart1
			// 
			chartArea1.AxisX.IsStartedFromZero = false;
			chartArea1.CursorX.IsUserEnabled = true;
			chartArea1.Name = "ChartArea1";
			this.chart1.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.chart1.Legends.Add(legend1);
			this.chart1.Location = new System.Drawing.Point(3, 603);
			this.chart1.Name = "chart1";
			series1.ChartArea = "ChartArea1";
			series1.Legend = "Legend1";
			series1.Name = "DC Coeffs Diffs";
			this.chart1.Series.Add(series1);
			this.chart1.Size = new System.Drawing.Size(1027, 179);
			this.chart1.TabIndex = 16;
			this.chart1.Text = "DC Coeffs Diffs";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(728, 25);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(77, 24);
			this.button1.TabIndex = 17;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(1034, 786);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.chart1);
			this.Controls.Add(this.fitToBlockButton);
			this.Controls.Add(this.quantCoeffLabel);
			this.Controls.Add(this.quantCoeff);
			this.Controls.Add(this.doAverageChannel);
			this.Controls.Add(this.CompressButton);
			this.Controls.Add(this.averageResultButton);
			this.Controls.Add(this.isFullImage);
			this.Controls.Add(this.colorSchemeComboBox);
			this.Controls.Add(this.channelComboBox);
			this.Controls.Add(this.yTextBox);
			this.Controls.Add(this.xTextBox);
			this.Controls.Add(this.dimentionTextBox);
			this.Controls.Add(this.resultPictureBox);
			this.Controls.Add(this.buttonToRGBMatrix);
			this.Controls.Add(this.sourceImageBox);
			this.Controls.Add(this.openPictureButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "MainForm";
			this.Text = "JPEGAlgorithm";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.sourceImageBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openPictureButton;
        private System.Windows.Forms.PictureBox sourceImageBox;
        private System.Windows.Forms.Button buttonToRGBMatrix;
        private System.Windows.Forms.PictureBox resultPictureBox;
        private System.Windows.Forms.TextBox dimentionTextBox;
        private System.Windows.Forms.TextBox xTextBox;
        private System.Windows.Forms.TextBox yTextBox;
        private System.Windows.Forms.ComboBox channelComboBox;
        private System.Windows.Forms.ComboBox colorSchemeComboBox;
        private System.Windows.Forms.CheckBox isFullImage;
        private System.Windows.Forms.Button averageResultButton;
        private System.Windows.Forms.Button CompressButton;
        private System.Windows.Forms.CheckBox doAverageChannel;
        private System.Windows.Forms.TextBox quantCoeff;
        private System.Windows.Forms.Label quantCoeffLabel;
        private System.Windows.Forms.Button fitToBlockButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
		private System.Windows.Forms.Button button1;
	}
}

