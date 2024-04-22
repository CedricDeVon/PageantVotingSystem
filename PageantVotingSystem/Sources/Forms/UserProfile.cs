
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.Configurations;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.ResourceLoaders;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class UserProfile : Form
    {
        public InformationLayout InformationLayout { get; private set; }

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public UserProfile()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
        }

        public void Render(UserEntity userEntity)
        {
            userProfileImageInput.Image =  ApplicationResourceLoader.SafeLoadResource(userEntity.ImageResourcePath);
            eventRoleLabel.Text = userEntity.UserRoleType;
            emailLabel.Text = userEntity.Email;
            fullNameLabel.Text = userEntity.FullName;
            descriptionLabel.Text = userEntity.Description;
        }

        private void ResetAllData()
        {
            userProfileImageInput.Image = ApplicationResourceLoader.SafeLoadResource(ApplicationConfiguration.DefaultUserProfileImagePath);
            eventRoleLabel.Text = "";
            emailLabel.Text = "";
            fullNameLabel.Text = "";
            descriptionLabel.Text = "";
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
                ResetAllData();
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ApplicationFormNavigator.DisplayPrevious();
                ResetAllData();
            }
        }
    }
}
