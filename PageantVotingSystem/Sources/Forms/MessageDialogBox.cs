
using System;
using System.Windows.Forms;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class MessageDialogBox : Form
    {
        public string Message
        {
            get { return messageLabel.Text; }

            set { messageLabel.Text = value; }
        }
        
        public MessageDialogBox()
        {
            SetAttributes();
        }

        public MessageDialogBox(string message)
        {
            SetAttributes(message);
        }

        public void ConnectToGoBackButtonClickEvent(EventHandler eventHandler)
        {
            okButton.Click += eventHandler;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == okButton)
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
