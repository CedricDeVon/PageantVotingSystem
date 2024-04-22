
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EditEventProfile : Form
    {
        private readonly InformationLayout informationBox;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly RadioButtonLayout scoringSystemOptions;
        
        public EditEventProfile()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationBox = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(
                topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
            scoringSystemOptions = new RadioButtonLayout(
                scoringSystemControl,
                ScoringSystemCache.Types);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationBox.StartLoadingMessageDisplay();

            if (sender == saveButton)
            {
                UpdateCache();
                ApplicationFormNavigator.DisplayPrevious();
            }
            else if (sender == resetButton)
            {
                ClearCache();
                ClearInputs();
            }

            informationBox.StopLoadingMessageDisplay();
        }

        private void Form_VisibleChanged(object sender, EventArgs e)
        {
            UpdateInputs();
        }

        private void UpdateInputs()
        {
            SetInputs(
                EditEventCache.Event.DateTimeStart,
                EditEventCache.Event.Name,
                EditEventCache.Event.HostAddress,
                EditEventCache.Event.Description,
                EditEventCache.Event.ScoringSystemType);
        }

        private void ClearInputs()
        {
            SetInputs(DateTime.Now.ToString());
            scoringSystemOptions.Clear();
        }

        private void UpdateCache()
        {
            SetCache(
                DateTime.Now.ToString(),
                name.Text,
                hostAddress.Text,
                description.Text,
                scoringSystemOptions.Value);
        }

        private void ClearCache()
        {
            SetCache(DateTime.Now.ToString());
        }

        private void SetCache(
            string scheduledDataValue,
            string name = "",
            string hostAddress = "",
            string description = "",
            string scoringSystemType = "")
        {
            EditEventCache.Event.Name = name;
            EditEventCache.Event.HostAddress = hostAddress;
            EditEventCache.Event.DateTimeStart = scheduledDataValue;
            EditEventCache.Event.Description = description;
            EditEventCache.Event.ScoringSystemType = scoringSystemType;
        }

        private void SetInputs(
            string scheduledDataValue,
            string nameValue = "",
            string hostAddressValue = "",
            string descriptionValue = "",
            string scoringSystemType = "")
        {
            name.Text = nameValue;
            hostAddress.Text = hostAddressValue;
            scheduledData.Value = DateTime.Parse(scheduledDataValue);
            description.Text = descriptionValue;
            scoringSystemOptions.Value = scoringSystemType;
        }

    }
}
