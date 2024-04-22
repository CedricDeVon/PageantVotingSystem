using System;

using PageantVotingSystem.Source.Caches;
using PageantVotingSystem.Source.Databases;
using PageantVotingSystem.Source.Utilities;

namespace PageantVotingSystem.Source.Security
{
    public class ApplicationSecurity
    {
        private static bool isLoaded;

        public static void Setup()
        {
            if (isLoaded)
            {
                throw new Exception("'ApplicationSecurity' is already loaded");
            }

            isLoaded = true;
        }

        public static Result AuthenticateOldUser(string email, string password)
        {
            Result securityResult = ValidateEmailInput(email);
            if (!securityResult.IsSuccessful)
            {
                return securityResult;
            }
            securityResult = ValidatePasswordInput(password);
            if (!securityResult.IsSuccessful)
            {
                return securityResult;
            }
            Result databaseOutput = ApplicationDatabase.ReadOneUser(email);
            securityResult = ValidateUserExistence(databaseOutput);
            if (!securityResult.IsSuccessful)
            {
                return securityResult;
            }
            securityResult = ValidateUserPassword(password, databaseOutput.Value<string>("password"));
            if (!securityResult.IsSuccessful)
            {
                return securityResult;
            }
            return Result.Success(databaseOutput.Data);
        }

        public static Result AuthenticateNewUser(string email, string fullName, string password, string passwordConfirmation, string roleType)
        {
            Result securityResult = ValidateEmailInput(email);
            if (!securityResult.IsSuccessful)
            {
                return securityResult;
            }
            securityResult = ValidateFullNameInput(fullName);
            if (!securityResult.IsSuccessful)
            {
                return securityResult;
            }
            securityResult = ValidatePasswordInput(password);
            if (!securityResult.IsSuccessful)
            {
                return securityResult;
            }
            securityResult = ValidatePasswordConfirmationInput(password, passwordConfirmation);
            if (!securityResult.IsSuccessful)
            {
                return securityResult;
            }
            securityResult = ValidateRoleTypeInput(roleType);
            if (!securityResult.IsSuccessful)
            {
                return securityResult;
            }
            securityResult = ValidateUserUniqueness(email);
            if (!securityResult.IsSuccessful)
            {
                return securityResult;
            }
            return Result.Success();
        }

        private static Result ValidateEmailInput(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Result.Failure("'Email' must not be empty");
            }
            else if (email.Length > ApplicationCache.Get<int>("EmailMaximumLength"))
            {
                return Result.Failure("'Email' length must be less than or equal to 32");
            }
            bool isNumberSignFound = false;
            bool isDotFound = false;
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
                    return Result.Failure("'Email' structure is invalid");
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
                return Result.Failure("'Email' structure is invalid");
            }
            return Result.Success();
        }

        private static Result ValidateFullNameInput(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return Result.Failure("'Full Name' must not be empty");
            }
            else if (fullName.Length > ApplicationCache.Get<int>("FullNameMaximumLength"))
            {
                return Result.Failure("'Full Name' length must be less than or equal to 128");
            }
            foreach (char character in fullName)
            {
                if (IsInvalidNameCharacter(character))
                {
                    return Result.Failure($"'Full Name' must not contain this character: '{character}'");
                }
                else if (StringUtility.IsAlphaNumericCharacter(character))
                {
                    return Result.Failure($"'Full Name' must not contain a number");
                }
            }
            return Result.Success();
        }

        private static Result ValidatePasswordInput(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return Result.Failure("'Password' must not be empty");
            }
            else if (password.Length < ApplicationCache.Get<int>("PasswordMinimumLength"))
            {
                return Result.Failure("'Password' length must be greater than or equal to 8");
            }
            else if (password.Length > ApplicationCache.Get<int>("PasswordMaximumLength"))
            {
                return Result.Failure("'Password' length must be less than or equal to 32");
            }
            bool foundSpecialCharacter = false;
            bool foundAlphaNumericCharacter = false;
            foreach (char character in password)
            {
                if (StringUtility.IsInvalidSpecialCharacter(character))
                {
                    return Result.Failure($"'Password' must not contain this character: {character}");
                }
                if (!foundAlphaNumericCharacter && StringUtility.IsAlphaNumericCharacter(character))
                {
                    foundAlphaNumericCharacter = true;
                }
                else if (!foundSpecialCharacter && StringUtility.IsSpecialCharacter(character))
                {
                    foundSpecialCharacter = true;
                }
            }
            if (!foundSpecialCharacter)
            {
                return Result.Failure("'Password' must contain at least 1 special character");
            }
            else if (!foundAlphaNumericCharacter)
            {
                return Result.Failure("'Password' must contain at least 1 number");
            }
            return Result.Success();
        }

        private static Result ValidatePasswordConfirmationInput(string password, string passwordConfirmation)
        {
            if (password != passwordConfirmation)
            {
                return Result.Failure("'Password' does not match");
            }

            return Result.Success();
        }

        private static Result ValidateUserPassword(string plainPassword, string hashedPassword)
        {
            if (!ApplicationCryptographer.ValidatePassword(plainPassword, hashedPassword))
            {
                return Result.Failure("'Password' does not match");
            }

            return Result.Success();
        }

        private static Result ValidateRoleTypeInput(string userRoleType)
        {
            if (!ApplicationCache.isUserRoleTypeFound(userRoleType))
            {
                return Result.Failure("'User Role' does not exist");
            }

            return Result.Success();
        }

        private static Result ValidateUserUniqueness(string email)
        {
            Result databaseResult = ApplicationDatabase.ReadOneUserEmail(email);
            if (!databaseResult.IsSuccessful)
            {
                return Result.Failure(databaseResult.ExceptionMessage);
            }
            else if (databaseResult.Data.Count != 0)
            {
                return Result.Failure("'User Name' already exists");
            }
            return Result.Success();
        }

        private static Result ValidateUserExistence(Result databaseResult)
        {
            if (!databaseResult.IsSuccessful)
            {
                return Result.Failure(databaseResult.ExceptionMessage);
            }
            else if (databaseResult.Data.Count == 0)
            {
                return Result.Failure("'User Name' does not exist");
            }
            return Result.Success();
        }

        private static bool IsInvalidNameCharacter(char character)
        {
            return character != ' ' && StringUtility.IsSpecialCharacter(character);
        }
    }
}
