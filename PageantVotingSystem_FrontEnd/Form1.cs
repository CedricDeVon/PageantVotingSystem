using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judging_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Show();
            button2.Show();
            panel1.Hide();
            button4.Enabled = false;
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Hide();
            button2.Hide();
            panel1.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Hide();
            button2.Hide();
            panel1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Show();
            button2.Show();
            panel1.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminView f = new AdminView();
            f.Show();
  
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            if (textBox1.TextLength > 0 && textBox2.TextLength > 7)
            {
                button4.Enabled = true;
            }
        }
    }
}
