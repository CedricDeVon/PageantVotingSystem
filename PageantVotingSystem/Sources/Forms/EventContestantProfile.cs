
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.Miscellaneous;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.ResourceLoaders;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EventContestantProfile : Form
    {
        private InformationLayout informationLayout;
        
        private readonly TopSideNavigationLayout topSideNavigationLayout;

        public EventContestantProfile()
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

        public void Render(int contestantId)
        {
            Update(ApplicationDatabase.ReadOneContestantEntity(contestantId));
        }

        private void Clear()
        {
            Update(new ContestantEntity());
        }

        public void Update(ContestantEntity contestantEntity)
        {
            contestantProfileImageInput.Image = ApplicationResourceLoader.SafeLoadResource(contestantEntity.ImageResourcePath);
            fullNameLabel.Text = contestantEntity.FullName;
            orderNumberLabel.Text = $"{contestantEntity.OrderNumber}";
            emailLabel.Text = contestantEntity.Email;
            phoneNumberLabel.Text = contestantEntity.PhoneNumber;
            homeAddressLabel.Text = contestantEntity.HomeAddress;
            int assumedAge = DateParser.CalculateAge(contestantEntity.BirthDate);
            birthDateLabel.Text = (assumedAge > 0) ? contestantEntity.BirthDate : "";
            ageLabel.Text = (assumedAge > 0) ? $"{assumedAge}" : "";
            genderLabel.Text = contestantEntity.GenderType;
            maritalStatusLabel.Text = contestantEntity.MaritalStatusType;
            heightLabel.Text = (contestantEntity.HeightInCentimeters > 0) ? $"{contestantEntity.HeightInCentimeters}" : "";
            weightLabel.Text = (contestantEntity.WeightInKilograms > 0) ? $"{contestantEntity.WeightInKilograms}" : "";
            talentsAndSkillsLabel.Text = contestantEntity.TalentsAndSkills;
            hobbiesLabel.Text = contestantEntity.Hobbies;
            languagesLabel.Text = contestantEntity.Languages;
            workExperiencesLabel.Text = contestantEntity.WorkExperiences;
            educationLabel.Text = contestantEntity.Education;
            mottoLabel.Text = contestantEntity.Motto;
        }
    }
}
