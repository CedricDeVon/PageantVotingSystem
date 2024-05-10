
using System;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Loggers;
using PageantVotingSystem.Sources.Entities;
using PageantVotingSystem.Sources.Miscellaneous;
using PageantVotingSystem.Sources.Configurations;
using System.Windows.Forms;

namespace PageantVotingSystem.Sources.Databases
{
    public class ApplicationDatabase : Database
    {
        public static string DatabaseName
        {
            get { return CurrentSettings.DatabaseName; }

            private set { }
        }

        public static void Setup(DatabaseSettings databaseSettings)
        {
            SetupRecorder.ThrowIfAlreadySetup("ApplicationDatabase");
            ApplicationLogger.LogInformationMessage("'ApplicationDatabase' setup began");
            
            CurrentSettings = databaseSettings;
            Result result = ExecuteFile(databaseSettings.SetupFilePath);
            if (!result.IsSuccessful)
            {
                throw new Exception(result.Message);
            }

            SetupRecorder.Add("ApplicationDatabase");
            ApplicationLogger.LogInformationMessage("'ApplicationDatabase' setup complete");
        }

        public static HashSet<object> ReadAllRawEventLayoutStatus()
        {
            return QueryUniqueTypes("SELECT type FROM event_layout_status");
        }

        public static HashSet<object> ReadAllRawRoundContestantStatus()
        {
            return QueryUniqueTypes("SELECT type FROM round_contestant_status");
        }

        public static HashSet<object> ReadAllRawJudgeStatus()
        {
            return QueryUniqueTypes("SELECT type FROM judge_status");
        }

        public static HashSet<object> ReadAllRawRoundStatus()
        {
            return QueryUniqueTypes("SELECT type FROM scoring_system");
        }

        public static HashSet<object> ReadAllRawContestantStatus()
        {
            return QueryUniqueTypes("SELECT type FROM contestant_status");
        }

        public static HashSet<object> ReadAllRawScoringSystems()
        {
            return QueryUniqueTypes("SELECT type FROM scoring_system");
        }

        public static HashSet<object> ReadAllRawUserRoles()
        {
            return QueryUniqueTypes("SELECT type FROM user_role");
        }

        public static HashSet<object> ReadAllRawMaritalStatus()
        {
            return QueryUniqueTypes("SELECT type FROM marital_status");
        }

        public static HashSet<object> ReadAllRawGenders()
        {
            return QueryUniqueTypes("SELECT type FROM gender");
        }
        
        public static HashSet<object> ReadAllRawResources()
        {
            return QueryUniqueTypes("SELECT path as type FROM resource");
        }

        public static Dictionary<object, object> ReadOneRawUserResult(string email)
        {
            return QueryOneEntity($"SELECT email, password, full_name, user_role_type, description, image_resource_path FROM user WHERE email = '{email}'");
        }

        public static bool IsUserEmailFound(string email)
        {
            return QueryEntityExistence($"SELECT email FROM user WHERE email = '{email}'");
        }

        public static bool IsUserEmailNotFound(string email)
        {
            return !IsUserEmailFound(email);
        }

        public static List<RoundEntity> ReadManyRoundsFromOngoingEvent(int segmentId)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT round_id as id, name, event_layout_status_type\r\nFROM event_layout\r\nINNER JOIN round\r\nON round.id = event_layout.round_id\r\nWHERE event_layout.segment_id = {segmentId}\r\nORDER BY round_id DESC");
            List<RoundEntity> roundEntities = new List<RoundEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                RoundEntity roundEntity = new RoundEntity(
                    Convert.ToInt32(result["id"]),
                    (string)result["name"],
                    (string)result["event_layout_status_type"]);
                roundEntities.Add(roundEntity);
            }
            return roundEntities;
        }

        public static List<JudgeUserEntity> ReadManyJudgeEntitiesFromPendingEventEntities(int eventId)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT order_number, judge_user_email, judge_status_type\r\nFROM event_judge\r\nWHERE event_id = {eventId}\r\nORDER BY order_number DESC");
            List<JudgeUserEntity> judgeUserEntities = new List<JudgeUserEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                JudgeUserEntity userEntity = new JudgeUserEntity(
                    Convert.ToInt32(result["order_number"]),
                    (string)result["judge_user_email"],
                    (string)result["judge_status_type"]);
                judgeUserEntities.Add(userEntity);
            }
            return judgeUserEntities;
        }

        public static List<JudgeUserEntity> ReadManyRoundContestantJudgesFromPendingEvent(int roundId, int contestantId)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT order_number, email, full_name, round_contestant_status_type\r\nFROM round_contestant\r\nINNER JOIN user\r\nINNER JOIN event_judge\r\nON\r\n\tuser.email = round_contestant.judge_user_email AND\r\n    event_judge.judge_user_email = round_contestant.judge_user_email\r\nWHERE round_id = {roundId} AND contestant_id = {contestantId}\r\n ORDER BY order_number DESC");
            List<JudgeUserEntity> judgeUserEntities = new List<JudgeUserEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                JudgeUserEntity userEntity = new JudgeUserEntity(
                    Convert.ToInt32(result["order_number"]),
                    (string)result["email"],
                    (string)result["full_name"],
                    (string)result["round_contestant_status_type"]);
                judgeUserEntities.Add(userEntity);
            }
            return judgeUserEntities;
        }

        public static List<ContestantEntity> ReadManyContestantEntitiesFromPendingEventEntities(int eventId)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT contestant_id as id, order_number, full_name, contestant_status_type\r\nFROM event_contestant\r\nINNER JOIN contestant\r\nON contestant.id = event_contestant.contestant_id\r\nWHERE event_id = {eventId}\r\nORDER BY order_number DESC;");
            List<ContestantEntity> contestantEntities = new List<ContestantEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                ContestantEntity contestantEntity = new ContestantEntity(
                    Convert.ToInt32(result["id"]),
                    Convert.ToInt32(result["order_number"]),
                    (string)result["full_name"],
                    (string)result["contestant_status_type"]);
                contestantEntities.Add(contestantEntity);
            }
            return contestantEntities;
        }

        public static UserEntity ReadOneUserEntity(string email)
        {
            Dictionary<object, object> result = QueryOneEntity($"SELECT email, password, full_name, user_role_type, description, image_resource_path FROM user WHERE email = '{email}'");
            UserEntity userEntity = new UserEntity();
            userEntity.Email = email;
            userEntity.Password = (string)result["password"];
            userEntity.FullName = (string)result["full_name"];
            userEntity.UserRoleType = (string)result["user_role_type"];
            userEntity.Description = (string)result["description"];
            userEntity.ImageResourcePath = (string)result["image_resource_path"];
            return userEntity;
        }

        public static List<Dictionary<object, object>> ReadManyJudges(string identifier, List<string> keywords = default, HashSet<string> blacklist = default)
        {
            string statement = GenerateStringQueryStatement("user", identifier, "user_role_type = 'Judge'", keywords, blacklist);
            return QueryManyEntities(statement);
        }

        public static List<string> ReadManyUniqueJudgeEmails(List<string> keywords = default, HashSet<string> blacklist = default)
        {
            List<string> judgeEmails = new List<string>();
            List<Dictionary<object, object>> judges = ReadManyJudges("email", keywords, blacklist);
            foreach (Dictionary<object, object> judge in judges)
            {
                string judgeEmail = (string) judge["email"];
                if (!blacklist.Contains(judgeEmail))
                {
                    judgeEmails.Add(judgeEmail);
                }
            }
            return judgeEmails;
        }

        public static List<string> ReadManyEventNames(string eventName, string eventManagerEmail)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT name FROM event INNER JOIN event_manager ON id = event_id WHERE name LIKE '%{eventName}%' AND manager_user_email LIKE '%{eventManagerEmail}%' ORDER BY name DESC");
            List<string> eventNames = new List<string>();
            foreach (Dictionary<object, object> result in results)
            {
                eventNames.Add((string) result["name"]);
            }
            return eventNames;
        }

        public static List<EventEntity> ReadManyEventEntitiesBasedOnManagerEmail(string eventName, string eventManagerEmail)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT id, name FROM event INNER JOIN event_manager ON id = event_id WHERE name LIKE '%{eventName}%' AND manager_user_email LIKE '%{eventManagerEmail}%' ORDER BY name DESC");
            List<EventEntity> entities = new List<EventEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                EventEntity entity = new EventEntity(
                    Convert.ToInt32(result["id"]),
                    (string)result["name"]);
                entities.Add(entity);
            }
            return entities;
        }

        public static List<EventEntity> ReadManyOngoingEventsBasedOnJudgeEmail(string eventName, string eventJudgeEmail)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT event_id as id, name FROM (SELECT event_layout.event_id FROM ( SELECT event_id FROM event_judge WHERE judge_user_email = '{eventJudgeEmail}' AND judge_status_type = 'Present') data INNER JOIN event_layout ON event_layout.event_id = data.event_id WHERE event_layout_status_type = 'Ongoing') data INNER JOIN event ON event.id = data.event_id WHERE name LIKE '%{eventName}%'");
            List<EventEntity> entities = new List<EventEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                EventEntity entity = new EventEntity(
                    Convert.ToInt32(result["id"]),
                    (string)result["name"]);
                entities.Add(entity);
            }
            return entities;
        }

        public static List<ContestantEntity> ReadManyUnjudgedEventContestants(int eventId)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT contestant_id as id, order_number, full_name\r\nFROM\r\n(\r\n\tSELECT contestant_id\r\n\tFROM event_layout\r\n\tINNER JOIN round_contestant\r\n\tON event_layout.round_id = round_contestant.round_id\r\n\tWHERE\r\n\t\tevent_id = {eventId} AND\r\n\t\tround_contestant_status_type = 'Incomplete'\r\n) data\r\nINNER JOIN contestant\r\nON contestant.id = data.contestant_id ORDER BY order_number DESC");
            List<ContestantEntity> entities = new List<ContestantEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                ContestantEntity entity = new ContestantEntity();
                entity.Id = Convert.ToInt32(result["id"]);
                entity.OrderNumber = Convert.ToInt32(result["order_number"]);
                entity.FullName = (string)result["full_name"];
                entities.Add(entity);
            }
            return entities;
        }

        public static List<ContestantEntity> ReadManyUnjudgedRoundContestantEntities(int roundId)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT contestant.id, order_number, full_name\r\nFROM\r\n(\r\n\tSELECT contestant_id\r\n\tFROM round_contestant\r\n    WHERE\r\n\t\tround_id = {roundId} AND\r\n        round_contestant_status_type = 'Incomplete'\r\n\tGROUP BY contestant_id\r\n) data\r\nINNER JOIN contestant\r\nON contestant.id = data.contestant_id\r\nORDER BY order_number DESC");
            List<ContestantEntity> entities = new List<ContestantEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                ContestantEntity entity = new ContestantEntity();
                entity.Id = Convert.ToInt32(result["id"]);
                entity.OrderNumber = Convert.ToInt32(result["order_number"]);
                entity.FullName = (string)result["full_name"];
                entities.Add(entity);
            }
            return entities;
        }

        public static void ReadOneEventLayoutEntity(EventEntity eventEntity)
        {
            List<SegmentEntity> segmentEntities = ReadManySegmentsEntities(eventEntity.Id);
            foreach (SegmentEntity segmentEntity in segmentEntities)
            {
                ReadOneSegmentLayout(segmentEntity);
                eventEntity.Segments.AddNewItem(segmentEntity);
            }
        }

        private static void ReadOneSegmentLayout(SegmentEntity segmentEntity)
        {
            List<RoundEntity> roundEntities = ReadManyRoundsEntities(segmentEntity.Id);
            foreach (RoundEntity roundEntity in roundEntities)
            {
                ReadOneRoundLayout(roundEntity);
                segmentEntity.Rounds.AddNewItem(roundEntity);
            }
        }

        private static void ReadOneRoundLayout(RoundEntity roundEntity)
        {
            List<CriteriumEntity> criteriumEntities = ReadManyCriteria(roundEntity.Id);
            foreach (CriteriumEntity criteriumEntity in criteriumEntities)
            {
                roundEntity.Criteria.AddNewItem(criteriumEntity);
            }
        }

        public static List<SegmentEntity> ReadManySegmentsEntities(int eventId)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT id, name, description FROM segment WHERE event_id = {eventId} ORDER BY id ASC");
            List<SegmentEntity> entities = new List<SegmentEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                SegmentEntity entity = new SegmentEntity(
                    Convert.ToInt32(result["id"]),
                    (string)result["name"]);
                entities.Add(entity);
            }
            return entities;
        }

        public static EventEntity ReadOneEventEntity(int eventId)
        {
            Dictionary<object, object> results = QueryOneEntity(
                $"SELECT id, name, scoring_system_type, host_address, manager_user_email, description FROM event INNER JOIN event_manager ON id = event_id WHERE id = {eventId}");
            EventEntity entity = new EventEntity();
            entity.Id = Convert.ToInt32(results["id"]);
            entity.Name = (string)results["name"];
            entity.HostAddress = (string)results["host_address"];
            entity.ScoringSystemType = (string)results["scoring_system_type"];
            entity.ManagerEmail = (string)results["manager_user_email"];
            entity.Description = (string)results["description"];
            return entity;
        }

        public static SegmentEntity ReadOneSegmentEntity(int segmentId)
        {
            Dictionary<object, object> results = QueryOneEntity($"SELECT id, name, description FROM segment WHERE id = {segmentId}");
            SegmentEntity entity = new SegmentEntity();
            entity.Id = Convert.ToInt32(results["id"]);
            entity.Name = (string) results["name"];
            entity.Description = (string)results["description"];
            return entity;
        }

        public static RoundEntity ReadOneRoundEntity(int roundId)
        {
            Dictionary<object, object> results = QueryOneEntity($"SELECT id, name, description FROM round WHERE id = {roundId}");
            RoundEntity entity = new RoundEntity();
            entity.Id = Convert.ToInt32(results["id"]);
            entity.Name = (string)results["name"];
            entity.Description = (string)results["description"];
            return entity;
        }

        public static CriteriumEntity ReadOneCriteriumEntity(int criteriumId)
        {
            Dictionary<object, object> results = QueryOneEntity($"SELECT id, name, description, maximum_value, percentage_weight FROM criterium WHERE id = {criteriumId}");
            CriteriumEntity entity = new CriteriumEntity();
            entity.Id = Convert.ToInt32(results["id"]);
            entity.Name = (string)results["name"];
            entity.Description = (string)results["description"];
            entity.MaximumValue = (float) Convert.ToDouble(results["maximum_value"]);
            entity.PercentageWeight = (float)Convert.ToDouble(results["percentage_weight"]);
            return entity;
        }

        public static ContestantEntity ReadOneContestantEntity(int contestantId)
        {
            Dictionary<object, object> results = QueryOneEntity($"SELECT * FROM contestant WHERE id = {contestantId}");
            ContestantEntity entity = new ContestantEntity();
            entity.Id = Convert.ToInt32(results["id"]);
            entity.FullName = (string)results["full_name"];
            entity.OrderNumber = Convert.ToInt32(results["order_number"]);
            entity.HeightInCentimeters = (results["height_in_centimeters"] is DBNull) ? 0 : (float)Convert.ToDecimal(results["height_in_centimeters"]);
            entity.WeightInKilograms = (results["weight_in_kilograms"] is DBNull) ? 0 : (float)Convert.ToDecimal(results["weight_in_kilograms"]);
            entity.BirthDate = (results["birth_date"] is DBNull) ? "" : DateParser.ShortenDate((DateTime)results["birth_date"]);
            entity.Email = (results["email"] is DBNull) ? "" : (string)results["email"];
            entity.PhoneNumber = (results["phone_number"] is DBNull) ? "" : (string)results["phone_number"];
            entity.Motto = (results["motto"] is DBNull) ? "" : (string)results["motto"];
            entity.HomeAddress = (results["home_address"] is DBNull) ? "" : (string)results["home_address"];
            entity.TalentsAndSkills = (results["talents_and_skills"] is DBNull) ? "" : (string)results["talents_and_skills"];
            entity.Hobbies = (results["hobbies"] is DBNull) ? "" : (string)results["hobbies"];
            entity.Languages = (results["languages"] is DBNull) ? "" : (string)results["languages"];
            entity.WorkExperiences = (results["work_experiences"] is DBNull) ? "" : (string)results["work_experiences"];
            entity.Education = (results["education"] is DBNull) ? "" : (string)results["education"];
            entity.MaritalStatusType = (results["marital_status_type"] is DBNull) ? "" : (string)results["marital_status_type"];
            entity.GenderType = (results["gender_type"] is DBNull) ? "" : (string)results["gender_type"];
            entity.ImageResourcePath = (results["image_resource_path"] is DBNull) ? ApplicationConfiguration.DefaultUserProfileImagePath : (string)results["image_resource_path"];
            return entity;
        }

        public static List<RoundEntity> ReadManyRoundsEntities(int segmentId)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT id, name FROM round WHERE segment_id = {segmentId} ORDER BY id ASC");
            List<RoundEntity> entities = new List<RoundEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                RoundEntity entity = new RoundEntity(
                    Convert.ToInt32(result["id"]),
                    (string)result["name"]);
                entities.Add(entity);
            }
            return entities;
        }
        
        public static List<CriteriumEntity> ReadManyCriteria(int roundId)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT id, name FROM criterium WHERE round_id = {roundId} ORDER BY id ASC");
            List<CriteriumEntity> entities = new List<CriteriumEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                CriteriumEntity entity = new CriteriumEntity(
                    Convert.ToInt32(result["id"]),
                    (string)result["name"]);
                entities.Add(entity);
            }
            return entities;
        }

        public static List<JudgeUserEntity> ReadManyJudgeEntities(int eventId)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT order_number, user.email, full_name FROM event_judge INNER JOIN user ON email = judge_user_email WHERE event_id = {eventId} ORDER BY order_number DESC");
            List<JudgeUserEntity> entities = new List<JudgeUserEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                JudgeUserEntity entity = new JudgeUserEntity(
                    (string)result["email"],
                    Convert.ToInt32(result["order_number"]),
                    (string)result["full_name"]);
                entities.Add(entity);
            }
            return entities;
        }

        public static List<ContestantEntity> ReadManyEventContestantEntities(int eventId)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT * FROM contestant INNER JOIN event_contestant ON id = contestant_id WHERE event_id = {eventId} ORDER BY order_number DESC");
            List<ContestantEntity> entities = new List<ContestantEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                ContestantEntity entity = new ContestantEntity();
                entity.Id = Convert.ToInt32(result["id"]);
                entity.FullName = (string)result["full_name"];
                entity.OrderNumber = Convert.ToInt32(result["order_number"]);
                entity.HeightInCentimeters = (result["height_in_centimeters"] is DBNull) ? 0 : (float)Convert.ToDecimal(result["height_in_centimeters"]);
                entity.WeightInKilograms = (result["weight_in_kilograms"] is DBNull) ? 0 : (float)Convert.ToDecimal(result["weight_in_kilograms"]);
                entity.BirthDate = (result["birth_date"] is DBNull) ? "" : DateParser.ShortenDate((DateTime)result["birth_date"]);
                entity.Email = (result["email"] is DBNull) ? "" : (string)result["email"];
                entity.PhoneNumber = (result["phone_number"] is DBNull) ? "" : (string)result["phone_number"];
                entity.Motto = (result["motto"] is DBNull) ? "" : (string)result["motto"];
                entity.HomeAddress = (result["home_address"] is DBNull) ? "" : (string)result["home_address"];
                entity.TalentsAndSkills = (result["talents_and_skills"] is DBNull) ? "" : (string)result["talents_and_skills"];
                entity.Hobbies = (result["hobbies"] is DBNull) ? "" : (string)result["hobbies"];
                entity.Languages = (result["languages"] is DBNull) ? "" : (string)result["languages"];
                entity.WorkExperiences = (result["work_experiences"] is DBNull) ? "" : (string)result["work_experiences"];
                entity.Education = (result["education"] is DBNull) ? "" : (string)result["education"];
                entity.ContestantStatusType = (result["contestant_status_type"] is DBNull) ? "" : (string)result["contestant_status_type"];
                entity.MaritalStatusType = (result["marital_status_type"] is DBNull) ? "" : (string)result["marital_status_type"];
                entity.GenderType = (result["gender_type"] is DBNull) ? "" : (string)result["gender_type"];
                entity.ImageResourcePath = (result["image_resource_path"] is DBNull) ? ApplicationConfiguration.DefaultUserProfileImagePath : (string)result["image_resource_path"];
                entities.Add(entity);
            }
            return entities;
        }

        public static List<ContestantEntity> ReadManySimplifiedEventContestants(int eventId)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT contestant.id, contestant.order_number, contestant.full_name, event_contestant.contestant_status_type FROM contestant INNER JOIN event_contestant ON id = contestant_id WHERE event_id = {eventId} ORDER BY order_number DESC");
            List<ContestantEntity> entities = new List<ContestantEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                ContestantEntity entity = new ContestantEntity();
                entity.Id = Convert.ToInt32(result["id"]);
                entity.FullName = (string)result["full_name"];
                entity.OrderNumber = Convert.ToInt32(result["order_number"]);
                entity.ContestantStatusType = (string) result["contestant_status_type"];
                entities.Add(entity);
            }
            return entities;
        }

        public static List<ContestantResultEntity> ReadManyCriteriumResultEntities(int id)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT\r\n\tRANK() OVER (ORDER BY net_value DESC) as ranking_number,\r\n    order_number,\r\n    full_name,\r\n    ROUND(net_value / maximum_value * 100, 2) as net_percentage, net_value,\r\n    maximum_value\r\nFROM\r\n(\r\n\tSELECT contestant_id,\r\n\t\tROUND(SUM(base_value * (percentage_weight / 100)), 2) as net_value,\r\n        ROUND(SUM(maximum_value * (percentage_weight / 100)), 2) as maximum_value\r\n\tFROM result\r\n\tINNER JOIN criterium\r\n\tON id = criterium_id\r\n\tWHERE criterium_id = {id}\r\n\tGROUP BY contestant_id\r\n) data\r\nINNER JOIN contestant\r\nON contestant.id = contestant_id\r\n");
            List<ContestantResultEntity> entities = new List<ContestantResultEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                ContestantResultEntity entity = new ContestantResultEntity(
                    Convert.ToInt32(result["ranking_number"]),
                    Convert.ToInt32(result["order_number"]),
                    (string)result["full_name"],
                    (float)Convert.ToDouble(result["net_percentage"]),
                    (float)Convert.ToDouble(result["net_value"]),
                    (float)Convert.ToDouble(result["maximum_value"]));
                entities.Add(entity);
            }
            return entities;
        }

        public static List<ContestantResultEntity> ReadManyRoundResults(int id)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT\r\n\tRANK() OVER (ORDER BY net_value DESC) as ranking_number,\r\n    order_number,\r\n    full_name,\r\n    ROUND(net_value / maximum_value * 100, 2) as net_percentage, net_value,\r\n    maximum_value\r\nFROM\r\n(\r\n\tSELECT contestant_id,\r\n\t\tROUND(SUM(base_value * (percentage_weight / 100)), 2) as net_value,\r\n        ROUND(SUM(maximum_value * (percentage_weight / 100)), 2) as maximum_value\r\n\tFROM result\r\n\tINNER JOIN criterium\r\n\tON id = criterium_id\r\n\tWHERE result.round_id = {id}\r\n\tGROUP BY contestant_id\r\n) data\r\nINNER JOIN contestant\r\nON contestant.id = contestant_id\r\n");
            List<ContestantResultEntity> entities = new List<ContestantResultEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                ContestantResultEntity entity = new ContestantResultEntity(
                    Convert.ToInt32(result["ranking_number"]),
                    Convert.ToInt32(result["order_number"]),
                    (string)result["full_name"],
                    (float)Convert.ToDouble(result["net_percentage"]),
                    (float)Convert.ToDouble(result["net_value"]),
                    (float)Convert.ToDouble(result["maximum_value"]));
                entities.Add(entity);
            }
            return entities;
        }

        public static List<ContestantResultEntity> ReadManySegmentResults(int id)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT\r\n\tRANK() OVER (ORDER BY net_value DESC) as ranking_number,\r\n    order_number,\r\n    full_name,\r\n    ROUND(net_value / maximum_value * 100, 2) as net_percentage, net_value,\r\n    maximum_value\r\nFROM\r\n(\r\n\tSELECT contestant_id,\r\n\t\tROUND(SUM(base_value * (percentage_weight / 100)), 2) as net_value,\r\n        ROUND(SUM(maximum_value * (percentage_weight / 100)), 2) as maximum_value\r\n\tFROM result\r\n\tINNER JOIN criterium\r\n\tON id = criterium_id\r\n\tWHERE result.segment_id = {id}\r\n\tGROUP BY contestant_id\r\n) data\r\nINNER JOIN contestant\r\nON contestant.id = contestant_id\r\n");
            List<ContestantResultEntity> entities = new List<ContestantResultEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                ContestantResultEntity entity = new ContestantResultEntity(
                    Convert.ToInt32(result["ranking_number"]),
                    Convert.ToInt32(result["order_number"]),
                    (string)result["full_name"],
                    (float)Convert.ToDouble(result["net_percentage"]),
                    (float)Convert.ToDouble(result["net_value"]),
                    (float)Convert.ToDouble(result["maximum_value"]));
                entities.Add(entity);
            }
            return entities;
        }

        public static List<JudgeCriteriumEntity> ReadManyJudgeCriteriumEntities(int roundId, int contestantId, string judgeUserEmail)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT data.event_id, data.segment_id, data.round_id, criterium.id as criterium_id, name as criterium_name, base_value, maximum_value, percentage_weight\r\nFROM\r\n(\r\nSELECT event_id, segment_id, round_id\r\nFROM event_layout\r\nWHERE\r\n\r round_id = {roundId} AND\r\n event_layout_status_type = 'Pending'\r\n) data\r\nINNER JOIN result\r\nINNER JOIN criterium\r\nON result.round_id = data.round_id AND\r\n\tresult.criterium_id = criterium.id\r\nWHERE\r\n\tcontestant_id = {contestantId} AND judge_user_email = '{judgeUserEmail}' \r\nORDER BY criterium_id DESC");
            List<JudgeCriteriumEntity> entities = new List<JudgeCriteriumEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                JudgeCriteriumEntity entity = new JudgeCriteriumEntity();
                entity.Result.ContestantId = contestantId;
                entity.Result.EventId = Convert.ToInt32(result["event_id"]);
                entity.Result.SegmentId = Convert.ToInt32(result["segment_id"]);
                entity.Result.RoundId = Convert.ToInt32(result["round_id"]);
                entity.Result.CriteriumId = Convert.ToInt32(result["criterium_id"]);
                entity.Result.BaseValue = (float) Convert.ToDecimal(result["base_value"]);
                entity.Criterium.Id = Convert.ToInt32(result["criterium_id"]);
                entity.Criterium.MaximumValue = (float)Convert.ToDecimal(result["maximum_value"]);
                entity.Criterium.PercentageWeight = (float)Convert.ToDecimal(result["percentage_weight"]);
                entity.Criterium.Name = (string)result["criterium_name"];
                entities.Add(entity);
            }
            return entities;
        }

        public static List<ContestantEntity> ReadManyUnjudgedContestantEntities(int roundId, string judgeUserEmail)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT contestant.id, order_number, full_name\r\nFROM\r\n(\r\nSELECT contestant_id\r\nFROM round_contestant\r\nWHERE round_contestant_status_type = 'Pending' AND round_id = {roundId} AND judge_user_email = '{judgeUserEmail}'\r\nGROUP BY contestant_id\r\n) data\r\nINNER JOIN contestant\r\nON contestant.id = data.contestant_id\r\nORDER BY order_number DESC");
            List<ContestantEntity> entities = new List<ContestantEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                ContestantEntity entity = new ContestantEntity();
                entity.Id = Convert.ToInt32(result["id"]);
                entity.OrderNumber = Convert.ToInt32(result["order_number"]);
                entity.FullName = (string)result["full_name"];
                entities.Add(entity);
            }
            return entities;
        }

        public static EventLayoutSequenceEntity ReadOnePendingEventLayoutSequenceEntity(int eventId)
        {
            Dictionary<object, object> results = QueryOneEntity($"SELECT event_id, segment_id, round_id FROM event_layout WHERE event_id = {eventId} AND event_layout_status_type = 'Pending'");
            Dictionary<object, object> eventResult = QueryOneEntity($"SELECT id, name FROM event WHERE id = {Convert.ToInt32(results["event_id"])}");
            Dictionary<object, object> segmentResult = QueryOneEntity($"SELECT id, name FROM segment WHERE id = {Convert.ToInt32(results["segment_id"])}");
            Dictionary<object, object> roundResult = QueryOneEntity($"SELECT id, name FROM round WHERE id = {Convert.ToInt32(results["round_id"])}");
            EventLayoutSequenceEntity eventLayoutSequenceEntity = new EventLayoutSequenceEntity();
            eventLayoutSequenceEntity.Event = new EventEntity(Convert.ToInt32(eventResult["id"]), (string)eventResult["name"]);
            eventLayoutSequenceEntity.Segment = new SegmentEntity(Convert.ToInt32(segmentResult["id"]), (string)segmentResult["name"]);
            eventLayoutSequenceEntity.Round = new RoundEntity(Convert.ToInt32(roundResult["id"]), (string)roundResult["name"]);
            return eventLayoutSequenceEntity;
        }

        public static List<EventLayoutSequenceEntity> ReadManyPendingEventLayoutSequenceEntitiesBasedOnJudgeUserEmail(string eventName, string judgeUserEmail)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT event_layout.event_id, event.name, event_layout.segment_id, event_layout.round_id\r\nFROM\r\n(\r\nSELECT event_id\r\nFROM event_judge\r\nWHERE judge_user_email = '{judgeUserEmail}'\r\n ) data\r\nINNER JOIN event_layout\r\nINNER JOIN event\r\nON event_layout.event_id = data.event_id AND event.id = data.event_id\r\nWHERE event_layout_status_type = 'Pending' AND event.name LIKE '%{eventName}%' ORDER BY event.name DESC");
            List<EventLayoutSequenceEntity> eventLayoutSequenceEntities = new List<EventLayoutSequenceEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                EventLayoutSequenceEntity eventLayoutSequenceEntity = new EventLayoutSequenceEntity();
                eventLayoutSequenceEntity.Event = new EventEntity(Convert.ToInt32(result["event_id"]), (string)result["name"]);
                eventLayoutSequenceEntity.Segment = new SegmentEntity(Convert.ToInt32(result["segment_id"]));
                eventLayoutSequenceEntity.Round = new RoundEntity(Convert.ToInt32(result["round_id"]));
                eventLayoutSequenceEntities.Add(eventLayoutSequenceEntity);
            }
            return eventLayoutSequenceEntities;
        }


        public static List<EventEntity> ReadManyPendingEventEntitiesBasedOnManagerEmail(string eventName, string managerUserEmail)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT event.id, event.name\r\nFROM event_manager\r\nINNER JOIN event_layout\r\nINNER JOIN event\r\nON\r\n\tevent_layout.event_id = event_manager.event_id AND\r\n    event.id = event_manager.event_id\r\nWHERE\r\n\t event.name LIKE '%{eventName}%' AND manager_user_email = '{managerUserEmail}' AND\r\n    event_layout_status_type = 'Pending'");
            List<EventEntity> entities = new List<EventEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                EventEntity entity = new EventEntity();
                entity.Id = Convert.ToInt32(result["id"]);
                entity.Name = (string) result["name"];
                entities.Add(entity);
            }
            return entities;
        }

        public static ContestantEntity ReadOnePendingContestantEntityUnderJudgement(int roundId)
        {
            Dictionary<object, object> results = QueryOneEntity($"SELECT contestant.id, order_number, full_name\r\nFROM\r\n(\r\nSELECT contestant_id\r\nFROM round_contestant\r\nWHERE round_contestant_status_type = 'Pending' AND round_id = {roundId}\r\nGROUP BY contestant_id\r\n) data\r\nINNER JOIN contestant\r\nON contestant.id = data.contestant_id;");
            if (results == null)
            {
                return null;
            }
            ContestantEntity contestantEntity = new ContestantEntity(
                Convert.ToInt32(results["id"]),
                (string)results["full_name"],
                Convert.ToInt32(results["order_number"]));
            return contestantEntity;
        }

        public static List<ContestantEntity> ReadManyQualifiedSegmentContestants(SegmentEntity segmentEntity)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT\r\n\tRANK() OVER (ORDER BY net_value DESC) as ranking_number, contestant_id\r\nFROM\r\n(\r\n\tSELECT contestant_id, ROUND(SUM(base_value * (percentage_weight / 100)), 2) as net_value, ROUND(SUM(maximum_value * (percentage_weight / 100)), 2) as maximum_value\r\n    FROM result\r\n    INNER JOIN criterium\r\n    ON id = criterium_id\r\n    WHERE result.segment_id = {segmentEntity.Id}\r\n    GROUP BY contestant_id\r\n) data\r\nINNER JOIN contestant\r\nON contestant.id = contestant_id\r\nLIMIT {segmentEntity.MaximumContestantCount}");
            List<ContestantEntity> contestantEntities = new List<ContestantEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                
            }
            return contestantEntities;
        }

        public static EventEntity ReadOneRecentEvent()
        {
            Dictionary<object, object> result = QueryOneEntity($"SELECT id FROM event ORDER BY id DESC LIMIT 1");
            if (result == null)
            {
                return new EventEntity(0);
            }
            EventEntity eventEntity = new EventEntity(
                Convert.ToInt32(result["id"]));
            return eventEntity;
        }

        public static SegmentEntity ReadOneRecentSegment()
        {
            Dictionary<object, object> result = QueryOneEntity($"SELECT id FROM segment ORDER BY id DESC LIMIT 1");
            if (result == null)
            {
                return new SegmentEntity(0);
            }
            SegmentEntity segmentEntity = new SegmentEntity(
                Convert.ToInt32(result["id"]));
            return segmentEntity;
        }

        public static RoundEntity ReadOneRecentRound()
        {
            Dictionary<object, object> result = QueryOneEntity($"SELECT id FROM round ORDER BY id DESC LIMIT 1");
            if (result == null)
            {
                return new RoundEntity(0);
            }
            RoundEntity roundEntity = new RoundEntity(
                Convert.ToInt32(result["id"]));
            return roundEntity;
        }

        public static CriteriumEntity ReadOneRecentCriterium()
        {
            Dictionary<object, object> result = QueryOneEntity($"SELECT id FROM criterium ORDER BY id DESC LIMIT 1");
            if (result == null)
            {
                return new CriteriumEntity(0);
            }
            CriteriumEntity criteriumEntity = new CriteriumEntity(
                Convert.ToInt32(result["id"]));
            return criteriumEntity;
        }

        public static ContestantEntity ReadOneRecentContestant()
        {
            Dictionary<object, object> result = QueryOneEntity($"SELECT id FROM contestant ORDER BY id DESC LIMIT 1");
            if (result == null)
            {
                return new ContestantEntity(0);
            }
            ContestantEntity contestantEntity = new ContestantEntity(
                Convert.ToInt32(result["id"]));
            return contestantEntity;
        }

        public static EventLayoutSequenceEntity ReadOneNextIncompleteEventLayoutSequence(int eventId)
        {
            Dictionary<object, object> firstResult = QueryOneEntity($"SELECT event_id, segment_id, round_id, event_layout_status_type FROM event_layout WHERE event_layout_status_type = 'Incomplete' AND event_id = {eventId} LIMIT 1");
            if (firstResult == null)
            {
                return null;
            }
            Dictionary<object, object> eventResult = QueryOneEntity($"SELECT id, name FROM event WHERE id = {Convert.ToInt32(firstResult["event_id"])}");
            Dictionary<object, object> segmentResult = QueryOneEntity($"SELECT id, name FROM segment WHERE id = {Convert.ToInt32(firstResult["segment_id"])}");
            Dictionary<object, object> roundResult = QueryOneEntity($"SELECT id, name FROM round WHERE id = {Convert.ToInt32(firstResult["round_id"])}");
            EventLayoutSequenceEntity eventLayoutSequenceEntity = new EventLayoutSequenceEntity();
            eventLayoutSequenceEntity.Event = new EventEntity(Convert.ToInt32(firstResult["event_id"]), (string)eventResult["name"]);
            eventLayoutSequenceEntity.Segment = new SegmentEntity(Convert.ToInt32(firstResult["segment_id"]), (string)segmentResult["name"]);
            eventLayoutSequenceEntity.Round = new RoundEntity(Convert.ToInt32(firstResult["round_id"]), (string)roundResult["name"]);
            eventLayoutSequenceEntity.EventLayoutStatusType = (string) firstResult["event_layout_status_type"];
            return eventLayoutSequenceEntity;
        }

        public static void CreateResource(string path)
        {
            ExecuteStatement($"INSERT INTO resource (path) VALUES ('{path}')");
        }

        public static void CreateContestant(ContestantEntity contestantEntity)
        {
            ExecuteStatement($"INSERT INTO contestant (id, order_number, image_resource_path, full_name, email, phone_number, home_address, birth_date, gender_type, marital_status_type, height_in_centimeters, weight_in_kilograms, talents_and_skills, hobbies, languages, work_experiences, education, motto)\r\nVALUES\r\n    ({contestantEntity.Id}, {contestantEntity.OrderNumber}, '{contestantEntity.ImageResourcePath}', '{contestantEntity.FullName}', '{contestantEntity.Email}', '{contestantEntity.PhoneNumber}', '{contestantEntity.HomeAddress}', '{contestantEntity.DateTimeBirthDate}', '{contestantEntity.GenderType}', '{contestantEntity.MaritalStatusType}', {contestantEntity.HeightInCentimeters}, {contestantEntity.WeightInKilograms}, '{contestantEntity.TalentsAndSkills}', '{contestantEntity.Hobbies}', '{contestantEntity.Languages}', '{contestantEntity.WorkExperiences}', '{contestantEntity.Education}', '{contestantEntity.Motto}')");
        }

        public static void CreateEvent(EventEntity eventEntity)
        {
            ExecuteStatement($"INSERT INTO event (id, name, description, host_address, scoring_system_type)\r\nVALUES\r\n    ({eventEntity.Id}, '{eventEntity.Name}', '{eventEntity.Description}', '{eventEntity.HostAddress}', '{eventEntity.ScoringSystemType}')");
        }

        public static void CreateSegment(SegmentEntity segmentEntity)
        {
            ExecuteStatement($"INSERT INTO segment (id, name, description, event_id)\r\nVALUES\r\n    ({segmentEntity.Id}, '{segmentEntity.Name}', '{segmentEntity.Description}', {segmentEntity.EventId})");
        }

        public static void CreateRound(RoundEntity roundEntity)
        {
            ExecuteStatement($"INSERT INTO round (id, name, description, segment_id)\r\nVALUES\r\n    ({roundEntity.Id}, '{roundEntity.Name}', '{roundEntity.Description}', {roundEntity.SegmentId})");
        }
        
        public static void CreateCriterium(CriteriumEntity criteriumEntity)
        {
            ExecuteStatement($"INSERT INTO criterium (id, name, description, maximum_value, percentage_weight, round_id)\r\nVALUES\r\n ({criteriumEntity.Id}, '{criteriumEntity.Name}', '{criteriumEntity.Description}', {criteriumEntity.MaximumValue}, {criteriumEntity.PercentageWeight}, {criteriumEntity.RoundId})");
        }

        public static void CreateEventLayout(int eventId, int segmentId, int roundId, string eventLayoutStatusType)
        {
            ExecuteStatement($"INSERT INTO event_layout (event_id, segment_id, round_id, event_layout_status_type)\r\nVALUES\r\n\t({eventId}, {segmentId}, {roundId}, '{eventLayoutStatusType}')");
        }

        public static void CreateEventManager(int eventId, string managerUserEmail)
        {
            ExecuteStatement($"INSERT INTO event_manager (event_id, manager_user_email)\r\nVALUES\r\n    ({eventId}, '{managerUserEmail}')");
        }

        public static void CreateEventJudge(int eventId, int orderNumber, string judgeUserEmail)
        {
            ExecuteStatement($"INSERT INTO event_judge (event_id, order_number, judge_user_email)\r\nVALUES\r\n    ({eventId}, {orderNumber}, '{judgeUserEmail}')");
        }

        public static void CreateEventContestant(int eventId, int contestantId)
        {
            ExecuteStatement($"INSERT INTO event_contestant (event_id, contestant_id)\r\nVALUES\r\n    ({eventId}, {contestantId})");
        }

        public static void CreateRoundContestant(int roundId, int contestantId, string judgeUserEmail)
        {
            ExecuteStatement($"INSERT INTO round_contestant(round_id, contestant_id, judge_user_email) VALUES({roundId}, {contestantId}, '{judgeUserEmail}')");
        }
        
        public static void UpdateEventLayoutToComplete(int roundId)
        {
            ExecuteStatement($"UPDATE event_layout SET event_layout_status_type = 'Complete' WHERE round_id = {roundId}");
        }

        public static void UpdateEventLayoutToCurrent(int roundId)
        {
            ExecuteStatement($"UPDATE event_layout SET event_layout_status_type = 'Ongoing' WHERE round_id = {roundId}");
        }

        public static void UpdateEventLayoutToIncomplete(int roundId)
        {
            ExecuteStatement($"UPDATE event_layout SET event_layout_status_type = 'Incomplete' WHERE round_id = {roundId}");
        }

        public static void UpdateEventContestantStatus(ContestantEntity contestantEntity)
        {
            ExecuteStatement($"UPDATE event_contestant SET contestant_status_type = '{contestantEntity.ContestantStatusType}' WHERE contestant_id = {contestantEntity.Id}");
        }

        public static bool IsIncompleteRoundContestantFound(int roundId)
        {
            return QueryEntityExistence($"SELECT round_id FROM round_contestant WHERE round_contestant_status_type = 'Incomplete' AND round_id = {roundId}");
        }

        public static bool IsIncompleteEventLayoutFound(int eventId)
        {
            return QueryEntityExistence($"SELECT event_id FROM event_layout WHERE event_id = {eventId} AND event_layout_status_type = 'Incomplete'");
        }

        public static bool IsIncompleteEventContestantResultNotFound(int eventId)
        {
            return !IsIncompleteEventContestantResultFound(eventId);
        }

        public static bool IsIncompleteEventContestantResultFound(int eventId)
        {
            return QueryEntityExistence($"SELECT COUNT(event_id) as count FROM event_layout WHERE event_id = {eventId} AND event_layout_status_type = 'Incomplete'");
        }

        public static bool IsResourceFound(string path)
        {
            return QueryEntityExistence($"SELECT * FROM resource WHERE path = '{path}'");
        }

        public static bool IsResourceNotFound(string path)
        {
            return !IsResourceFound(path);
        }

        public static void CreateNewUser(UserEntity entity)
        {
            ExecuteStatement($"INSERT INTO user (email, full_name, password, user_role_type, description, image_resource_path) VALUES ('{entity.Email}', '{entity.FullName}', '{entity.Password}', '{entity.UserRoleType}', '{entity.Description}', '{entity.ImageResourcePath}')");
        }

        public static void CreateNewResource(string path)
        {
            ExecuteStatement($"INSERT INTO resource (path) VALUES ('{path}')");
        }

        public static void CreateNewResult(int eventId, int segmentId, int roundId, int criteriumId, int contestantId, string judgeUserEmail)
        {
            ExecuteStatement($"INSERT INTO result(event_id, segment_id, round_id, criterium_id, contestant_id, judge_user_email) VALUES({eventId}, {segmentId}, {roundId}, {criteriumId}, {contestantId}, '{judgeUserEmail}')");
        }

        public static void UpdateOldUser(UserEntity entity)
        {
            if (IsResourceNotFound(entity.ImageResourcePath))
            {
                CreateNewResource(entity.ImageResourcePath);
            }

            ExecuteStatement($"UPDATE user SET full_name = '{entity.FullName}', description = '{entity.Description}', image_resource_path = '{entity.ImageResourcePath}' WHERE email = '{entity.Email}'");
        }

        public static void UpdateContestantJudgeCriteriumEntity(JudgeCriteriumEntity judgeCriteriumEntity, string judgeUserEmail)
        {
            ExecuteStatement($"UPDATE result SET base_value = {judgeCriteriumEntity.Result.BaseValue} WHERE criterium_id = {judgeCriteriumEntity.Result.CriteriumId} AND judge_user_email = '{judgeUserEmail}' AND contestant_id = {judgeCriteriumEntity.Result.ContestantId}");
        }

        public static void UpdateRoundContestantStatusEntityToComplete(JudgeCriteriumEntity judgeCriteriumEntity, string judgeUserEmail)
        {
            ExecuteStatement($"UPDATE round_contestant SET round_contestant_status_type = 'Complete' WHERE round_id = {judgeCriteriumEntity.Result.RoundId} AND judge_user_email = '{judgeUserEmail}' AND contestant_id = {judgeCriteriumEntity.Result.ContestantId}");
        }

        public static void UpdateRoundContestantStatusToComplete(int roundId, int contestantId)
        {
            ExecuteStatement($"UPDATE round_contestant SET round_contestant_status_type = 'Complete' WHERE round_id = {roundId} AND contestant_id = {contestantId}");
        }

        public static void UpdateRoundContestantStatusToPending(int roundId, int contestantId)
        {
            ExecuteStatement($"\r\nUPDATE round_contestant\r\nSET round_contestant_status_type = 'Pending'\r\nWHERE round_id = {roundId} AND contestant_id = {contestantId}\r\n");
        }

        private static HashSet<object> QueryUniqueTypes(string statement)
        {
            Result result = ExecuteStatement(statement);
            if (!result.IsSuccessful)
            {
                throw new Exception(result.Message);
            }

            HashSet<object> values = new HashSet<object>();
            foreach (Dictionary<object, object> row in result.Data)
            {
                values.Add(row["type"]);
            }
            return values;
        }

        private static Dictionary<object, object> QueryKeyValuePairs(string statement, string keyAttribute = "key", string valueAttribute = "value")
        {
            Result result = ExecuteStatement(statement);
            if (!result.IsSuccessful)
            {
                throw new Exception(result.Message);
            }

            Dictionary<object, object> values = new Dictionary<object, object>();
            foreach (Dictionary<object, object> row in result.Data)
            {
                values[row[keyAttribute]] = row[valueAttribute];
            
            }
            return values;
        }

        private static Dictionary<object, object> QueryOneEntity(string statement)
        {
            Result result = ExecuteStatement(statement);
            if (!result.IsSuccessful)
            {
                throw new Exception(result.Message);
            }
            if (result.Data.Count == 0)
            {
                return null;
            }

            return result.Data[0];
        }

        private static List<Dictionary<object, object>> QueryManyEntities(string statement)
        {
            Result result = ExecuteStatement(statement);
            if (!result.IsSuccessful)
            {
                throw new Exception(result.Message);
            }
            if (result.Data.Count == 0)
            {
                return new List<Dictionary<object, object>>();
            }

            return result.Data;
        }

        private static bool QueryEntityExistence(string statement)
        {
            Result result = ExecuteStatement(statement);
            if (!result.IsSuccessful)
            {
                throw new Exception(result.Message);
            }

            return result.Data.Count != 0;
        }

        private static string GenerateStringQueryStatement(string table, string attribute, string filterStatement = "", List<string> keywords = default, HashSet<string> blacklist = default)
        {
            if (keywords == null || keywords.Count == 0)
            {
                return $"SELECT * FROM {table}";
            }
            string statement = $"SELECT * FROM (SELECT * FROM {table} WHERE {attribute} ";
            statement += "LIKE ";
            for (int index = 0; index < keywords.Count; index++)
            {
                statement += $"'%{keywords[index]}%'";
                if (index < keywords.Count - 1)
                {
                    statement += " OR ";
                }
            }
            if (!string.IsNullOrEmpty(filterStatement))
            {
                statement += $" AND {filterStatement}";
            }
            statement += ") query ";
            if (blacklist == null || blacklist.Count == 0)
            {
                return statement;
            }
            statement += $"WHERE query.{attribute} NOT LIKE";
            int blacklistIndex = 0;
            foreach (string value in blacklist)
            {
                statement += $"'{value}'";
                if (blacklistIndex < blacklist.Count - 1)
                {
                    statement += " OR ";
                }
                blacklistIndex++;
            }
            
            return statement;
        }
    }
}
