
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Loggers;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.Miscellaneous;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.ResourceLoaders;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EditUserProfile : Form
    {
        private readonly InformationLayout informationLayout;
     
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public EditUserProfile()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideEditUserProfileButton();
            topSideNavigationLayout.HideAboutButton();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                DisplayPreviousForm();
            }
            else if (sender == signOutButton)
            {
                LogOutAndDisplayStartingMenuForm();
            }
            else if (sender == userProfileImageInput)
            {
                userImageProfileFileDialog.ShowDialog(this);
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                DisplayPreviousForm();
                e.Handled = true;
            }
        }

        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (sender == userImageProfileFileDialog)
            {
                SetupPictureBox(userImageProfileFileDialog.FileName);
            }
        }

        private void DisplayPreviousForm()
        {
            informationLayout.StartLoadingMessageDisplay();

            ApplicationFormNavigator.DisplayPreviousForm();
            UpdateOldUser();

            informationLayout.StopLoadingMessageDisplay();
        }

        private void LogOutAndDisplayStartingMenuForm()
        {
            informationLayout.StartLoadingMessageDisplay();

            ApplicationLogger.LogInformationMessage($"'EditUserProfile' user '{UserProfileCache.Data.Email}' loged out");
            UserProfileCache.Clear();
            ApplicationFormNavigator.LogOut();

            informationLayout.StopLoadingMessageDisplay();
        }

        public void Render()
        {
            UpdateInputs();
        }

        private void UpdateInputs()
        {
            SetInputs(
                UserProfileCache.Data.Email,
                UserProfileCache.Data.UserRoleType,
                UserProfileCache.Data.FullName,
                UserProfileCache.Data.Description);
        }

        private void UpdateOldUser()
        {
            UserEntity entity = new UserEntity(
                userEmailLabel.Text,
                userFullNameInput.Text, 
                userRoleTypeLabel.Text,
                userDescriptionInput.Text,
                StringParser.StandardizeFilePath(userImageProfileFileDialog.FileName));
            UserProfileCache.Update(entity);
            ApplicationDatabase.UpdateOldUser(entity);
        }

        private void SetInputs(
            string email = "",
            string userRoleType = "",
            string fullName = "",
            string description = "")
        {
            userEmailLabel.Text = email;
            userRoleTypeLabel.Text = userRoleType;
            userFullNameInput.Text = fullName;
            userDescriptionInput.Text = description;
            SetupPictureBox(UserProfileCache.Data.ImageResourcePath);
        }

        private void SetupPictureBox(string fileName)
        {
            userProfileImageInput.SizeMode = PictureBoxSizeMode.Zoom;
            userProfileImageInput.ClientSize = new Size(
                userProfileImageInput.Size.Width,
                userProfileImageInput.Size.Height);
            userProfileImageInput.Image = ApplicationResourceLoader.SafeLoadResource(
                StringParser.StandardizeFilePath(fileName));
        }
    }
}
