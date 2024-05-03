
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Security;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EditEventProfile : Form
    {
        private readonly InformationLayout informationLayout;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly RadioButtonLayout scoringSystemOptions;
        
        public EditEventProfile()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(
                topSideNavigationLayoutControl);
            scoringSystemOptions = new RadioButtonLayout(
                scoringSystemControl,
                ScoringSystemCache.Types);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();

            if (sender == goBackButton)
            {
                UpdateCache();
                ApplicationFormNavigator.DisplayPreviousForm();
            }
            else if (sender == resetButton)
            {
                ClearCache();
                ClearInputs();
            }

            informationLayout.StopLoadingMessageDisplay();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                UpdateCache();
                ApplicationFormNavigator.DisplayPreviousForm();
                e.Handled = true;
            }
        }

        public void Render()
        {
            UpdateInputs();
        }

        private void UpdateInputs()
        {
            SetInputs(
                EditEventCache.EventEntity.DateTimeStart,
                EditEventCache.EventEntity.Name,
                EditEventCache.EventEntity.HostAddress,
                EditEventCache.EventEntity.Description,
                EditEventCache.EventEntity.ScoringSystemType);
        }

        private void ClearInputs()
        {
            SetInputs(DateTime.Now.ToString());
            scoringSystemOptions.Clear();
        }

        private void UpdateCache()
        {
            SetCache(
                scheduledData.Text,
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
            EditEventCache.EventEntity.Name = name;
            EditEventCache.EventEntity.HostAddress = hostAddress;
            EditEventCache.EventEntity.DateTimeStart = scheduledDataValue;
            EditEventCache.EventEntity.Description = description;
            EditEventCache.EventEntity.ScoringSystemType = scoringSystemType;
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
