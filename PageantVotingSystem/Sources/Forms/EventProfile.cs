
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.Entities;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EventProfile : Form
    {
        public InformationLayout InformationLayout { get; private set; }

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public EventProfile()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
        }

        public void Render(EventEntity eventEntity)
        {
            managerEmailLabel.Text = eventEntity.ManagerEmail;
            nameLabel.Text = eventEntity.Name;
            scoringSystemLabel.Text = eventEntity.ScoringSystemType;
            hostAddressLabel.Text = eventEntity.HostAddress;
            dateStartLabel.Text = eventEntity.DateTimeStart;
            dateEndLabel.Text = eventEntity.DateTimeEnd;
            descriptionLabel.Text = eventEntity.Description;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
                ResetAllData();
            }
        }

        private void ResetAllData()
        {
            managerEmailLabel.Text = "";
            nameLabel.Text = "";
            scoringSystemLabel.Text = "";
            hostAddressLabel.Text = "";
            dateStartLabel.Text = "";
            dateEndLabel.Text = "";
            descriptionLabel.Text = "";
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ApplicationFormNavigator.DisplayPrevious();
                ResetAllData();
            }
        }
    }
}
