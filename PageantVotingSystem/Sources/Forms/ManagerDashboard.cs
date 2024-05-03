
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class ManagerDashboard : Form
    {
        private readonly InformationLayout informationLayout;
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public ManagerDashboard()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();
            
            if (sender == editEventButton)
            {
                ApplicationFormNavigator.DisplayEditEventForm();
            }
            else if (sender == administerEventButton)
            {
                ApplicationFormNavigator.DisplayAdministerEventQueryForm();
            }
            else if (sender == eventResultsEvent)
            {
                ApplicationFormNavigator.DisplayEventResultsForm();
            }

            informationLayout.StopLoadingMessageDisplay();
        }
    }
}
