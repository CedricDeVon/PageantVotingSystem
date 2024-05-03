
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Security;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EditEventSegmentStructure : Form
    {
        private readonly InformationLayout informationLayout;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        private readonly OrderedValueItemLayout segmentsLayout;

        public EditEventSegmentStructure()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            segmentsLayout = new OrderedValueItemLayout(segmentsLayoutControl);
            segmentsLayout.ItemSingleClick += new EventHandler(SegmentsLayoutItem_SingleClick);
            segmentsLayout.ItemDoubleClick += new EventHandler(SegmentsLayoutItem_DoubleClick);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();

            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPreviousForm();
                segmentsLayout.Unfocus();
                segmentsDataLayoutControl.Hide();
                segmentNameInput.Text = "";
                segmentDescriptionInput.Text = "";
            }
            else if (sender == createSegmentButton)
            {
                string segmentName = "Segment";
                SegmentEntity segmentEntity = new SegmentEntity();
                segmentEntity.Name = segmentName;
                EditEventCache.EventEntity.Segments.AddNewItem(segmentEntity);
                segmentsLayout.Render(segmentName);
                segmentCountLabel.Text = $"{EditEventCache.EventEntity.Segments.ItemCount}";
            }
            else if (sender == resetButton)
            {
                EditEventCache.EventEntity.Segments.ClearAllItems();
                segmentsLayout.Clear();
                segmentsLayout.Unfocus();
                segmentsDataLayoutControl.Hide();            
                segmentCountLabel.Text = "0";
                segmentNameInput.Text = "";
                segmentDescriptionInput.Text = "";
            }

            informationLayout.StopLoadingMessageDisplay();
        }

        private void Form_KeyPress(object sender, KeyEventArgs e)
        {
            if (segmentsLayout.SelectedItem != null && e.KeyCode == Keys.NumPad8)
            {
                OrderedValueItem selectedItem = segmentsLayout.SelectedItem;
                EditEventCache.EventEntity.Segments.MoveItemAtIndexUpwards(EditEventCache.EventEntity.Segments.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                segmentsLayout.MoveSelectedUpwards();
                e.Handled = true;
            }
            else if (segmentsLayout.SelectedItem != null && e.KeyCode == Keys.NumPad2)
            {
                OrderedValueItem selectedItem = segmentsLayout.SelectedItem;
                EditEventCache.EventEntity.Segments.MoveItemAtIndexDownwards(EditEventCache.EventEntity.Segments.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                segmentsLayout.MoveSelectedDownwards();
                e.Handled = true;
            }
            else if (segmentsLayout.SelectedItem != null && e.KeyCode == Keys.Delete)
            {
                OrderedValueItem selectedItem = segmentsLayout.SelectedItem;
                EditEventCache.EventEntity.Segments.RemoveItemAtIndex(EditEventCache.EventEntity.Segments.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                segmentsLayout.RemoveSelected();

                segmentCountLabel.Text = $"{EditEventCache.EventEntity.Segments.Items.Count}";
                selectedItem = segmentsLayout.SelectedItem;

                if (selectedItem == null)
                {
                    segmentCountLabel.Text = "0";
                    segmentNameInput.Text = "";
                    segmentDescriptionInput.Text = "";
                    segmentsDataLayoutControl.Hide();
                }
                else
                {
                    SegmentEntity newSegment = EditEventCache.EventEntity.Segments.Items[EditEventCache.EventEntity.Segments.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber)];
                    segmentNameInput.Text = newSegment.Name;
                    segmentDescriptionInput.Text = newSegment.Description;
                }

                e.Handled = true;
            }

            if (e.KeyData == Keys.Escape)
            {
                ApplicationFormNavigator.DisplayPreviousForm();
                e.Handled = true;
            }
        }

        private void SegmentsLayoutItem_SingleClick(object sender, EventArgs e)
        {
            OrderedValueItem b = (OrderedValueItem)sender;
            if (b.Features.IsToggled)
            {
                segmentsDataLayoutControl.Hide();
            }
            else
            {
                segmentsDataLayoutControl.Show();
            }

            if (segmentsLayout.SelectedItem != null)
            {
                OrderedValueItem oldOrderedValueItem = segmentsLayout.SelectedItem;
                SegmentEntity oldSegment = EditEventCache.EventEntity.Segments.Items[EditEventCache.EventEntity.Segments.ItemCount - Convert.ToInt32(oldOrderedValueItem.OrderedNumber)];
                oldSegment.Name = segmentNameInput.Text;
                oldSegment.Description = segmentDescriptionInput.Text;
                oldOrderedValueItem.Value = segmentNameInput.Text;
            }

            OrderedValueItem newOrderedValueItem = (OrderedValueItem) sender;
            SegmentEntity newSegment = EditEventCache.EventEntity.Segments.Items[EditEventCache.EventEntity.Segments.ItemCount - Convert.ToInt32(newOrderedValueItem.OrderedNumber)];
            segmentNameInput.Text = newSegment.Name;
            segmentDescriptionInput.Text = newSegment.Description;
        }

        private void SegmentsLayoutItem_DoubleClick(object sender, EventArgs e)
        {
            OrderedValueItem oldOrderedValueItem = (OrderedValueItem)sender;
            SegmentEntity oldSegment = EditEventCache.EventEntity.Segments.Items[EditEventCache.EventEntity.Segments.ItemCount - Convert.ToInt32(oldOrderedValueItem.OrderedNumber)];
            segmentsLayout.Unfocus();
            segmentsDataLayoutControl.Hide();
            segmentNameInput.Text = "";
            segmentDescriptionInput.Text = "";
            ApplicationFormNavigator.DisplayEditEventRoundStructureForm(oldSegment);
        }

        public void Render()
        {
            segmentsLayout.Clear();
            segmentsDataLayoutControl.Hide();
            segmentCountLabel.Text = $"{EditEventCache.EventEntity.Segments.Items.Count}";
            segmentsLayout.Render(EditEventCache.EventEntity.SegmentNamesInReverseOrder);
            segmentNameInput.Text = "";
            segmentDescriptionInput.Text = "";
        }
    }
}
