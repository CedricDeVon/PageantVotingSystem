
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class AdministerEvent : Form
    {
        public InformationLayout InformationLayout { get; private set; }
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public AdministerEvent()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == layoutButton)
            {
                ApplicationFormNavigator.DisplayAdministerEventLayoutForm();
            }
            else if (sender == judgesButton)
            {
                ApplicationFormNavigator.DisplayAdministerEventJudgesForm();
            }
            else if (sender == contestantsButton)
            {
                ApplicationFormNavigator.DisplayAdministerEventContestantsForm();
            }
            else if (sender == startVotingButton)
            {
                ApplicationFormNavigator.DisplayAdministerVotingSessionForm();
            }
            else if (sender == goBackButton)
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
