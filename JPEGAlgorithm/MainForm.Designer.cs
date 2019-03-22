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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.openPictureButton = new System.Windows.Forms.Button();
			this.CompressButton = new System.Windows.Forms.Button();
			this.quantCoeffAC = new System.Windows.Forms.TextBox();
			this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.quantCoeffDC = new System.Windows.Forms.TextBox();
			this.resultPictureBox = new System.Windows.Forms.PictureBox();
			this.sourceImageBox = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.compressProgressBar = new System.Windows.Forms.ProgressBar();
			this.quantCoeffsGroupBox = new System.Windows.Forms.GroupBox();
			this.infoGroupBox = new System.Windows.Forms.GroupBox();
			this.compressTimeValue = new System.Windows.Forms.Label();
			this.compressTimeLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sourceImageBox)).BeginInit();
			this.quantCoeffsGroupBox.SuspendLayout();
			this.infoGroupBox.SuspendLayout();
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
			// quantCoeffAC
			// 
			this.quantCoeffAC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.quantCoeffAC.Location = new System.Drawing.Point(111, 26);
			this.quantCoeffAC.Name = "quantCoeffAC";
			this.quantCoeffAC.Size = new System.Drawing.Size(25, 23);
			this.quantCoeffAC.TabIndex = 13;
			this.quantCoeffAC.Text = "1";
			// 
			// chart
			// 
			chartArea1.AxisX.IsStartedFromZero = false;
			chartArea1.CursorX.IsUserEnabled = true;
			chartArea1.Name = "ChartArea1";
			this.chart.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.chart.Legends.Add(legend1);
			this.chart.Location = new System.Drawing.Point(2, 348);
			this.chart.Name = "chart";
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
			series1.Legend = "Legend1";
			series1.Name = "DCDiffsBitLength";
			this.chart.Series.Add(series1);
			this.chart.Size = new System.Drawing.Size(480, 156);
			this.chart.TabIndex = 16;
			this.chart.Text = "DC Coeffs Diffs";
			this.chart.Visible = false;
			// 
			// quantCoeffDC
			// 
			this.quantCoeffDC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.quantCoeffDC.Location = new System.Drawing.Point(47, 26);
			this.quantCoeffDC.Name = "quantCoeffDC";
			this.quantCoeffDC.Size = new System.Drawing.Size(25, 23);
			this.quantCoeffDC.TabIndex = 17;
			this.quantCoeffDC.Text = "1";
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
			this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(11, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 19);
			this.label1.TabIndex = 18;
			this.label1.Text = "DC :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(75, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 19);
			this.label2.TabIndex = 19;
			this.label2.Text = "AC :";
			// 
			// compressProgressBar
			// 
			this.compressProgressBar.Location = new System.Drawing.Point(482, 72);
			this.compressProgressBar.Name = "compressProgressBar";
			this.compressProgressBar.Size = new System.Drawing.Size(50, 10);
			this.compressProgressBar.TabIndex = 20;
			this.compressProgressBar.Visible = false;
			// 
			// quantCoeffsGroupBox
			// 
			this.quantCoeffsGroupBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.quantCoeffsGroupBox.Controls.Add(this.quantCoeffDC);
			this.quantCoeffsGroupBox.Controls.Add(this.quantCoeffAC);
			this.quantCoeffsGroupBox.Controls.Add(this.label2);
			this.quantCoeffsGroupBox.Controls.Add(this.label1);
			this.quantCoeffsGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.quantCoeffsGroupBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.quantCoeffsGroupBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.quantCoeffsGroupBox.Location = new System.Drawing.Point(94, 3);
			this.quantCoeffsGroupBox.Name = "quantCoeffsGroupBox";
			this.quantCoeffsGroupBox.Size = new System.Drawing.Size(146, 65);
			this.quantCoeffsGroupBox.TabIndex = 21;
			this.quantCoeffsGroupBox.TabStop = false;
			this.quantCoeffsGroupBox.Text = "Quant coeffs";
			// 
			// infoGroupBox
			// 
			this.infoGroupBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.infoGroupBox.Controls.Add(this.compressTimeValue);
			this.infoGroupBox.Controls.Add(this.compressTimeLabel);
			this.infoGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.infoGroupBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.infoGroupBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.infoGroupBox.Location = new System.Drawing.Point(246, 3);
			this.infoGroupBox.Name = "infoGroupBox";
			this.infoGroupBox.Size = new System.Drawing.Size(183, 65);
			this.infoGroupBox.TabIndex = 22;
			this.infoGroupBox.TabStop = false;
			this.infoGroupBox.Text = "Info";
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(1254, 506);
			this.Controls.Add(this.infoGroupBox);
			this.Controls.Add(this.quantCoeffsGroupBox);
			this.Controls.Add(this.compressProgressBar);
			this.Controls.Add(this.chart);
			this.Controls.Add(this.CompressButton);
			this.Controls.Add(this.resultPictureBox);
			this.Controls.Add(this.sourceImageBox);
			this.Controls.Add(this.openPictureButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "MainForm";
			this.Text = "JPEGAlgorithm";
			((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sourceImageBox)).EndInit();
			this.quantCoeffsGroupBox.ResumeLayout(false);
			this.quantCoeffsGroupBox.PerformLayout();
			this.infoGroupBox.ResumeLayout(false);
			this.infoGroupBox.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button openPictureButton;
        private System.Windows.Forms.PictureBox sourceImageBox;
        private System.Windows.Forms.PictureBox resultPictureBox;
        private System.Windows.Forms.Button CompressButton;
        private System.Windows.Forms.TextBox quantCoeffAC;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.TextBox quantCoeffDC;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ProgressBar compressProgressBar;
		private System.Windows.Forms.GroupBox quantCoeffsGroupBox;
		private System.Windows.Forms.GroupBox infoGroupBox;
		private System.Windows.Forms.Label compressTimeValue;
		private System.Windows.Forms.Label compressTimeLabel;
	}
}

