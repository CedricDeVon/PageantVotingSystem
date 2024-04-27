
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
            topSideNavigationLayout.HideReloadButton();
            segmentsLayout = new OrderedValueItemLayout(segmentsLayoutControl);
            segmentsLayout.ItemSingleClick += new EventHandler(SegmentsLayoutItem_SingleClick);
            segmentsLayout.ItemDoubleClick += new EventHandler(SegmentsLayoutItem_DoubleClick);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();

            if (sender == saveButton)
            {
                Result securityResult = ApplicationSecurity.AuthenticateNewEventSegmentsLayout(EditEventCache.Event);
                if (!securityResult.IsSuccessful)
                {
                    informationLayout.DisplayErrorMessage(securityResult.Message);
                    return;
                }

                ApplicationFormNavigator.DisplayPrevious();
                segmentsLayout.Unfocus();
                segmentsDataLayoutControl.Hide();
                segmentNameInput.Text = "";
                segmentDescriptionInput.Text = "";
                segmentMaximumContestantLimitInput.Value = 0;
            }
            else if (sender == createSegmentButton)
            {
                string segmentName = $"Segment";
                SegmentEntity segmentEntity = new SegmentEntity();
                segmentEntity.Name = segmentName;
                EditEventCache.Event.Segments.AddNewItem(segmentEntity);
                segmentsLayout.RenderOrdered(segmentName);
                segmentCountLabel.Text = $"{EditEventCache.Event.Segments.ItemCount}";
            }
            else if (sender == resetButton)
            {
                EditEventCache.Event.Segments.ClearAllItems();
                segmentsLayout.Clear();
                segmentsLayout.Unfocus();
                segmentsDataLayoutControl.Hide();            
                segmentCountLabel.Text = "0";
                segmentNameInput.Text = "";
                segmentDescriptionInput.Text = "";
                segmentMaximumContestantLimitInput.Value = 0;
            }

            informationLayout.StopLoadingMessageDisplay();
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
                SegmentEntity oldSegment = EditEventCache.Event.Segments.Items[EditEventCache.Event.Segments.ItemCount - Convert.ToInt32(oldOrderedValueItem.OrderedNumber)];
                oldSegment.Name = segmentNameInput.Text;
                oldSegment.Description = segmentDescriptionInput.Text;
                oldSegment.MaximumContestantCount = Convert.ToInt32(segmentMaximumContestantLimitInput.Text);
                oldOrderedValueItem.Value = segmentNameInput.Text;
            }

            OrderedValueItem newOrderedValueItem = (OrderedValueItem) sender;
            SegmentEntity newSegment = EditEventCache.Event.Segments.Items[EditEventCache.Event.Segments.ItemCount - Convert.ToInt32(newOrderedValueItem.OrderedNumber)];
            segmentNameInput.Text = newSegment.Name;
            segmentDescriptionInput.Text = newSegment.Description;
            segmentMaximumContestantLimitInput.Text = $"{newSegment.MaximumContestantCount}";
        }

        private void SegmentsLayoutItem_DoubleClick(object sender, EventArgs e)
        {
            OrderedValueItem oldOrderedValueItem = (OrderedValueItem)sender;
            SegmentEntity oldSegment = EditEventCache.Event.Segments.Items[EditEventCache.Event.Segments.ItemCount - Convert.ToInt32(oldOrderedValueItem.OrderedNumber)];
            segmentsLayout.Unfocus();
            segmentsDataLayoutControl.Hide();
            segmentNameInput.Text = "";
            segmentDescriptionInput.Text = "";
            segmentMaximumContestantLimitInput.Value = 0;
            ApplicationFormNavigator.DisplayEditEventRoundStructureForm(oldSegment);
        }

        private void Form_KeyPress(object sender, KeyEventArgs e)
        {
            if (segmentsLayout.SelectedItem != null && e.KeyCode == Keys.NumPad8)
            {
                OrderedValueItem selectedItem = segmentsLayout.SelectedItem;
                EditEventCache.Event.Segments.MoveItemAtIndexUpwards(EditEventCache.Event.Segments.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                segmentsLayout.MoveSelectedUpwards();
                e.Handled = true;
            }
            else if (segmentsLayout.SelectedItem != null && e.KeyCode == Keys.NumPad2)
            {
                OrderedValueItem selectedItem = segmentsLayout.SelectedItem;
                EditEventCache.Event.Segments.MoveItemAtIndexDownwards(EditEventCache.Event.Segments.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                segmentsLayout.MoveSelectedDownwards();
                e.Handled = true;
            }
            else if (segmentsLayout.SelectedItem != null && e.KeyCode == Keys.Delete)
            {
                OrderedValueItem selectedItem = segmentsLayout.SelectedItem;
                EditEventCache.Event.Segments.RemoveItemAtIndex(EditEventCache.Event.Segments.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                segmentsLayout.RemoveSelected();

                segmentCountLabel.Text = $"{EditEventCache.Event.Segments.Items.Count}";
                selectedItem = segmentsLayout.SelectedItem;

                if (selectedItem == null)
                {
                    segmentCountLabel.Text = "0";
                    segmentNameInput.Text = "";
                    segmentDescriptionInput.Text = "";
                    segmentMaximumContestantLimitInput.Value = 0;
                    segmentsDataLayoutControl.Hide();
                }
                else
                {
                    SegmentEntity newSegment = EditEventCache.Event.Segments.Items[EditEventCache.Event.Segments.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber)];
                    segmentNameInput.Text = newSegment.Name;
                    segmentDescriptionInput.Text = newSegment.Description;
                    segmentMaximumContestantLimitInput.Text = $"{newSegment.MaximumContestantCount}";
                }

                e.Handled = true;
            }
        }
    }
}
