using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2A
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            createchart();

        }
        void createchart()
        {
            var chart = chart1.ChartAreas[0];
            chart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;

            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisX.LabelStyle.IsEndLabelVisible = true;

            chart.AxisX.Minimum = 0;
            chart.AxisY.Minimum = 0;

            chart.AxisX.Interval = 5;
            chart.AxisY.Interval = 10;

            chart1.Series[0].IsVisibleInLegend = false;

            chart1.Series.Add("pasmo");
            chart1.Series["pasmo"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["pasmo"].Color = Color.DarkRed;

            chart1.Series.Add("bufor");
            chart1.Series["bufor"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["bufor"].Color = Color.Blue;

            string[] data1 = System.IO.File.ReadAllLines(@"D:\czas.txt");
            string[] data2 = System.IO.File.ReadAllLines(@"D:\pasmo.txt");
            string[] data3 = System.IO.File.ReadAllLines(@"D:\bufor.txt");

            for (int i = 0; i < data1.Length; i++)
            {
                chart1.Series["pasmo"].Points.AddXY(data1[i], data2[i]);
                chart1.Series["bufor"].Points.AddXY(data1[i], data3[i]);
            }
        }
    }


}