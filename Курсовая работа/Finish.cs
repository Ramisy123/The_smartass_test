using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Drawing.Printing;
using System.IO;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Курсовая_работа
{
    public partial class Finish : Form
    {
        public Finish()
        {
            InitializeComponent();
            label4.Text += Convert.ToString(Курсовая_работа.Program.pers.Result);
            ExcelDoc excel = new ExcelDoc(@"C:\Users\1\Desktop\Курсовая работа\Курсовая работа\Resources\Результаты.xlsx", 1);
            int x = 3;
            do
            {
                x++;
            }
            while (excel.ReadCell(x, 1) != null);
            excel.WriteToCell(x, 1, Курсовая_работа.Program.pers.name);
            excel.WriteToCell(x, 2, Курсовая_работа.Program.pers.sex);
            excel.WriteToCell(x, 3, Курсовая_работа.Program.pers.age.ToString());
            excel.WriteToCell(x, 16, Курсовая_работа.Program.pers.res.ToString());
            int z = 4;
            foreach (Int16 c in Курсовая_работа.Program.pers.answers1) excel.WriteToCell(x, z++, c.ToString());
            foreach (string c in Курсовая_работа.Program.pers.answers2) excel.WriteToCell(x, z++, c);

            excel.Save(); excel.Close();
        }
        WordDoc wordDocSv, wordDocSh;
        private void пройтиЗановоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
        }

        private void пройтиЗановоToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Курсовая_работа.Program.pers.res = 0;
            Form f = new B1(); f.Show(); this.Close();
        }

        private void посмотретьПравильныеОтветыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new Answers(); f.Show(); this.Close();
        }


        private void шаблонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wordDocSh = null;
            try
            {
                wordDocSh = new WordDoc(@"C:\Users\1\Desktop\Курсовая работа\Курсовая работа\Resources\Шаблон.docx");
                Person p = Курсовая_работа.Program.pers;
                wordDocSh.TextToTemplate(p);
            }
            catch (Exception error)
            {
                if (wordDocSh != null) { wordDocSh.Close(); }
                MessageBox.Show("Ошибка при замене текста на метке " +
                                    "в документе  Word. Подробности " + error.Message);
                return;
            }
            wordDocSh.Visible = true;
        }

        private void свободнаяФормаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wordDocSv = null;
            try
            {
                wordDocSv = new WordDoc(@"C:\Users\1\Desktop\Курсовая работа\Курсовая работа\Resources\Свобода.docx");
                Person p = Курсовая_работа.Program.pers;
                wordDocSv.TextToFree(p);
            }
            catch (Exception error)
            {
                if (wordDocSv != null) { wordDocSv.Close(); }
                MessageBox.Show("Ошибка при замене текста на метке в " +
                                "документе  Word. Подробности " + error.Message);
                return;
            }
            wordDocSv.Visible = true;
            MessageBox.Show("Сохраните файл");
        }

        private string documentContents;

        private string stringToPrint;

        private void ReadDocument()
        {
            string docName = Курсовая_работа.Program.pers.name+".docx";
            string docPath = @"C:\Users\1\Desktop\Курсовая работа\Курсовая работа\Resources\Шаблоны\";
            printDocument1.DocumentName = docName;
            using (FileStream stream = new FileStream(docPath + docName, FileMode.Open))
            using (StreamReader reader = new StreamReader(stream))
            {
                documentContents = reader.ReadToEnd();
            }
            stringToPrint = documentContents;
        }
        void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;

            e.Graphics.MeasureString(stringToPrint, this.Font,
                e.MarginBounds.Size, StringFormat.GenericTypographic,
                out charactersOnPage, out linesPerPage);

            e.Graphics.DrawString(stringToPrint, this.Font, Brushes.Black,
            e.MarginBounds, StringFormat.GenericTypographic);

            stringToPrint = stringToPrint.Substring(charactersOnPage);

            e.HasMorePages = (stringToPrint.Length > 0);

            if (!e.HasMorePages)
                stringToPrint = documentContents;
        }

        private void разработчикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Максмова ММ ст гр АИБ-18");
        }

        private void предыдущиеРезультатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new Hystory(); f.Show(); this.Close();
        }

        private void тестированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void завершитьТестированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new Start(); f.Close(); this.Close();
            System.Windows.Forms.Application.Exit();
        }

        private void петатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadDocument();
            printDocument1.Print();
        }
        
    }
}
