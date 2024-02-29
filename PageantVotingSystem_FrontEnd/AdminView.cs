using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judging_System
{
    public partial class AdminView : Form
    {
        public AdminView()
        {
            InitializeComponent();
        }

        private void AdminView_Load(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel6.Visible = false;
            panel7.Visible = false;

            label11.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Add
            panel2.Visible = true;
            panel6.Visible = false;
            panel7.Visible = false;

            label11.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //view

            panel2.Visible = false;
            panel6.Visible = false;

            label11.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Start
            panel2.Visible = false;
            panel6.Visible = false;

            label11.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //print
            panel2.Visible = false;
            panel6.Visible = false;

            label11.Visible = false;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Add event
            panel2.Visible = false;
            panel6.Visible = true;
            panel7.Visible = false;
            label11.Visible = true;
            label11.Text = "ADD EVENT";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Addjudge
            panel2.Visible = false;
            panel6.Visible = false;
            panel7.Visible = true;
            label11.Visible = true;
            label11.Text = "ADD JUDGE";
        }

        private void button7_Click(object sender, EventArgs e)
        {

            //ADD CONTESTANT
            panel2.Visible = false;
            panel6.Visible = false;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
            panel2.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("EVENT SUCCESSFULLY ADDED!");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("JUDGE SUCCESSFULLY ADDED!");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            label11.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel2.Visible = true;
        }
    }
}
