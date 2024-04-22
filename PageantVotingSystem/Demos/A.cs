using System;
using System.Windows.Forms;

namespace PageantVotingSystem.Demos
{
    public partial class A : Form
    {
        public A()
        {
            InitializeComponent();

            c1.Hide();
            c2.Hide();

            this.Controls.Add(c1);
            this.Controls.Add(c2);
        }

        private bool a = true;

        private UserControl c1 = new UserControl1();
        private UserControl c2 = new UserControl2();

        private void Button_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            if (a)
            {
                c2.Show();
                c1.Hide();
                a = false;
            }
            else
            {
                c1.Show();
                c2.Hide();
                a = true;
            }
            this.ResumeLayout(false);

        }
    }
}
