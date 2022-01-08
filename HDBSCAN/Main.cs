using HdbscanSharp.Distance;
using HdbscanSharp.Runner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HDBSCAN
{
    public partial class Main : Form
    {

        private double[][] _rawDataset;
        private double[][] _dataset;
        private string _fileName;

        public Main()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            Clear();
            DefaultSettings();
        }

        private void ClearResult()
        {
            processedDataTable.Columns.Clear();
        }

        private void Clear()
        {
            _dataset = new double[0][];
            _fileName = "";
            rawDataTable.Columns.Clear();
            datasetNameLbl.Text = "Не выбрано";
            classifierBtn.Enabled = false;
            chart2.Series.Clear();
        }

        private void DefaultSettings()
        {
            clusterSizeTextBox.Text = "25";
            instanceNumberTextBox.Text = "2";
            paralLevelTextBox.Text = "10";
            evDistanceLbl.Checked = true;
        }

        private void InitializeRawTable(int numCount, double[][] data)
        {
            rawDataTable.Columns.Add("Num", "Num");

            for (int i=0; i< numCount; i++)
            {
                rawDataTable.Columns.Add(i.ToString(), i.ToString());
            }

            for (int i = 0; i < data.GetLength(0); i++)
            {
                int rowId = rawDataTable.Rows.Add();
                DataGridViewRow row = rawDataTable.Rows[rowId];

                row.Cells["Num"].Value = i;

                for (int j = 0; j < numCount; j++)
                {
                    row.Cells[j.ToString()].Value = data[i][j];
                }

                rawDataTable.AutoResizeColumns();
            }
        }

        private void InitializeResultTable(int numCount, int[] data)
        {
            ClearResult();
            processedDataTable.Columns.Add("Num", "Num");

            var clusterName = data.Distinct().ToList();

            if (testingCheckBox.Checked)
            {
                chart2.Series.Clear();

                foreach (var cluster in clusterName)
                {
                    chart2.Series.Add(cluster.ToString());
                    chart2.Series[cluster.ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
                }
            }

            for (int i = 0; i < numCount; i++)
            {
                processedDataTable.Columns.Add(i.ToString(), i.ToString());
            }

            for (int i = 0; i < data.Length; i++)
            {
                int rowId = processedDataTable.Rows.Add();
                DataGridViewRow row = processedDataTable.Rows[rowId];

                row.Cells["Num"].Value = i;
                row.Cells["0"].Value = data[i];

                if (testingCheckBox.Checked)
                {
                    chart2.Series[data[i].ToString()].Points.AddXY(_dataset[i][0], _dataset[i][1]);

                }
                processedDataTable.AutoResizeColumns();
            }
        }

        private void LoadCsv(string fileName, double[][] rawDataset, double[][] dataset, bool header = false)
        {
            List<string> lines = new List<string>();

            if (header)
            {
                lines = File.ReadLines(fileName).Skip(1).ToList();
            }

           lines = File.ReadLines(fileName).ToList();

            try
            {
                _dataset = lines.Select(line => line.Split(',').Take(line.Split(',').Length-1).Select(m => double.Parse(m, CultureInfo.InvariantCulture)).ToArray()).ToArray();
                _rawDataset = lines.Select(line => line.Split(',').Select(m => double.Parse(m, CultureInfo.InvariantCulture)).ToArray()).ToArray();
            }
            catch
            {
                MessageBox.Show("Некорректные данные!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Classiffier();
        }

        private void OnlyDigits(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        public void SelectDataset()
        {
            Clear();
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "|*.csv";
            openFileDialog1.AddExtension = true;

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            _fileName = openFileDialog1.FileName;

            LoadCsv(_fileName, _rawDataset, _dataset);

            if (_rawDataset == null)
            {
                return;
            }

            if(_rawDataset.GetLength(0) == 0)
            {
                MessageBox.Show("Датасет пуст!");
            }

            InitializeRawTable(_rawDataset[0].Length, _rawDataset);
            classifierBtn.Enabled = true;
            datasetNameLbl.Text = Path.GetFileName(_fileName);
        }

        public void Classiffier()
        {

            IDistanceCalculator<double[]> distance = null;

            if(evDistanceLbl.Checked)
            {
                distance = new EuclideanDistance();
            }
            else if(manDistanceLbl.Checked)
            {
                distance = new ManhattanDistance();
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var result = HdbscanRunner.Run(new HdbscanParameters<double[]>
            {
                DataSet = _dataset,
                MinPoints = int.Parse(instanceNumberTextBox.Text),
                MinClusterSize = int.Parse(clusterSizeTextBox.Text),
                CacheDistance = true,
                MaxDegreeOfParallelism = int.Parse(paralLevelTextBox.Text),
                DistanceFunction = distance
            });
            sw.Stop();
            MessageBox.Show(sw.Elapsed.TotalMilliseconds.ToString());

            InitializeResultTable(1, result.Labels);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SelectDataset();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
