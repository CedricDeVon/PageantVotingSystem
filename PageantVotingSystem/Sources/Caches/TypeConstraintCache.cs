
using System;
using System.Linq;
using System.Collections.Generic;

using PageantVotingSystem.Sources.Setups;
using PageantVotingSystem.Sources.Loggers;

namespace PageantVotingSystem.Sources.Caches
{
    public class TypeConstraintCache : Cache
    {
        public static int MinimumPercentageWeight
        {
            get { return GetType<int>("minimum_percentage_weight"); }

            private set { }
        }

        public static int MaximumPercentageWeight
        {
            get { return GetType<int>("maximum_percentage_weight"); }

            private set { }
        }

        public static int MinimumJudgeRemarkValue
        {
            get { return GetType<int>("minimum_judge_remark_value"); }

            private set { }
        }

        public static int MaximumJudgeRemarkValue
        {
            get { return GetType<int>("maximum_judge_remark_value"); }

            private set { }
        }

        public static int MinimumOrderNumber
        {
            get { return GetType<int>("minimum_order_number"); }

            private set { }
        }

        public static int MaximumOrderNumber
        {
            get { return GetType<int>("maximum_order_number"); }

            private set { }
        }

        public static int MinimumHeightInCentimeters
        {
            get { return GetType<int>("minimum_height_in_centimeters"); }

            private set { }
        }

        public static int MaximumHeightInCentimeters
        {
            get { return GetType<int>("maximum_height_in_centimeters"); }

            private set { }
        }

        public static int MinimumWeightInKilograms
        {
            get { return GetType<int>("minimum_weight_in_kilograms"); }

            private set { }
        }

        public static int MaximumWeightInKilograms
        {
            get { return GetType<int>("maximum_weight_in_kilograms"); }

            private set { }
        }

        public static int MinimumTextCharacterLength
        {
            get { return GetType<int>("minimum_text_character_length"); }

            private set { }
        }

        public static int MaximumTextCharacterLength
        {
            get { return GetType<int>("maximum_text_character_length"); }

            private set { }
        }

        public static int MinimumEmailCharacterLength
        {
            get { return GetType<int>("minimum_email_character_length"); }

            private set { }
        }

        public static int MaximumEmailCharacterLength
        {
            get { return GetType<int>("maximum_email_character_length"); }
            
            private set { } 
        }

        public static HashSet<char> ValidEmailCharacters
        {
            get { return GetType<HashSet<char>>("valid_email_characters").ToHashSet(); }

            private set { }
        }

        public static int MinimumPersonNameCharacterLength
        {
            get { return GetType<int>("minimum_person_name_character_length"); }

            private set { }
        }

        public static int MaximumPersonNameCharacterLength
        {
            get { return GetType<int>("maximum_person_name_character_length"); }

            private set { }
        }

        public static HashSet<char> ValidPersonNameCharacters
        {
            get { return GetType<HashSet<char>>("valid_name_characters").ToHashSet(); }

            private set { }
        }

        public static int MinimumPasswordCharacterLength
        {
            get { return GetType<int>("minimum_password_character_length"); }

            private set { }
        }

        public static int MaximumPasswordCharacterLength
        {
            get { return GetType<int>("maximum_password_character_length"); }

            private set { }
        }

        public static HashSet<char> ValidPasswordCharacters
        {
            get { return GetType<HashSet<char>>("valid_password_characters").ToHashSet(); }

            private set { }
        }

        public static HashSet<object> Names { get { return data.Keys.ToHashSet(); } }

        private static Dictionary<object, object> data;
        
        public static void Setup()
        {
            SetupRecorder.ThrowIfAlreadySetup("TypeConstraintCache");
            ApplicationLogger.LogInformationMessage("'EditEventContestantCache' setup began");

            data = new Dictionary<object, object>();
            Data.SetDataToPrivate("TypeConstraintCache", data);

            data["minimum_percentage_weight"] = 0;
            data["maximum_percentage_weight"] = 100;

            data["minimum_judge_remark_value"] = 0;
            data["maximum_judge_remark_value"] = 1000;

            data["minimum_order_number"] = 1;
            data["maximum_order_number"] = 1000;

            data["minimum_height_in_centimeters"] = 1;
            data["maximum_height_in_centimeters"] = 1000;

            data["minimum_weight_in_kilograms"] = 1;
            data["maximum_weight_in_kilograms"] = 1000;
            
            data["minimum_text_character_length"] = 0;
            data["maximum_text_character_length"] = 1024;

            data["minimum_email_character_length"] = 5;
            data["maximum_email_character_length"] = 128;
            data["valid_email_characters"] = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890_.@".ToHashSet();

            data["minimum_person_name_character_length"] = 1;
            data["maximum_person_name_character_length"] = 128;
            data["valid_person_name_characters"] = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890.,_ ".ToHashSet();

            data["minimum_password_character_length"] = 8;
            data["maximum_password_character_length"] = 32;
            data["valid_password_characters"] = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890`-=[]\\;,.<>?:{}|~!@#$%^&*()_+".ToHashSet();

            SetupRecorder.Add("TypeConstraintCache");
            ApplicationLogger.LogInformationMessage("'EditEventContestantCache' setup complete");
        }

        public static bool IsValidPercentageWeight(int value)
        {
            return MinimumPercentageWeight < value && value <= MaximumPercentageWeight;
        }

        public static bool IsValidJudgeRemarkValue(int value)
        {
            return MinimumJudgeRemarkValue <= value && value <= MaximumJudgeRemarkValue;
        }

        public static bool IsValidOrderNumber(int value)
        {
            return MinimumOrderNumber <= value && value <= MaximumOrderNumber;
        }

        public static bool IsValidHeightInCentimeters(int value)
        {
            return MinimumHeightInCentimeters <= value && value <= MaximumHeightInCentimeters;
        }

        public static bool IsValidWeightInKilograms(int value)
        {
            return MinimumWeightInKilograms <= value && value <= MaximumWeightInKilograms;
        }

        public static bool IsValidTextCharacterLength(string value)
        {
            return MinimumTextCharacterLength <= value.Length && value.Length <= MaximumTextCharacterLength;
        }

        public static bool IsValidEmailCharacterLength(string value)
        {
            return MinimumEmailCharacterLength <= value.Length && value.Length <= MaximumEmailCharacterLength;
        }

        public static bool IsValidEmailCharacter(char value)
        {
            return GetType<HashSet<char>>("valid_email_characters").Contains(value);
        }

        public static bool IsValidPersonNameCharacterLength(string value)
        {
            return MinimumPersonNameCharacterLength <= value.Length && value.Length <= MaximumPersonNameCharacterLength;
        }

        public static bool IsValidPersonNameCharacter(char value)
        {
            return GetType<HashSet<char>>("valid_person_name_characters").Contains(value);
        }

        public static bool IsValidPasswordCharacterLength(string value)
        {
            return MinimumPasswordCharacterLength <= value.Length && value.Length <= MaximumPasswordCharacterLength;
        }

        public static bool IsValidPasswordCharacter(char value)
        {
            return GetType<HashSet<char>>("valid_password_characters").Contains(value);
        }

        public static bool IsInvalidPercentageWeight(int value)
        {
            return !IsValidPercentageWeight(value);
        }

        public static bool IsInvalidJudgeRemarkValue(int value)
        {
            return !IsValidJudgeRemarkValue(value);
        }

        public static bool IsInvalidOrderNumber(int value)
        {
            return !IsValidOrderNumber(value);
        }

        public static bool IsInvalidHeightInCentimeters(int value)
        {
            return !IsValidHeightInCentimeters(value);
        }

        public static bool IsInvalidWeightInKilograms(int value)
        {
            return !IsValidWeightInKilograms(value);
        }

        public static bool IsInvalidTextCharacterLength(string value)
        {
            return !IsValidTextCharacterLength(value);
        }

        public static bool IsInvalidEmailCharacterLength(string value)
        {
            return !IsValidEmailCharacterLength(value);
        }

        public static bool IsInvalidEmailCharacter(char value)
        {
            return !IsValidEmailCharacter(value);
        }
        
        public static bool IsInvalidPersonNameCharacterLength(string value)
        {
            return !IsValidPersonNameCharacterLength(value);
        }

        public static bool IsInvalidPersonNameCharacter(char value)
        {
            return !IsValidPersonNameCharacter(value);
        }

        public static bool IsInvalidPasswordCharacterLength(string value)
        {
            return !IsValidPasswordCharacterLength(value);
        }

        public static bool IsInvalidPasswordCharacter(char value)
        {
            return !IsValidPasswordCharacter(value);
        }

        public static bool IsFound(string name)
        {
            return data.ContainsKey(name);
        }

        public static bool IsNotFound(string name)
        {
            return !IsFound(name);
        }

        private static Type GetType<Type>(string key)
        {
            return (Type)data[key];
        }
    }
}
