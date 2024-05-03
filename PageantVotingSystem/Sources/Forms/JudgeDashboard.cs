
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
            topSideNavigationLayout =
                new TopSideNavigationLayout(topSideNavigationLayoutControl);
            eventQueryLayout = new SingleValuedItemLayout(eventQueryLayoutControl);
            eventQueryLayout.ItemSingleClick += new EventHandler(EventQueryItem_SingleClick);
            selectedContestantLayout = new OrderedValueItemLayout(contestantsLayoutControl);
            selectedContestantLayout.ItemSingleClick +=
                new EventHandler(SelectedContestantItem_SingleClick);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();

            if (sender == searchButton)
            {
                SearchForEventEntities();
            }
            else if (sender == judgeButton)
            {
                DisplayJudgeContestantDashboardForm();
            }
            else if (sender == resetButton)
            {
                Clear();
            }

            informationLayout.StopLoadingMessageDisplay();
        }

        private void EventQueryItem_SingleClick(object sender, EventArgs e)
        {
            if (eventQueryLayout.SelectedItem == null ||
                eventQueryLayout.SelectedItem != sender)
            {
                SingleValuedItem singleValuedItem = (SingleValuedItem)sender;
                EventLayoutSequenceEntity eventLayoutSequenceEntity =
                    (EventLayoutSequenceEntity)singleValuedItem.Data;
                List<ContestantEntity> contestantEntities =
                    ApplicationDatabase.ReadManyUnjudgedContestantEntities(
                        eventLayoutSequenceEntity.Round.Id,
                        UserProfileCache.Data.Email);
                contestantCountLabel.Text = $"{contestantEntities.Count}";
                selectedContestantLayout.Clear();
                optionsControl.Hide();
                foreach (ContestantEntity contestantEntity in contestantEntities)
                {
                    selectedContestantLayout.Render(
                        $"{contestantEntity.OrderNumber}",
                        contestantEntity.FullName,
                        contestantEntity);
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
            if (selectedContestantLayout.SelectedItem == null ||
                selectedContestantLayout.SelectedItem != sender)
            {
                optionsControl.Show();
            }
            else
            {
                optionsControl.Hide();
            }
        }

        private void SearchForEventEntities()
        {
            List<EventLayoutSequenceEntity> eventLayoutSequenceEntities =
            ApplicationDatabase.ReadManyPendingEventLayoutSequenceEntitiesBasedOnJudgeUserEmail(
                eventQueryInput.Text, UserProfileCache.Data.Email);
            Clear();
            eventQueryResultCountLabel.Text = $"{eventLayoutSequenceEntities.Count}";
            foreach (EventLayoutSequenceEntity eventLayoutSequenceEntity in eventLayoutSequenceEntities)
            {
                eventQueryLayout.Render(
                    eventLayoutSequenceEntity.Event.Name, eventLayoutSequenceEntity);
            }
        }

        private void DisplayJudgeContestantDashboardForm()
        {
            ContestantEntity contestantEntity =
                    (ContestantEntity)selectedContestantLayout.SelectedItem.Data;
            EventLayoutSequenceEntity eventSequenceLayoutEntity =
                (EventLayoutSequenceEntity)eventQueryLayout.SelectedItem.Data;
            eventSequenceLayoutEntity.Segment =
                ApplicationDatabase.ReadOneSegmentEntity(
                    eventSequenceLayoutEntity.Segment.Id);
            eventSequenceLayoutEntity.Round =
                ApplicationDatabase.ReadOneRoundEntity(eventSequenceLayoutEntity.Round.Id);
            ApplicationFormNavigator.DisplayJudgeContestantDashboardForm(
                eventSequenceLayoutEntity, contestantEntity);
            Clear();
        }

        private void Clear()
        {
            eventQueryInput.Text = "";
            eventQueryResultCountLabel.Text = "0";
            contestantCountLabel.Text = "0";
            eventQueryLayout.Clear();
            selectedContestantLayout.Clear();
            optionsControl.Hide();
        }
    }
}
