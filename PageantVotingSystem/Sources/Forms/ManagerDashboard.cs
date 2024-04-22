
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.FormStyles;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class ManagerDashboard : Form
    {
        public InformationLayout InformationLayout { get; private set; }
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public ManagerDashboard()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            InformationLayout.StartLoadingMessageDisplay();

            if (sender == editEventButton)
            {
                ApplicationFormNavigator.DisplayEditEventForm();
            }
            else if (sender == administerEventButton)
            {
                ApplicationFormNavigator.DisplayAdministerEventForm();
            }
            else if (sender == eventResultsEvent)
            {
                ApplicationFormNavigator.DisplayEventResultsForm();
            }

            InformationLayout.StopLoadingMessageDisplay();
        }
    }
}
