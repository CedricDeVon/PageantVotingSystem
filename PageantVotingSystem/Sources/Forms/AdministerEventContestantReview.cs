
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
    public partial class AdministerEventContestantReview : Form
    {
        private readonly InformationLayout informationLayout;
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly EntityStatusItemLayout contestantLayout;

        public AdministerEventContestantReview()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            contestantLayout = new EntityStatusItemLayout(contestantLayoutControl);
            contestantLayout.ItemSingleClick += Item_SingleClick;
        }

        public void Render()
        {
            contestantLayout.Clear();
            AdministerEventCache.ContestantEntities = ApplicationDatabase.ReadManySimplifiedEventContestants(AdministerEventCache.EventLayoutSequence.Event.Id);
            foreach (ContestantEntity contestantEntity in AdministerEventCache.ContestantEntities)
            {
                if (contestantEntity.ContestantStatusType == "Qualified")
                {
                    contestantLayout.RenderSuccess($"{contestantEntity.OrderNumber}", contestantEntity.FullName, contestantEntity.ContestantStatusType, contestantEntity);
                }
                else
                {
                    contestantLayout.RenderFailure($"{contestantEntity.OrderNumber}", contestantEntity.FullName, contestantEntity.ContestantStatusType, contestantEntity);
                }
            }
            contestantLayout.Text = $"{AdministerEventCache.ContestantEntities.Count}";
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == profileButton)
            {
                ContestantEntity contestantEntity = (ContestantEntity)contestantLayout.SelectedItem.Data;
                ApplicationFormNavigator.DisplayEventContestantProfileForm(contestantEntity.Id);
            }
            else if (sender == toggleButton)
            {
                ContestantEntity contestantEntity = (ContestantEntity)contestantLayout.SelectedItem.Data;
                if (contestantLayout.SelectedItem.Status == "Qualified")
                {
                    contestantEntity.ContestantStatusType = "Disqualified";
                    contestantLayout.SelectedItem.SetFailureStatus(contestantEntity.ContestantStatusType);
                }
                else
                {
                    contestantEntity.ContestantStatusType = "Qualified";
                    contestantLayout.SelectedItem.SetSuccessStatus(contestantEntity.ContestantStatusType);
                }
            }
            else if (sender == resultsButton)
            {
                ApplicationFormNavigator.DisplayEventSegmentResultForm(AdministerEventCache.EventLayoutSequence.Segment.Id);
            }
            else if (sender == confirmButton)
            {
                if (AdministerEventCache.QualifiedContestantCount < 2)
                {
                    informationLayout.DisplayErrorMessage("At least 2 contestants must be present");
                    return;
                }

                EventLayoutSequenceEntity oldEventLayoutSequenceEntity = AdministerEventCache.EventLayoutSequence;
                EventLayoutSequenceEntity newEventLayoutSequenceEntity = ApplicationDatabase.ReadOneNextIncompleteEventLayoutSequence(oldEventLayoutSequenceEntity.Event.Id);
                ApplicationDatabase.UpdateEventLayoutToComplete(oldEventLayoutSequenceEntity.Round.Id);
                ApplicationDatabase.UpdateEventLayoutToCurrent(newEventLayoutSequenceEntity.Round.Id);
                AdministerEventCache.EventLayoutSequence = newEventLayoutSequenceEntity;
                int eventId = AdministerEventCache.EventLayoutSequence.Event.Id;
                int roundId = AdministerEventCache.EventLayoutSequence.Round.Id;
                List<ContestantEntity> contestantEntities = AdministerEventCache.ContestantEntities;
                List<JudgeUserEntity> judgeUserEntities = ApplicationDatabase.ReadManyJudgeEntitiesFromPendingEventEntities(eventId);
                foreach (ContestantEntity contestantEntity in contestantEntities)
                {
                    if (contestantEntity.ContestantStatusType == "Qualified")
                    {
                        foreach (JudgeUserEntity judgeUserEntity in judgeUserEntities)
                        {
                            ApplicationDatabase.CreateRoundContestant(roundId, contestantEntity.Id, judgeUserEntity.Email);
                        }
                    }
                    ApplicationDatabase.UpdateEventContestantStatus(contestantEntity);
                }

                ApplicationFormNavigator.DisplayAdministerEventForm();
            }
        }

        private void Item_SingleClick(object sender, EventArgs e)
        {
            if (contestantLayout.SelectedItem == null || contestantLayout.SelectedItem != sender)
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
