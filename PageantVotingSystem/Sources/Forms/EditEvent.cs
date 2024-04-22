
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.FormStyles;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EditEvent : Form
    {
        private readonly InformationLayout informationLayout;
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public EditEvent()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();

            if (sender == informationButton)
            {
                ApplicationFormNavigator.DisplayEditEventProfileForm();
            }
            else if (sender == structureButton)
            {
                ApplicationFormNavigator.DisplayEditEventSegmentStructureForm();
            }
            else if (sender == judgesButton)
            {
                ApplicationFormNavigator.DisplayEditEventJudgesForm();
            }
            else if (sender == contestantsButton)
            {
                ApplicationFormNavigator.DisplayEditEventContestantsForm();
            }
            else if (sender == saveButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
            }
            else if (sender == resetButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
            }

            informationLayout.StopLoadingMessageDisplay();
        }
    }
}

