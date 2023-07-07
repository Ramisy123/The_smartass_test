using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая_работа
{
    public partial class B2 : Form
    {
        char s = '1';
        char h = '6';

        public B2()
        {
            InitializeComponent();
            
        }

        string[] ans = new string[6] { "Дело не сдвинется с места, ", "Ласточка весну начинает, ", "Дважды в год ", "Зимой солнце ", "В июне первую ягоду в рот кладут, ", "Осенью скот жиреет, " };

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
                if ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != ' '))
                    if ((e.KeyChar < s) || (e.KeyChar > h)) e.Handled = true;

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
                if ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != ' '))
                    if ((e.KeyChar < s) || (e.KeyChar > h)) e.Handled = true;

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
                if ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != ' '))
                    if ((e.KeyChar < s) || (e.KeyChar > h)) e.Handled = true;

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
                if ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != ' '))
                    if ((e.KeyChar < s) || (e.KeyChar > h)) e.Handled = true;

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
                if ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != ' '))
                    if ((e.KeyChar < s) || (e.KeyChar > h)) e.Handled = true;

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
                if ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != ' '))
                    if ((e.KeyChar < s) || (e.KeyChar > h)) e.Handled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ans[Convert.ToInt16(textBox1.Text) - 1] += label14.Text;
                ans[Convert.ToInt16(textBox2.Text) - 1] += label15.Text;
                ans[Convert.ToInt16(textBox3.Text) - 1] += label18.Text;
                ans[Convert.ToInt16(textBox4.Text) - 1] += label19.Text;
                ans[Convert.ToInt16(textBox5.Text) - 1] += label16.Text;
                ans[Convert.ToInt16(textBox6.Text) - 1] += label17.Text;
            }
            catch { ans = new string[6] { "", "", "", "", "", "" }; }
            Курсовая_работа.Program.pers.SetAnser(ans);
            Form f = new Finish(); f.Show(); this.Close();
        }
    }
}
