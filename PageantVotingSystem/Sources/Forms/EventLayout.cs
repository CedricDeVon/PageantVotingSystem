
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EventLayout : Form
    {
        public InformationLayout InformationLayout { get; private set; }
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly EventStructureItemLayout eventStructureItemLayout;

        public EventLayout()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
            eventStructureItemLayout = new EventStructureItemLayout(resultLayoutControl, Item_Click);
        }

        public void Render(EventEntity eventEntity)
        {
            ApplicationDatabase.ReadOneEventLayout(eventEntity);
            eventStructureItemLayout.Render(eventEntity);
        }

        private void Item_Click(object sender, EventArgs e)
        {
            EventStructureItem item = (EventStructureItem)sender;
            if (item.Layer == 0)
            {
                resultsButton.Hide();
            }
            else
            {
                resultsButton.Show();
            }

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
            else if (sender == informationButton)
            {
                EventStructureItem item = eventStructureItemLayout.SelectedItem;
                if (item.Layer == 0)
                {
                    EventEntity entity = ApplicationDatabase.ReadOneEvent(((EventEntity)item.Data).Id);
                    ApplicationFormNavigator.DisplayEventProfileForm(entity);
                }
                else if (item.Layer == 1)
                {
                    SegmentEntity entity = ApplicationDatabase.ReadOneSegment(((SegmentEntity)item.Data).Id);
                    ApplicationFormNavigator.DisplayEventSegmentProfileForm(entity);
                }
                else if (item.Layer == 2)
                {
                    RoundEntity entity = ApplicationDatabase.ReadOneRound(((RoundEntity)item.Data).Id);
                    ApplicationFormNavigator.DisplayEventRoundProfileForm(entity);
                }
                else if (item.Layer == 3)
                {
                    CriteriumEntity entity = ApplicationDatabase.ReadOneCriterium(((CriteriumEntity)item.Data).Id);
                    ApplicationFormNavigator.DisplayEventCriteriumProfileForm(entity);
                }
            }
            else if (sender == resultsButton)
            {
                EventStructureItem item = eventStructureItemLayout.SelectedItem;
                if (item.Layer == 1)
                {
                    ApplicationFormNavigator.DisplayEventSegmentResultForm(((SegmentEntity)item.Data).Id);
                }
                else if (item.Layer == 2)
                {
                    ApplicationFormNavigator.DisplayEventRoundResultForm(((RoundEntity)item.Data).Id);
                }
                else if (item.Layer == 3)
                {
                    ApplicationFormNavigator.DisplayEventCriteriumResultForm(((CriteriumEntity)item.Data).Id);
                }
            }
        }

        private void ResetAllData()
        {
            eventStructureItemLayout.Clear();
            optionsControl.Hide();
            resultsButton.Hide();
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
