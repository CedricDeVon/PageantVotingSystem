
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EventRoundProfile : Form
    {
        private readonly InformationLayout informationLayout;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public EventRoundProfile()
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

        private void DisplayPreviousForm()
        {
            ApplicationFormNavigator.DisplayPreviousForm();
            Clear();
        }

        public void Render(int roundId)
        {
            Update(ApplicationDatabase.ReadOneRoundEntity(roundId));
        }

        private void Clear()
        {
            Update(new RoundEntity());
        }

        private void Update(RoundEntity roundEntity)
        {
            nameLabel.Text = roundEntity.Name;
            descriptionLabel.Text = roundEntity.Description;
        }
    }
}
