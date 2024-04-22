
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.FeatureCollections;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class SingleValuedItem : UserControl
    {
        public string Value
        {
            get { return value.Text; }

            set { this.value.Text = value; }
        }

        public object Data { get; private set; }

        public AllButtonItemFeatureCollection Features { get; private set; }

        public SingleValuedItem(Panel parentControl, string value, object data = null)
        {
            ThrowIfParentControlIsNull(parentControl);
            InitializeComponent();

            Value = value;
            this.value.Text = value;
            List<Button> buttons = new List<Button>() { this.value };
            Features = new AllButtonItemFeatureCollection(this, parentControl, itemControl, buttons);
            Features.ConnectButtonsToAllEvents(buttons);
            Data = data;
        }

        private void ThrowIfParentControlIsNull(Panel parentControl)
        {
            if (parentControl == null)
            {
                throw new Exception("'SingleValuedItem' - 'parentControl' cannot be null");
            }
        }
    }
}
