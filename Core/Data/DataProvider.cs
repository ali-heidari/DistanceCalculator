using System.Collections.Generic;
using System.Reflection;
using System;
using Newtonsoft.Json;
using System.Linq;

namespace Core.Data
{
    public class DataProvider
    {
        private Dictionary<string, Dictionary<Guid, string>> _data;
        private static DataProvider _dp;
        private DataProvider() { }

        public static DataProvider DataProviderFactory()
        {
            if (_dp == null)
                _dp = new DataProvider();
            return _dp;
        }


        public bool Insert(DataObject obj)
        {
            try
            {
                Dictionary<Guid, string> rows = null;
                if (!_data.ContainsKey(obj.DocumentName))
                    _data.Add(obj.DocumentName, new Dictionary<Guid, string>());

                rows = _data[obj.DocumentName];

                rows.Add(Guid.NewGuid(), JsonConvert.SerializeObject(obj));

                return true;
            }
            catch (Exception er)
            {
                return false;
            }
        }

        public T GetData<T>(string docName, Dictionary<string, object> fields) where T : DataObject
        {
            try
            {
                Dictionary<Guid, string> rows = null;
                if (!_data.ContainsKey(docName))
                    return null;

                rows = _data[docName];
                bool match = false;
                T t = default;
                foreach (var item in rows)
                {
                    t = (T)JsonConvert.DeserializeObject(item.Value);
                    foreach (var field in fields)
                    {
                        if (t.GetType().GetProperties().FirstOrDefault(x => x.Name == field.Key) != null)
                        {
                            match = true;
                        }
                    }
                    if (match)
                        break;
                }
                return t;
            }
            catch (Exception er)
            {
                return null;
            }
        }
        public T GetData<T>(string docName, Guid guid) where T : DataObject
        {

            try
            {
                Dictionary<Guid, string> rows = null;
                if (!_data.ContainsKey(docName))
                    return null;

                rows = _data[docName];

                return (T)JsonConvert.DeserializeObject(rows[guid]);
            }
            catch (Exception er)
            {
                return null;
            }
        }
    }
}