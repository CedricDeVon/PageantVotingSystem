
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class AdministerEventContestants : Form
    {
        public InformationLayout InformationLayout { get; private set; }
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public AdministerEventContestants()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == profileButton)
            {
                // ApplicationFormNavigator.DisplayEventContestantProfileForm();
            }
            else if (sender == toggleButton)
            {
                // 
            }
            else if (sender == saveButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
            }
            else if (sender == resetButton)
            {
                //
            }
        }
    }
}
