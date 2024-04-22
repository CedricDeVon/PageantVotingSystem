
using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Caches;
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
        public InformationLayout InformationLayout { get; private set; }
     
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public EditUserProfile()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
            topSideNavigationLayout.HideEditUserProfileButton();
            topSideNavigationLayout.HideAboutButton();
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

        private void UpdateCache()
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

        private void Button_Click(object sender, EventArgs e)
        {
            InformationLayout.StartLoadingMessageDisplay();

            if (sender == saveButton)
            {
                UpdateCache();
                ApplicationFormNavigator.DisplayPrevious();
            }
            else if (sender == signOutButton)
            {
                ApplicationFormNavigator.LogOut();
            }
            else if (sender == userProfileImageInput)
            {
                userImageProfileFileDialog.ShowDialog(this);
            }

            InformationLayout.StopLoadingMessageDisplay();
        }

        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (sender == userImageProfileFileDialog)
            {
                SetupPictureBox(userImageProfileFileDialog.FileName);
            }
        }
    }
}
