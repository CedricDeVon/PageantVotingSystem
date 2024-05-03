
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Generics;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.ResourceLoaders;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class JudgeContestantDashboard : Form
    {
        private readonly InformationLayout informationLayout;
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly JudgeCriteriumResultItemLayout judgeCriteriumResultItemLayout;

        private ContestantEntity contestantEntity;

        private List<JudgeCriteriumEntity> judgeCriteriumEntities;

        public JudgeContestantDashboard()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout =
                new TopSideNavigationLayout(topSideNavigationLayoutControl);
            judgeCriteriumResultItemLayout =
                new JudgeCriteriumResultItemLayout(criteriumLayoutControl);
            judgeCriteriumResultItemLayout.ItemSingleClick += Item_SingleClick;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();

            if (sender == confirmButton)
            {
                SubmitContestantResults();
            }
            else if (sender == resetButton)
            {
                judgeCriteriumResultItemLayout.ClearInputs();
            }
            else if (sender == fullInfoButton)
            {
                ApplicationFormNavigator.DisplayEventContestantProfileForm(contestantEntity.Id);
            }

            informationLayout.StopLoadingMessageDisplay();
        }

        private void Item_SingleClick(object sender, EventArgs e)
        {
            JudgeCriteriumEntity entity = ((JudgeCriteriumResultItem) sender).Data;
            ApplicationFormNavigator.DisplayEventCriteriumProfileForm(entity.Criterium.Id);
        }

        public void Render(
            EventLayoutSequenceEntity eventLayoutSequenceEntity,
            ContestantEntity contestantEntity)
        {
            this.contestantEntity = contestantEntity;
            judgeCriteriumEntities = ApplicationDatabase.ReadManyJudgeCriteriumEntities(
                eventLayoutSequenceEntity.Round.Id,
                contestantEntity.Id,
                UserProfileCache.Data.Email);
            eventNameLabel.Text = eventLayoutSequenceEntity.Event.Name;
            segmentNameLabel.Text = eventLayoutSequenceEntity.Segment.Name;
            roundNameLabel.Text = eventLayoutSequenceEntity.Round.Name;
            contestantProfileImage.Image = ApplicationResourceLoader.SafeLoadResource(
                contestantEntity.ImageResourcePath);
            contestantNameLabel.Text = contestantEntity.FullName;
            contestantOrderNumber.Text = $"{contestantEntity.OrderNumber}";
            judgeCriteriumResultItemLayout.Render(judgeCriteriumEntities);
        }

        private void SubmitContestantResults()
        {
            GenericDoublyLinkedListItem currentItem = judgeCriteriumResultItemLayout.Items.FirstItem;
            JudgeCriteriumResultItem judgeCriteriumResultItem = null;
            while (currentItem != null)
            {
                judgeCriteriumResultItem = (JudgeCriteriumResultItem)currentItem.Value;
                ApplicationDatabase.UpdateContestantJudgeCriteriumEntity(judgeCriteriumResultItem.Data);
                currentItem = currentItem.NextItem;
            }
            if (judgeCriteriumResultItem != null)
            {
                ApplicationDatabase.UpdateRoundContestantStatusEntityToComplete(
                    judgeCriteriumResultItem.Data, UserProfileCache.Data.Email);
            }
            ApplicationFormNavigator.DisplayPreviousForm();
            judgeCriteriumResultItemLayout.Clear();
        }
    }
}
