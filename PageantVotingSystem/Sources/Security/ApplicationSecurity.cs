
using System;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Caches;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Generics;
using PageantVotingSystem.Sources.Miscellaneous;
using PageantVotingSystem.Sources.ResourceLoaders;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace PageantVotingSystem.Sources.Security
{
    public class ApplicationSecurity
    {
        public static Result AuthenticateOldUser(
            string email,
            string password)
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

        public static Result AuthenticateNewUser(
            string email,
            string fullName,
            string password,
            string passwordConfirmation,
            string roleType)
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
            result = ApplicationValidator.ValidatePasswordConfirmation(
                password,
                passwordConfirmation);
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

        public static Result AuthenticateNewEvent(
            EventEntity eventEntity,
            GenericOrderedList<string> judgeEntities,
            GenericOrderedList<ContestantEntity> contestantEntities)
        {
            Result result = ApplicationValidator.ValidateEventEntity(eventEntity);
            if (!result.IsSuccessful)
            {
                return result;
            }
            result = ApplicationValidator.ValidateEventSegmentEntities(eventEntity);
            if (!result.IsSuccessful)
            {
                return result;
            }
            result = ApplicationValidator.ValidateEventJudgeUserEntities(judgeEntities);
            if (!result.IsSuccessful)
            {
                return result;
            }
            result = ApplicationValidator.ValidateEventContestantEntities(contestantEntities);
            if (!result.IsSuccessful)
            {
                return result;
            }
            return new ResultSuccess();
        }
    }
}
