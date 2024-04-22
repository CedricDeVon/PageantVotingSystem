using System;
using System.Drawing.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Org.BouncyCastle.Utilities;
using Svg;
using System.Xml;
using Svg.FilterEffects;


namespace PageantVotingSystem
{
    public partial class Template : Form
    {
        public Template()
        {
            InitializeComponent();

            // Importing Fonts
            //byte[] fontData = Properties.Resources.AbrilFatface_Regular;
            //IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            //System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            //fonts.AddMemoryFont(fontPtr, Properties.Resources.AbrilFatface_Regular.Length);
            //System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            //foreach (Control c in this.Controls)
            //{
            //    c.Font = Font;
            //}

            // Project Directory Path
            //string assmblypath = Assembly.GetEntryAssembly().Location;
            //string appPath = Path.GetDirectoryName(assmblypath);
            //MessageBox.Show(appPath);


            // Exe Title
            // this.Text = "Pageant Voting System";

            // Exe Icon
            // this.Icon = Properties.Resources.icon;

            // Windows State
            // this.WindowState = FormWindowState.Maximized;
            // this.WindowState = FormWindowState.Minimized;
            // this.WindowState = FormWindowState.Normal;

            // Minimal and Maximum Windows Size
            // this.MinimumSize = new Size(500, 700);
            // this.MaximumSize = new Size(1440, 1080);
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(0)))), ((int)(((byte)(241)))));
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Template_Load(object sender, EventArgs e)
        {

        }

        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            this.label4.Text = Convert.ToString(((TrackBar)sender).Value);
        }

        private void Image_MouseEnter(object sender, EventArgs e)
        {
            // ((Button) sender).Image = Properties.Resources.favicon_16x161;
        }

        private void Image_MouseLeave(object sender, EventArgs e)
        {
            // ((Button)sender).Image = Properties.Resources.favicon_16x16;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
