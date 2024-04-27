
using System;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Generics;
using PageantVotingSystem.Sources.Miscellaneous;
using PageantVotingSystem.Sources.ResourceLoaders;


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

        public static Result AuthenticateNewEventProfile(EventEntity eventEntity)
        {
            if (string.IsNullOrEmpty(eventEntity.Name))
            {
                return new ResultFailed($"Name of event must not be blank");
            }
            else if (eventEntity.Name.Length > 128)
            {
                return new ResultFailed($"Name length of event '{eventEntity.Name}' must be at most 128");
            }
            else if (eventEntity.HostAddress.Length > 1024)
            {
                return new ResultFailed($"Host address length of event '{eventEntity.Name}' must be at most 128");
            }
            else if (DateParser.IsInThePast(DateTime.Parse(eventEntity.DateTimeStart)))
            {
                return new ResultFailed($"Scheduled date of event '{eventEntity.Name}' must be in the future");
            }
            else if (ScoringSystemCache.IsNotFound(eventEntity.ScoringSystemType))
            {
                return new ResultFailed($"Scoring system of event '{eventEntity.Name}' must be filled");
            }
            else if (eventEntity.Description.Length > 1024)
            {
                return new ResultFailed($"Description length of event '{eventEntity.Name}' must be at most 1024");
            }

            return new ResultSuccess();
        }
 
        public static Result AuthenticateNewEventSegmentsLayout(EventEntity eventEntity)
        {
            GenericOrderedList<SegmentEntity> segmentEntities = eventEntity.Segments;
            if (segmentEntities.ItemCount == 0)
            {
                return new ResultFailed($"Segment count must be at least 1");
            }
            else if (segmentEntities.ItemCount > 1000)
            {
                return new ResultFailed($"Segment count must be at most 1000");
            }

            HashSet<string> assumedSegments = new HashSet<string>();
            foreach (SegmentEntity segmentEntity in segmentEntities.Items)
            {
                if (string.IsNullOrEmpty(segmentEntity.Name))
                {
                    return new ResultFailed($"Name of segment must not be blank");
                }
                else if (assumedSegments.Contains(segmentEntity.Name))
                {
                    return new ResultFailed($"Segment '{segmentEntity.Name}' already exists");
                }
                else if (segmentEntity.MaximumContestantCount < 2)
                {
                    return new ResultFailed($"Maximum contestant count of segment '{segmentEntity.Name}' must be at least 2");
                }
                else if (segmentEntity.MaximumContestantCount > 1000)
                {
                    return new ResultFailed($"Maximum contestant count of segment '{segmentEntity.Name}' must be at most 1000");
                }
                Result result = AuthenticateNewEventRoundLayout(segmentEntity);
                if (!result.IsSuccessful)
                {
                    return result;
                }

                assumedSegments.Add(segmentEntity.Name);
            }

            return new ResultSuccess();
        }
        
        public static Result AuthenticateNewEventRoundLayout(SegmentEntity segmentEntity)
        {
            GenericOrderedList<RoundEntity> roundEntities = segmentEntity.Rounds;
            if (roundEntities.ItemCount == 0)
            {
                return new ResultFailed($"Round count must be at least 1");
            }
            else if (roundEntities.ItemCount > 1000)
            {
                return new ResultFailed($"Round count must be at most 1000");
            }

            HashSet<string> assumedRounds = new HashSet<string>();
            foreach (RoundEntity roundEntity in roundEntities.Items)
            {
                if (string.IsNullOrEmpty(roundEntity.Name))
                {
                    return new ResultFailed($"Name of round must not be blank");
                }
                else if (assumedRounds.Contains(roundEntity.Name))
                {
                    return new ResultFailed($"Round '{roundEntity.Name}' already exists at segment '{segmentEntity.Name}'");
                }
                Result result = AuthenticateNewEventCriteriumLayout(roundEntity);
                if (!result.IsSuccessful)
                {
                    return result;
                }

                assumedRounds.Add(roundEntity.Name);
            }

            return new ResultSuccess();
        }

        public static Result AuthenticateNewEventCriteriumLayout(RoundEntity roundEntity)
        {
            GenericOrderedList<CriteriumEntity> criteriumEntities = roundEntity.Criteria;
            if (criteriumEntities.ItemCount == 0)
            {
                return new ResultFailed($"Criterium count must be at least 1");
            }
            else if (criteriumEntities.ItemCount > 1000)
            {
                return new ResultFailed($"Criterium count must be at most 1000");
            }

            HashSet<string> assumedCriterium = new HashSet<string>();
            float assumedTotalPercentageWeight = 0;
            foreach (CriteriumEntity criteriumEntity in criteriumEntities.Items)
            {
                if (string.IsNullOrEmpty(criteriumEntity.Name))
                {
                    return new ResultFailed($"Name of criterium must not be blank");
                }
                else if (assumedCriterium.Contains(criteriumEntity.Name))
                {
                    return new ResultFailed($"Criterium '{criteriumEntity.Name}' already exists at round '{roundEntity.Name}'");
                }
                else if (criteriumEntity.MaximumValue < -1000)
                {
                    return new ResultFailed($"Maximum value of criterium '{criteriumEntity.Name}' must be at least -1000");
                }
                else if (criteriumEntity.MaximumValue > 1000)
                {
                    return new ResultFailed($"Maximum value of criterium '{criteriumEntity.Name}' must be at most 1000");
                }
                else if (criteriumEntity.MinimumValue < -1000)
                {
                    return new ResultFailed($"Minimum value of criterium '{criteriumEntity.Name}' must be at least -1000");
                }
                else if (criteriumEntity.MinimumValue > criteriumEntity.MaximumValue)
                {
                    return new ResultFailed($"Minimum value of criterium '{criteriumEntity.Name}' must be at most {criteriumEntity.MaximumValue}");
                }
                else if (criteriumEntity.MinimumValue > 1000)
                {
                    return new ResultFailed($"Minimum value of criterium '{criteriumEntity.Name}' must be at most 1000");
                }
                else if (criteriumEntity.PercentageWeight < 0)
                {
                    return new ResultFailed($"Percentage weight of criterium '{criteriumEntity.Name}' must be greater than 0");
                }
                else if (criteriumEntity.PercentageWeight > 100)
                {
                    return new ResultFailed($"Percentage weight of criterium '{criteriumEntity.Name}' must be at most 100");
                }

                assumedCriterium.Add(criteriumEntity.Name);
                assumedTotalPercentageWeight += criteriumEntity.PercentageWeight;
            }
            
            if (assumedTotalPercentageWeight != 100)
            {
                return new ResultFailed($"Accumulated criterium percentage weight does not match to 100% but {assumedTotalPercentageWeight}%");
            }
            return new ResultSuccess();
        }

        public static Result AuthenticateNewEventJudges(GenericOrderedList<string> judges)
        {
            if (judges.ItemCount == 0)
            {
                return new ResultFailed($"Judge count must be at least 1");
            }
            else if (judges.ItemCount > 1000)
            {
                return new ResultFailed($"Judge count must be at most 1000");
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

        public static Result AuthenticateNewEventContestants(GenericOrderedList<ContestantEntity> contestants)
        {
            if (contestants.ItemCount < 2)
            {
                return new ResultFailed($"Contestant count must be at least 1");
            }
            else if (contestants.ItemCount > 1000)
            {
                return new ResultFailed($"Contestant count must be at most 1000");
            }

            HashSet<int> assumedContestants = new HashSet<int>();
            foreach (ContestantEntity contestant in contestants.Items)
            {
                if (string.IsNullOrEmpty(contestant.FullName))
                {
                    return new ResultFailed($"Name of contestant must not be blank");
                }
                else if (assumedContestants.Contains(contestant.OrderNumber))
                {
                    return new ResultFailed($"Contestant '{contestant.FullName}' already exists");
                }
                else if (contestant.HeightInCentimeters <= 0)
                {
                    return new ResultFailed($"Height in centimeters of contestant '{contestant.FullName}' must be greater than 0");
                }
                else if (contestant.HeightInCentimeters > 1000)
                {
                    return new ResultFailed($"Height in centimeters of contestant '{contestant.FullName}' must be at most 1000");
                }
                else if (contestant.WeightInKilograms <= 0)
                {
                    return new ResultFailed($"Weight in kilograms of contestant '{contestant.FullName}' must be greater than 0");
                }
                else if (contestant.WeightInKilograms > 1000)
                {
                    return new ResultFailed($"Weight in kilograms of contestant '{contestant.FullName}' must be at most 1000");
                }
                else if (DateParser.IsInThePast(DateTime.Parse(contestant.BirthDate)))
                {
                    return new ResultFailed($"Birth date of contestant '{contestant.FullName}' must be filled");
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
                else if (ContestantStatusCache.IsNotFound(contestant.ContestantStatusType))
                {
                    return new ResultFailed($"Contestant status type of contestant '{contestant.FullName}' must be filled");
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

                assumedContestants.Add(contestant.OrderNumber);
            }

            return new ResultSuccess();
        }
    }
}
