
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Generics;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class SingleValuedItemLayout : UserControl
    {
        public SingleValuedItem SelectedItem { get; private set; }
        
        public GenericDoublyLinkedList Items { get; private set; }

        public EventHandler ItemSingleClick { get; set; }

        public EventHandler ItemDoubleClick { get; set; }

        private readonly Panel parentControl;

        public SingleValuedItemLayout(
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

        public void Render(string value, object data = null)
        {
            Items.AddToLast(GenerateItem(value, data).Features.GenericItemReference);
        }

        public void Render(List<string> values)
        {
            ThrowIfValuesIsNull(values);
            Clear();

            Hide();
            for (int index = values.Count - 1; index > -1; index--)
            {
                Items.AddToLast(GenerateItem(values[index]).Features.GenericItemReference);
            }
            Show();
        }
        private SingleValuedItem GenerateItem(string value, object data = null)
        {
            SingleValuedItem newItem = new SingleValuedItem(parentControl, value, data);
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

        public void Unfocus()
        {
            if (SelectedItem != null)
            {
                SelectedItem.Features.DisableToggle();
                SelectedItem = null;
            }
        }

        public void MoveSelectedUpwards()
        {
            if (SelectedItem == null ||
                SelectedItem.Features.GenericItemReference.NextItem == null)
            {
                return;
            }

            SingleValuedItem targetItem = GenericDoublyLinkedListItem.GetNextItemValue<SingleValuedItem>(
                SelectedItem.Features.GenericItemReference);
            string targetItemValue = targetItem.Value;
            targetItem.Value = SelectedItem.Value;
            SelectedItem.Value = targetItemValue;
            SelectedItem.Features.Toggle();
            SelectedItem = targetItem;
            SelectedItem.Features.Toggle();
        }

        public void MoveSelectedDownwards()
        {
            if (SelectedItem == null ||
                SelectedItem.Features.GenericItemReference.PreviousItem == null)
            {
                return;
            }

            SingleValuedItem targetItem = GenericDoublyLinkedListItem.GetPreviousItemValue<SingleValuedItem>(
                SelectedItem.Features.GenericItemReference);
            string targetItemValue = targetItem.Value;
            targetItem.Value = SelectedItem.Value;
            SelectedItem.Value = targetItemValue;
            SelectedItem.Features.Toggle();
            SelectedItem = targetItem;
            SelectedItem.Features.Toggle();
        }

        public void RemoveSelected()
        {
            if (SelectedItem == null)
            {
                return;
            }

            SingleValuedItem targetItem = SelectedItem;
            SelectedItem = (SelectedItem != Items.FirstItemValue) ?
                GenericDoublyLinkedListItem.GetPreviousItemValue<SingleValuedItem>(SelectedItem.Features.GenericItemReference) :
                GenericDoublyLinkedListItem.GetNextItemValue<SingleValuedItem>(SelectedItem.Features.GenericItemReference);
            DisposeItem(Items.RemoveItem<SingleValuedItem>(targetItem.Features.GenericItemReference));
            SelectedItem?.Features.Toggle();
        }

        public void Clear()
        {
            Hide();
            while (Items.Count != 0)
            {
                DisposeItem(Items.RemoveLast<SingleValuedItem>());
            }
            SelectedItem = null;
            Show();
        }

        private void DisposeItem(SingleValuedItem targetItem)
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
                SelectedItem = (SingleValuedItem)sender;
                SelectedItem.Features.Toggle();
            }
            else if (SelectedItem == (SingleValuedItem)sender)
            {
                SelectedItem.Features.Toggle();
                SelectedItem = null;
            }
            else
            {
                SelectedItem.Features.Toggle();
                SelectedItem = (SingleValuedItem)sender;
                SelectedItem.Features.Toggle();
            }
        }

        private void ThrowIfParentControlIsNull(Panel parentControl)
        {
            if (parentControl == null)
            {
                throw new Exception("'SingleValuedItemLayout' - 'parentControl' cannot be null");
            }
        }

        private void ThrowIfValuesIsNull(List<string> values)
        {
            if (values == null)
            {
                throw new Exception("'SingleValuedItemLayout' - 'values' cannot be null");
            }
        }

        private void ThrowIfSingleValuedItemIsNull(SingleValuedItem singleValueItem)
        {
            if (singleValueItem == null)
            {
                throw new Exception("'SingleValuedItemLayout' - 'singleValueItem' cannot be null");
            }
        }
    }
}

