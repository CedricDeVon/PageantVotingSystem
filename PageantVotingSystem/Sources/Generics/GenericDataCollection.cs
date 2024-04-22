
using System;
using System.Collections.Generic;

namespace PageantVotingSystem.Sources.Generics
{
    public class GenericDataCollection : Generic
    {
        public GenericDataCollection() : base(new Dictionary<object, object>())
        {
            Dictionary<object, object> data = (Dictionary<object, object>) Data;
            data["Public"] = new Dictionary<object, object>();
            data["Private"] = new Dictionary<object, object>();
        }

        public void SetPublicData(object key, object value)
        {
            if (getDataCollection("Private").ContainsKey(key))
            {
                throw new Exception($"'GenericDataCollection' - Attribute '{key}' already exists");
            }
            try
            {
                getDataCollection("Public")[key] = value;
            }
            catch
            {
                throw new Exception($"'GenericDataCollection' - Data cannot be set at '{key}' key");
            }
        }

        public void SetPrivateData(object key, object value)
        {
            if (getDataCollection("Private").ContainsKey(key))
            {
                throw new Exception($"'GenericDataCollection' - Cannot set '{key}' as a attribute");
            }
            try
            {
                getDataCollection("Private")[key] = value;
            }
            catch
            {
                throw new Exception($"'GenericDataCollection' - Data cannot be set at '{key}' key");
            }
        }

        public GenericData GetData(object key)
        {
            Dictionary<object, object> dataCollection = getDataCollectionAttribute(key);
            try
            {
                return new GenericData(dataCollection[key]);
            }
            catch
            {
                throw new Exception($"'GenericDataCollection' - Data cannot be read at '{key}' key");
            }
        }

        public Type GetData<Type>(object key)
        {
            Dictionary<object, object> dataCollection = getDataCollectionAttribute(key);
            try
            {
                return (Type) dataCollection[key];
            }
            catch
            {
                throw new Exception($"'GenericDataCollection' - Data cannot be read at '{key}' key");
            }
        }

        public void RemoveData(object key)
        {
            Dictionary<object, object> dataCollection = getDataCollectionAttribute(key);
            try
            {
                dataCollection.Remove(key);
            }
            catch
            {
                throw new Exception($"'GenericDataCollection' - Data cannot be removed at '{key}' key"); ;
            }
        }

        public void ClearPublicData()
        {
            try
            {
                getDataCollection("Public").Clear();
            }
            catch
            {
                throw new Exception($"'GenericDataCollection' - Invalid clear operation");
            }
        }

        public void ClearPrivateData()
        {
            try
            {
                getDataCollection("Private").Clear();
            }
            catch
            {
                throw new Exception($"'GenericDataCollection' - Invalid clear operation");
            }
        }

        public void ClearAllData()
        {
            try
            {
                ClearPublicData();
                ClearPrivateData();
            }
            catch
            {
                throw new Exception($"'GenericDataCollection' - Invalid clear operation");
            }
        }

        public bool IsPublicDataFound(object key)
        {
            try
            {
                return getDataCollection("Public").ContainsKey(key);
            }
            catch
            {
                throw new Exception($"'GenericDataCollection' - Data cannot be searched at '{key}' key");
            }
        }

        public bool IsPrivateDataFound(object key)
        {
            try
            {
                return getDataCollection("Private").ContainsKey(key);
            }
            catch
            {
                throw new Exception($"'GenericDataCollection' - Data cannot be searched at '{key}' key");
            }
        }
        
        public bool IsDataFound(object key)
        {
            try
            {
                return IsPublicDataFound(key) || IsPrivateDataFound(key);
            }
            catch
            {
                throw new Exception($"'GenericDataCollection' - Data cannot be searched at '{key}' key");
            }
        }

        private Dictionary<object, object> getDataCollectionAttribute(object key)
        {
            if (getDataCollection("Public").ContainsKey(key))
            {
                return getDataCollection("Public");
            }
            else if (getDataCollection("Private").ContainsKey(key))
            {
                return getDataCollection("Private");
            }
            else
            {
                throw new Exception($"'GenericDataCollection' - Attribute '{key}' does not exist");
            }
        }

        private Dictionary<object, object> getDataCollection(string accessor)
        {
            Dictionary<object, object> data = (Dictionary<object, object>)Data;
            if (!data.ContainsKey(accessor))
            {
                throw new Exception($"'GenericDataCollection' - Accessor '{accessor}' does not exist");
            }

            if (accessor == "Public")
            {
                data = (Dictionary<object, object>)data["Public"];
            }
            else if (accessor == "Private")
            {
                data = (Dictionary<object, object>)data["Private"];
            }
            return data;
        }

    }
}
