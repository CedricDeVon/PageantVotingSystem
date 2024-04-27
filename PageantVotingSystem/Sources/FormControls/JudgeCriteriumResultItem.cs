
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.FeatureCollections;
using Google.Protobuf.WellKnownTypes;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class JudgeCriteriumResultItem : UserControl
    {
        public new string Name
        {
            get { return nameLabel.Text; }

            set { nameLabel.Text = value; }
        }

        public string Value
        {
            get { return $"{valueInput.Value}"; }

            set
            {
                Data.Result.BaseValue = (float) Convert.ToDecimal(value);
                valueInput.Value = (decimal) Data.Result.BaseValue;
            }
        }

        public JudgeCriteriumEntity Data { get; private set; }

        public AllButtonItemFeatureCollection Features { get; private set; }

        public JudgeCriteriumResultItem(
            Panel parentControl,
            JudgeCriteriumEntity judgeCriteriumEntity)
        {
            InitializeComponent();

            Data = judgeCriteriumEntity;
            Name = judgeCriteriumEntity.Criterium.Name;
            Value = $"{judgeCriteriumEntity.Result.BaseValue}";
            List<Button> buttons = new List<Button>() { nameLabel };
            Features = new AllButtonItemFeatureCollection(this, parentControl, itemControl, buttons);
            Features.ConnectButtonsToAllEvents(buttons);
            valueInput.Maximum = (decimal) judgeCriteriumEntity.Criterium.MaximumValue;
            SetCurrentBar(Data.Result.BaseValue);
        }

        public void ResetValue()
        {
            Value = "0";
            Data.Result.BaseValue = 0;
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (sender == valueInput)
            {
                Data.Result.BaseValue = (float)valueInput.Value;
                SetCurrentBar(Data.Result.BaseValue);
            }
        }

        private void SetCurrentBar(float value = 0)
        {
            currentValueBar.Width = CalculateNewCurrentBarWidth(value);
        }

        private int CalculateNewCurrentBarWidth(float value)
        {
            return Convert.ToInt32(value / Data.Criterium.MaximumValue * totalValueBar.Width);
        }
    }
}
