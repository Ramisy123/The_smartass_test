using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Курсовая_работа
{
    class ExcelDoc : Document
    {
        public string path;
        _Application excel = new _Excel.Application();
        public Workbook wb;
        public Worksheet ws;

        public ExcelDoc(string path, int sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[sheet];
        }

        public void SetPage(int c)
        {
            this.ws = wb.Worksheets[c];
        }
        public string ReadCell(int i, int j)
        {
            if (ws.Cells[i, j].Value != null || ws.Cells[i, j].Value != 0)
            {
                string c = Convert.ToString(ws.Cells[i, j].Value);
                return c;
            }
            else return null;
        }

        public void WriteToCell(int i, int j, string data)
        {
            ws.Cells[i, j].Value = data;
        }
        public Range SelectRange(int starti, int startj, int endi, int endj)
        {
            Range range = ws.Range[ws.Cells[starti, startj], ws.Cells[endi, endj]];
            return range;
        }
        
        public void SetGistagram(Range range)
        {
            ChartObjects chartob = (ChartObjects)ws.ChartObjects();
            ChartObject ch = chartob.Add(5, 50, 300, 300);
            Chart chart = ch.Chart;
            object misvalue = System.Reflection.Missing.Value;
            chart.ChartType = XlChartType.xl3DColumn;
            chart.SetSourceData(range);
        }

        public void Save()
        {
            wb.Save();
        }

        public void Close()
        {
            wb.Close();
        }
    }
}
