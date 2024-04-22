using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class StartingMenu : Form
    {
        public InformationLayout InformationLayout { get; private set; }

        private readonly TopSideNavigationLayout topSideNavigationLayout;
        
        public StartingMenu()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideEditUserProfileButton();
            topSideNavigationLayout.HideReloadButton();
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
