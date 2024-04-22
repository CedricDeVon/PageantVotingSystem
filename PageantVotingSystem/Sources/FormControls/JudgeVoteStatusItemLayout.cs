
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Generics;
using System;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class JudgeVoteStatusItemLayout : UserControl
    {
        public JudgeVoteStatusItem SelectedItem { get; private set; }

        public GenericDoublyLinkedList Items { get; private set; }

        private readonly Panel parentControl;

        public JudgeVoteStatusItemLayout(
            Panel parentControl)
        {
            ThrowIfParentControlIsNull(parentControl);
            InitializeComponent();

            this.parentControl = parentControl;
            Items = new GenericDoublyLinkedList();
        }

        public void Render(List<string> values)
        {
            ThrowIfValuesIsNull(values);
            Clear();

            Hide();
            for (int index = values.Count - 1; index > -1; index--)
            {
                Items.AddToLast(GenerateItem($"{index + 1}", values[index], "YES").Features.GenericItemReference);
            }
            Show();
        }

        public void Clear()
        {
            Hide();
            while (Items.Count != 0)
            {
                DisposeItem(Items.RemoveLast<JudgeVoteStatusItem>());
            }
            SelectedItem = null;
            Show();
        }

        private JudgeVoteStatusItem GenerateItem(string orderNumber, string value, string hasJudged)
        {
            return new JudgeVoteStatusItem(parentControl, orderNumber, value, hasJudged);
        }

        private void DisposeItem(JudgeVoteStatusItem targetItem)
        {
            targetItem.Features.Dispose();
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
