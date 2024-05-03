
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Generics;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class EntityStatusItemLayout : UserControl
    {
        public EntityStatusItem SelectedItem { get; private set; }

        public GenericDoublyLinkedList Items { get; private set; }

        public EventHandler ItemSingleClick { get; set; }

        public EventHandler ItemDoubleClick { get; set; }

        public EventHandler ItemBeforeSingleClick { get; set; }

        public EventHandler ItemAfterSingleClick { get; set; }

        private readonly Panel parentControl;

        public EntityStatusItemLayout(
            Panel parentControl)
        {
            ThrowIfParentControlIsNull(parentControl);
            InitializeComponent();

            this.parentControl = parentControl;
            Items = new GenericDoublyLinkedList();

        }

        public void RenderSuccess(
            string orderNumber,
            string value,
            string status,
            object data = null)
        {
            EntityStatusItem entityStatusItem =
                GenerateItem(orderNumber, value, status, data);
            entityStatusItem.SetSuccessStatus();
            Items.AddToLast(
                entityStatusItem.Features.GenericItemReference);
        }

        public void RenderFailure(
            string orderNumber,
            string value,
            string status,
            object data = null)
        {
            EntityStatusItem entityStatusItem =
                GenerateItem(orderNumber, value, status, data);
            entityStatusItem.SetFailureStatus();
            Items.AddToLast(entityStatusItem.Features.GenericItemReference);
        }

        private EntityStatusItem GenerateItem(
            string orderNumber,
            string value,
            string status,
            object data = null)
        {
            EntityStatusItem newItem =
                new EntityStatusItem(parentControl, orderNumber, value, status, data);
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

        public void Clear()
        {
            Hide();
            while (Items.Count != 0)
            {
                DisposeItem(Items.RemoveLast<EntityStatusItem>());
            }
            SelectedItem = null;
            Show();
        }

        private void DisposeItem(EntityStatusItem targetItem)
        {
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
            ItemBeforeSingleClick?.Invoke(SelectedItem, eventArgs);

            if (SelectedItem == null)
            {
                SelectedItem = (EntityStatusItem)sender;
                SelectedItem.Features.Toggle();
            }
            else if (SelectedItem == (EntityStatusItem)sender)
            {
                SelectedItem.Features.Toggle();
                SelectedItem = null;
            }
            else
            {
                SelectedItem.Features.Toggle();
                SelectedItem = (EntityStatusItem)sender;
                SelectedItem.Features.Toggle();
            }

            ItemAfterSingleClick?.Invoke(sender, eventArgs);
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
    }
}
