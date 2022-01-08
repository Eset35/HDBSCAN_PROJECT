
namespace HDBSCAN
{
    partial class Main
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.mainPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.testingCheckBox = new System.Windows.Forms.CheckBox();
            this.clearBtn = new System.Windows.Forms.Button();
            this.clusterSizeLbl = new System.Windows.Forms.Label();
            this.paralLevelTextBox = new System.Windows.Forms.TextBox();
            this.parLevelLbl = new System.Windows.Forms.Label();
            this.settingsDescLbl = new System.Windows.Forms.Label();
            this.minParamsDescLbl = new System.Windows.Forms.Label();
            this.distanceNameDescLbl = new System.Windows.Forms.Label();
            this.instNumberLbl = new System.Windows.Forms.Label();
            this.datasetNameLbl = new System.Windows.Forms.Label();
            this.instanceNumberTextBox = new System.Windows.Forms.TextBox();
            this.clusterSizeTextBox = new System.Windows.Forms.TextBox();
            this.manDistanceLbl = new System.Windows.Forms.RadioButton();
            this.evDistanceLbl = new System.Windows.Forms.RadioButton();
            this.selectDatasetBtn = new System.Windows.Forms.Button();
            this.classifierBtn = new System.Windows.Forms.Button();
            this.datasetPageControl = new System.Windows.Forms.TabControl();
            this.datasetRawPage = new System.Windows.Forms.TabPage();
            this.rawDataTable = new System.Windows.Forms.DataGridView();
            this.processedDatasetPage = new System.Windows.Forms.TabPage();
            this.processedDataTable = new System.Windows.Forms.DataGridView();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.mainPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.datasetPageControl.SuspendLayout();
            this.datasetRawPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rawDataTable)).BeginInit();
            this.processedDatasetPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.processedDataTable)).BeginInit();
            this.mainTabControl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // mainPage
            // 
            this.mainPage.Controls.Add(this.splitContainer1);
            this.mainPage.Location = new System.Drawing.Point(4, 22);
            this.mainPage.Name = "mainPage";
            this.mainPage.Padding = new System.Windows.Forms.Padding(3);
            this.mainPage.Size = new System.Drawing.Size(877, 428);
            this.mainPage.TabIndex = 1;
            this.mainPage.Text = "Кластеризация";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.testingCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.clearBtn);
            this.splitContainer1.Panel1.Controls.Add(this.clusterSizeLbl);
            this.splitContainer1.Panel1.Controls.Add(this.paralLevelTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.parLevelLbl);
            this.splitContainer1.Panel1.Controls.Add(this.settingsDescLbl);
            this.splitContainer1.Panel1.Controls.Add(this.minParamsDescLbl);
            this.splitContainer1.Panel1.Controls.Add(this.distanceNameDescLbl);
            this.splitContainer1.Panel1.Controls.Add(this.instNumberLbl);
            this.splitContainer1.Panel1.Controls.Add(this.datasetNameLbl);
            this.splitContainer1.Panel1.Controls.Add(this.instanceNumberTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.clusterSizeTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.manDistanceLbl);
            this.splitContainer1.Panel1.Controls.Add(this.evDistanceLbl);
            this.splitContainer1.Panel1.Controls.Add(this.selectDatasetBtn);
            this.splitContainer1.Panel1.Controls.Add(this.classifierBtn);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.datasetPageControl);
            this.splitContainer1.Size = new System.Drawing.Size(871, 422);
            this.splitContainer1.SplitterDistance = 218;
            this.splitContainer1.TabIndex = 1;
            // 
            // testingCheckBox
            // 
            this.testingCheckBox.AutoSize = true;
            this.testingCheckBox.Location = new System.Drawing.Point(10, 323);
            this.testingCheckBox.Name = "testingCheckBox";
            this.testingCheckBox.Size = new System.Drawing.Size(173, 17);
            this.testingCheckBox.TabIndex = 15;
            this.testingCheckBox.Text = "Тестирование на X-Y данных";
            this.testingCheckBox.UseVisualStyleBackColor = true;
            // 
            // clearBtn
            // 
            this.clearBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.clearBtn.Location = new System.Drawing.Point(0, 23);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(218, 23);
            this.clearBtn.TabIndex = 14;
            this.clearBtn.Text = "Очистить";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // clusterSizeLbl
            // 
            this.clusterSizeLbl.AutoSize = true;
            this.clusterSizeLbl.Location = new System.Drawing.Point(12, 200);
            this.clusterSizeLbl.Name = "clusterSizeLbl";
            this.clusterSizeLbl.Size = new System.Drawing.Size(96, 13);
            this.clusterSizeLbl.TabIndex = 13;
            this.clusterSizeLbl.Text = "Размер кластера";
            // 
            // paralLevelTextBox
            // 
            this.paralLevelTextBox.Location = new System.Drawing.Point(141, 290);
            this.paralLevelTextBox.Name = "paralLevelTextBox";
            this.paralLevelTextBox.Size = new System.Drawing.Size(39, 20);
            this.paralLevelTextBox.TabIndex = 12;
            this.paralLevelTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyDigits);
            // 
            // parLevelLbl
            // 
            this.parLevelLbl.AutoSize = true;
            this.parLevelLbl.Location = new System.Drawing.Point(7, 293);
            this.parLevelLbl.Name = "parLevelLbl";
            this.parLevelLbl.Size = new System.Drawing.Size(128, 13);
            this.parLevelLbl.TabIndex = 11;
            this.parLevelLbl.Text = "Уровень параллелизма";
            // 
            // settingsDescLbl
            // 
            this.settingsDescLbl.AutoSize = true;
            this.settingsDescLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settingsDescLbl.Location = new System.Drawing.Point(60, 262);
            this.settingsDescLbl.Name = "settingsDescLbl";
            this.settingsDescLbl.Size = new System.Drawing.Size(71, 13);
            this.settingsDescLbl.TabIndex = 10;
            this.settingsDescLbl.Text = "Настройки";
            // 
            // minParamsDescLbl
            // 
            this.minParamsDescLbl.AutoSize = true;
            this.minParamsDescLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minParamsDescLbl.Location = new System.Drawing.Point(12, 169);
            this.minParamsDescLbl.Name = "minParamsDescLbl";
            this.minParamsDescLbl.Size = new System.Drawing.Size(161, 13);
            this.minParamsDescLbl.TabIndex = 9;
            this.minParamsDescLbl.Text = "Минимальные параметры";
            // 
            // distanceNameDescLbl
            // 
            this.distanceNameDescLbl.AutoSize = true;
            this.distanceNameDescLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.distanceNameDescLbl.Location = new System.Drawing.Point(54, 82);
            this.distanceNameDescLbl.Name = "distanceNameDescLbl";
            this.distanceNameDescLbl.Size = new System.Drawing.Size(77, 13);
            this.distanceNameDescLbl.TabIndex = 8;
            this.distanceNameDescLbl.Text = "Расстояние";
            // 
            // instNumberLbl
            // 
            this.instNumberLbl.AutoSize = true;
            this.instNumberLbl.Location = new System.Drawing.Point(7, 226);
            this.instNumberLbl.Name = "instNumberLbl";
            this.instNumberLbl.Size = new System.Drawing.Size(110, 13);
            this.instNumberLbl.TabIndex = 7;
            this.instNumberLbl.Text = "Число экземпляров";
            this.instNumberLbl.Visible = false;
            // 
            // datasetNameLbl
            // 
            this.datasetNameLbl.AutoSize = true;
            this.datasetNameLbl.Location = new System.Drawing.Point(-3, 47);
            this.datasetNameLbl.Name = "datasetNameLbl";
            this.datasetNameLbl.Size = new System.Drawing.Size(68, 13);
            this.datasetNameLbl.TabIndex = 6;
            this.datasetNameLbl.Text = "Не выбрано";
            // 
            // instanceNumberTextBox
            // 
            this.instanceNumberTextBox.Location = new System.Drawing.Point(123, 223);
            this.instanceNumberTextBox.Name = "instanceNumberTextBox";
            this.instanceNumberTextBox.Size = new System.Drawing.Size(50, 20);
            this.instanceNumberTextBox.TabIndex = 5;
            this.instanceNumberTextBox.Visible = false;
            this.instanceNumberTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyDigits);
            // 
            // clusterSizeTextBox
            // 
            this.clusterSizeTextBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.clusterSizeTextBox.Location = new System.Drawing.Point(123, 197);
            this.clusterSizeTextBox.Name = "clusterSizeTextBox";
            this.clusterSizeTextBox.Size = new System.Drawing.Size(50, 20);
            this.clusterSizeTextBox.TabIndex = 4;
            this.clusterSizeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyDigits);
            // 
            // manDistanceLbl
            // 
            this.manDistanceLbl.AutoSize = true;
            this.manDistanceLbl.Location = new System.Drawing.Point(10, 130);
            this.manDistanceLbl.Name = "manDistanceLbl";
            this.manDistanceLbl.Size = new System.Drawing.Size(85, 17);
            this.manDistanceLbl.TabIndex = 3;
            this.manDistanceLbl.TabStop = true;
            this.manDistanceLbl.Text = "Манхэттена";
            this.manDistanceLbl.UseVisualStyleBackColor = true;
            // 
            // evDistanceLbl
            // 
            this.evDistanceLbl.AutoSize = true;
            this.evDistanceLbl.Location = new System.Drawing.Point(10, 107);
            this.evDistanceLbl.Name = "evDistanceLbl";
            this.evDistanceLbl.Size = new System.Drawing.Size(80, 17);
            this.evDistanceLbl.TabIndex = 2;
            this.evDistanceLbl.TabStop = true;
            this.evDistanceLbl.Text = "Евклидово";
            this.evDistanceLbl.UseVisualStyleBackColor = true;
            // 
            // selectDatasetBtn
            // 
            this.selectDatasetBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectDatasetBtn.Location = new System.Drawing.Point(0, 0);
            this.selectDatasetBtn.Name = "selectDatasetBtn";
            this.selectDatasetBtn.Size = new System.Drawing.Size(218, 23);
            this.selectDatasetBtn.TabIndex = 1;
            this.selectDatasetBtn.Text = "Выбрать датасет";
            this.selectDatasetBtn.UseVisualStyleBackColor = true;
            this.selectDatasetBtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // classifierBtn
            // 
            this.classifierBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.classifierBtn.Location = new System.Drawing.Point(0, 399);
            this.classifierBtn.Name = "classifierBtn";
            this.classifierBtn.Size = new System.Drawing.Size(218, 23);
            this.classifierBtn.TabIndex = 0;
            this.classifierBtn.Text = "Запуск алгоритма";
            this.classifierBtn.UseVisualStyleBackColor = true;
            this.classifierBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // datasetPageControl
            // 
            this.datasetPageControl.Controls.Add(this.datasetRawPage);
            this.datasetPageControl.Controls.Add(this.processedDatasetPage);
            this.datasetPageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datasetPageControl.Location = new System.Drawing.Point(0, 0);
            this.datasetPageControl.Name = "datasetPageControl";
            this.datasetPageControl.SelectedIndex = 0;
            this.datasetPageControl.Size = new System.Drawing.Size(649, 422);
            this.datasetPageControl.TabIndex = 1;
            // 
            // datasetRawPage
            // 
            this.datasetRawPage.Controls.Add(this.rawDataTable);
            this.datasetRawPage.Location = new System.Drawing.Point(4, 22);
            this.datasetRawPage.Name = "datasetRawPage";
            this.datasetRawPage.Padding = new System.Windows.Forms.Padding(3);
            this.datasetRawPage.Size = new System.Drawing.Size(641, 396);
            this.datasetRawPage.TabIndex = 0;
            this.datasetRawPage.Text = "Сырые данные";
            this.datasetRawPage.UseVisualStyleBackColor = true;
            // 
            // rawDataTable
            // 
            this.rawDataTable.AllowUserToAddRows = false;
            this.rawDataTable.AllowUserToDeleteRows = false;
            this.rawDataTable.AllowUserToOrderColumns = true;
            this.rawDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rawDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rawDataTable.Location = new System.Drawing.Point(3, 3);
            this.rawDataTable.Name = "rawDataTable";
            this.rawDataTable.ReadOnly = true;
            this.rawDataTable.Size = new System.Drawing.Size(635, 390);
            this.rawDataTable.TabIndex = 0;
            // 
            // processedDatasetPage
            // 
            this.processedDatasetPage.Controls.Add(this.processedDataTable);
            this.processedDatasetPage.Location = new System.Drawing.Point(4, 22);
            this.processedDatasetPage.Name = "processedDatasetPage";
            this.processedDatasetPage.Padding = new System.Windows.Forms.Padding(3);
            this.processedDatasetPage.Size = new System.Drawing.Size(641, 396);
            this.processedDatasetPage.TabIndex = 1;
            this.processedDatasetPage.Text = "Результат классификации";
            this.processedDatasetPage.UseVisualStyleBackColor = true;
            // 
            // processedDataTable
            // 
            this.processedDataTable.AllowUserToAddRows = false;
            this.processedDataTable.AllowUserToDeleteRows = false;
            this.processedDataTable.AllowUserToOrderColumns = true;
            this.processedDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.processedDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processedDataTable.Location = new System.Drawing.Point(3, 3);
            this.processedDataTable.Name = "processedDataTable";
            this.processedDataTable.ReadOnly = true;
            this.processedDataTable.Size = new System.Drawing.Size(635, 390);
            this.processedDataTable.TabIndex = 0;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.mainPage);
            this.mainTabControl.Controls.Add(this.tabPage2);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(885, 454);
            this.mainTabControl.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chart2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(877, 428);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Синтетические тесты";
            // 
            // chart2
            // 
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            this.chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(3, 3);
            this.chart2.Name = "chart2";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart2.Series.Add(series1);
            this.chart2.Size = new System.Drawing.Size(871, 422);
            this.chart2.TabIndex = 1;
            this.chart2.Text = "chart2";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 454);
            this.Controls.Add(this.mainTabControl);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.mainPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.datasetPageControl.ResumeLayout(false);
            this.datasetRawPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rawDataTable)).EndInit();
            this.processedDatasetPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.processedDataTable)).EndInit();
            this.mainTabControl.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage mainPage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label clusterSizeLbl;
        private System.Windows.Forms.TextBox paralLevelTextBox;
        private System.Windows.Forms.Label parLevelLbl;
        private System.Windows.Forms.Label settingsDescLbl;
        private System.Windows.Forms.Label minParamsDescLbl;
        private System.Windows.Forms.Label distanceNameDescLbl;
        private System.Windows.Forms.Label instNumberLbl;
        private System.Windows.Forms.Label datasetNameLbl;
        private System.Windows.Forms.TextBox instanceNumberTextBox;
        private System.Windows.Forms.TextBox clusterSizeTextBox;
        private System.Windows.Forms.RadioButton manDistanceLbl;
        private System.Windows.Forms.RadioButton evDistanceLbl;
        private System.Windows.Forms.Button selectDatasetBtn;
        private System.Windows.Forms.Button classifierBtn;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.TabControl datasetPageControl;
        private System.Windows.Forms.TabPage datasetRawPage;
        private System.Windows.Forms.DataGridView rawDataTable;
        protected System.Windows.Forms.TabPage processedDatasetPage;
        private System.Windows.Forms.DataGridView processedDataTable;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox testingCheckBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
    }
}

