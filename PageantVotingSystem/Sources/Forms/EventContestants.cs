
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EventContestants : Form
    {
        private readonly InformationLayout informationLayout;
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly OrderedValueItemLayout resultsLayout;

        public EventContestants()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            resultsLayout = new OrderedValueItemLayout(resultsLayoutControl, Item_Click);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                DisplayPreviousForm();
            }
            else if (sender == informationButton && resultsLayout.SelectedItem != null)
            {
                DisplayEventContestantProfileForm();
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DisplayPreviousForm();
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            OrderedValueItem item = (OrderedValueItem)sender;
            if (item.Features.IsToggled)
            {
                optionsControl.Hide();
            }
            else
            {
                optionsControl.Show();
            }
        }

        public void DisplayPreviousForm()
        {
            informationLayout.StartLoadingMessageDisplay();

            ApplicationFormNavigator.DisplayPreviousForm();
            Clear();

            informationLayout.StopLoadingMessageDisplay();
        }

        public void DisplayEventContestantProfileForm()
        {
            informationLayout.StartLoadingMessageDisplay();

            ApplicationFormNavigator.DisplayEventContestantProfileForm(((ContestantEntity)resultsLayout.SelectedItem.Data).Id);
            resultsLayout.Unfocus();
            optionsControl.Hide();

            informationLayout.StopLoadingMessageDisplay();
        }

        public void Render(EventEntity entity)
        {
            List<ContestantEntity> contestantEntities = ApplicationDatabase.ReadManyEventContestantEntities(entity.Id);
            foreach (ContestantEntity contestantEntity in contestantEntities)
            {
                resultsLayout.Render($"{contestantEntity.OrderNumber}", contestantEntity.FullName, contestantEntity);
            }
            resultCountLabel.Text = $"{contestantEntities.Count}";
        }

        private void Clear()
        {
            resultCountLabel.Text = "0";
            resultsLayout.Clear();
            optionsControl.Hide();
        }
    }
}

