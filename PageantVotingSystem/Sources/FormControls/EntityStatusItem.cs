
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FeatureCollections;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class EntityStatusItem : UserControl
    {
        public string OrderNumber
        {
            get { return orderNumberLabel.Text; }

            set { orderNumberLabel.Text = value; }
        }

        public string Value
        {
            get { return valueLabel.Text; }

            set { valueLabel.Text = value; }
        }

        public string Status
        {
            get { return statusLabel.Text; }

            set { statusLabel.Text = value; }
        }

        public object Data { get; private set; }

        public AllButtonItemFeatureCollection Features { get; private set; }

        public EntityStatusItem(
            Panel parentControl,
            string orderNumber,
            string value,
            string status,
            object data = null)
        {
            InitializeComponent();

            OrderNumber = orderNumber;
            Value = value;
            Status = status;
            List<Button> buttons = new List<Button>() { orderNumberLabel, valueLabel };
            Features =
                new AllButtonItemFeatureCollection(this, parentControl, itemControl, buttons);
            Features.ConnectButtonsToAllEvents(buttons);
            Data = data; 
        }

        public void SetSuccessStatus()
        {
            ApplicationFormStyle.ButtonSuccess(statusLabel);
        }

        public void SetFailureStatus()
        {
            ApplicationFormStyle.ButtonError(statusLabel);
        }

        public void SetSuccessStatus(string status)
        {
            statusLabel.Text = status;
            ApplicationFormStyle.ButtonSuccess(statusLabel);
        }

        public void SetFailureStatus(string status)
        {
            statusLabel.Text = status;
            ApplicationFormStyle.ButtonError(statusLabel);
        }
    }
}
