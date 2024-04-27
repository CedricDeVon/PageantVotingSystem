
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.ResourceLoaders;
using PageantVotingSystem.Sources.Databases;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class JudgeContestantDashboard : Form
    {
        public InformationLayout InformationLayout { get; private set; }
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly JudgeCriteriumResultItemLayout judgeCriteriumResultItemLayout;

        private ContestantEntity contestantEntity;

        private EventLayoutSequenceEntity eventLayoutSequenceEntity;

        private List<JudgeCriteriumEntity> judgeCriteriumEntities;

        public JudgeContestantDashboard()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
            judgeCriteriumResultItemLayout = new JudgeCriteriumResultItemLayout(criteriumLayoutControl);
            judgeCriteriumResultItemLayout.ItemSingleClick += Item_SingleClick;
        }

        public void Render(
            EventEntity eventEntity,
            ContestantEntity contestantEntity)
        {
            this.contestantEntity = ApplicationDatabase.ReadOneContestant(contestantEntity.Id);
            eventLayoutSequenceEntity = ApplicationDatabase.ReadOneOngoingEventLayoutSequenceEntity(eventEntity.Id);
            judgeCriteriumEntities = ApplicationDatabase.ReadManyJudgeCriteriumEntity(eventEntity.Id, contestantEntity.Id, UserProfileCache.Data.Email);
            eventNameLabel.Text = eventLayoutSequenceEntity.Event.Name;
            segmentNameLabel.Text = eventLayoutSequenceEntity.Segment.Name;
            roundNameLabel.Text = eventLayoutSequenceEntity.Round.Name;
            contestantProfileImage.Image = ApplicationResourceLoader.SafeLoadResource(contestantEntity.ImageResourcePath);
            contestantNameLabel.Text = contestantEntity.FullName;
            contestantOrderNumber.Text = $"{contestantEntity.OrderNumber}";
            judgeCriteriumResultItemLayout.Render(judgeCriteriumEntities);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == submitButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
                judgeCriteriumResultItemLayout.UpdateContestantResults();
                judgeCriteriumResultItemLayout.Clear();
            }
            else if (sender == resetButton)
            {
                judgeCriteriumResultItemLayout.ClearInputs();
            }
            else if (sender == fullInfoButton)
            {
                ApplicationFormNavigator.DisplayEventContestantProfileForm(contestantEntity);
            }
        }

        private void Item_SingleClick(object sender, EventArgs e)
        {
            JudgeCriteriumEntity entity = ((JudgeCriteriumResultItem) sender).Data;
            ApplicationFormNavigator.DisplayEventCriteriumProfileForm(entity.Criterium);
        }
    }
}
