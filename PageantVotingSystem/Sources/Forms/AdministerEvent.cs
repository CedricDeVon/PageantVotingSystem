
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
    public partial class AdministerEvent : Form
    {
        private readonly InformationLayout informationLayout;
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public AdministerEvent()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();

            if (sender == contestantsButton)
            {
                ApplicationFormNavigator.DisplayAdministerEventContestantsForm();
            }
            else if (sender == startVotingButton)
            {
                StartVoting();
            }
            else if (sender == goBackButton)
            {
                DisplayAdministerEventQueryForm();
            }

            informationLayout.StopLoadingMessageDisplay();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DisplayAdministerEventQueryForm();
            }
        }

        private void DisplayAdministerEventQueryForm()
        {
            ApplicationFormNavigator.DisplayAdministerEventQueryForm();
            Clear();

        }

        private void StartVoting()
        {
            int eventId = AdministerEventCache.EventLayoutSequence.Event.Id;
            int segmentId = AdministerEventCache.EventLayoutSequence.Segment.Id;
            int roundId = AdministerEventCache.EventLayoutSequence.Round.Id;
            int contestantId = AdministerEventCache.SelectedContestant.Id;
            List<JudgeUserEntity> judgeEntities = ApplicationDatabase.ReadManyJudgeEntitiesFromPendingEventEntities(eventId);
            List<CriteriumEntity> criteriumEntities = ApplicationDatabase.ReadManyCriteria(roundId);
            foreach (JudgeUserEntity judgeEntity in judgeEntities)
            {
                foreach (CriteriumEntity criteriumEntity in criteriumEntities)
                {
                    ApplicationDatabase.CreateNewResult(eventId, segmentId, roundId, criteriumEntity.Id, contestantId, judgeEntity.Email);
                }
            }
            ApplicationDatabase.UpdateRoundContestantStatusToPending(roundId, contestantId);
            ApplicationFormNavigator.DisplayAdministerVotingSessionForm();
        }

        public void Render()
        {
            eventNameLabel.Text = AdministerEventCache.EventLayoutSequence.Event.Name;
            segmentNameLabel.Text = AdministerEventCache.EventLayoutSequence.Segment.Name;
            roundNameLabel.Text = AdministerEventCache.EventLayoutSequence.Round.Name;
            contestantNameLabel.Text = (AdministerEventCache.SelectedContestant == null) ? "" : AdministerEventCache.SelectedContestant.FullName;
            contestantNumberLabel.Text = (AdministerEventCache.SelectedContestant == null) ? "" : $"{AdministerEventCache.SelectedContestant.OrderNumber}";
            if (AdministerEventCache.SelectedContestant != null)
            {
                startVotingControl.Show();
            }
            else
            {
                startVotingControl.Hide();
            }
        }

        private void Clear()
        {
            AdministerEventCache.Clear();
            eventNameLabel.Text = "";
            segmentNameLabel.Text = "";
            roundNameLabel.Text = "";
            contestantNameLabel.Text = "";
            contestantNumberLabel.Text = "";
            startVotingControl.Hide();
        }
    }
}
