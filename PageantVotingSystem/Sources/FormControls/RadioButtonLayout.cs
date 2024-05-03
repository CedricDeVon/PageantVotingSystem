
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class RadioButtonLayout : UserControl
    {
        public string Value
        {
            get { return valueItem; }

            set
            {
                foreach (RadioButton button in radioButtons)
                {
                    button.Checked = button.Text == value;
                }
            }        
        }

        public EventHandler InputChanged { get; private set; }

        private string valueItem;

        private readonly HashSet<RadioButton> radioButtons;

        public RadioButtonLayout(
            Panel parentControl,
            HashSet<object> values,
            EventHandler controlInputChanged = null)
        {
            ThrowIfParentControlIsNull(parentControl);
            ThrowIfValuesIsNullOrEmpty(values);
            InitializeComponent();

            radioButtons = new HashSet<RadioButton>();
            foreach (object value in values)
            {
                RadioButton radioButton = CreateRadioButton((string) value);
                radioButtons.Add(radioButton);
                parentControl.Controls.Add(CreatePadding());
                parentControl.Controls.Add(radioButton);
            }
            if (controlInputChanged != null)
            {
                InputChanged += new EventHandler(controlInputChanged);
            }
        }

        private RadioButton CreateRadioButton(string name)
        {
            RadioButton radioButton = new RadioButton();
            radioButton.AutoSize = true;
            radioButton.Dock = DockStyle.Left;
            radioButton.FlatAppearance.BorderColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            radioButton.FlatAppearance.CheckedBackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            radioButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            radioButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            radioButton.ForeColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            radioButton.Location = new Point(3, 3);
            radioButton.Name = name;
            radioButton.Size = new Size(80, 21);
            radioButton.TabIndex = 26;
            radioButton.TabStop = true;
            radioButton.Text = name;
            radioButton.UseVisualStyleBackColor = true;
            radioButton.Click += RadioButton_Click;
            return radioButton;
        }

        private Panel CreatePadding()
        {
            Panel panel = new Panel();
            panel.Dock = System.Windows.Forms.DockStyle.Left;
            panel.Location = new System.Drawing.Point(0, 0);
            panel.MaximumSize = new System.Drawing.Size(10, 21);
            panel.Name = "panel";
            panel.Size = new System.Drawing.Size(10, 21);
            panel.TabIndex = 23;
            return panel;
        }

        public void Enable()
        {
            foreach (RadioButton radioButton in radioButtons)
            {
                radioButton.Enabled = true;
            }
        }

        public void Disable()
        {
            foreach (RadioButton radioButton in radioButtons)
            {
                radioButton.Enabled = false;
            }
        }

        public void Clear()
        {
            valueItem = "";
            foreach (RadioButton radioButton in radioButtons)
            {
                radioButton.Checked = false;
            }
        }

        private void RadioButton_Click(object sender, EventArgs e)
        {
            if (radioButtons.Contains((RadioButton)sender))
            {
                valueItem = ((RadioButton) sender).Text;
                InputChanged?.Invoke(this, e);
            }
        }

        private void ThrowIfParentControlIsNull(Panel parentControl)
        {
            if (parentControl == null)
            {
                throw new Exception("'RadioButtonLayout' - 'parentControl' cannot be null");
            }
        }

        private void ThrowIfValuesIsNullOrEmpty(HashSet<object> values)
        {
            if (values == null || values.Count <= 0)
            {
                throw new Exception("'RadioButtonLayout' - 'values' cannot be null or empty");
            }

            foreach (object value in values)
            {
                if (value == null)
                {
                    throw new Exception("'RadioButtonLayout' - A 'value' of 'values' cannot be null");
                }
            }
        }
    }
}


