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
    public partial class Data : Form
    {

        public Data()
        {
            InitializeComponent();
            button2.Enabled = false;
        }


        char s = 'А';
        char h = 'я';
        char s1 = '0';
        char h1 = '9';

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
                if ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != ' '))
                    if ((e.KeyChar < s) || (e.KeyChar > h)) e.Handled = true;
                    
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
                if ((e.KeyChar != (char)Keys.Back) && (e.KeyChar != ' '))
                    if ((e.KeyChar < s1) || (e.KeyChar > h1)) e.Handled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked) button2.Enabled = true;
            else button2.Enabled = false;
        }
        string sex;
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox2.Text == "" || radioButton3.Checked == false & radioButton4.Checked == false)
            { MessageBox.Show("Заполните все поля"); }
            else
            {
                if (radioButton3.Checked) sex = "Мужчина";
                else sex = "Женщина";
                Курсовая_работа.Program.people.Add(new Person(textBox2.Text, sex, Convert.ToInt16(textBox4.Text)));
                Курсовая_работа.Program.pers = Курсовая_работа.Program.people.Last<Person>();
                Form f = new B1(); f.Show(); this.Close();
            }
        }

    }
}
