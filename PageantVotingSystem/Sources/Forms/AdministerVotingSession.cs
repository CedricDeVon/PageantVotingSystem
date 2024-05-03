
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
    public partial class AdministerVotingSession : Form
    {
        private readonly InformationLayout informationLayout;
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;
        
        private readonly EntityStatusItemLayout judgesLayout;

        public AdministerVotingSession()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            judgesLayout = new EntityStatusItemLayout(judgesLayoutControl);
            judgesLayout.ItemSingleClick += new EventHandler(Item_SingleClick);
        }

        public void Render()
        {
            eventNameLabel.Text = AdministerEventCache.EventLayoutSequence.Event.Name;
            segmentNameLabel.Text = AdministerEventCache.EventLayoutSequence.Segment.Name;
            roundNameLabel.Text = AdministerEventCache.EventLayoutSequence.Round.Name;
            contestantNumberLabel.Text = $"{AdministerEventCache.SelectedContestant.OrderNumber}";
            contestantNameLabel.Text = AdministerEventCache.SelectedContestant.FullName;
            List<JudgeUserEntity> judgeUserEntities = ApplicationDatabase.ReadManyRoundContestantJudgesFromPendingEvent(AdministerEventCache.EventLayoutSequence.Round.Id, AdministerEventCache.SelectedContestant.Id);
            judgesLayout.Clear();
            foreach (JudgeUserEntity judgeUserEntity in judgeUserEntities)
            {
                if (judgeUserEntity.RoundContestantStatusType == "Complete")
                {
                    judgesLayout.RenderSuccess($"{judgeUserEntity.OrderNumber}", judgeUserEntity.FullName, judgeUserEntity.RoundContestantStatusType, judgeUserEntity);
                }
                else
                {
                    judgesLayout.RenderFailure($"{judgeUserEntity.OrderNumber}", judgeUserEntity.FullName, judgeUserEntity.RoundContestantStatusType, judgeUserEntity);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == confirmButton)
            {
                ApplicationDatabase.UpdateRoundContestantStatusToComplete(AdministerEventCache.EventLayoutSequence.Round.Id, AdministerEventCache.SelectedContestant.Id);
                bool isRoundIncomplete = ApplicationDatabase.IsIncompleteRoundContestantFound(AdministerEventCache.EventLayoutSequence.Round.Id);
                AdministerEventCache.SelectedContestant = null;

                if (!isRoundIncomplete)
                {
                    EventLayoutSequenceEntity oldEventLayoutSequenceEntity = AdministerEventCache.EventLayoutSequence;
                    ApplicationDatabase.UpdateEventLayoutToComplete(oldEventLayoutSequenceEntity.Round.Id);
                    bool isEventLayoutIncomplete = ApplicationDatabase.IsIncompleteEventLayoutFound(AdministerEventCache.EventLayoutSequence.Event.Id);
                    if (!isEventLayoutIncomplete)
                    {
                        ApplicationFormNavigator.DisplayManagerDashboardForm();
                        AdministerEventCache.Clear();
                        return;
                    }

                    EventLayoutSequenceEntity newEventLayoutSequenceEntity = ApplicationDatabase.ReadOneNextIncompleteEventLayoutSequence(oldEventLayoutSequenceEntity.Event.Id);
                    if (newEventLayoutSequenceEntity.Segment.Id != oldEventLayoutSequenceEntity.Segment.Id)
                    {
                        ApplicationFormNavigator.DisplayAdministerEventContestantReviewForm();
                        return;
                    }

                    ApplicationDatabase.UpdateEventLayoutToCurrent(newEventLayoutSequenceEntity.Round.Id);
                    AdministerEventCache.EventLayoutSequence = newEventLayoutSequenceEntity;
                    int eventId = AdministerEventCache.EventLayoutSequence.Event.Id;
                    int roundId = AdministerEventCache.EventLayoutSequence.Round.Id;
                    List<ContestantEntity> contestantEntities = ApplicationDatabase.ReadManyContestantEntitiesFromPendingEventEntities(eventId);
                    List<JudgeUserEntity> judgeUserEntities = ApplicationDatabase.ReadManyJudgeEntitiesFromPendingEventEntities(eventId);
                    foreach (ContestantEntity contestantEntity in contestantEntities)
                    {
                        foreach (JudgeUserEntity judgeUserEntity in judgeUserEntities)
                        {
                            ApplicationDatabase.CreateRoundContestant(roundId, contestantEntity.Id, judgeUserEntity.Email);
                        }
                    }

                    ApplicationFormNavigator.DisplayAdministerEventForm();
                }
                else if (isRoundIncomplete)
                {
                    ApplicationFormNavigator.DisplayAdministerEventForm();
                }

                eventNameLabel.Text = "Event Name";
                segmentNameLabel.Text = "Segment Name";
                roundNameLabel.Text = "Round Name";
                contestantNumberLabel.Text = "0";
                contestantNameLabel.Text = "Contestant Name";
                judgesLayout.Clear();
            }
            else if (sender == refreshButton)
            {
                List<JudgeUserEntity> judgeUserEntities = ApplicationDatabase.ReadManyRoundContestantJudgesFromPendingEvent(AdministerEventCache.EventLayoutSequence.Round.Id, AdministerEventCache.SelectedContestant.Id);
                judgesLayout.Clear();
                foreach (JudgeUserEntity judgeUserEntity in judgeUserEntities)
                {
                    if (judgeUserEntity.RoundContestantStatusType == "Complete")
                    {
                        judgesLayout.RenderSuccess($"{judgeUserEntity.OrderNumber}", judgeUserEntity.FullName, judgeUserEntity.RoundContestantStatusType, judgeUserEntity);
                    }
                    else
                    {
                        judgesLayout.RenderFailure($"{judgeUserEntity.OrderNumber}", judgeUserEntity.FullName, judgeUserEntity.RoundContestantStatusType, judgeUserEntity);
                    }
                }
            }
        }

        private void Item_SingleClick(object sender, EventArgs e)
        {
            JudgeUserEntity judgeUserEntity = (JudgeUserEntity) ((EntityStatusItem)sender).Data;
            ApplicationFormNavigator.DisplayUserProfileForm(judgeUserEntity.Email);
        }
    }
}
