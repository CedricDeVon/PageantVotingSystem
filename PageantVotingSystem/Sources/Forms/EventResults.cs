
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.Entities;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EventResults : Form
    {
        public InformationLayout InformationLayout { get; private set; }
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly SingleValuedItemLayout dataOptionsLayout;

        private readonly SingleValuedItemLayout resultLayout;

        public EventResults()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
            dataOptionsLayout = new SingleValuedItemLayout(dataOptionsLayoutControl, DataOptionsLayoutItem_Click);
            resultLayout = new SingleValuedItemLayout(resultLayoutControl, ResultLayoutItem_Click);
            dataOptionsLayout.Render(new List<string>() { "Layout", "Judges", "Contestants" });
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
                ResetAllInputs();
            }
            else if (sender == searchButton)
            {
                resultLayout.Clear();
                QueryResults();
            }
            else if (sender == resetButton)
            {
                ResetAllInputs();
            }
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
            ResetAllInputs();
        }

        private void QueryResults()
        {
            List<EventEntity> eventEntities = ApplicationDatabase.ReadManyEventsBasedOnManagerEmail(
                eventNameInput.Text,
                eventManagerEmailInput.Text);
            resultCountLabel.Text = $"{eventEntities.Count}";
            foreach (EventEntity eventEntity in eventEntities)
            {
                resultLayout.Render(eventEntity.Name, eventEntity);
            }
        }

        private void ResetAllInputs()
        {
            eventNameInput.Text = "";
            eventManagerEmailInput.Text = "";
            dataOptionsLayout.Unfocus();
            resultCountLabel.Text = "0";
            resultLayout.Clear();
            resultsControl.Hide();
            resetButton.Hide();
        }
    }
}
