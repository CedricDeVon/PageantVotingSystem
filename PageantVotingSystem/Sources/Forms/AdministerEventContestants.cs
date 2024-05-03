
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
    public partial class AdministerEventContestants : Form
    {
        private readonly InformationLayout informationLayout;
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly OrderedValueItemLayout contestantsLayout;

        public AdministerEventContestants()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            contestantsLayout = new OrderedValueItemLayout(contestantsLayoutControl);
            contestantsLayout.ItemSingleClick += Item_SingleClick;
        }
        
        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == profileButton)
            {
                DisplayEventContestantProfileForm();
            }
            else if (sender == selectButton)
            {
                SelectContestant();
            }
            else if (sender == goBackButton)
            {
                DisplayAdministerEventForm();
            }
        }

        private void Item_SingleClick(object sender, EventArgs e)
        {
            if (contestantsLayout.SelectedItem == null ||
                contestantsLayout.SelectedItem != sender)
            {
                optionsControl.Show();
            }
            else
            {
                optionsControl.Hide();
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DisplayAdministerEventForm();
            }
        }

        public void Render()
        {
            contestantsLayout.Clear();
            List<ContestantEntity> contestantEntities = ApplicationDatabase.ReadManyUnjudgedRoundContestantEntities(
                AdministerEventCache.EventLayoutSequence.Round.Id);
            foreach (ContestantEntity contestantEntity in contestantEntities)
            {
                contestantsLayout.Render($"{contestantEntity.OrderNumber}", contestantEntity.FullName, contestantEntity);
            }
            contestantCountLabel.Text = $"{contestantEntities.Count}";
        }

        private void DisplayAdministerEventForm()
        {
            ApplicationFormNavigator.DisplayAdministerEventForm();
            UnfocusLayouts();
        }

        private void DisplayEventContestantProfileForm()
        {
            ContestantEntity contestantEntity = (ContestantEntity)contestantsLayout.SelectedItem.Data;
            ApplicationFormNavigator.DisplayEventContestantProfileForm(contestantEntity.Id);
        }

        private void SelectContestant()
        {
            AdministerEventCache.SelectedContestant = (ContestantEntity)contestantsLayout.SelectedItem.Data;
            DisplayAdministerEventForm();
        }

        private void UnfocusLayouts()
        {
            contestantsLayout.Unfocus();
            optionsControl.Hide();
        }
    }
}
