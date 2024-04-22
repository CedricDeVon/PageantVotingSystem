
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

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
            label.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(14)))));
            label.Text = message;
            label.Show();
        }

        public void DisplayHighlightedMessage(string message = "")
        {
            loadingTimer.Stop();
            label.BackColor = Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(0)))), ((int)(((byte)(241)))));
            label.Text = message;
            label.Show();
        }

        public void DisplayErrorMessage(string message = "")
        {
            loadingTimer.Stop();
            label.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(14)))), ((int)(((byte)(14)))));
            label.Text = message;
            label.Show();
        }

        public void DisplaySuccessfulMessage(string message = "")
        {
            loadingTimer.Stop();
            label.BackColor = Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(255)))), ((int)(((byte)(14)))));
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
