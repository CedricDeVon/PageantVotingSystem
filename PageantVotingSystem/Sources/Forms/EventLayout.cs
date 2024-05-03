
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EventLayout : Form
    {
        private readonly InformationLayout informationLayout;
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly EventStructureItemLayout eventStructureItemLayout;

        public EventLayout()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            eventStructureItemLayout = new EventStructureItemLayout(resultLayoutControl, Item_Click);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                DisplayPreviousForm();
            }
            else if (sender == informationButton)
            {
                DisplayEventStructureItemProfile();
            }
            else if (sender == resultsButton)
            {
                DisplayEventStructureItemResult();
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
            EventStructureItem eventStructureItem = (EventStructureItem)sender;
            HideOrShowResultsButton(eventStructureItem);
            HideOrShowOptions(eventStructureItem);
        }

        public void DisplayPreviousForm()
        {
            informationLayout.StartLoadingMessageDisplay();

            ApplicationFormNavigator.DisplayPreviousForm();
            Clear();

            informationLayout.StopLoadingMessageDisplay();
        }

        private void DisplayEventStructureItemProfile()
        {
            EventStructureItem eventStructureItem = eventStructureItemLayout.SelectedItem;
            if (eventStructureItem.Layer == 0)
            {
                ApplicationFormNavigator.DisplayEventProfileForm(((EventEntity)eventStructureItem.Data).Id);
            }
            else if (eventStructureItem.Layer == 1)
            {
                ApplicationFormNavigator.DisplayEventSegmentProfileForm(((SegmentEntity)eventStructureItem.Data).Id);
            }
            else if (eventStructureItem.Layer == 2)
            {
                ApplicationFormNavigator.DisplayEventRoundProfileForm(((RoundEntity)eventStructureItem.Data).Id);
            }
            else if (eventStructureItem.Layer == 3)
            {
                ApplicationFormNavigator.DisplayEventCriteriumProfileForm(((CriteriumEntity)eventStructureItem.Data).Id);
            }
            Unfocus();

        }

        private void DisplayEventStructureItemResult()
        {
            EventStructureItem eventStructureItem = eventStructureItemLayout.SelectedItem;
            if (eventStructureItem.Layer == 1)
            {
                ApplicationFormNavigator.DisplayEventSegmentResultForm(((SegmentEntity)eventStructureItem.Data).Id);
            }
            else if (eventStructureItem.Layer == 2)
            {
                ApplicationFormNavigator.DisplayEventRoundResultForm(((RoundEntity)eventStructureItem.Data).Id);
            }
            else if (eventStructureItem.Layer == 3)
            {
                ApplicationFormNavigator.DisplayEventCriteriumResultForm(((CriteriumEntity)eventStructureItem.Data).Id);
            }
            Unfocus();
        }

        private void HideOrShowResultsButton(EventStructureItem eventStructureItem)
        {
            if (eventStructureItem.Layer == 0)
            {
                resultsButton.Hide();
            }
            else
            {
                resultsButton.Show();
            }
        }

        private void HideOrShowOptions(EventStructureItem eventStructureItem)
        {
            if (eventStructureItem.Features.IsToggled)
            {
                optionsControl.Hide();
            }
            else
            {
                optionsControl.Show();
            }
        }

        public void Render(EventEntity eventEntity)
        {
            ApplicationDatabase.ReadOneEventLayoutEntity(eventEntity);
            eventStructureItemLayout.Render(eventEntity);
        }

        private void Clear()
        {
            eventStructureItemLayout.Clear();
            optionsControl.Hide();
            resultsButton.Hide();
        }

        private void Unfocus()
        {
            eventStructureItemLayout.Unfocus();
            optionsControl.Hide();
        }
    }
}
