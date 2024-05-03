
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EventSegmentProfile : Form
    {
        private readonly InformationLayout informationLayout;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public EventSegmentProfile()
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
            }
        }

        private void DisplayPreviousForm()
        {
            informationLayout.StartLoadingMessageDisplay();

            ApplicationFormNavigator.DisplayPreviousForm();
            Clear();

            informationLayout.StopLoadingMessageDisplay();
        }

        public void Render(int segmentId)
        {
            SegmentEntity entity = ApplicationDatabase.ReadOneSegmentEntity(segmentId);
            nameLabel.Text = entity.Name;
            descriptionLabel.Text = entity.Description;
        }

        private void Clear()
        {
            nameLabel.Text = "";
            descriptionLabel.Text = "";
        }
    }
}
