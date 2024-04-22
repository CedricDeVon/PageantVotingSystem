
using System;
using System.Windows.Forms;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class DialogBox : Form
    {
        public string Message
        {
            get { return messageLabel.Text; }

            set { messageLabel.Text = value; }
        }

        public DialogBox(string message)
        {
            InitializeComponent();

            Message = message;
            Hide();
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


    }
}
