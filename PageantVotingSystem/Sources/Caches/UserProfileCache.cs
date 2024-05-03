
using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Loggers;
using PageantVotingSystem.Sources.Entities;

namespace PageantVotingSystem.Sources.Caches
{
    public class UserProfileCache : Cache
    {
        public new static UserEntity Data { get; private set; }

        public static void Setup()
        {
            SetupRecorder.ThrowIfAlreadySetup("UserInformationCache");
            ApplicationLogger.LogInformationMessage("'UserInformationCache' setup began");

            Data = new UserEntity();

            SetupRecorder.Add("UserInformationCache");
            ApplicationLogger.LogInformationMessage("'UserInformationCache' setup complete");
        }

        public static void Update(UserEntity entity)
        {
            Data.Email = entity.Email;
            Data.FullName = entity.FullName;
            Data.UserRoleType = entity.UserRoleType;
            Data.Description = entity.Description;
            Data.ImageResourcePath = entity.ImageResourcePath;
        }

        public static void Update(Result result)
        {
            Data.Email = result.GetData<string>("email");
            Data.FullName = result.GetData<string>("full_name");
            Data.UserRoleType = result.GetData<string>("user_role_type");
            Data.Description = $"{result.GetData<object>("description")}";
            Data.ImageResourcePath = $"{result.GetData<object>("image_resource_path")}";
        }

        public static void Clear()
        {
            Data.Email = "";
            Data.FullName = "";
            Data.UserRoleType = "";
            Data.Description = "";
            Data.ImageResourcePath = "";
        }
    }
}
