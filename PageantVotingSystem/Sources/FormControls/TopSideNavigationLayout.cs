
using System;
using System.Windows.Forms;

using PageantVotingSystem.Properties;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class TopSideNavigationLayout : UserControl
    {
        public event EventHandler DisplayEditUserProfile;

        public event EventHandler DisplayApplicationAbout;

        public event EventHandler ReloadApplication;

        public event EventHandler ExitApplication;

        public event EventHandler ButtonBeforeClick;

        public event EventHandler ButtonAfterClick;

        private readonly Panel parentControl;

        public TopSideNavigationLayout(Panel parentControl)
        {
            ThrowIfParentControlIsNull(parentControl);

            InitializeComponent();
            this.parentControl = parentControl;
            this.parentControl.Controls.Add(control);
        }

        public void HideEditUserProfileButton()
        {
            editUserProfileControl.Hide();
        }

        public void ShowEditUserProfileButton()
        {
            editUserProfileControl.Show();
        }

        public void HideAboutButton()
        {
            aboutControl.Hide();
        }

        public void ShowAboutButton()
        {
            aboutControl.Show();
        }

        public void HideReloadButton()
        {
            reloadControl.Hide();
        }

        public void ShowReloadButton()
        {
            reloadControl.Show();
        }

        public void HideExitButton()
        {
            exitControl.Hide();
        }

        public void ShowExitButton()
        {
            exitControl.Show();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ButtonBeforeClick?.Invoke(this, e);

            if (sender == editUserProfileButton)
            {
                DisplayEditUserProfile?.Invoke(this, e);
                ApplicationFormNavigator.DisplayEditUserProfileForm();
            }
            else if (sender == aboutButton)
            {
                DisplayApplicationAbout?.Invoke(this, e);
                ApplicationFormNavigator.DisplayAboutForm();
            }
            else if (sender == reloadButton)
            {
                ReloadApplication?.Invoke(this, e);
            }
            else if (sender == exitButton)
            {
                ExitApplication?.Invoke(this, e);
                ApplicationFormNavigator.StopDisplay();
            }

            ButtonAfterClick?.Invoke(this, e);
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (sender == editUserProfileButton)
            {
                ApplicationFormStyle.UpdateImage(editUserProfileButton, Resources.PersonCircleHover32x32);
            }
            else if (sender == aboutButton)
            {
                ApplicationFormStyle.UpdateImage(aboutButton, Resources.InformationHover32x32);
            }
            else if (sender == reloadButton)
            {
                ApplicationFormStyle.UpdateImage(reloadButton, Resources.ArrowClockwiseHover32x32);
            }
            else if (sender == exitButton)
            {
                ApplicationFormStyle.UpdateImage(exitButton, Resources.XHover32x32);
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender == editUserProfileButton)
            {
                ApplicationFormStyle.UpdateImage(editUserProfileButton, Resources.PersonCircleNormal32x32);
            }
            else if (sender == aboutButton)
            {
                ApplicationFormStyle.UpdateImage(aboutButton, Resources.InformationNormal32x32);
            }
            else if (sender == reloadButton)
            {
                ApplicationFormStyle.UpdateImage(reloadButton, Resources.ArrowClockwiseNormal32x32);
            }
            else if (sender == exitButton)
            {
                ApplicationFormStyle.UpdateImage(exitButton, Resources.XNormal32x32);
            }
        }

        private void ThrowIfParentControlIsNull(Panel parentControl)
        {
            if (parentControl == null)
            {
                throw new Exception("'TopSideNavigationLayout' - 'parentControl' cannot be null");
            }
        }

    }
}
