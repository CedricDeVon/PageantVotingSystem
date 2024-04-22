
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Generics;
using PageantVotingSystem.Sources.Entities;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class EventResultItemLayout : UserControl
    {
        public EventHandler ItemSingleClick { get; set; }

        public EventHandler ItemDoubleClick { get; set; }

        public EventResultItem SelectedItem { get; private set; }

        public GenericDoublyLinkedList Items { get; private set; }

        private readonly Panel parentControl;

        public EventResultItemLayout(
            Panel parentControl,
            EventHandler itemSingleClick = null,
            EventHandler itemDoubleClick = null)
        {
            ThrowIfParentControlIsNull(parentControl);
            InitializeComponent();

            this.parentControl = parentControl;
            Items = new GenericDoublyLinkedList();
            ItemSingleClick = itemSingleClick;
            ItemDoubleClick = itemDoubleClick;
        }

        public void Render(List<ContestantResultEntity> entities)
        {
            Clear();

            Hide();
            for (int index = entities.Count - 1; index > -1; index--)
            {
                Items.AddToLast(GenerateItem(entities[index]).Features.GenericItemReference);
            }
            Show();
        }

        public void Clear()
        {
            Hide();
            while (Items.Count != 0)
            {
                DisposeItem(Items.RemoveLast<EventResultItem>());
            }
            SelectedItem = null;
            Show();
        }

        private EventResultItem GenerateItem(ContestantResultEntity entity)
        {
            EventResultItem newItem = new EventResultItem(parentControl, entity);
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

        private void DisposeItem(EventResultItem targetItem)
        {
            ThrowIfEventResultItemIsNull(targetItem);

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
                SelectedItem = (EventResultItem)sender;
                SelectedItem.Features.Toggle();
            }
            else if (SelectedItem == (EventResultItem)sender)
            {
                SelectedItem.Features.Toggle();
                SelectedItem = null;
            }
            else
            {
                SelectedItem.Features.Toggle();
                SelectedItem = (EventResultItem)sender;
                SelectedItem.Features.Toggle();
            }
        }

        private void ThrowIfParentControlIsNull(Panel parentControl)
        {
            if (parentControl == null)
            {
                throw new Exception("'EventResultItemLayout' - 'parentControl' cannot be null");
            }
        }

        private void ThrowIfEventResultItemIsNull(EventResultItem eventResultItem)
        {
            if (eventResultItem == null)
            {
                throw new Exception("'EventResultItem' - 'eventResultItem' cannot be null");
            }
        }
    }
}
