
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class JudgeDashboard : Form
    {
        private readonly InformationLayout informationLayout;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly SingleValuedItemLayout eventQueryLayout;

        private readonly OrderedValueItemLayout selectedContestantLayout;

        public JudgeDashboard()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
            eventQueryLayout = new SingleValuedItemLayout(eventQueryLayoutControl);
            eventQueryLayout.ItemSingleClick += new EventHandler(EventQueryItem_SingleClick);
            selectedContestantLayout = new OrderedValueItemLayout(contestantsLayoutControl);
            selectedContestantLayout.ItemSingleClick += new EventHandler(SelectedContestantItem_SingleClick);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();

            if (sender == searchButton)
            {
                List<EventEntity> eventEntities = ApplicationDatabase.ReadManyOngoingEventsBasedOnJudgeEmail(
                    eventQueryInput.Text, UserProfileCache.Data.Email);
                eventQueryResultCountLabel.Text = $"{eventEntities.Count}";
                eventQueryInput.Text = "";
                eventQueryLayout.Clear();
                contestantCountLabel.Text = "0";
                selectedContestantLayout.Clear();
                optionsControl.Hide();
                foreach (EventEntity eventEntity in eventEntities)
                {
                    eventQueryLayout.Render(eventEntity.Name, eventEntity);
                }
            }
            else if (sender == judgeButton)
            {
                EventEntity eventEntity = (EventEntity) eventQueryLayout.SelectedItem.Data;
                ContestantEntity contestantEntity = (ContestantEntity) selectedContestantLayout.SelectedItem.Data;
                ApplicationFormNavigator.DisplayJudgeContestantDashboardForm(
                    eventEntity, contestantEntity);

                eventQueryInput.Text = "";
                eventQueryResultCountLabel.Text = "0";
                contestantCountLabel.Text = "0";
                eventQueryLayout.Clear();
                selectedContestantLayout.Clear();
                optionsControl.Hide();
            }
            else if (sender == resetButton)
            {
                eventQueryInput.Text = "";
                eventQueryResultCountLabel.Text = "0";
                eventQueryLayout.Clear();
                contestantCountLabel.Text = "0";
                selectedContestantLayout.Clear();
                optionsControl.Hide();
            }

            informationLayout.StopLoadingMessageDisplay();
        }

        private void EventQueryItem_SingleClick(object sender, EventArgs e)
        {
            if (eventQueryLayout.SelectedItem == null)
            {
                SingleValuedItem singleValuedItem = (SingleValuedItem)sender;
                List<ContestantEntity> contestantEntities = ApplicationDatabase.ReadManyUnjudgedContestants(((EventEntity)singleValuedItem.Data).Id);
                contestantCountLabel.Text = $"{contestantEntities.Count}";
                selectedContestantLayout.Clear();
                optionsControl.Hide();
                foreach (ContestantEntity contestantEntity in contestantEntities)
                {
                    selectedContestantLayout.RenderOrdered($"{contestantEntity.OrderNumber}", contestantEntity.FullName, contestantEntity);
                }
            }
            else
            {
                contestantCountLabel.Text = "0";
                selectedContestantLayout.Clear();
                optionsControl.Hide();
            }
        }

        private void SelectedContestantItem_SingleClick(object sender, EventArgs e)
        {
            if (selectedContestantLayout.SelectedItem == null)
            {
                optionsControl.Show();
            }
            else
            {
                optionsControl.Hide();
            }
        }
    }
}
