
using System.Windows.Forms;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Security;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

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
            else if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPreviousForm();
            }
            else if (sender == confirmButton)
            {
                Result result = ApplicationSecurity.AuthenticateNewEvent(
                    EditEventCache.EventEntity,
                    EditEventCache.JudgeEntities,
                    EditEventCache.ContestantEntities);
                if (!result.IsSuccessful)
                {
                    informationLayout.DisplayErrorMessage(result.Message);
                    return;
                }

                int judgeOrderNumber = EditEventCache.JudgeEntities.ItemCount;
                int currentEventId = ApplicationDatabase.ReadOneRecentEvent().Id + 1;
                EditEventCache.EventEntity.Id = currentEventId;
                ApplicationDatabase.CreateEvent(EditEventCache.EventEntity);
                ApplicationDatabase.CreateEventManager(currentEventId, UserProfileCache.Data.Email);
                foreach (string judgeUserEmail in EditEventCache.JudgeEntities.Items)
                {
                    ApplicationDatabase.CreateEventJudge(currentEventId, judgeOrderNumber--, judgeUserEmail);
                }

                int currentSegmentId = ApplicationDatabase.ReadOneRecentSegment().Id + 1;
                int firstRoundId = ApplicationDatabase.ReadOneRecentRound().Id + 1;
                int currentRoundId = firstRoundId;
                int currentCriteriumId = ApplicationDatabase.ReadOneRecentCriterium().Id + 1;
                foreach (SegmentEntity segmentEntity in EditEventCache.EventEntity.Segments.Items)
                {
                    segmentEntity.Id = currentSegmentId;
                    segmentEntity.EventId = currentEventId;
                    ApplicationDatabase.CreateSegment(segmentEntity);
                    foreach (RoundEntity roundEntity in segmentEntity.Rounds.Items)
                    {
                        roundEntity.Id = currentRoundId;
                        roundEntity.SegmentId = segmentEntity.Id;
                        ApplicationDatabase.CreateRound(roundEntity);
                        foreach (CriteriumEntity criteriumEntity in roundEntity.Criteria.Items)
                        {
                            criteriumEntity.Id = currentCriteriumId;
                            criteriumEntity.RoundId = roundEntity.Id;
                            ApplicationDatabase.CreateCriterium(criteriumEntity);
                            currentCriteriumId++;
                        }
                        ApplicationDatabase.CreateEventLayout(currentEventId, currentSegmentId, currentRoundId, (currentRoundId == firstRoundId) ? "Pending" : "Incomplete");
                        currentRoundId++;
                    }
                    currentSegmentId++;
                }

                int currentContestantOrderNumber = EditEventCache.ContestantEntities.ItemCount;
                int currentContestantId = ApplicationDatabase.ReadOneRecentContestant().Id + 1;
                foreach (ContestantEntity contestantEntity in EditEventCache.ContestantEntities.Items)
                {
                    contestantEntity.Id = currentContestantId;
                    contestantEntity.OrderNumber = currentContestantOrderNumber--;
                    bool isResourceNotFound = ApplicationDatabase.IsResourceNotFound(contestantEntity.ImageResourcePath);
                    if (isResourceNotFound)
                    {
                        ApplicationDatabase.CreateResource(contestantEntity.ImageResourcePath);
                    }
                    ApplicationDatabase.CreateContestant(contestantEntity);
                    ApplicationDatabase.CreateEventContestant(currentEventId, currentContestantId);
                    foreach (string judgeUserEmail in EditEventCache.JudgeEntities.Items)
                    {
                        ApplicationDatabase.CreateRoundContestant(firstRoundId, contestantEntity.Id, judgeUserEmail);
                    }
                    currentContestantId++;
                }

                ApplicationFormNavigator.DisplayManagerDashboardForm();
                EditEventCache.Clear();
            }
            else if (sender == resetButton)
            {
                EditEventCache.Clear();
            }

            informationLayout.StopLoadingMessageDisplay();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                ApplicationFormNavigator.DisplayPreviousForm();
                e.Handled = true;
            }
        }
    }
}

