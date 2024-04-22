
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Generics;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class EventStructureItemLayout : UserControl
    {
        public EventHandler ItemSingleClick { get; set; }

        public EventHandler ItemDoubleClick { get; set; }

        public EventStructureItem SelectedItem { get; private set; }

        public GenericDoublyLinkedList Items { get; private set; }

        private readonly Panel parentControl;

        public EventStructureItemLayout(
            Panel parentControl,
            EventHandler itemSingleClick = null,
            EventHandler itemDoubleClick = null)
        {
            ThrowIfParentControlIsNull(parentControl);
            InitializeComponent();

            this.parentControl = parentControl;
            ItemSingleClick = itemSingleClick;
            ItemDoubleClick = itemDoubleClick;
            Items = new GenericDoublyLinkedList();
        }

        public void MoveSelectedUpwards()
        {
            if (SelectedItem == null || SelectedItem.Features.GenericItemReference.NextItem == null)
            {
                return;
            }

            EventStructureItem targetItem = GenericDoublyLinkedListItem.GetNextItemValue<EventStructureItem>(SelectedItem.Features.GenericItemReference);
            string targetItemValue = targetItem.Value;
            targetItem.Value = SelectedItem.Value;
            SelectedItem.Value = targetItemValue;
            SelectedItem.Features.Toggle();
            SelectedItem = targetItem;
            SelectedItem.Features.Toggle();
        }

        public void MoveSelectedDownwards()
        {
            if (SelectedItem == null || SelectedItem.Features.GenericItemReference.PreviousItem == null)
            {
                return;
            }

            EventStructureItem targetItem = GenericDoublyLinkedListItem.GetPreviousItemValue<EventStructureItem>(SelectedItem.Features.GenericItemReference);
            string targetItemValue = targetItem.Value;
            targetItem.Value = SelectedItem.Value;
            SelectedItem.Value = targetItemValue;
            SelectedItem.Features.Toggle();
            SelectedItem = targetItem;
            SelectedItem.Features.Toggle();
        }

        public void Render(EventEntity entity)
        {
            Clear();

            Hide();
            for (int index = entity.Segments.ItemCount - 1; index > -1; index--)
            {
                Render(entity.Segments.GetItemAtIndex(index));
            }
            Items.AddToLast(GenerateItem(entity.Name, entity, 0).Features.GenericItemReference);
            Show();
        }

        private void Render(SegmentEntity entity)
        {
            for (int index = entity.Rounds.ItemCount - 1; index > -1; index--)
            {
                Render(entity.Rounds.GetItemAtIndex(index));
            }
            Items.AddToLast(GenerateItem(entity.Name, entity, 1).Features.GenericItemReference);
        }

        private void Render(RoundEntity entity)
        {
            for (int index = entity.Criteria.ItemCount - 1; index > -1; index--)
            {
                CriteriumEntity criteriumEntity = entity.Criteria.GetItemAtIndex(index);
                Items.AddToLast(GenerateItem(criteriumEntity.Name, criteriumEntity, 3).Features.GenericItemReference);
            }
            Items.AddToLast(GenerateItem(entity.Name, entity, 2).Features.GenericItemReference);
        }

        public void Clear()
        {
            Hide();
            while (Items.Count != 0)
            {
                DisposeItem(Items.RemoveLast<EventStructureItem>());
            }
            SelectedItem = null;
            Show();
        }

        private EventStructureItem GenerateItem(string value, object data, int ratio = 0)
        {
            EventStructureItem newItem = new EventStructureItem(parentControl, value, data, ratio);
            if (ItemSingleClick != null)
            {
                newItem.Features.SingleClick += new EventHandler(ItemSingleClick);
            }
            if (ItemDoubleClick != null)
            {
                newItem.Features.DoubleClick += new EventHandler(ItemDoubleClick);
            }
            newItem.Features.SingleClick += new EventHandler(Item_SingleClick);
            return newItem;
        }

        private void DisposeItem(EventStructureItem targetItem)
        {
            ThrowIfSingleValuedItemIsNull(targetItem);

            if (ItemSingleClick != null)
            {
                targetItem.Features.SingleClick -= new EventHandler(ItemSingleClick);
            }
            if (ItemDoubleClick != null)
            {
                targetItem.Features.DoubleClick -= new EventHandler(ItemDoubleClick);
            }
            targetItem.Features.SingleClick -= new EventHandler(Item_SingleClick);
            targetItem.Features.Dispose();
        }

        private void Item_SingleClick(object sender, EventArgs eventArgs)
        {
            if (SelectedItem == null)
            {
                SelectedItem = (EventStructureItem)sender;
                SelectedItem.Features.Toggle();
            }
            else if (SelectedItem == (EventStructureItem)sender)
            {
                SelectedItem.Features.Toggle();
                SelectedItem = null;
            }
            else
            {
                SelectedItem.Features.Toggle();
                SelectedItem = (EventStructureItem)sender;
                SelectedItem.Features.Toggle();
            }
        }

        private void ThrowIfParentControlIsNull(Panel parentControl)
        {
            if (parentControl == null)
            {
                throw new Exception("'TreeStructureItem' - 'parentControl' cannot be null");
            }
        }

        private void ThrowIfSingleValuedItemIsNull(EventStructureItem singleValueItem)
        {
            if (singleValueItem == null)
            {
                throw new Exception("'TreeStructureItem' - 'singleValueItem' cannot be null");
            }
        }
    }
}
