
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
        public InformationLayout InformationLayout { get; private set; }
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly OrderedValueItemLayout resultsLayout;

        public EventContestants()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
            resultsLayout = new OrderedValueItemLayout(resultsLayoutControl, Item_Click);
        }

        public void Render(EventEntity entity)
        {
            List<ContestantEntity> contestantEntities = ApplicationDatabase.ReadManyEventContestants(entity.Id);
            foreach (ContestantEntity contestantEntity in contestantEntities)
            {
                resultsLayout.RenderOrdered($"{contestantEntity.OrderNumber}", contestantEntity.FullName, contestantEntity);
            }
            resultCountLabel.Text = $"{contestantEntities.Count}";
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

        private void ResetAllData()
        {
            resultCountLabel.Text = "0";
            resultsLayout.Clear();
            optionsControl.Hide();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
                ResetAllData();
            }
            else if (sender == informationButton && resultsLayout.SelectedItem != null)
            {
                ContestantEntity entity = ApplicationDatabase.ReadOneContestant(((ContestantEntity)resultsLayout.SelectedItem.Data).Id);
                ApplicationFormNavigator.DisplayEventContestantProfileForm(entity);
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ApplicationFormNavigator.DisplayPrevious();
                ResetAllData();
            }
        }
    }
}


// DisplayEventContestantProfile