
using System;
using System.Windows.Forms;

using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.Configurations;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.ResourceLoaders;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EventContestantProfile : Form
    {
        public InformationLayout InformationLayout { get; private set; }

        public EventContestantProfile()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            InformationLayout = new InformationLayout(informationLayoutControl);

            ApplicationFormNavigator.ListenToFormKeyDownEvent(this);
        }

        public void Render(ContestantEntity contestantEntity)
        {
            contestantProfileImageInput.Image = ApplicationResourceLoader.SafeLoadResource(contestantEntity.ImageResourcePath);
            fullNameLabel.Text = contestantEntity.FullName;
            orderNumberLabel.Text = $"{contestantEntity.OrderNumber}";
            emailLabel.Text = contestantEntity.Email;
            phoneNumberLabel.Text = contestantEntity.PhoneNumber;
            homeAddressLabel.Text = contestantEntity.HomeAddress;
            birthDateLabel.Text = contestantEntity.BirthDate;
            genderLabel.Text = contestantEntity.GenderType;
            maritalStatusLabel.Text = contestantEntity.MaritalStatusType;
            heightLabel.Text = $"{contestantEntity.HeightInCentimeters}";
            weightLabel.Text = $"{contestantEntity.WeightInKilograms}";
            talentsAndSkillsLabel.Text = contestantEntity.TalentsAndSkills;
            hobbiesLabel.Text = contestantEntity.Hobbies;
            languagesLabel.Text = contestantEntity.Languages;
            workExperiencesLabel.Text = contestantEntity.WorkExperiences;
            educationLabel.Text = contestantEntity.Education;
            mottoLabel.Text = contestantEntity.Motto;
        }

        private void ResetAllData()
        {
            contestantProfileImageInput.Image = ApplicationResourceLoader.SafeLoadResource(ApplicationConfiguration.DefaultUserProfileImagePath);
            fullNameLabel.Text = "";
            orderNumberLabel.Text = "";
            emailLabel.Text = "";
            phoneNumberLabel.Text = "";
            homeAddressLabel.Text = "";
            birthDateLabel.Text = "";
            genderLabel.Text = "";
            maritalStatusLabel.Text = "";
            heightLabel.Text = "";
            weightLabel.Text = "";
            talentsAndSkillsLabel.Text = "";
            hobbiesLabel.Text = "";
            languagesLabel.Text = "";
            workExperiencesLabel.Text = "";
            educationLabel.Text = "";
            mottoLabel.Text = "";
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
                ResetAllData();
            }
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
