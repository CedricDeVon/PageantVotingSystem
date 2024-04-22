
using System;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Results;
using PageantVotingSystem.Sources.Entities;
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
            
            CurrentSettings = databaseSettings;
            Result result = ExecuteFile(databaseSettings.SetupFilePath);
            if (!result.IsSuccessful)
            {
                throw new Exception(result.Message);
            }

            SetupRecorder.Add("ApplicationDatabase");
        }

        public static HashSet<object> ReadJudgeStatus()
        {
            return QueryUniqueTypes("SELECT type FROM judge_status");
        }

        public static HashSet<object> ReadRoundStatus()
        {
            return QueryUniqueTypes("SELECT type FROM scoring_system");
        }

        public static HashSet<object> ReadContestantStatus()
        {
            return QueryUniqueTypes("SELECT type FROM contestant_status");
        }

        public static HashSet<object> ReadScoringSystems()
        {
            return QueryUniqueTypes("SELECT type FROM scoring_system");
        }

        public static HashSet<object> ReadResultRemarks()
        {
            return QueryUniqueTypes("SELECT type FROM result_remark");
        }

        public static HashSet<object> ReadUserRoles()
        {
            return QueryUniqueTypes("SELECT type FROM user_role");
        }

        public static HashSet<object> ReadMaritalStatus()
        {
            return QueryUniqueTypes("SELECT type FROM marital_status");
        }

        public static HashSet<object> ReadGenders()
        {
            return QueryUniqueTypes("SELECT type FROM gender");
        }
        
        public static HashSet<object> ReadResources()
        {
            return QueryUniqueTypes("SELECT path as type FROM resource");
        }

        public static Dictionary<object, object> ReadTypeConstraints()
        {
            return QueryKeyValuePairs("SELECT name AS 'key', value FROM type_constraint");
        }

        public static Dictionary<object, object> ReadOneUser(string email)
        {
            return QueryOneEntity($"SELECT email, password, full_name, user_role_type, description, image_resource_path FROM user WHERE email = '{email}'");
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

        public static List<string> ReadManyEventNames(string eventName = "", string eventManagerEmail = "")
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT name FROM event INNER JOIN event_manager ON id = event_id WHERE name LIKE '%{eventName}%' AND manager_user_email LIKE '%{eventManagerEmail}%'");
            List<string> eventNames = new List<string>();
            foreach (Dictionary<object, object> result in results)
            {
                eventNames.Add((string) result["name"]);
            }
            return eventNames;
        }

        public static List<EventEntity> ReadManyEvents(string eventName = "", string eventManagerEmail = "")
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT id, name FROM event INNER JOIN event_manager ON id = event_id WHERE name LIKE '%{eventName}%' AND manager_user_email LIKE '%{eventManagerEmail}%'");
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

        public static void ReadOneEventLayout(EventEntity eventEntity)
        {
            List<SegmentEntity> segmentEntities = ReadManySegments(eventEntity.Id);
            foreach (SegmentEntity segmentEntity in segmentEntities)
            {
                ReadOneSegmentLayout(segmentEntity);
                eventEntity.Segments.AddNewItem(segmentEntity);
            }
        }

        private static void ReadOneSegmentLayout(SegmentEntity segmentEntity)
        {
            List<RoundEntity> roundEntities = ReadManyRounds(segmentEntity.Id);
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

        public static List<SegmentEntity> ReadManySegments(int eventId)
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

        public static EventEntity ReadOneEvent(int eventId)
        {
            Dictionary<object, object> results = QueryOneEntity(
                $"SELECT name, scoring_system_type, host_address, datetime_start, datetime_end, manager_user_email, description FROM event INNER JOIN event_manager ON id = event_id WHERE id = {eventId}");
            EventEntity entity = new EventEntity();
            entity.Name = (string)results["name"];
            entity.HostAddress = (string)results["host_address"];
            entity.ScoringSystemType = (string)results["scoring_system_type"];
            entity.DateTimeStart = ((DateTime) results["datetime_start"]).ToString();
            entity.DateTimeEnd = (results["datetime_end"] is DBNull) ? "" : ((DateTime) results["datetime_end"]).ToString();
            entity.ManagerEmail = (string)results["manager_user_email"];
            entity.Description = (string)results["description"];
            return entity;
        }

        public static SegmentEntity ReadOneSegment(int segmentId)
        {
            Dictionary<object, object> results = QueryOneEntity($"SELECT name, maximum_contestant_count, description FROM segment WHERE id = {segmentId}");
            SegmentEntity entity = new SegmentEntity();
            entity.Name = (string) results["name"];
            entity.MaximumContestantCount = Convert.ToInt32(results["maximum_contestant_count"]);
            entity.Description = (string)results["description"];
            return entity;
        }

        public static RoundEntity ReadOneRound(int roundId)
        {
            Dictionary<object, object> results = QueryOneEntity($"SELECT name, description FROM round WHERE id = {roundId}");
            RoundEntity entity = new RoundEntity();
            entity.Name = (string)results["name"];
            entity.Description = (string)results["description"];
            return entity;
        }

        public static CriteriumEntity ReadOneCriterium(int criteriumId)
        {
            Dictionary<object, object> results = QueryOneEntity($"SELECT name, description, maximum_value, percentage_weight FROM criterium WHERE id = {criteriumId}");
            CriteriumEntity entity = new CriteriumEntity();
            entity.Name = (string)results["name"];
            entity.Description = (string)results["description"];
            entity.MaximumValue = (float) Convert.ToDouble(results["maximum_value"]);
            entity.PercentageWeight = (float)Convert.ToDouble(results["percentage_weight"]);
            return entity;
        }

        public static ContestantEntity ReadOneContestant(int contestantId)
        {
            Dictionary<object, object> results = QueryOneEntity($"SELECT full_name, order_number, image_resource_path FROM contestant WHERE id = {contestantId}");
            ContestantEntity entity = new ContestantEntity();
            entity.FullName = (string)results["full_name"];
            entity.OrderNumber = Convert.ToInt32(results["order_number"]);
            entity.ImageResourcePath = (results["image_resource_path"] is DBNull) ?
                ApplicationConfiguration.DefaultUserProfileImagePath :
                (string)results["image_resource_path"];
            return entity;
        }

        public static List<RoundEntity> ReadManyRounds(int segmentId)
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

        public static List<UserEntity> ReadManyUsers(int eventId)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT email, full_name, user_role_type, image_resource_path FROM event_judge INNER JOIN user ON email = judge_user_email WHERE event_id = {eventId}");
            List<UserEntity> entities = new List<UserEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                UserEntity entity = new UserEntity();
                entity.Email = (string)result["email"];
                entity.FullName = (string)result["full_name"];
                entity.UserRoleType = (string)result["user_role_type"];
                entity.ImageResourcePath = (string)result["image_resource_path"];
                entities.Add(entity);
            }
            return entities;
        }

        public static List<ContestantEntity> ReadManyEventContestants(int eventId)
        {
            List<Dictionary<object, object>> results = QueryManyEntities($"SELECT id, full_name, order_number FROM contestant INNER JOIN event_contestant ON id = contestant_id WHERE event_id = {eventId} ORDER BY order_number DESC");
            List<ContestantEntity> entities = new List<ContestantEntity>();
            foreach (Dictionary<object, object> result in results)
            {
                ContestantEntity entity = new ContestantEntity(
                    Convert.ToInt32(result["id"]),
                    (string) result["full_name"],
                    Convert.ToInt32(result["order_number"]));
                entities.Add(entity);
            }
            return entities;
        }

        public static List<ContestantResultEntity> ReadManyCriteriumResults(int id)
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

        public static bool IsUserEmailFound(string email)
        {
            return QueryEntityExistence($"SELECT email FROM user WHERE email = '{email}'");
        }

        public static bool IsUserEmailNotFound(string email)
        {
            return !IsUserEmailFound(email);
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

        public static void UpdateOldUser(UserEntity entity)
        {
            if (IsResourceNotFound(entity.ImageResourcePath))
            {
                CreateNewResource(entity.ImageResourcePath);
            }

            ExecuteStatement($"UPDATE user SET full_name = '{entity.FullName}', description = '{entity.Description}', image_resource_path = '{entity.ImageResourcePath}' WHERE email = '{entity.Email}'");
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
