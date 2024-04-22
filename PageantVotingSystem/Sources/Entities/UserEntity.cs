
using PageantVotingSystem.Sources.Configurations;

namespace PageantVotingSystem.Sources.Entities
{
    public class UserEntity : Entity
    {
        public string Email { get; set; }
        
        public string FullName { get; set; }

        public string UserRoleType { get; set; }

        public string Description { get; set; }
        
        public string ImageResourcePath { get; set; }
        
        public string Password { get; set; }

        public UserEntity()
        {
            SetAllAttributesToDefault();
        }

        public UserEntity(
            string email)
        {
            SetAllAttributes(ApplicationConfiguration.DefaultUserProfileImagePath, email);
        }

        public UserEntity(
            string email,
            string fullName)
        {
            SetAllAttributes(ApplicationConfiguration.DefaultUserProfileImagePath, email, fullName);
        }

        public UserEntity(
            string email,
            string fullName,
            string userRoleType)
        {
            SetAllAttributes(ApplicationConfiguration.DefaultUserProfileImagePath, email, fullName, userRoleType);
        }

        public UserEntity(
            string email,
            string fullName,
            string userRoleType,
            string password)
        {
            SetAllAttributes(ApplicationConfiguration.DefaultUserProfileImagePath, email, fullName, userRoleType, password);
        }

        public UserEntity(
            string email,
            string fullName,
            string userRoleType,
            string description,
            string imageResourcePath)
        {
            SetAllAttributes(imageResourcePath, email, fullName, userRoleType, "", description);
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
            string email = "",
            string fullName = "",
            string userRoleType = "",
            string password = "",
            string description = "")
        {
            Email = email;
            FullName = fullName;
            UserRoleType = userRoleType;
            Password = password;
            Description = description;
            ImageResourcePath = imageResourcePath;
        }
    }
}
