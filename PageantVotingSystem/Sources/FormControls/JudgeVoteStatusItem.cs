
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FeatureCollections;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class JudgeVoteStatusItem : UserControl
    {
        public string OrderNumber
        {
            get { return orderNumber.Text; }

            set { orderNumber.Text = value; }
        }

        public string JudgeName
        {
            get { return judgeName.Text; }

            set { judgeName.Text = value; }
        }

        public string HasJudgeVoted
        {
            get { return $"{TranslateHasVotedResult()}"; }

            private set { hasJudgeVoted.Text = value; }
        }

        public AllButtonItemFeatureCollection Features { get; private set; }

        public JudgeVoteStatusItem(Panel parentControl, string judgeOrderNumber, string judgeName, string hasJudgeVoted)
        {
            ThrowIfParentControlIsNull(parentControl);
            InitializeComponent();

            OrderNumber = judgeOrderNumber;
            JudgeName = judgeName;
            HasJudgeVoted = hasJudgeVoted;
            if (hasJudgeVoted == "YES")
            {
                ApplicationFormStyle.ButtonSuccess(this.hasJudgeVoted);
            }
            else
            {
                ApplicationFormStyle.ButtonError(this.hasJudgeVoted);
            }
            List<Button> buttons = new List<Button>() { orderNumber, this.judgeName };
            Features = new AllButtonItemFeatureCollection(this, parentControl, itemControl, buttons);
            Features.ConnectButtonsToAllEvents(buttons);
        }

        public void SetJudgeToVoted()
        {
            hasJudgeVoted.Text = "YES";
            ApplicationFormStyle.ButtonSuccess(hasJudgeVoted);
        }

        public void SetJudgeToUnvoted()
        {
            hasJudgeVoted.Text = "NO";
            ApplicationFormStyle.ButtonError(hasJudgeVoted);
        }

        private bool TranslateHasVotedResult()
        {
            return hasJudgeVoted.Text == "YES";
        }

        private void ThrowIfParentControlIsNull(Panel parentControl)
        {
            if (parentControl == null)
            {
                throw new Exception("'JudgeVoteStatusItem' - 'parentControl' cannot be null");
            }
        }
    }
}
