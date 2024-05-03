
using System;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Generics;
using PageantVotingSystem.Sources.Databases;
using PageantVotingSystem.Sources.Miscellaneous;
using PageantVotingSystem.Sources.ResourceLoaders;

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
            Dictionary<object, object> userEntity = ApplicationDatabase.ReadOneRawUserResult(email);
            if (userEntity == null)
            {
                return new ResultFailed("'Email' does not exist");
            }
            Result result = ValidateUserPassword(password, (string) userEntity["password"]);
            if (!result.IsSuccessful)
            {
                return new ResultFailed("'Password' does not match");
            }

            return new ResultSuccess(userEntity);
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

        public static Result ValidateEventEntity(EventEntity eventEntity)
        {
            if (eventEntity == null)
            {
                return new ResultFailed($"An Event must exist");
            }
            else if (string.IsNullOrEmpty(eventEntity.Name))
            {
                return new ResultFailed($"Name of Event must not be blank");
            }
            else if (eventEntity.Name.Length > 128)
            {
                return new ResultFailed($"Name of Event '{eventEntity.Name}' must contain at most 128 characters");
            }
            else if (eventEntity.HostAddress.Length > 1024)
            {
                return new ResultFailed($"Host Address of Event '{eventEntity.Name}' must contain at most 128 characters");
            }
            else if (DateParser.IsInThePast(DateTime.Parse(eventEntity.DateTimeStart)))
            {
                return new ResultFailed($"Scheduled date of Event '{eventEntity.Name}' must be in the future");
            }
            else if (ScoringSystemCache.IsNotFound(eventEntity.ScoringSystemType))
            {
                return new ResultFailed($"Scoring system of Event '{eventEntity.Name}' must be filled");
            }
            else if (eventEntity.Description.Length > 1024)
            {
                return new ResultFailed($"Description of Event '{eventEntity.Name}'  must contain at most 1,024 characters");
            }

            return new ResultSuccess();
        }

        public static Result ValidateEventSegmentEntities(EventEntity eventEntity)
        {
            if (eventEntity == null)
            {
                return new ResultFailed($"An Event must exist");
            }

            GenericOrderedList<SegmentEntity> segmentEntities = eventEntity.Segments;
            if (segmentEntities.ItemCount == 0)
            {
                return new ResultFailed($"Event '{eventEntity.Name}' must contain at least 1 Segment");
            }
            else if (segmentEntities.ItemCount > 1000)
            {
                return new ResultFailed($"Event '{eventEntity.Name}' must contain at most 1,000 Segments");
            }

            HashSet<string> assumedSegments = new HashSet<string>();
            foreach (SegmentEntity segmentEntity in segmentEntities.Items)
            {
                if (segmentEntity == null)
                {
                    return new ResultFailed($"A Segment must exist");
                }
                else if (string.IsNullOrEmpty(segmentEntity.Name))
                {
                    return new ResultFailed($"Name of Segment from Event '{eventEntity.Name}' must not be blank");
                }
                else if (assumedSegments.Contains(segmentEntity.Name))
                {
                    return new ResultFailed($"Segment '{segmentEntity.Name}' from Event '{eventEntity.Name}' already exists");
                }
                Result result = ValidateEventRoundLayout(segmentEntity);
                if (!result.IsSuccessful)
                {
                    return result;
                }

                assumedSegments.Add(segmentEntity.Name);
            }

            return new ResultSuccess();
        }

        public static Result ValidateEventRoundLayout(SegmentEntity segmentEntity)
        {
            if (segmentEntity == null)
            {
                return new ResultFailed($"A Segment must exist");
            }

            GenericOrderedList<RoundEntity> roundEntities = segmentEntity.Rounds;
            if (roundEntities.ItemCount == 0)
            {
                return new ResultFailed($"Segment '{segmentEntity.Name}' must contain at least 1 Round");
            }
            else if (roundEntities.ItemCount > 1000)
            {
                return new ResultFailed($"Segment '{segmentEntity.Name}' must contain at most 1,000 Rounds");
            }

            HashSet<string> assumedRounds = new HashSet<string>();
            foreach (RoundEntity roundEntity in roundEntities.Items)
            {
                if (roundEntity == null)
                {
                    return new ResultFailed($"A Round must exist");
                }
                else if (string.IsNullOrEmpty(roundEntity.Name))
                {
                    return new ResultFailed($"Name of Round from Segment '{segmentEntity.Name}' must not be blank");
                }
                else if (assumedRounds.Contains(roundEntity.Name))
                {
                    return new ResultFailed($"Round '{roundEntity.Name}' from Segment '{segmentEntity.Name}' already exists");
                }
                Result result = ValidateEventCriteriumLayout(roundEntity);
                if (!result.IsSuccessful)
                {
                    return result;
                }

                assumedRounds.Add(roundEntity.Name);
            }

            return new ResultSuccess();
        }

        public static Result ValidateEventCriteriumLayout(RoundEntity roundEntity)
        {
            if (roundEntity == null)
            {
                return new ResultFailed($"A Round must exist");
            }

            GenericOrderedList<CriteriumEntity> criteriumEntities = roundEntity.Criteria;
            if (criteriumEntities.ItemCount == 0)
            {
                return new ResultFailed($"Round '{roundEntity.Name}' must contain at least 1 Criterium");
            }
            else if (criteriumEntities.ItemCount > 1000)
            {
                return new ResultFailed($"Round '{roundEntity.Name}' must contain at most 1,000 Criteria");
            }

            HashSet<string> assumedCriterium = new HashSet<string>();
            float assumedTotalPercentageWeight = 0;
            foreach (CriteriumEntity criteriumEntity in criteriumEntities.Items)
            {
                if (criteriumEntity == null)
                {
                    return new ResultFailed($"A Criterium must exist");
                }
                else if (string.IsNullOrEmpty(criteriumEntity.Name))
                {
                    return new ResultFailed($"Name of Criterium from Round '{roundEntity.Name}' must not be blank");
                }
                else if (assumedCriterium.Contains(criteriumEntity.Name))
                {
                    return new ResultFailed($"Criterium '{criteriumEntity.Name}' from Round '{roundEntity.Name}' already exists");
                }
                else if (criteriumEntity.MaximumValue < 0)
                {
                    return new ResultFailed($"Maximum value of Criterium '{criteriumEntity.Name}' from Round '{roundEntity.Name}' must be at least 0");
                }
                else if (criteriumEntity.MaximumValue > 1000)
                {
                    return new ResultFailed($"Maximum value of Criterium '{criteriumEntity.Name}' from Round '{roundEntity.Name}' must be at most 1,000");
                }
                else if (criteriumEntity.MinimumValue < 0)
                {
                    return new ResultFailed($"Minimum value of Criterium '{criteriumEntity.Name}' from Round '{roundEntity.Name}' must be at least 0");
                }
                else if (criteriumEntity.MinimumValue > criteriumEntity.MaximumValue)
                {
                    return new ResultFailed($"Minimum value of Criterium '{criteriumEntity.Name}' from Round '{roundEntity.Name}' must be at most {criteriumEntity.MaximumValue}");
                }
                else if (criteriumEntity.MinimumValue > 1000)
                {
                    return new ResultFailed($"Minimum value of Criterium '{criteriumEntity.Name}' from Round '{roundEntity.Name}' must be at most 1,000");
                }
                else if (criteriumEntity.PercentageWeight < 0)
                {
                    return new ResultFailed($"Percentage weight of Criterium '{criteriumEntity.Name}' from Round '{roundEntity.Name}' must be greater than 0");
                }
                else if (criteriumEntity.PercentageWeight > 100)
                {
                    return new ResultFailed($"Percentage weight of Criterium '{criteriumEntity.Name}' from Round '{roundEntity.Name}' must be at most 100");
                }

                assumedCriterium.Add(criteriumEntity.Name);
                assumedTotalPercentageWeight += criteriumEntity.PercentageWeight;
            }

            if (assumedTotalPercentageWeight != 100)
            {
                return new ResultFailed($"Accumulated Criterium Percentage Weight from Round '{roundEntity.Name}' must be exactly 100% not {assumedTotalPercentageWeight}%");
            }
            return new ResultSuccess();
        }

        public static Result ValidateEventJudgeUserEntities(GenericOrderedList<string> judges)
        {
            if (judges == null)
            {
                return new ResultFailed($"Judges must exist");
            }
            else if (judges.ItemCount == 0)
            {
                return new ResultFailed($"Judge count must be at least 1");
            }
            else if (judges.ItemCount > 1000)
            {
                return new ResultFailed($"Judge count must be at most 1,000");
            }

            HashSet<string> assumedJudges = new HashSet<string>();
            foreach (string judge in judges.Items)
            {
                if (string.IsNullOrEmpty(judge))
                {
                    return new ResultFailed($"Name of judge must not be blank");
                }
                else if (assumedJudges.Contains(judge))
                {
                    return new ResultFailed($"Judge '{judge}' already exists");
                }

                assumedJudges.Add(judge);
            }

            return new ResultSuccess();
        }

        public static Result ValidateEventContestantEntities(GenericOrderedList<ContestantEntity> contestants)
        {
            if (contestants == null)
            {
                return new ResultFailed($"Contestants must exist");
            }
            else if (contestants.ItemCount < 2)
            {
                return new ResultFailed($"Contestant count must be at least 1");
            }
            else if (contestants.ItemCount > 1000)
            {
                return new ResultFailed($"Contestant count must be at most 1,000");
            }

            foreach (ContestantEntity contestant in contestants.Items)
            {
                if (contestant == null)
                {
                    return new ResultFailed($"A contestant must exist");
                }
                else if (string.IsNullOrEmpty(contestant.FullName))
                {
                    return new ResultFailed($"Name of contestant must not be blank");
                }
                else if (contestant.HeightInCentimeters < 0)
                {
                    return new ResultFailed($"Height in centimeters of contestant '{contestant.FullName}' must be greater than 0");
                }
                else if (contestant.HeightInCentimeters > 1000)
                {
                    return new ResultFailed($"Height in centimeters of contestant '{contestant.FullName}' must be at most 1,000");
                }
                else if (contestant.WeightInKilograms < 0)
                {
                    return new ResultFailed($"Weight in kilograms of contestant '{contestant.FullName}' must be greater than 0");
                }
                else if (contestant.WeightInKilograms > 1000)
                {
                    return new ResultFailed($"Weight in kilograms of contestant '{contestant.FullName}' must be at most 1000");
                }
                else if (DateParser.IsInTheFuture(DateTime.Parse(contestant.BirthDate)))
                {
                    return new ResultFailed($"Birth date of contestant '{contestant.FullName}' must be in the past");
                }
                else if (contestant.Motto.Length > 1024)
                {
                    return new ResultFailed($"Motto length of contestant '{contestant.FullName}' must be at most 1024");
                }
                else if (contestant.HomeAddress.Length > 1024)
                {
                    return new ResultFailed($"Home address length of contestant '{contestant.FullName}' must be at most 1024");
                }
                else if (contestant.TalentsAndSkills.Length > 1024)
                {
                    return new ResultFailed($"Talents and skills length of contestant '{contestant.FullName}' must be at most 1024");
                }
                else if (contestant.Hobbies.Length > 1024)
                {
                    return new ResultFailed($"Hobbies length of contestant '{contestant.FullName}' must be at most 1024");
                }
                else if (contestant.Languages.Length > 1024)
                {
                    return new ResultFailed($"Languages length of contestant '{contestant.FullName}' must be at most 1024");
                }
                else if (contestant.WorkExperiences.Length > 1024)
                {
                    return new ResultFailed($"Work experiences length of contestant '{contestant.FullName}' must be at most 1024");
                }
                else if (MaritalStatusCache.IsNotFound(contestant.MaritalStatusType))
                {
                    return new ResultFailed($"Marital status type of contestant '{contestant.FullName}' must be filled");
                }
                else if (GenderCache.IsNotFound(contestant.GenderType))
                {
                    return new ResultFailed($"Gender type of contestant '{contestant.FullName}' must be filled");
                }
                else if (ApplicationResourceLoader.IsResourceNotFound(contestant.ImageResourcePath))
                {
                    return new ResultFailed($"Image resource path of contestant '{contestant.FullName}' must be valid");
                }
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
