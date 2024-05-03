
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
    public partial class AdministerEventQuery : Form
    {
        private readonly InformationLayout informationLayout;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly SingleValuedItemLayout pendingEventsLayout;

        public AdministerEventQuery()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            pendingEventsLayout = new SingleValuedItemLayout(ongoingEventsLayoutControl);
            pendingEventsLayout.ItemSingleClick += new EventHandler(PendingEventsItem_SingleClick);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();

            if (sender == searchButton)
            {
                SearchForEventEntities();
            }
            else if (sender == profileButton)
            {
                DisplayEventProfileForm();
            }
            else if (sender == administerButton)
            {
                AdministerEvent();
            }
            else if (sender == goBackButton)
            {
                DisplayManagerDashboardForm();
            }
            else if (sender == clearButton)
            {
                Clear();
            }

            informationLayout.StopLoadingMessageDisplay();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DisplayManagerDashboardForm();
            }
        }

        private void PendingEventsItem_SingleClick(object sender, EventArgs e)
        {
            if (pendingEventsLayout.SelectedItem == null ||
                pendingEventsLayout.SelectedItem != sender)
            {
                optionsControl.Show();
            }
            else
            {
                optionsControl.Hide();
            }
        }

        private void DisplayManagerDashboardForm()
        {
            ApplicationFormNavigator.DisplayManagerDashboardForm();
            Clear();
        }

        private void SearchForEventEntities()
        {
            List<EventEntity> eventEntities = ApplicationDatabase.ReadManyPendingEventEntitiesBasedOnManagerEmail(
                    eventNameInput.Text, UserProfileCache.Data.Email);
            eventNameInput.Text = "";
            pendingEventsLayout.Clear();
            foreach (EventEntity eventEntity in eventEntities)
            {
                pendingEventsLayout.Render(eventEntity.Name, eventEntity);
            }
            ongoingEventsCountLabel.Text = $"{eventEntities.Count}";
        }

        private void DisplayEventProfileForm()
        {
            EventEntity eventEntity = (EventEntity)pendingEventsLayout.SelectedItem.Data;
            ApplicationFormNavigator.DisplayEventProfileForm(eventEntity.Id);
        }

        private void AdministerEvent()
        {
            EventEntity eventEntity = (EventEntity)pendingEventsLayout.SelectedItem.Data;
            AdministerEventCache.EventLayoutSequence = ApplicationDatabase.ReadOnePendingEventLayoutSequenceEntity(eventEntity.Id);
            AdministerEventCache.SelectedContestant = ApplicationDatabase.ReadOnePendingContestantEntityUnderJudgement(AdministerEventCache.EventLayoutSequence.Round.Id);
            if (AdministerEventCache.SelectedContestant != null)
            {
                ApplicationFormNavigator.DisplayAdministerVotingSessionForm();
            }
            else
            {
                ApplicationFormNavigator.DisplayAdministerEventForm();
            }
            Clear();
        }

        private void Clear()
        {
            eventNameInput.Text = "";
            ongoingEventsCountLabel.Text = "0";
            pendingEventsLayout.Clear();
            optionsControl.Hide();
        }
    }
}
