
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Generics;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class JudgeCriteriumResultItemLayout : UserControl
    {
        public GenericDoublyLinkedList Items { get; private set; }

        public EventHandler ItemSingleClick { get; set; }

        public EventHandler ItemDoubleClick { get; set; }

        private readonly Panel parentControl;

        public JudgeCriteriumResultItemLayout(Panel parentControl)
        {
            InitializeComponent();

            this.parentControl = parentControl;
            Items = new GenericDoublyLinkedList();
        }

        public void Render(JudgeCriteriumEntity judgeCriteriumEntity)
        {
            Items.AddToLast(
                GenerateItem(judgeCriteriumEntity).Features.GenericItemReference);
        }

        public void Render(List<JudgeCriteriumEntity> judgeCriteriumEntities)
        {
            Clear();

            Hide();
            for (int index = judgeCriteriumEntities.Count - 1; index > -1; index--)
            {
                Items.AddToLast(
                    GenerateItem(judgeCriteriumEntities[index]).Features.GenericItemReference);
            }
            Show();
        }

        private JudgeCriteriumResultItem GenerateItem(JudgeCriteriumEntity judgeCriteriumEntity)
        {
            JudgeCriteriumResultItem newItem =
                new JudgeCriteriumResultItem(parentControl, judgeCriteriumEntity);
            if (ItemSingleClick != null)
            {
                newItem.Features.SingleClick += new EventHandler(ItemSingleClick);
            }
            if (ItemDoubleClick != null)
            {
                newItem.Features.DoubleClick += new EventHandler(ItemDoubleClick);
            }
            return newItem;

        }

        public void Clear()
        {
            Hide();
            while (Items.Count != 0)
            {
                DisposeItem(Items.RemoveLast<JudgeCriteriumResultItem>());
            }
            Show();
        }
        
        public void ClearInputs()
        {
            GenericDoublyLinkedListItem currentItem = Items.FirstItem;
            while (currentItem != null)
            {
                JudgeCriteriumResultItem itemValue = (JudgeCriteriumResultItem) currentItem.Value;
                itemValue.ResetValue();
                currentItem = currentItem.NextItem;
            }
        }

        private void DisposeItem(JudgeCriteriumResultItem targetItem)
        {
            if (ItemSingleClick != null)
            {
                targetItem.Features.SingleClick -= new EventHandler(ItemSingleClick);
            }
            if (ItemDoubleClick != null)
            {
                targetItem.Features.DoubleClick -= new EventHandler(ItemDoubleClick);
            }
            targetItem.Features.Dispose();
        }
    }
}
