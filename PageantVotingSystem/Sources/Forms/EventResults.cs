
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Caches;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EventResults : Form
    {
        private readonly InformationLayout informationLayout;
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly SingleValuedItemLayout dataOptionsLayout;

        private readonly SingleValuedItemLayout resultLayout;

        public EventResults()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            dataOptionsLayout = new SingleValuedItemLayout(dataOptionsLayoutControl, DataOptionsLayoutItem_Click);
            resultLayout = new SingleValuedItemLayout(resultLayoutControl, ResultLayoutItem_Click);
            dataOptionsLayout.Render(new List<string>() { "Layout", "Judges", "Contestants" });
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();

            if (sender == goBackButton)
            {
                DisplayPreviousForm();
            }
            else if (sender == searchButton)
            {
                QueryResults();
            }
            else if (sender == resetButton)
            {
                Clear();
            }

            informationLayout.StopLoadingMessageDisplay();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                DisplayPreviousForm();
            }
        }

        private void DisplayPreviousForm()
        {
            ApplicationFormNavigator.DisplayPreviousForm();
            Clear();
        }

        private void DataOptionsLayoutItem_Click(object sender, EventArgs e)
        {
            SingleValuedItem item = (SingleValuedItem)sender;
            if (item.Features.IsToggled)
            {
                resultsControl.Hide();
                resetButton.Hide();
            }
            else
            {
                resultsControl.Show();
                resetButton.Show();
            }
        }

        private void ResultLayoutItem_Click(object sender, EventArgs e)
        {
            SingleValuedItem dataOptionItem = dataOptionsLayout.SelectedItem;
            if (dataOptionItem == null)
            {
                return;
            }

            EventEntity eventEntity = (EventEntity)((SingleValuedItem)sender).Data;
            if (dataOptionItem.Value == "Layout")
            {
                ApplicationFormNavigator.DisplayEventLayoutForm(eventEntity);
            }
            else if (dataOptionItem.Value == "Judges")
            {
                ApplicationFormNavigator.DisplayEventJudgesForm(eventEntity);
            }
            else if (dataOptionItem.Value == "Contestants")
            {
                ApplicationFormNavigator.DisplayEventContestantsForm(eventEntity);
            }
            Clear();
        }

        private void QueryResults()
        {
            resultLayout.Clear();
            List<EventEntity> eventEntities = ApplicationDatabase.ReadManyEventEntitiesBasedOnManagerEmail(
                eventNameInput.Text,
                UserProfileCache.Data.Email);
            resultCountLabel.Text = $"{eventEntities.Count}";
            foreach (EventEntity eventEntity in eventEntities)
            {
                resultLayout.Render(eventEntity.Name, eventEntity);
            }
        }

        private void Clear()
        {
            eventNameInput.Text = "";
            dataOptionsLayout.Unfocus();
            resultCountLabel.Text = "0";
            resultLayout.Clear();
            resultsControl.Hide();
            resetButton.Hide();
        }
    }
}
