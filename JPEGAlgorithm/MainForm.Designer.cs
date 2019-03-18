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
			((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sourceImageBox)).BeginInit();
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
			this.CompressButton.Location = new System.Drawing.Point(94, 3);
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
			this.quantCoeffAC.Location = new System.Drawing.Point(283, 9);
			this.quantCoeffAC.Name = "quantCoeffAC";
			this.quantCoeffAC.Size = new System.Drawing.Size(25, 20);
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
			this.chart.Location = new System.Drawing.Point(3, 314);
			this.chart.Name = "chart";
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
			series1.Legend = "Legend1";
			series1.Name = "DCDiffsBitLength";
			this.chart.Series.Add(series1);
			this.chart.Size = new System.Drawing.Size(480, 156);
			this.chart.TabIndex = 16;
			this.chart.Text = "DC Coeffs Diffs";
			// 
			// quantCoeffDC
			// 
			this.quantCoeffDC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.quantCoeffDC.Location = new System.Drawing.Point(216, 9);
			this.quantCoeffDC.Name = "quantCoeffDC";
			this.quantCoeffDC.Size = new System.Drawing.Size(25, 20);
			this.quantCoeffDC.TabIndex = 17;
			this.quantCoeffDC.Text = "1";
			// 
			// resultPictureBox
			// 
			this.resultPictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
			this.resultPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.resultPictureBox.Image = global::JPEGAlgorithm.Properties.Resources.Fallout_3_Test_Pattern_52_www_FullHDWpp_com_;
			this.resultPictureBox.InitialImage = null;
			this.resultPictureBox.Location = new System.Drawing.Point(484, 38);
			this.resultPictureBox.Name = "resultPictureBox";
			this.resultPictureBox.Size = new System.Drawing.Size(768, 432);
			this.resultPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.resultPictureBox.TabIndex = 3;
			this.resultPictureBox.TabStop = false;
			// 
			// sourceImageBox
			// 
			this.sourceImageBox.BackColor = System.Drawing.SystemColors.ControlDark;
			this.sourceImageBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sourceImageBox.BackgroundImage")));
			this.sourceImageBox.Image = global::JPEGAlgorithm.Properties.Resources.plyazh_tropiki_more_pesok_leto_84726_1920x1080;
			this.sourceImageBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("sourceImageBox.InitialImage")));
			this.sourceImageBox.Location = new System.Drawing.Point(3, 38);
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
			this.label1.Location = new System.Drawing.Point(180, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 19);
			this.label1.TabIndex = 18;
			this.label1.Text = "DC :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(247, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 19);
			this.label2.TabIndex = 19;
			this.label2.Text = "AC :";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(1254, 472);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.quantCoeffDC);
			this.Controls.Add(this.chart);
			this.Controls.Add(this.quantCoeffAC);
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
			this.ResumeLayout(false);
			this.PerformLayout();

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
	}
}

