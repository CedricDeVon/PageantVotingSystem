
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.FeatureCollections;
using PageantVotingSystem.Sources.Entities;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class EventResultItem : UserControl
    {
        public string Remark
        {
            get { return remarkLabel.Text; }

            private set { remarkLabel.Text = value; }
        }

        public string Rank
        {
            get { return rankLabel.Text; }

            private set { rankLabel.Text = value; }
        }

        public string OrderNumber
        {
            get { return orderNumberLabel.Text; }

            private set { orderNumberLabel.Text = value; }
        }

        public string ContestantName
        {
            get { return contestantNameLabel.Text; }

            private set { contestantNameLabel.Text = value; }
        }

        public string NetPercentageScore
        {
            get { return netPercentageScoreLabel.Text; }

            private set { netPercentageScoreLabel.Text = value; }
        }

        public string NetValueScore
        {
            get { return netValueScoreLabel.Text; }

            private set { netValueScoreLabel.Text = value; }
        }

        public AllButtonItemFeatureCollection Features { get; private set; }

        public EventResultItem(
            Panel parentControl,
            ContestantResultEntity entity)
        {
            ThrowIfParentControlIsNull(parentControl);
            ThrowIfContestantResultEntityIsNull(entity);
            InitializeComponent();

            Remark = "";
            Rank = entity.RankingLabel;
            OrderNumber = $"{entity.OrderNumber}";
            ContestantName = entity.FullName;
            NetPercentageScore = entity.NetPercentageLabel;
            NetValueScore = entity.ValueLabel;

            List<Button> buttons = new List<Button>()
            {
                remarkLabel,
                rankLabel,
                orderNumberLabel,
                contestantNameLabel,
                netPercentageScoreLabel,
                netValueScoreLabel
            };
            Features = new AllButtonItemFeatureCollection(this, parentControl, control, buttons);
            Features.ConnectButtonsToAllEvents(buttons);
        }

        private void ThrowIfParentControlIsNull(Panel parentControl)
        {
            if (parentControl == null)
            {
                throw new Exception("'EventResultItem' - 'parentControl' cannot be null");
            }
        }

        private void ThrowIfContestantResultEntityIsNull(ContestantResultEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("'EventResultItem' - 'entity' cannot be null");
            }
        }
    }
}
