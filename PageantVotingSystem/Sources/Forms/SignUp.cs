
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Loggers;
using PageantVotingSystem.Sources.Security;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class SignUp : Form
    {
        private readonly InformationLayout informationLayout;

        private readonly RadioButtonLayout userRoleOptions;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public SignUp()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            userRoleOptions =
                new RadioButtonLayout(rolesLayoutControl, UserRoleCache.Types);
            topSideNavigationLayout =
                new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideEditUserProfileButton();
            topSideNavigationLayout.HideAboutButton();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                GoBack();
            }
            else if (sender == enterButton)
            {
                SignUpUser();
            }
        }

        private void GoBack()
        {
            informationLayout.StartLoadingMessageDisplay();

            ApplicationFormNavigator.DisplayPreviousForm();
            Clear();

            informationLayout.StopLoadingMessageDisplay();
        }

        private void SignUpUser()
        {
            informationLayout.StartLoadingMessageDisplay();

            Result result = AuthenticateNewUser();
            if (!result.IsSuccessful)
            {
                informationLayout.DisplayErrorMessage(result.Message);
                return;
            }

            ApplicationLogger.LogInformationMessage($"'SignUp' user '{emailInput.Text}' signed up");
            CreateNewUser();
            UpdateUserProfileCache();
            DisplayManagerOrJudgeDashboardForm();
            Clear();

            informationLayout.StopLoadingMessageDisplay();
        }

        private Result AuthenticateNewUser()
        {
            return ApplicationSecurity.AuthenticateNewUser(
                    emailInput.Text,
                    fullNameInput.Text,
                    passwordInput.Text,
                    passwordConfirmationInput.Text,
                    userRoleOptions.Value);
        }

        private void CreateNewUser()
        {
            ApplicationDatabase.CreateNewUser(
                    new UserEntity(
                        emailInput.Text,
                        fullNameInput.Text,
                        userRoleOptions.Value,
                        ApplicationCryptographer.SecurePasswordViaMethod1(passwordInput.Text)
                    )
            );
        }

        private void UpdateUserProfileCache()
        {
            UserProfileCache.Update(
                    new UserEntity(
                        emailInput.Text,
                        fullNameInput.Text,
                        userRoleOptions.Value
                    )
                );
        }

        private void DisplayManagerOrJudgeDashboardForm()
        {
            ApplicationFormNavigator.DisplayManagerOrJudgeDashboardForm(
                    userRoleOptions.Value);
        }

        private void Clear()
        {
            emailInput.Text = "";
            fullNameInput.Text = "";
            passwordInput.Text = "";
            passwordConfirmationInput.Text = "";
            userRoleOptions.Clear();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                GoBack();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SignUpUser();
            }


            if (e.KeyCode == Keys.Escape ||
                e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        }
    }
}
