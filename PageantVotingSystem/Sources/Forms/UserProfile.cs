
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.ResourceLoaders;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class UserProfile : Form
    {
        private readonly InformationLayout informationLayout;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public UserProfile()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                DisplayPreviousForm();
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DisplayPreviousForm();
            }
        }

        private void DisplayPreviousForm()
        {
            informationLayout.StartLoadingMessageDisplay();

            ApplicationFormNavigator.DisplayPreviousForm();
            Clear();

            informationLayout.StopLoadingMessageDisplay();
        }

        public void Render(string email)
        {
            Update(ApplicationDatabase.ReadOneUserEntity(email));
        }

        private void Clear()
        {
            Update(new UserEntity());
        }

        private void Update(UserEntity userEntity)
        {
            userProfileImage.Image = ApplicationResourceLoader.SafeLoadResource(userEntity.ImageResourcePath);
            eventRoleLabel.Text = userEntity.UserRoleType;
            emailLabel.Text = userEntity.Email;
            fullNameLabel.Text = userEntity.FullName;
            descriptionLabel.Text = userEntity.Description;
        }
    }
}
