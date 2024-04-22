using System;
using System.Windows.Forms;

using PageantVotingSystem.Properties;
using PageantVotingSystem.Source.FormNavigators;
using PageantVotingSystem.Source.FormStyles;

namespace PageantVotingSystem.Source.Forms
{
    public partial class JudgeDashboard : Form
    {
        public JudgeDashboard()
        {
            InitializeComponent();

            ApplicationFormStyle.Setup(this);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == button1)
            {
                ApplicationFormNavigator.Next("UserInformation");
            }
            else if (sender == button2)
            {
                ApplicationFormNavigator.Next("About");
            }
            else if (sender == button5)
            {
                ApplicationFormNavigator.Stop();
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            ApplicationFormStyle.ButtonNewImage(sender, button1, Resources.PersonCircleHover32x32);
            ApplicationFormStyle.ButtonNewImage(sender, button2, Resources.InformationHover32x32);
            ApplicationFormStyle.ButtonNewImage(sender, button5, Resources.XHover32x32);
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            ApplicationFormStyle.ButtonNewImage(sender, button1, Resources.PersonCircleNormal32x32);
            ApplicationFormStyle.ButtonNewImage(sender, button2, Resources.InformationNormal32x32);
            ApplicationFormStyle.ButtonNewImage(sender, button5, Resources.XNormal32x32);
        }
    }
}
