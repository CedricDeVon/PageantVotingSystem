
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Loggers;
using PageantVotingSystem.Sources.Security;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class LogIn : Form
    {
        private readonly InformationLayout informationLayout;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public LogIn()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
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
                LogInUser();
            }
        }

        private void GoBack()
        {
            informationLayout.StartLoadingMessageDisplay();

            ApplicationFormNavigator.DisplayStartingMenuForm();
            Clear();

            informationLayout.StopLoadingMessageDisplay();
        }

        private void LogInUser()
        {
            informationLayout.StartLoadingMessageDisplay();

            Result securityResult = ReadTargetUser();
            if (!securityResult.IsSuccessful)
            {
                informationLayout.DisplayErrorMessage(securityResult.Message);
                return;
            }

            ApplicationLogger.LogInformationMessage($"'LogIn' user '{securityResult.GetData<string>("email")}' loged in");
            UserProfileCache.Update(securityResult);
            ApplicationFormNavigator.DisplayManagerOrJudgeDashboardForm(
                securityResult.GetData<string>("user_role_type"));
            Clear();

            informationLayout.StopLoadingMessageDisplay();
        }

        private Result ReadTargetUser()
        {
            return ApplicationSecurity.AuthenticateOldUser(emailInput.Text, passwordInput.Text);
        }

        private void Clear()
        {
            emailInput.Text = "";
            passwordInput.Text = "";
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                GoBack();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                LogInUser();
            }


            if (e.KeyCode == Keys.Escape ||
                e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        }
    }
}
