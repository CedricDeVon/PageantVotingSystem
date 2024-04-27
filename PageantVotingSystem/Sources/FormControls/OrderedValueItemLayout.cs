
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Generics;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class OrderedValueItemLayout : UserControl
    {
        public OrderedValueItem SelectedItem { get; private set; }

        public EventHandler ItemBeforeSingleClick { get; set; }

        public EventHandler ItemAfterSingleClick { get; set; }

        public EventHandler ItemSingleClick { get; set; }

        public EventHandler ItemDoubleClick { get; set; }

        public KeyPressEventHandler ItemKeyPressed { get; set; }

        public GenericDoublyLinkedList Items { get; private set; }

        private readonly Panel parentControl;

        public OrderedValueItemLayout(
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
            if (SelectedItem == null || SelectedItem.Features.GenericItemReference.NextItem == null)
            {
                return;
            }

            OrderedValueItem targetItem = GenericDoublyLinkedListItem.GetNextItemValue<OrderedValueItem>(SelectedItem.Features.GenericItemReference);
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

            OrderedValueItem targetItem = GenericDoublyLinkedListItem.GetPreviousItemValue<OrderedValueItem>(SelectedItem.Features.GenericItemReference);
            string targetItemValue = targetItem.Value;
            targetItem.Value = SelectedItem.Value;
            SelectedItem.Value = targetItemValue;
            SelectedItem.Features.Toggle();
            SelectedItem = targetItem;
            SelectedItem.Features.Toggle();
        }

        public OrderedValueItem RemoveSelected()
        {
            if (SelectedItem == null)
            {
                return null;
            }
            else
            {
                GenericDoublyLinkedListItem currentItem = SelectedItem.Features.GenericItemReference;
                while (currentItem != null)
                {
                    OrderedValueItem currentItemValue = (OrderedValueItem)currentItem.Value;
                    currentItemValue.OrderedNumber = $"{Convert.ToInt32(currentItemValue.OrderedNumber) - 1}";
                    currentItem = currentItem.PreviousItem;
                }
            }
            OrderedValueItem targetItem = SelectedItem;
            SelectedItem = (SelectedItem != Items.FirstItemValue) ?
                GenericDoublyLinkedListItem.GetPreviousItemValue<OrderedValueItem>(SelectedItem.Features.GenericItemReference) :
                GenericDoublyLinkedListItem.GetNextItemValue<OrderedValueItem>(SelectedItem.Features.GenericItemReference);
            DisposeItem(Items.RemoveItem<OrderedValueItem>(targetItem.Features.GenericItemReference));
            SelectedItem?.Features.Toggle();
            return targetItem;
        }

        public void RenderOrdered(string orderNumber, string value, object data = null)
        {
            Items.AddToLast(GenerateItem($"{orderNumber}", value, data).Features.GenericItemReference);
        }

        public void RenderOrdered(string value)
        {
            Items.AddToLast(GenerateItem("0", value).Features.GenericItemReference);
            GenericDoublyLinkedListItem currentItem = ((OrderedValueItem)Items.LastItemValue).Features.GenericItemReference;
            while (currentItem != null)
            {
                OrderedValueItem currentItemValue = (OrderedValueItem)currentItem.Value;
                currentItemValue.OrderedNumber = $"{Convert.ToInt32(currentItemValue.OrderedNumber) + 1}";
                currentItem = currentItem.PreviousItem;
            }
        }

        public void RenderOrdered(HashSet<string> values)
        {
            ThrowIfValuesIsNull(values);
            Clear();

            Hide();
            int index = 1;
            foreach (string value in values)
            {
                Items.AddToLast(GenerateItem($"{index++}", value).Features.GenericItemReference);
            }
            Show();
        }

        public void RenderOrdered(List<string> values)
        {
            ThrowIfValuesIsNull(values);
            Clear();

            Hide();
            for (int index = values.Count - 1; index > -1; index--)
            {
                Items.AddToLast(GenerateItem($"{index + 1}", values[index]).Features.GenericItemReference);
            }
            Show();
        }

        public void Clear()
        {
            Hide();
            while (Items.Count != 0)
            {
                DisposeItem(Items.RemoveLast<OrderedValueItem>());
            }
            SelectedItem = null;
            Show();
        }

        private OrderedValueItem GenerateItem(string orderNumber, string value, object data = null)
        {
            OrderedValueItem newItem = new OrderedValueItem(parentControl, orderNumber, value, data);
            if (ItemSingleClick != null)
            {
                newItem.Features.SingleClick += new EventHandler(ItemSingleClick);
            }
            if (ItemDoubleClick != null)
            {
                newItem.Features.DoubleClick += new EventHandler(ItemDoubleClick);
            }
            if (ItemKeyPressed != null)
            {
                newItem.Features.ConnectButtonsToKeyPress(ItemKeyPressed);
            }
            newItem.Features.SingleClick += new EventHandler(Item_SingleClick);
            return newItem;
        }

        private void DisposeItem(OrderedValueItem targetItem)
        {
            ThrowIfOrderedValueIsNull(targetItem);

            if (ItemSingleClick != null)
            {
                targetItem.Features.SingleClick -= new EventHandler(ItemSingleClick);
            }
            if (ItemDoubleClick != null)
            {
                targetItem.Features.DoubleClick -= new EventHandler(ItemDoubleClick);
            }
            if (ItemKeyPressed != null)
            {
                targetItem.Features.DisconnectButtonsToKeyPress(ItemKeyPressed);
            }
            targetItem.Features.SingleClick -= new EventHandler(Item_SingleClick);
            targetItem.Features.Dispose();
        }

        private void Item_SingleClick(object sender, EventArgs eventArgs)
        {
            ItemBeforeSingleClick?.Invoke(SelectedItem, eventArgs);

            if (SelectedItem == null)
            {
                SelectedItem = (OrderedValueItem)sender;
                SelectedItem.Features.Toggle();
            }
            else if (SelectedItem == (OrderedValueItem)sender)
            {
                SelectedItem.Features.Toggle();
                SelectedItem = null;
            }
            else
            {
                SelectedItem.Features.Toggle();
                SelectedItem = (OrderedValueItem)sender;
                SelectedItem.Features.Toggle();
            }

            ItemAfterSingleClick?.Invoke(sender, eventArgs);
        }


        private void ThrowIfParentControlIsNull(Panel parentControl)
        {
            if (parentControl == null)
            {
                throw new Exception("'OrderedValueItemLayout' - 'parentControl' cannot be null");
            }
        }

        private void ThrowIfValuesIsNull(List<string> values)
        {
            if (values == null)
            {
                throw new Exception("'OrderedValueItemLayout' - 'values' cannot be null");
            }
        }

        private void ThrowIfValuesIsNull(HashSet<string> values)
        {
            if (values == null)
            {
                throw new Exception("'OrderedValueItemLayout' - 'values' cannot be null");
            }
        }

        private void ThrowIfOrderedValueIsNull(OrderedValueItem orderedValueItem)
        {
            if (orderedValueItem == null)
            {
                throw new Exception("'OrderedValueItemLayout' - 'orderedValueItem' cannot be null");
            }
        }
    }
}
