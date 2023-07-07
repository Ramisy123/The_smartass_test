using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using xls = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace Курсовая_работа
{
    public partial class Hystory : Form
    {
        public Hystory()
        {
            InitializeComponent();
            ExcelDoc file = new ExcelDoc(@"C:\Users\1\Desktop\Курсовая работа\Курсовая работа\Resources\Результаты.xlsx", 1);
            List<string> name = new List<string>();
            List<double> result = new List<double>();
            int i = 3;
            do 
            { 
                name.Add(file.ReadCell(i, 1));
                result.Add(Convert.ToDouble(file.ReadCell(i, 16)));
                i++;
            } while (file.ReadCell(i, 1)!=null);

            file.Close();

            Chart1.Series.Clear();
            Chart1.Titles.Add("Диаграмма");
            Chart1.Titles[0].Font = new System.Drawing.Font("Utopia", 16);

            string[] xValues = new string[name.Count];
            double[] yValues = new double[result.Count];

            for (int j=0; j< name.Count; j++)
            {
                xValues[j] = name[j];
                yValues[j] = result[j];
            }

            Chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("ColumnSeries")
            { ChartType = SeriesChartType.Column });

            Chart1.Series["ColumnSeries"].Points.DataBindXY(xValues, yValues);

            Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            Chart1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form f = new Finish(); f.Show(); this.Close();
        }
    }
}
