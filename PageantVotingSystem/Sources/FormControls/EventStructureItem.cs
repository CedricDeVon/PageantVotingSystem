
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.FeatureCollections;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class EventStructureItem : UserControl
    {
        public string Value
        {
            get { return valueLabel.Text; }

            set { valueLabel.Text = value; }
        }

        public int Layer
        {
            get { return layer; }

            private set
            {
                layer = value;
                leftMargin.Width = value * 40;
            }
        }

        public AllButtonItemFeatureCollection Features { get; private set; }

        public object Data { get; private set; }

        private int layer;

        public EventStructureItem(
            Panel parentControl,
            string value,
            object data,
            int layer = 0)
        {
            ThrowIfParentControlIsNull(parentControl);
            ThrowIfDataIsNull(data);
            InitializeComponent();

            Value = value;
            valueLabel.Text = value;
            List<Button> buttons = new List<Button>() { valueLabel };
            Features = new AllButtonItemFeatureCollection(this, parentControl, control, buttons);
            Features.ConnectButtonsToAllEvents(buttons);
            Data = data;
            Layer = layer;
        }

        private void ThrowIfParentControlIsNull(Panel parentControl)
        {
            if (parentControl == null)
            {
                throw new Exception("'TreeStructureItem' - 'parentControl' cannot be null");
            }
        }

        private void ThrowIfDataIsNull(object genericData)
        {
            if (genericData == null)
            {
                throw new Exception("'TreeStructureItem' - 'genericData' cannot be null");
            }
        }
    }
}

