
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
    public partial class EventJudges : Form
    {
        public InformationLayout InformationLayout { get; private set; }

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly SingleValuedItemLayout resultsLayout;

        public EventJudges()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
            resultsLayout = new SingleValuedItemLayout(resultsLayoutControl, Item_Click);
        }

        public void Render(EventEntity entity)
        {
            List<UserEntity> userEntities = ApplicationDatabase.ReadManyUsers(entity.Id);
            foreach (UserEntity userEntity in userEntities)
            {
                resultsLayout.Render(userEntity.Email, userEntity);
            }
            resultCountLabel.Text = $"{userEntities.Count}";
        }

        private void ResetAllData()
        {
            resultCountLabel.Text = "0";
            resultsLayout.Clear();
            optionsControl.Hide();
        }

        private void Item_Click(object sender, EventArgs e)
        {
            SingleValuedItem item = (SingleValuedItem)sender;
            if (item.Features.IsToggled)
            {
                optionsControl.Hide();
            }
            else
            {
                optionsControl.Show();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
                ResetAllData();
            }
            else if (sender == informationButton &&
                resultsLayout.SelectedItem != null)
            {
                ApplicationFormNavigator.DisplayUserProfileForm((UserEntity)resultsLayout.SelectedItem.Data);
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
