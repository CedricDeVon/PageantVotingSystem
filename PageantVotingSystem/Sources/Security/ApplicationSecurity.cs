
using PageantVotingSystem.Sources.Results;

namespace PageantVotingSystem.Sources.Security
{
    public class ApplicationSecurity
    {
        public static Result AuthenticateOldUser(string email, string password)
        {
            Result result = ApplicationValidator.ValidateEmail(email);
            if (!result.IsSuccessful)
            {
                return result;
            }
            result = ApplicationValidator.ValidateNewPassword(password);
            if (!result.IsSuccessful)
            {
                return result;
            }
            return ApplicationValidator.ValidateUser(email, password);
        }

        public static Result AuthenticateNewUser(string email, string fullName, string password, string passwordConfirmation, string roleType)
        {
            Result result = ApplicationValidator.ValidateEmail(email);
            if (!result.IsSuccessful)
            {
                return result;
            }
            result = ApplicationValidator.ValidateFullName(fullName);
            if (!result.IsSuccessful)
            {
                return result;
            }
            result = ApplicationValidator.ValidateNewPassword(password);
            if (!result.IsSuccessful)
            {
                return result;
            }
            result = ApplicationValidator.ValidatePasswordConfirmation(password, passwordConfirmation);
            if (!result.IsSuccessful)
            {
                return result;
            }
            result = ApplicationValidator.ValidateRoleType(roleType);
            if (!result.IsSuccessful)
            {
                return result;
            }
            result = ApplicationValidator.ValidateUserUniqueness(email);
            if (!result.IsSuccessful)
            {
                return result;
            }
            return new ResultSuccess();
        }
    }
}
