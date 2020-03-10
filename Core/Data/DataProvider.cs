using System.Collections.Generic;
using System.Reflection;
using System;
using Newtonsoft.Json;
using System.Linq;

namespace Core.Data
{
    /// <summary>
    /// Database layer
    /// </summary>
    public class DataProvider
    {
        public Dictionary<string, Dictionary<Guid, string>> _data;
        private static DataProvider _dp;
        private DataProvider()
        {
            _data = new Dictionary<string, Dictionary<Guid, string>>();
        }

        /// <summary>
        /// Ensure there is only one object of database through app
        /// </summary>
        /// <returns>Returns DatabasProvider</returns>
        public static DataProvider DataProviderFactory()
        {
            if (_dp == null)
                _dp = new DataProvider();
            return _dp;
        }

        /// <summary>
        /// Insert into database
        /// </summary>
        /// <param name="obj">Dataobject to be inserted</param>
        /// <returns>Returns true if successful otherwise false</returns>
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

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="email">email of user</param>
        /// <param name="password">password of user</param>
        /// <returns>Returns user</returns>
        public User GetUser(string email, string password)
        {
            try
            {
                Dictionary<Guid, string> rows = null;
                if (!_data.ContainsKey("User"))
                    return null;

                rows = _data["User"];

                User user = null;
                foreach (var item in rows)
                {
                    user = JsonConvert.DeserializeObject<User>(item.Value);
                    user.guid = item.Key;
                    if (user.email == email && user.password == password)
                        return user;
                }
                return null;
            }
            catch (Exception er)
            {
                return null;
            }
        }
        /// <summary>
        /// Get record by its guid
        /// </summary>
        /// <param name="docName">Name of document</param>
        /// <param name="guid">the guid</param>
        /// <typeparam name="T">Type of object which is inherited from DataObject</typeparam>
        /// <returns>Returns object otherwise null</returns>
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