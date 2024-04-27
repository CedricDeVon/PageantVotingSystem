
using System;
using System.Windows.Forms;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class ConfirmationDialogBox : Form
    {
        public string Message
        {
            get { return messageLabel.Text; }

            set { messageLabel.Text = value; }
        }
        
        public ConfirmationDialogBox()
        {
            SetAttributes();
        }

        public ConfirmationDialogBox(string message)
        {
            SetAttributes(message);
        }

        public void ConnectToConfirmButtonClickEvent(EventHandler eventHandler)
        {
            confirmButton.Click += eventHandler;
        }

        public void ConnectToGoBackButtonClickEvent(EventHandler eventHandler)
        {
            goBackButton.Click += eventHandler;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == confirmButton ||
                sender == goBackButton)
            {
                Hide();
            }
        }

        private void SetAttributes(string message = "")
        {
            InitializeComponent();

            Message = message;
            Hide();
        }
    }
}
