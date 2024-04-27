
using System;

using PageantVotingSystem.Sources.Configurations;
using PageantVotingSystem.Sources.Miscellaneous;

namespace PageantVotingSystem.Sources.Entities
{
    public class ContestantEntity : Entity
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        
        public int OrderNumber { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string BirthDate { get; set; }

        public float HeightInCentimeters { get; set; }

        public float WeightInKilograms { get; set; }

        public string Motto { get; set; }

        public string HomeAddress { get; set; }

        public string TalentsAndSkills { get; set; }

        public string Hobbies { get; set; }

        public string Languages { get; set; }

        public string WorkExperiences { get; set; }

        public string Education { get; set; }

        public string ContestantStatusType { get; set; }

        public string MaritalStatusType { get; set; }

        public string GenderType { get; set; }

        public string ImageResourcePath { get; set; }

        public ContestantEntity()
        {
            SetAllAttributesToDefault();
            BirthDate = DateParser.ShortenDate(DateTime.Now);
        }

        public ContestantEntity(int id, string full_name, int orderNumber)
        {
            SetAllAttributesToDefault();
            Id = id;
            FullName = full_name;
            OrderNumber = orderNumber;
            BirthDate = DateParser.ShortenDate(DateTime.Now);
        }

        public override void ClearAllAttributes()
        {
            SetAllAttributesToDefault();
        }

        private void SetAllAttributesToDefault()
        {
            SetAllAttributes(ApplicationConfiguration.DefaultUserProfileImagePath);
        }

        private void SetAllAttributes(
            string imageResourcePath,
            string birthDate = "",
            int id = 0,
            string fullName = "",
            int orderNumber = 0,
            string email = "",
            string phoneNumber = "",
            int heightInCentimeters = 0,
            int weightInKilograms = 0,
            string motto = "",
            string homeAddress = "",
            string talentsAndSkills = "",
            string hobbies = "",
            string languages = "",
            string workExperiences = "",
            string education = "",
            string contestantStatusType = "",
            string maritalStatusType = "",
            string genderType = "")
        {
            Id = id;
            FullName = fullName;
            OrderNumber = orderNumber;
            Email = email;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            HeightInCentimeters = heightInCentimeters;
            WeightInKilograms = weightInKilograms;
            Motto = motto;
            HomeAddress = homeAddress;
            TalentsAndSkills = talentsAndSkills;
            Hobbies = hobbies;
            Languages = languages;
            WorkExperiences = workExperiences;
            Education = education;
            ContestantStatusType = contestantStatusType;
            MaritalStatusType = maritalStatusType;
            GenderType = genderType;
            ImageResourcePath = imageResourcePath;
        }
    }
}
