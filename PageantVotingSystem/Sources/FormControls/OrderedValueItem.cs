
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.FeatureCollections;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class OrderedValueItem : UserControl
    {
        public string OrderedNumber
        {
            get { return orderedNumber.Text; }

            set { orderedNumber.Text = value; }
        }

        public string Value
        {
            get { return value.Text; }

            set { this.value.Text = value; }
        }

        public object Data { get; private set; }

        public AllButtonItemFeatureCollection Features { get; private set; }
        
        public OrderedValueItem(Panel parentControl, string orderedNumber, string value, object data = null)
        {
            ThrowIfParentControlIsNull(parentControl);
            InitializeComponent();

            OrderedNumber = orderedNumber;
            Value = value;
            List<Button> buttons = new List<Button>() { this.orderedNumber, this.value };
            Features = new AllButtonItemFeatureCollection(this, parentControl, itemControl, buttons);
            Features.ConnectButtonsToAllEvents(buttons);
            Data = data;
        }

        private void ThrowIfParentControlIsNull(Panel parentControl)
        {
            if (parentControl == null)
            {
                throw new Exception("'OrderedValueItem' - 'parentControl' cannot be null");
            }
        }
    }
}
