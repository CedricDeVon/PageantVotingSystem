
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.Forms;
using PageantVotingSystem.Sources.Entities;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EventCriteriumProfile : Form
    {
        public InformationLayout InformationLayout { get; private set; }

        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public EventCriteriumProfile()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
        }

        public void Render(CriteriumEntity entity)
        {
            nameLabel.Text = entity.Name;
            maximumValueLabel.Text = $"{entity.MaximumValue}";
            percentageWeightLabel.Text = $"{entity.PercentageWeight} %";
            descriptionLabel.Text = entity.Description;
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
            nameLabel.Text = "";
            maximumValueLabel.Text = "";
            percentageWeightLabel.Text = "";
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
