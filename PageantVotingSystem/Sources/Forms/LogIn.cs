
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.Security;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.FormControls;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class LogIn : Form
    {
        public InformationLayout InformationLayout { get; private set; }

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public LogIn()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideEditUserProfileButton();
            topSideNavigationLayout.HideAboutButton();
            topSideNavigationLayout.HideReloadButton();
            ApplicationFormNavigator.ListenToFormKeyDownEvent(this);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            InformationLayout.StartLoadingMessageDisplay();

            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
            }
            else if (sender == enterButton)
            {
                Result securityResult = ApplicationSecurity.AuthenticateOldUser(
                    emailInput.Text, passwordInput.Text);
                if (!securityResult.IsSuccessful)
                {
                    InformationLayout.DisplayErrorMessage(securityResult.Message);
                    return;
                }

                UserProfileCache.Update(securityResult);
                ApplicationFormNavigator.DisplayManagerOrJudgeDashboardForm(
                    securityResult.GetData<string>("user_role_type"));
                ResetAllInputs();
            }
 
            InformationLayout.StopLoadingMessageDisplay();
        }

        private void ResetAllInputs()
        {
            emailInput.Text = "";
            passwordInput.Text = "";
        }
    }
}
