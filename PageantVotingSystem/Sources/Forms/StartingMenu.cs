
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class StartingMenu : Form
    {
        private readonly InformationLayout informationLayout;

        private readonly TopSideNavigationLayout topSideNavigationLayout;
        
        public StartingMenu()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideEditUserProfileButton();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == logInButton)
            {
                ApplicationFormNavigator.DisplayLogInForm();
            }
            else if (sender == signUpButton)
            {
                ApplicationFormNavigator.DisplaySignUpForm();
            }
        }
    }
}
