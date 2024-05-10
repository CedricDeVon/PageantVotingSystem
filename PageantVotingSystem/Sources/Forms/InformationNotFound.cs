
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class InformationNotFound : Form
    {
        public InformationLayout InformationLayout { get; private set; }

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public InformationNotFound()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(panel19);
            topSideNavigationLayout = new TopSideNavigationLayout(panel11);
            topSideNavigationLayout.HideEditUserProfileButton();
            topSideNavigationLayout.HideAboutButton();

            ApplicationFormNavigator.ListenToFormKeyDownEvent(this);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPreviousForm();
            }
        }
    }
}
