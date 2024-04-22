
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class AdministerVotingSession : Form
    {
        public InformationLayout InformationLayout { get; private set; }
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public AdministerVotingSession()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == confirmButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
            }
            else if (sender == abortButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
            }
        }
    }
}
