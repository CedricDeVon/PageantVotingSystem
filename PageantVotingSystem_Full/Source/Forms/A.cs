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

namespace PageantVotingSystem.Source.Forms
{
    public partial class A : Form
    {
        private PrivateFontCollection fonts = new PrivateFontCollection();

        public A()
        {
            fonts.AddFontFile("../../Assets/Fonts/Montserrat/static/Montserrat-Regular.ttf");
            fonts.AddFontFile("../../Assets/Fonts/Abril_Fatface/AbrilFatface-Regular.ttf");
            InitializeComponent();

            foreach (Control c in this.Controls)
            {
                c.Font = new Font(fonts.Families[1], 20, FontStyle.Regular);
                c.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            }

            label1.Font = new Font(fonts.Families[0], 96, FontStyle.Regular);
            label2.Font = new Font(fonts.Families[0], 96, FontStyle.Regular);
        }

        private void A_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HoverIn(object sender, EventArgs e)
        {
            if (sender == this.button1)
            {
                this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(0)))), ((int)(((byte)(241)))));
            }
            else if (sender == this.button2)
            {
                this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(0)))), ((int)(((byte)(241)))));
            }
        }

        private void HoverOut(object sender, EventArgs e)
        {
            if (sender == this.button1)
            {
                this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            }
            else if (sender == this.button2)
            {
                this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World");
        }
    }
}
