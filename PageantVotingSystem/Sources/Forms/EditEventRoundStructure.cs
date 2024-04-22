
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EditEventRoundStructure : Form
    {
        private readonly InformationLayout informationBox;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly OrderedValueItemLayout roundsLayout;

        private SegmentEntity currentSegmentEntity;

        public EditEventRoundStructure()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationBox = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
            roundsLayout = new OrderedValueItemLayout(roundsLayoutControl);
            roundsLayout.ItemSingleClick += new EventHandler(RoundsLayoutItem_SingleClick);
            roundsLayout.ItemDoubleClick += new EventHandler(RoundsLayoutItem_DoubleClick);
        }

        public void Render(SegmentEntity segmentEntity)
        {
            currentSegmentEntity = segmentEntity;
            segmentNameLabel.Text = segmentEntity.Name;
            roundNameInput.Text = "";
            roundDescriptionInput.Text = "";
            roundCountLabel.Text = $"{segmentEntity.Rounds.Items.Count}";
            roundsLayout.RenderOrdered(segmentEntity.RoundNamesInReverseOrder);
            roundsLayout.Unfocus();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationBox.StartLoadingMessageDisplay();

            if (sender == roundSaveButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
                roundsLayout.Unfocus();
                roundNameInput.Text = "";
                roundDescriptionInput.Text = "";
            }
            else if (sender == createRoundButton)
            {
                string roundName = "Round";
                RoundEntity roundEntity = new RoundEntity();
                roundEntity.Name = roundName;
                currentSegmentEntity.Rounds.AddNewItem(roundEntity);
                roundsLayout.RenderOrdered(roundName);
                roundCountLabel.Text = $"{currentSegmentEntity.Rounds.ItemCount}";
            }
            else if (sender == roundResetButton)
            {
                currentSegmentEntity.Rounds.ClearAllItems();
                roundsLayout.Clear();
                roundsLayout.Unfocus();
                roundCountLabel.Text = "0";
                roundNameInput.Text = "";
                roundDescriptionInput.Text = "";
            }

            informationBox.StopLoadingMessageDisplay();
        }

        private void RoundsLayoutItem_SingleClick(object sender, EventArgs e)
        {
            if (roundsLayout.SelectedItem != null)
            {
                OrderedValueItem oldOrderedValueItem = roundsLayout.SelectedItem;
                RoundEntity oldRound = currentSegmentEntity.Rounds.Items[currentSegmentEntity.Rounds.ItemCount - Convert.ToInt32(oldOrderedValueItem.OrderedNumber)];
                oldRound.Name = roundNameInput.Text;
                oldRound.Description = roundDescriptionInput.Text;
                oldOrderedValueItem.Value = roundNameInput.Text;
            }

            OrderedValueItem newOrderedValueItem = (OrderedValueItem)sender;
            RoundEntity newRound = currentSegmentEntity.Rounds.Items[currentSegmentEntity.Rounds.ItemCount - Convert.ToInt32(newOrderedValueItem.OrderedNumber)];
            roundNameInput.Text = newRound.Name;
            roundDescriptionInput.Text = newRound.Description;
        }

        private void RoundsLayoutItem_DoubleClick(object sender, EventArgs e)
        {
            OrderedValueItem oldOrderedValueItem = (OrderedValueItem)sender;
            RoundEntity oldRoundEntity = currentSegmentEntity.Rounds.Items[currentSegmentEntity.Rounds.ItemCount - Convert.ToInt32(oldOrderedValueItem.OrderedNumber)];
            roundsLayout.Unfocus();
            roundNameInput.Text = "";
            roundDescriptionInput.Text = "";
            ApplicationFormNavigator.DisplayEditEventCriteriumStructureForm(currentSegmentEntity, oldRoundEntity);
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (roundsLayout.SelectedItem != null && e.KeyCode == Keys.NumPad8)
            {
                OrderedValueItem selectedItem = roundsLayout.SelectedItem;
                currentSegmentEntity.Rounds.MoveItemAtIndexUpwards(currentSegmentEntity.Rounds.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                roundsLayout.MoveSelectedUpwards();
                e.Handled = true;
            }
            else if (roundsLayout.SelectedItem != null && e.KeyCode == Keys.NumPad2)
            {
                OrderedValueItem selectedItem = roundsLayout.SelectedItem;
                currentSegmentEntity.Rounds.MoveItemAtIndexDownwards(currentSegmentEntity.Rounds.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                roundsLayout.MoveSelectedDownwards();
                e.Handled = true;
            }
            else if (roundsLayout.SelectedItem != null && e.KeyCode == Keys.Delete)
            {
                OrderedValueItem selectedItem = roundsLayout.SelectedItem;
                currentSegmentEntity.Rounds.RemoveItemAtIndex(currentSegmentEntity.Rounds.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                roundsLayout.RemoveSelected();

                roundCountLabel.Text = $"{currentSegmentEntity.Rounds.Items.Count}";
                selectedItem = roundsLayout.SelectedItem;

                if (selectedItem == null)
                {
                    roundCountLabel.Text = "0";
                    roundNameInput.Text = "";
                    roundDescriptionInput.Text = "";
                }
                else
                {
                    RoundEntity newRound = currentSegmentEntity.Rounds.Items[currentSegmentEntity.Rounds.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber)];
                    roundNameInput.Text = newRound.Name;
                    roundDescriptionInput.Text = newRound.Description;
                }

                e.Handled = true;
            }
        }
    }
}
