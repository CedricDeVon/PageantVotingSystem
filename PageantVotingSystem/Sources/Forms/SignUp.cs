
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.Security;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.Entities;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class SignUp : Form
    {
        public InformationLayout InformationLayout { get; private set; }

        private readonly RadioButtonLayout userRoleOptions;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public SignUp()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            userRoleOptions = new RadioButtonLayout(rolesLayoutControl, UserRoleCache.Types);
            ApplicationFormNavigator.ListenToFormKeyDownEvent(this);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideEditUserProfileButton();
            topSideNavigationLayout.HideAboutButton();
            topSideNavigationLayout.HideReloadButton();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            InformationLayout.StartLoadingMessageDisplay();
            
            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayStartingMenuForm();
            }
            else if (sender == enterButton)
            {
                Result result = ApplicationSecurity.AuthenticateNewUser(emailInput.Text, fullNameInput.Text, passwordInput.Text, passwordConfirmationInput.Text, userRoleOptions.Value);
                if (!result.IsSuccessful)
                {
                    InformationLayout.DisplayErrorMessage(result.Message);
                    return;
                }

                ApplicationDatabase.CreateNewUser(
                    new UserEntity(emailInput.Text, fullNameInput.Text, userRoleOptions.Value, ApplicationCryptographer.SecurePasswordViaMethod1(passwordInput.Text))
                );
                UserProfileCache.Update(new UserEntity(emailInput.Text, fullNameInput.Text, userRoleOptions.Value));
                ApplicationFormNavigator.DisplayManagerOrJudgeDashboardForm(userRoleOptions.Value);
                ResetAllInputs();
            }

            InformationLayout.StopLoadingMessageDisplay();
        }

        private void ResetAllInputs()
        {
            emailInput.Text = "";
            fullNameInput.Text = "";
            passwordInput.Text = "";
            passwordConfirmationInput.Text = "";
            userRoleOptions.Clear();
        }
    }
}
