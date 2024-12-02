using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Popytka_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button_1 form = new Button_1();
            form.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Button_2 form = new Button_2();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button_3 form = new Button_3();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Button_4 form = new Button_4();
            form.ShowDialog();
        }
    }
}
