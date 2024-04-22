
using System.Collections.Generic;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Databases;

namespace PageantVotingSystem.Sources.Security
{
    public class ApplicationValidator
    {
        public static Result ValidatePercentageWeight(int value)
        {
            if (value < TypeConstraintCache.MinimumPercentageWeight)
            {
                return new ResultFailed($"'Percentage Weight' must be greater than {TypeConstraintCache.MinimumPercentageWeight}");
            }
            else if (value > TypeConstraintCache.MaximumPercentageWeight)
            {
                return new ResultFailed($"'Percentage Weight' must be less than or equal to {TypeConstraintCache.MaximumPercentageWeight}");
            }
            return new ResultSuccess();
        }

        public static Result ValidateOrderNumber(int value)
        {
            if (value < TypeConstraintCache.MinimumOrderNumber)
            {
                return new ResultFailed($"'Order Number' must be greater than or equal to {TypeConstraintCache.MinimumOrderNumber}");
            }
            else if (value > TypeConstraintCache.MaximumOrderNumber)
            {
                return new ResultFailed($"'Order Number' must be less than or equal to {TypeConstraintCache.MaximumOrderNumber}");
            }
            return new ResultSuccess();
        }

        public static Result ValidateHeightInCentimeters(int value)
        {
            if (value < TypeConstraintCache.MinimumHeightInCentimeters)
            {
                return new ResultFailed($"'Height In Centimeters' must be greater than or equal to {TypeConstraintCache.MinimumHeightInCentimeters}");
            }
            else if (value > TypeConstraintCache.MaximumHeightInCentimeters)
            {
                return new ResultFailed($"'Height In Centimeters' must be less than or equal to {TypeConstraintCache.MaximumHeightInCentimeters}");
            }
            return new ResultSuccess();
        }

        public static Result ValidateText(string value)
        {
            if (value.Length < TypeConstraintCache.MinimumTextCharacterLength)
            {
                return new ResultFailed($"'Text' must be greater than or equal to {TypeConstraintCache.MinimumTextCharacterLength}");
            }
            else if (value.Length > TypeConstraintCache.MaximumTextCharacterLength)
            {
                return new ResultFailed($"'Text' must be less than or equal to {TypeConstraintCache.MaximumTextCharacterLength}");
            }
            return new ResultSuccess();
        }

        public static Result ValidateWeightInKilograms(int value)
        {
            if (value < TypeConstraintCache.MinimumWeightInKilograms)
            {
                return new ResultFailed($"'Weight In Kilograms' must be greater than or equal to {TypeConstraintCache.MinimumWeightInKilograms}");
            }
            else if (value > TypeConstraintCache.MaximumWeightInKilograms)
            {
                return new ResultFailed($"'Weight In Kilograms' must be less than or equal to {TypeConstraintCache.MaximumWeightInKilograms}");
            }
            return new ResultSuccess();
        }

        public static Result ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new ResultFailed("'Email' must not be empty");
            }
            else if (email.Length < TypeConstraintCache.MinimumEmailCharacterLength)
            {
                return new ResultFailed($"'Email' length must be greater than or equal to '{TypeConstraintCache.MinimumEmailCharacterLength}'");
            }
            else if (email.Length > TypeConstraintCache.MaximumEmailCharacterLength)
            {
                return new ResultFailed($"'Email' length must be less than or equal to '{TypeConstraintCache.MaximumEmailCharacterLength}'");
            }
            bool isDotFound = false;
            bool isNumberSignFound = false;
            bool isDotFoundAfterNumberSignFound = false;
            for (int index = 0; index < email.Length; index++)
            {
                char character = email[index];
                if (character != '@' && character != '.')
                {
                    continue;
                }
                if ((character == '@' &&
                        (isNumberSignFound ||
                        index == 0 ||
                        index == email.Length - 1)) ||
                    character == '.' &&
                        (index == 0 || index == email.Length - 1 ||
                        (index < email.Length - 1 && email[index + 1] == '@') ||
                        email[index - 1] == '@'))
                {
                    return new ResultFailed("'Email' structure is invalid");
                }
                if (!isNumberSignFound && character == '@')
                {
                    isNumberSignFound = true;
                }
                else if (!isDotFound && character == '.')
                {
                    isDotFound = true;
                }
                if (character == '.' && isNumberSignFound)
                {
                    isDotFoundAfterNumberSignFound = true;
                }
            }
            if (!isNumberSignFound || !isDotFound || !isDotFoundAfterNumberSignFound)
            {
                return new ResultFailed("'Email' structure is invalid");
            }
            return new ResultSuccess();
        }

        public static Result ValidateFullName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return new ResultFailed("'Full Name' must not be empty");
            }
            else if (fullName.Length < TypeConstraintCache.MinimumPersonNameCharacterLength)
            {
                return new ResultFailed($"'Full Name' length must be greater than or equal to '{TypeConstraintCache.MinimumPersonNameCharacterLength}'");
            }
            else if (fullName.Length > TypeConstraintCache.MaximumPersonNameCharacterLength)
            {
                return new ResultFailed($"'Full Name' length must be less than or equal to '{TypeConstraintCache.MaximumPersonNameCharacterLength}'");
            }
            foreach (char character in fullName)
            {
                if (TypeConstraintCache.IsInvalidPersonNameCharacter(character))
                {
                    return new ResultFailed($"'Full Name' must not contain this character: ' {character} '");
                }
            }
            return new ResultSuccess();
        }

        public static Result ValidateNewPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return new ResultFailed("'Password' must not be empty");
            }
            else if (password.Length < TypeConstraintCache.MinimumPasswordCharacterLength)
            {
                return new ResultFailed($"'Password' length must be greater than or equal to {TypeConstraintCache.MinimumPasswordCharacterLength}");
            }
            else if (password.Length > TypeConstraintCache.MaximumPasswordCharacterLength)
            {
                return new ResultFailed($"'Password' length must be less than or equal to {TypeConstraintCache.MaximumPasswordCharacterLength}");
            }
            bool foundSpecialCharacter = false;
            bool foundAlphaNumericCharacter = false;
            foreach (char character in password)
            {
                if (TypeConstraintCache.IsInvalidPasswordCharacter(character))
                {
                    return new ResultFailed($"'Password' must not contain this character: ' {character} '");
                }
                if (!foundAlphaNumericCharacter && IsAlphaNumericCharacter(character)) //
                {
                    foundAlphaNumericCharacter = true;
                }
                else if (!foundSpecialCharacter && IsSpecialCharacter(character))
                {
                    foundSpecialCharacter = true;
                }
            }
            if (!foundSpecialCharacter)
            {
                return new ResultFailed("'Password' must contain at least 1 special character");
            }
            else if (!foundAlphaNumericCharacter)
            {
                return new ResultFailed("'Password' must contain at least 1 number");
            }
            return new ResultSuccess();
        }

        public static Result ValidatePasswordConfirmation(string password, string passwordConfirmation)
        {
            if (password != passwordConfirmation)
            {
                return new ResultFailed("'Password' does not match");
            }

            return new ResultSuccess();
        }

        public static Result ValidateUserPassword(string plainPassword, string hashedPassword)
        {
            if (ApplicationCryptographer.IsPasswordInvalidViaMethod1(plainPassword, hashedPassword))
            {
                return new ResultFailed("'Password' does not match");
            }

            return new ResultSuccess();
        }
        
        public static Result ValidateRoleType(string userRoleType)
        {
            if (string.IsNullOrEmpty(userRoleType))
            {
                return new ResultFailed($"'User Role' must not be empty");
            }
            else if (UserRoleCache.IsNotFound(userRoleType))
            {
                return new ResultFailed($"'User Role' of type '{userRoleType}' is invalid");
            }

            return new ResultSuccess();
        }

        public static Result ValidateUser(string email, string password)
        {
            Dictionary<object, object> entity = ApplicationDatabase.ReadOneUser(email);
            if (entity == null)
            {
                return new ResultFailed("'Email' does not exist");
            }
            Result result = ValidateUserPassword(password, (string) entity["password"]);
            if (!result.IsSuccessful)
            {
                return new ResultFailed("'Password' does not match");
            }
            
            return new ResultSuccess(entity);
        }

        public static Result ValidateUserUniqueness(string email)
        {
            if (ApplicationDatabase.IsUserEmailFound(email))
            {
                return new ResultFailed("'Email' already exist");
            }

            return new ResultSuccess();
        }

        public static Result ValidateUserExistence(string email)
        {
            if (ApplicationDatabase.IsUserEmailNotFound(email))
            {
                return new ResultFailed("'Email' does not exist");
            }

            return new ResultSuccess();
        }

        private static bool IsSpecialCharacter(char character)
        {
            return !IsAlphaNumericCharacter(character) && !IsLowerCaseAlphabeticalCharacter(character) && !IsUpperCaseAlphabeticalCharacter(character);
        }

        private static bool IsAlphaNumericCharacter(char character)
        {
            return 47 < character && character < 58;
        }

        private static bool IsLowerCaseAlphabeticalCharacter(char character)
        {
            return 96 < character && character < 123;
        }

        private static bool IsUpperCaseAlphabeticalCharacter(char character)
        {
            return 64 < character && character < 91;
        }
    }
}
