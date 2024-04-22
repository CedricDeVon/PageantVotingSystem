using System;
using System.Drawing;
using System.Windows.Forms;

namespace PageantVotingSystem.Source.Forms
{
    public partial class Features : Form
    {
        public Features()
        {
            InitializeComponent();

            ShowMyImage("C:\\Users\\Dell\\OneDrive\\workspace\\projects\\work_in_progress\\PageantVotingSystem\\PageantVotingSystem\\PageantVotingSystem\\Assets\\ArrowClockwiseHover16x16.png", 32, 32);

            FormBorderStyle = FormBorderStyle.None;
        }

        private Bitmap MyImage;
        public void ShowMyImage(String fileToDisplay, int xSize, int ySize)
        {
            // Sets up an image object to be displayed.
            if (MyImage != null)
            {
                MyImage.Dispose();
            }

            // Stretches the image to fit the pictureBox.
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            MyImage = new Bitmap(fileToDisplay);
            pictureBox2.ClientSize = new Size(xSize, ySize);
            pictureBox2.Image = (Image)MyImage;

            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            textBox1.Text = "";

            Label newLabel = new Label();
            flowLayoutPanel1.Controls.Add(newLabel);

            newLabel.AutoEllipsis = true;
            newLabel.AutoSize = true;
            newLabel.BackColor = System.Drawing.Color.White;
            newLabel.Location = new System.Drawing.Point(3, 0);
            newLabel.MaximumSize = new System.Drawing.Size(80, 20);
            newLabel.Name = "label";
            newLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            newLabel.Size = new System.Drawing.Size(73, 20);
            newLabel.Text = $"{input}";
            newLabel.Click += Label_Click;
        }

        private int count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = $"{count}";
            count++;
            if (count == 10)
            {
                timer1.Stop();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = dateTimePicker1.Text;
        }

        private Label targetLabel;

        private void Label_Click(object sender, EventArgs e)
        {
            targetLabel = (Label) sender;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (targetLabel == null)
            {
                return;
            }
            else if (sender == button2)
            {
                Label temporary = (Label) flowLayoutPanel1.Controls[(flowLayoutPanel1.Controls.Count - 1 == targetLabel.TabIndex) ? 0 : targetLabel.TabIndex + 1];
                flowLayoutPanel1.Controls.SetChildIndex(targetLabel, temporary.TabIndex);
                flowLayoutPanel1.Controls.SetChildIndex(temporary, targetLabel.TabIndex);
            }
            else if (sender == button3 && flowLayoutPanel1.Controls.Contains(targetLabel))
            {
                targetLabel.Click -= new EventHandler(Label_Click);
                flowLayoutPanel1.Controls.Remove(targetLabel);
                targetLabel.Dispose();
                
            }
        }
    }
}
