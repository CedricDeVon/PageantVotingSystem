
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EventProfile : Form
    {
        private readonly InformationLayout informationLayout;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public EventProfile()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                DisplayPreviousForm();
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DisplayPreviousForm();
                e.Handled = true;
            }
        }

        public void DisplayPreviousForm()
        {
            informationLayout.StartLoadingMessageDisplay();

            ApplicationFormNavigator.DisplayPreviousForm();
            Clear();

            informationLayout.StopLoadingMessageDisplay();
        }

        public void Render(int eventId)
        {
            Update(ApplicationDatabase.ReadOneEventEntity(eventId));
        }

        private void Clear()
        {
            Update(new EventEntity());
        }

        private void Update(EventEntity eventEntity)
        {
            managerEmailLabel.Text = eventEntity.ManagerEmail;
            nameLabel.Text = eventEntity.Name;
            scoringSystemLabel.Text = eventEntity.ScoringSystemType;
            hostAddressLabel.Text = eventEntity.HostAddress;
            descriptionLabel.Text = eventEntity.Description;
        }
    }
}
