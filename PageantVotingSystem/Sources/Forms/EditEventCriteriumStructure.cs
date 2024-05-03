
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Security;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EditEventCriteriumStructure : Form
    {
        private readonly InformationLayout informationLayout;

        private readonly TopSideNavigationLayout topSideNavigationLayout;
        
        private readonly OrderedValueItemLayout criteriaLayout;

        private SegmentEntity currentSegmentEntity;

        private RoundEntity currentRoundEntity;

        public EditEventCriteriumStructure()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            criteriaLayout = new OrderedValueItemLayout(criteriaLayoutControl);
            criteriaLayout.ItemSingleClick += new EventHandler(CriteriaLayoutItem_SingleClick);
        }

        public void Render(SegmentEntity segmentEntity, RoundEntity roundEntity)
        {
            currentSegmentEntity = segmentEntity;
            currentRoundEntity = roundEntity;
            segmentNameLabel.Text = segmentEntity.Name;
            roundNameLabel.Text = roundEntity.Name;
            criteriumNameInput.Text = "";
            criteriumDescriptionInput.Text = "";
            criteriumMaximumValueInput.Value = 100;
            criteriumMinimumValueInput.Value = 0;
            criteriumPercentageWeightInput.Value = 100;
            criteriumCountLabel.Text = $"{roundEntity.Criteria.ItemCount}";
            criteriaLayout.Render(roundEntity.CriteriumNamesInReverseOrder);
            criteriaLayout.Unfocus();
            criteriumDataLayoutControl.Hide();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();

            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPreviousForm();
                criteriaLayout.Unfocus();
                criteriumDataLayoutControl.Hide();
                criteriumNameInput.Text = "";
                criteriumDescriptionInput.Text = "";
                criteriumMaximumValueInput.Value = 100;
                criteriumMinimumValueInput.Value = 0;
                criteriumPercentageWeightInput.Value = 100;
            }
            else if (sender == createCriteriumButton)
            {
                string criteriumName = "Criterium";
                CriteriumEntity criteriumEntity = new CriteriumEntity();
                criteriumEntity.Name = criteriumName;
                currentRoundEntity.Criteria.AddNewItem(criteriumEntity);
                criteriaLayout.Render(criteriumName);
                criteriumCountLabel.Text = $"{currentRoundEntity.Criteria.ItemCount}";
            }
            else if (sender == criteriumResetButton)
            {
                criteriaLayout.Clear();
                criteriaLayout.Unfocus();
                criteriumDataLayoutControl.Hide();
                criteriumNameInput.Text = "";
                criteriumDescriptionInput.Text = "";
                criteriumMaximumValueInput.Value = 100;
                criteriumMinimumValueInput.Value = 0;
                criteriumPercentageWeightInput.Value = 100;
            }

            informationLayout.StopLoadingMessageDisplay();
        }

        private void CriteriaLayoutItem_SingleClick(object sender, EventArgs e)
        {
            OrderedValueItem b = (OrderedValueItem)sender;
            if (b.Features.IsToggled)
            {
                criteriumDataLayoutControl.Hide();
            }
            else
            {
                criteriumDataLayoutControl.Show();
            }

            if (criteriaLayout.SelectedItem != null)
            {
                OrderedValueItem oldOrderedValueItem = criteriaLayout.SelectedItem;
                CriteriumEntity oldCriterium = currentRoundEntity.Criteria.Items[currentRoundEntity.Criteria.ItemCount - Convert.ToInt32(oldOrderedValueItem.OrderedNumber)];
                oldCriterium.Name = criteriumNameInput.Text;
                oldCriterium.Description = criteriumDescriptionInput.Text;
                oldCriterium.MaximumValue = (float) criteriumMaximumValueInput.Value;
                oldCriterium.MinimumValue = (float) criteriumMinimumValueInput.Value;
                oldCriterium.PercentageWeight = (float) criteriumPercentageWeightInput.Value;
                oldOrderedValueItem.Value = criteriumNameInput.Text;
            }

            OrderedValueItem newOrderedValueItem = (OrderedValueItem)sender;
            CriteriumEntity newCriterium = currentRoundEntity.Criteria.Items[currentRoundEntity.Criteria.ItemCount - Convert.ToInt32(newOrderedValueItem.OrderedNumber)];
            criteriumNameInput.Text = newCriterium.Name;
            criteriumDescriptionInput.Text = newCriterium.Description;
            criteriumMaximumValueInput.Value = Convert.ToDecimal(newCriterium.MaximumValue);
            criteriumMinimumValueInput.Value = Convert.ToDecimal(newCriterium.MinimumValue);
            criteriumPercentageWeightInput.Value = Convert.ToDecimal(newCriterium.PercentageWeight);
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (criteriaLayout.SelectedItem != null && e.KeyCode == Keys.NumPad8)
            {
                OrderedValueItem selectedItem = criteriaLayout.SelectedItem;
                currentRoundEntity.Criteria.MoveItemAtIndexUpwards(currentRoundEntity.Criteria.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                criteriaLayout.MoveSelectedUpwards();
                e.Handled = true;
            }
            else if (criteriaLayout.SelectedItem != null && e.KeyCode == Keys.NumPad2)
            {
                OrderedValueItem selectedItem = criteriaLayout.SelectedItem;
                currentRoundEntity.Criteria.MoveItemAtIndexDownwards(currentRoundEntity.Criteria.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                criteriaLayout.MoveSelectedDownwards();
                e.Handled = true;
            }
            else if (criteriaLayout.SelectedItem != null && e.KeyCode == Keys.Delete)
            {
                OrderedValueItem selectedItem = criteriaLayout.SelectedItem;
                currentRoundEntity.Criteria.RemoveItemAtIndex(currentRoundEntity.Criteria.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                criteriaLayout.RemoveSelected();

                criteriaLayout.Text = $"{currentRoundEntity.Criteria.Items.Count}";
                selectedItem = criteriaLayout.SelectedItem;

                if (selectedItem == null)
                {
                    criteriumCountLabel.Text = "0";
                    criteriumNameInput.Text = "";
                    criteriumDescriptionInput.Text = "";
                    criteriumMaximumValueInput.Value = 100;
                    criteriumMinimumValueInput.Value = 0;
                    criteriumPercentageWeightInput.Value = 100;
                    criteriaLayoutControl.Hide();
                }
                else
                {
                    CriteriumEntity newCriteriumEntity = currentRoundEntity.Criteria.Items[currentRoundEntity.Criteria.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber)];
                    criteriumNameInput.Text = newCriteriumEntity.Name;
                    criteriumDescriptionInput.Text = newCriteriumEntity.Description;
                    criteriumMaximumValueInput.Value = Convert.ToDecimal(newCriteriumEntity.MaximumValue);
                    criteriumMinimumValueInput.Value = Convert.ToDecimal(newCriteriumEntity.MinimumValue);
                    criteriumPercentageWeightInput.Value = Convert.ToDecimal(newCriteriumEntity.PercentageWeight);
                }

                e.Handled = true;
            }

            if (e.KeyData == Keys.Escape)
            {
                ApplicationFormNavigator.DisplayPreviousForm();
                e.Handled = true;
            }
        }
    }
}
