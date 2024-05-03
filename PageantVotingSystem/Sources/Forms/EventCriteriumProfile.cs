
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EventCriteriumProfile : Form
    {
        private InformationLayout informationLayout;

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public EventCriteriumProfile()
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

        public void Render(int criteriumId)
        {
            Update(ApplicationDatabase.ReadOneCriteriumEntity(criteriumId));
        }

        private void Clear()
        {
            Update(new CriteriumEntity());
        }

        private void Update(CriteriumEntity criteriumEntity)
        {
            nameLabel.Text = criteriumEntity.Name;
            maximumValueLabel.Text = $"{criteriumEntity.MaximumValue}";
            percentageWeightLabel.Text = $"{criteriumEntity.PercentageWeight} %";
            descriptionLabel.Text = criteriumEntity.Description;
        }
    }
}
