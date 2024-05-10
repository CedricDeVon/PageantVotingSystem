
using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Security;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.Miscellaneous;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.Configurations;
using PageantVotingSystem.Sources.ResourceLoaders;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EditEventContestants : Form
    {
        private readonly InformationLayout informationLayout;

        private readonly TopSideNavigationLayout topSideNavigationLayout;
        
        private readonly OrderedValueItemLayout contestantsLayout;

        private readonly RadioButtonLayout gendersLayout;

        private readonly RadioButtonLayout maritalStatusLayout;

        public EditEventContestants()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationLayout = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            contestantsLayout = new OrderedValueItemLayout(contestantsLayoutControl, ContestantLayoutItem_SingleClick);
            contestantsLayout.Render(EditEventCache.ContestantFullNames);
            contestantsCountLabel.Text = $"{EditEventCache.ContestantEntities.ItemCount}";
            gendersLayout = new RadioButtonLayout(gendersLayoutControl, GenderCache.Types);
            maritalStatusLayout = new RadioButtonLayout(maritalStatusLayoutControl, MaritalStatusCache.Types);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationLayout.StartLoadingMessageDisplay();

            if (sender == goBackButton)
            {
                ApplicationFormNavigator.DisplayPreviousForm();

                contestantsLayout.Unfocus();
                contestantDataLayout.Hide();
                SetupPictureBox(ApplicationConfiguration.DefaultUserProfileImagePath);
                contestantFullNameInput.Text = "";
                contestantEmailInput.Text = "";
                contestantPhoneNumberInput.Text = "";
                contestantHomeAddressInput.Text = "";
                contestantBirthDateInput.Text = "";
                contestantAssumedAge.Text = "0";
                gendersLayout.Value = "Rather Not Say";
                maritalStatusLayout.Value = "Rather Not Say";
                contestantHeightInCentimetersInput.Value = 0;
                contestantWeightInKilogramsInput.Value = 0;
                contestantTalentsAndSkillsInput.Text = "";
                contestantHobbiesInput.Text = "";
                contestantLanguagesInput.Text = "";
                contestantWorkExperiencesInput.Text = "";
                contestantEducationInput.Text = "";
                contestantMottoInput.Text = "";
            }
            else if (sender == createContestantButton)
            {
                string contestantName = $"Contestant";
                ContestantEntity contestantEntity = new ContestantEntity();
                contestantEntity.FullName = contestantName;
                EditEventCache.ContestantEntities.AddNewItem(contestantEntity);
                contestantsLayout.Render(contestantName);
                contestantsCountLabel.Text = $"{EditEventCache.ContestantEntities.ItemCount}";
            }
            else if (sender == resetButton)
            {
                EditEventCache.ContestantEntities.ClearAllItems();
                contestantsLayout.Clear();
                contestantsLayout.Unfocus();
                contestantsCountLabel.Text = "0";
                contestantDataLayout.Hide();
            }
            else if (sender == contestantImageInput)
            {
                contestantImageFileInput.ShowDialog(this);
            }

            informationLayout.StopLoadingMessageDisplay();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (contestantsLayout.SelectedItem != null && e.KeyCode == Keys.NumPad8)
            {
                OrderedValueItem selectedItem = contestantsLayout.SelectedItem;
                EditEventCache.ContestantEntities.MoveItemAtIndexUpwards(EditEventCache.ContestantEntities.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                contestantsLayout.MoveSelectedUpwards();
                e.Handled = true;
            }
            else if (contestantsLayout.SelectedItem != null && e.KeyCode == Keys.NumPad2)
            {
                OrderedValueItem selectedItem = contestantsLayout.SelectedItem;
                EditEventCache.ContestantEntities.MoveItemAtIndexDownwards(EditEventCache.ContestantEntities.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                contestantsLayout.MoveSelectedDownwards();
                e.Handled = true;
            }
            else if (contestantsLayout.SelectedItem != null && e.KeyCode == Keys.Delete)
            {
                OrderedValueItem selectedItem = contestantsLayout.SelectedItem;
                EditEventCache.ContestantEntities.RemoveItemAtIndex(EditEventCache.ContestantEntities.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                contestantsLayout.RemoveSelected();

                contestantsCountLabel.Text = $"{EditEventCache.ContestantEntities.Items.Count}";
                if (EditEventCache.ContestantEntities.ItemCount > 0)
                {
                    OrderedValueItem a = contestantsLayout.SelectedItem;
                    ContestantEntity c = EditEventCache.ContestantEntities.Items[EditEventCache.ContestantEntities.ItemCount - Convert.ToInt32(a.OrderedNumber)];
                    contestantImageFileInput.FileName = c.ImageResourcePath;
                    SetupPictureBox(c.ImageResourcePath);
                    contestantFullNameInput.Text = c.FullName;
                    contestantEmailInput.Text = c.Email;
                    contestantPhoneNumberInput.Text = c.PhoneNumber;
                    contestantHomeAddressInput.Text = c.HomeAddress;
                    contestantBirthDateInput.Value = DateTime.Parse(c.BirthDate);
                    contestantAssumedAge.Text = $"{DateParser.CalculateAge(c.BirthDate)}";
                    gendersLayout.Value = c.GenderType;
                    maritalStatusLayout.Value = c.MaritalStatusType;
                    contestantHeightInCentimetersInput.Value = (decimal)c.HeightInCentimeters;
                    contestantWeightInKilogramsInput.Value = (decimal)c.WeightInKilograms;
                    contestantTalentsAndSkillsInput.Text = c.TalentsAndSkills;
                    contestantHobbiesInput.Text = c.Hobbies;
                    contestantLanguagesInput.Text = c.Languages;
                    contestantWorkExperiencesInput.Text = c.WorkExperiences;
                    contestantEducationInput.Text = c.Education;
                    contestantMottoInput.Text = c.Motto;
                }
                else
                {
                    contestantDataLayout.Hide();
                    SetupPictureBox(ApplicationConfiguration.DefaultUserProfileImagePath);
                    contestantFullNameInput.Text = "";
                    contestantEmailInput.Text = "";
                    contestantPhoneNumberInput.Text = "";
                    contestantHomeAddressInput.Text = "";
                    contestantBirthDateInput.Text = "";
                    contestantAssumedAge.Text = "0";
                    gendersLayout.Value = "Rather Not Say";
                    maritalStatusLayout.Value = "Rather Not Say";
                    contestantHeightInCentimetersInput.Value = 0;
                    contestantWeightInKilogramsInput.Value = 0;
                    contestantTalentsAndSkillsInput.Text = "";
                    contestantHobbiesInput.Text = "";
                    contestantLanguagesInput.Text = "";
                    contestantWorkExperiencesInput.Text = "";
                    contestantEducationInput.Text = "";
                    contestantMottoInput.Text = "";
                }
                e.Handled = true;
            }

            if (e.KeyData == Keys.Escape)
            {
                ApplicationFormNavigator.DisplayPreviousForm();
                e.Handled = true;
            }
        }

        private void ContestantLayoutItem_SingleClick(object sender, EventArgs e)
        {
            OrderedValueItem b = (OrderedValueItem)sender;
            if (b.Features.IsToggled)
            {
                contestantDataLayout.Hide();
            }
            else
            {
                contestantDataLayout.Show();
            }

            if (contestantsLayout.SelectedItem != null)
            {
                OrderedValueItem orderedValueItem = contestantsLayout.SelectedItem;
                ContestantEntity contestant = EditEventCache.ContestantEntities.Items[EditEventCache.ContestantEntities.ItemCount - Convert.ToInt32(orderedValueItem.OrderedNumber)];
                contestant.ImageResourcePath = FilePathParser.Standardize(contestantImageFileInput.FileName);
                contestant.FullName = contestantFullNameInput.Text;
                orderedValueItem.Value = contestantFullNameInput.Text;
                contestant.Email = contestantEmailInput.Text;
                contestant.PhoneNumber = contestantPhoneNumberInput.Text;
                contestant.HomeAddress = contestantHomeAddressInput.Text;
                contestant.BirthDate = Convert.ToString(contestantBirthDateInput.Value);
                contestant.GenderType = gendersLayout.Value;
                contestant.MaritalStatusType = maritalStatusLayout.Value;
                contestant.HeightInCentimeters = (float) contestantHeightInCentimetersInput.Value;
                contestant.WeightInKilograms = (float) contestantWeightInKilogramsInput.Value;
                contestant.TalentsAndSkills = contestantTalentsAndSkillsInput.Text;
                contestant.Hobbies = contestantHobbiesInput.Text;
                contestant.Languages = contestantLanguagesInput.Text;
                contestant.WorkExperiences = contestantWorkExperiencesInput.Text;
                contestant.Education = contestantEducationInput.Text;
                contestant.Motto = contestantMottoInput.Text;
            }

            OrderedValueItem a = (OrderedValueItem) sender;
            ContestantEntity c = EditEventCache.ContestantEntities.Items[EditEventCache.ContestantEntities.ItemCount - Convert.ToInt32(a.OrderedNumber)];
            contestantImageFileInput.FileName = c.ImageResourcePath;
            SetupPictureBox(c.ImageResourcePath);
            contestantFullNameInput.Text = c.FullName;
            contestantEmailInput.Text = c.Email;
            contestantPhoneNumberInput.Text = c.PhoneNumber;
            contestantHomeAddressInput.Text = c.HomeAddress;
            contestantBirthDateInput.Value = DateTime.Parse(c.BirthDate);
            contestantAssumedAge.Text = $"{DateParser.CalculateAge(c.BirthDate)}";
            gendersLayout.Value = c.GenderType;
            maritalStatusLayout.Value = c.MaritalStatusType;
            contestantHeightInCentimetersInput.Value = (decimal)c.HeightInCentimeters;
            contestantWeightInKilogramsInput.Value = (decimal)c.WeightInKilograms;
            contestantTalentsAndSkillsInput.Text = c.TalentsAndSkills;
            contestantHobbiesInput.Text = c.Hobbies;
            contestantLanguagesInput.Text = c.Languages;
            contestantWorkExperiencesInput.Text = c.WorkExperiences;
            contestantEducationInput.Text = c.Education;
            contestantMottoInput.Text = c.Motto;
        }

        private void OpenFileDial_FileOk(object sender, CancelEventArgs e)
        {
            if (sender == contestantImageFileInput)
            {
                SetupPictureBox(contestantImageFileInput.FileName);
            }
        }

        public void Render()
        {
            contestantsLayout.Clear();
            contestantsCountLabel.Text = $"{EditEventCache.ContestantEntities.Items.Count}";
            for (int orderdNumber = EditEventCache.ContestantEntities.Items.Count; orderdNumber > 0; orderdNumber--)
            {
                ContestantEntity contestantEntity = EditEventCache.ContestantEntities.Items[EditEventCache.ContestantEntities.Items.Count - orderdNumber];
                contestantsLayout.Render($"{orderdNumber}", contestantEntity.FullName, contestantEntity);
            }
            contestantDataLayout.Hide();
            SetupPictureBox(ApplicationConfiguration.DefaultUserProfileImagePath);
            contestantFullNameInput.Text = "";
            contestantEmailInput.Text = "";
            contestantPhoneNumberInput.Text = "";
            contestantHomeAddressInput.Text = "";
            contestantBirthDateInput.Text = "";
            contestantAssumedAge.Text = "0";
            gendersLayout.Value = "Rather Not Say";
            maritalStatusLayout.Value = "Rather Not Say";
            contestantHeightInCentimetersInput.Value = 0;
            contestantWeightInKilogramsInput.Value = 0;
            contestantTalentsAndSkillsInput.Text = "";
            contestantHobbiesInput.Text = "";
            contestantLanguagesInput.Text = "";
            contestantWorkExperiencesInput.Text = "";
            contestantEducationInput.Text = "";
            contestantMottoInput.Text = "";
        }

        private void SetupPictureBox(string fileName)
        {
            contestantImageInput.SizeMode = PictureBoxSizeMode.Zoom;
            contestantImageInput.ClientSize = new Size(
                contestantImageInput.Size.Width,
                contestantImageInput.Size.Height);
            contestantImageInput.Image = ApplicationResourceLoader.SafeLoadResource(
                FilePathParser.Standardize(fileName));
        }
    }
}
