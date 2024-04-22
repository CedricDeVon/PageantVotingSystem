
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.FormStyles;
using PageantVotingSystem.Sources.FormControls;
using PageantVotingSystem.Sources.Miscellaneous;
using PageantVotingSystem.Sources.FormNavigators;
using PageantVotingSystem.Sources.ResourceLoaders;
using System.Drawing;

namespace PageantVotingSystem.Sources.Forms
{
    public partial class EditEventContestants : Form
    {
        private readonly InformationLayout informationBox;

        private readonly TopSideNavigationLayout topSideNavigationLayout;
        
        private readonly OrderedValueItemLayout contestantsLayout;

        private readonly RadioButtonLayout gendersLayout;

        private readonly RadioButtonLayout maritalStatusLayout;

        public EditEventContestants()
        {
            InitializeComponent();

            ApplicationFormStyle.SetupFormStyles(this);
            informationBox = new InformationLayout(informationLayoutControl);
            topSideNavigationLayout = new TopSideNavigationLayout(topSideNavigationLayoutControl);
            topSideNavigationLayout.HideReloadButton();
            contestantsLayout = new OrderedValueItemLayout(contestantsLayoutControl, ContestantLayoutItem_SingleClick);
            contestantsLayout.RenderOrdered(EditEventCache.ContestantFullNames);
            contestantsCountLabel.Text = $"{EditEventCache.Contestants.ItemCount}";
            gendersLayout = new RadioButtonLayout(gendersLayoutControl, GenderCache.Types);
            maritalStatusLayout = new RadioButtonLayout(maritalStatusLayoutControl, MaritalStatusCache.Types);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            informationBox.StartLoadingMessageDisplay();

            if (sender == saveButton)
            {
                ApplicationFormNavigator.DisplayPrevious();
                contestantsLayout.Unfocus();
            }
            else if (sender == addContestantButton)
            {
                string contestantName = $"Contestant";
                ContestantEntity contestantEntity = new ContestantEntity();
                contestantEntity.FullName = contestantName;
                EditEventCache.Contestants.AddNewItem(contestantEntity);
                contestantsLayout.RenderOrdered(contestantName);
                contestantsCountLabel.Text = $"{EditEventCache.Contestants.ItemCount}";
            }
            else if (sender == resetButton)
            {
                EditEventCache.Contestants.ClearAllItems();
                contestantsLayout.Clear();
                contestantsLayout.Unfocus();
                contestantsCountLabel.Text = "0";
                contestantDataLayout.Hide();
            }
            else if (sender == contestantImageInput)
            {
                contestantImageFileInput.ShowDialog(this);
            }

            informationBox.StopLoadingMessageDisplay();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (contestantsLayout.SelectedItem != null && e.KeyCode == Keys.W)
            {
                OrderedValueItem selectedItem = contestantsLayout.SelectedItem;
                EditEventCache.Contestants.MoveItemAtIndexUpwards(EditEventCache.Contestants.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                contestantsLayout.MoveSelectedUpwards();
                e.Handled = true;
            }
            else if (contestantsLayout.SelectedItem != null && e.KeyCode == Keys.S)
            {
                OrderedValueItem selectedItem = contestantsLayout.SelectedItem;
                EditEventCache.Contestants.MoveItemAtIndexDownwards(EditEventCache.Contestants.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                contestantsLayout.MoveSelectedDownwards();
                e.Handled = true;
            }
            else if (contestantsLayout.SelectedItem != null && e.KeyCode == Keys.Delete)
            {
                OrderedValueItem selectedItem = contestantsLayout.SelectedItem;
                EditEventCache.Contestants.RemoveItemAtIndex(EditEventCache.Contestants.ItemCount - Convert.ToInt32(selectedItem.OrderedNumber));

                contestantsLayout.RemoveSelected();

                contestantsCountLabel.Text = $"{EditEventCache.Contestants.Items.Count}";
                OrderedValueItem a = contestantsLayout.SelectedItem;
                ContestantEntity c = EditEventCache.Contestants.Items[EditEventCache.Contestants.ItemCount - Convert.ToInt32(a.OrderedNumber)];
                contestantImageFileInput.FileName = c.ImageResourcePath;
                SetupPictureBox(c.ImageResourcePath);
                contestantFullNameInput.Text = c.FullName;
                contestantEmailInput.Text = c.Email;
                contestantPhoneNumberInput.Text = c.PhoneNumber;
                contestantHomeAddressInput.Text = c.HomeAddress;
                contestantBirthDateInput.Text = c.BirthDate;
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
                e.Handled = true;
            }
        }

        private void ContestantLayoutItem_SingleClick(object sender, EventArgs e)
        {
            OrderedValueItem b = (OrderedValueItem)sender;
            if (b.Features.IsToggled)
            {
                contestantDataLayout.Hide();
                return;
            }
            else
            {
                contestantDataLayout.Show();
            }

            if (contestantsLayout.SelectedItem != null)
            {
                OrderedValueItem orderedValueItem = contestantsLayout.SelectedItem;
                ContestantEntity contestant = EditEventCache.Contestants.Items[EditEventCache.Contestants.ItemCount - Convert.ToInt32(orderedValueItem.OrderedNumber)];
                contestant.ImageResourcePath = StringParser.StandardizeFilePath(contestantImageFileInput.FileName);
                contestant.FullName = contestantFullNameInput.Text;
                orderedValueItem.Value = contestantFullNameInput.Text;
                contestant.Email = contestantEmailInput.Text;
                contestant.PhoneNumber = contestantPhoneNumberInput.Text;
                contestant.HomeAddress = contestantHomeAddressInput.Text;
                contestant.BirthDate = contestantBirthDateInput.Text;
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
            ContestantEntity c = EditEventCache.Contestants.Items[EditEventCache.Contestants.ItemCount - Convert.ToInt32(a.OrderedNumber)];
            contestantImageFileInput.FileName = c.ImageResourcePath;
            SetupPictureBox(c.ImageResourcePath);
            contestantFullNameInput.Text = c.FullName;
            contestantEmailInput.Text = c.Email;
            contestantPhoneNumberInput.Text = c.PhoneNumber;
            contestantHomeAddressInput.Text = c.HomeAddress;
            contestantBirthDateInput.Text = c.BirthDate;
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

        private void SetupPictureBox(string fileName)
        {
            contestantImageInput.SizeMode = PictureBoxSizeMode.Zoom;
            contestantImageInput.ClientSize = new Size(
                contestantImageInput.Size.Width,
                contestantImageInput.Size.Height);
            contestantImageInput.Image = ApplicationResourceLoader.SafeLoadResource(
                StringParser.StandardizeFilePath(fileName));
        }
    }
}


// MessageBox.Show($"{((OrderedValueItem) sender).OrderedNumber} - {((OrderedValueItem)sender).Value}");
//OrderedValueItem orderedValueItem = (OrderedValueItem)sender;
//ContestantEntity contestant = EditEventCache.SelectedContestants.Values[EditEventCache.SelectedContestants.Count - Convert.ToInt32(orderedValueItem.OrderedNumber)];
//MessageBox.Show($"{contestantsLayout.CurrentSelectedItem?.Value} {((OrderedValueItem)sender).Value}");
//OrderedValueItem orderedValueItem = (OrderedValueItem) sender;
//ContestantEntity contestant = EditEventCache.SelectedContestants.Values[EditEventCache.SelectedContestants.Count - Convert.ToInt32(orderedValueItem.OrderedNumber)];
//contestantFullNameInput.Text = contestant.FullName;
//contestantEmailInput.Text = contestant.Email;

//contestantPhoneNumberInput.Text = contestant.PhoneNumber;
//contestantHomeAddressInput.Text = contestant.HomeAddress;
//contestantBirthDateInput.Value = contestant.BirthDate;
//gendersLayout.Value = contestant.GenderType;
//maritalStatusLayout.Value = contestant.MaritalStatusType;
//contestantHeightInCentimetersInput.Value = (decimal)contestant.HeightInCentimeters;
//contestantWeightInKilogramsInput.Value = (decimal)contestant.WeightInKilograms;
//contestantTalentsAndSkillsInput.Text = contestant.TalentsAndSkills;
//contestantHobbiesInput.Text = contestant.Hobbies;
//contestantLanguagesInput.Text = contestant.Languages;
//contestantWorkExperiencesInput.Text = contestant.JobExperiences;
//contestantEducationInput.Text = contestant.Education;
//contestantMottoInput.Text = contestant.Motto;

//contestant.PhoneNumber = contestantPhoneNumberInput.Text;
//contestant.HomeAddress = contestantHomeAddressInput.Text;
//contestant.BirthDate = contestantBirthDateInput.Value;
//contestant.GenderType = gendersLayout.Value;
//contestant.MaritalStatusType = maritalStatusLayout.Value;
//contestant.HeightInCentimeters = (float)contestantHeightInCentimetersInput.Value;
//contestant.WeightInKilograms = (float)contestantWeightInKilogramsInput.Value;
//contestant.TalentsAndSkills = contestantTalentsAndSkillsInput.Text;
//contestant.Hobbies = contestantHobbiesInput.Text;
//contestant.Languages = contestantLanguagesInput.Text;
//contestant.JobExperiences = contestantWorkExperiencesInput.Text;
//contestant.Education = contestantEducationInput.Text;
//contestant.Motto = contestantMottoInput.Text;