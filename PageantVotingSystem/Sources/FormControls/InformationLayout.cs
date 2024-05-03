
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using PageantVotingSystem.Sources.FormStyles;

namespace PageantVotingSystem.Sources.FormControls
{
    public partial class InformationLayout : UserControl
    {
        private int currentLoadingMessageIndex;

        private readonly List<string> loadingMessages;

        public InformationLayout(Panel parentControl)
        {
            ThrowIfParentControlIsNull(parentControl);
            InitializeComponent();

            currentLoadingMessageIndex = 0;
            loadingMessages = new List<string>() { "Loading .", "Loading . .", "Loading . . ." };
            parentControl.Controls.Add(label);
        }

        public void DisplayNormalMessage(string message = "")
        {
            loadingTimer.Stop();
            label.BackColor = ApplicationFormStyle.NormalColor;
            label.Text = message;
            label.Show();
        }

        public void DisplayHighlightedMessage(string message = "")
        {
            loadingTimer.Stop();
            label.BackColor = ApplicationFormStyle.HighlightColor;
            label.Text = message;
            label.Show();
        }

        public void DisplayErrorMessage(string message = "")
        {
            loadingTimer.Stop();
            label.BackColor = ApplicationFormStyle.ErrorColor;
            label.Text = message;
            label.Show();
        }

        public void DisplaySuccessfulMessage(string message = "")
        {
            loadingTimer.Stop();
            label.BackColor = ApplicationFormStyle.SuccessColor;
            label.Text = message;
            label.Show();
        }

        public void StartLoadingMessageDisplay()
        {
            label.Show();
            loadingTimer.Start();
            currentLoadingMessageIndex = 0;
            DisplayHighlightedMessage(loadingMessages[currentLoadingMessageIndex]);
        }

        public void StopLoadingMessageDisplay(string message)
        {
            label.Show();
            loadingTimer.Stop();
            currentLoadingMessageIndex = 0;
            DisplaySuccessfulMessage(message);
            displayDelayTimer.Start();
        }

        public void StopLoadingMessageDisplay()
        {
            label.Hide();
            loadingTimer.Stop();
            currentLoadingMessageIndex = 0;
            label.Text = "";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (sender == loadingTimer)
            {
                currentLoadingMessageIndex = (currentLoadingMessageIndex + 1) % loadingMessages.Count;
                DisplayHighlightedMessage(loadingMessages[currentLoadingMessageIndex]);
            }
            else if (sender == displayDelayTimer)
            {
                displayDelayTimer.Stop();
                label.Hide();
            }
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
