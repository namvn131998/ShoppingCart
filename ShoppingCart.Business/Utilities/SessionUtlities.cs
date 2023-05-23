using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.DataAccess.Helper;

namespace ShoppingCart.Business.Utilities
{
    public static class SessionUtilities
    {
        public static string SessionCurrentUserkey = "CurrentUser";
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }

        public static void SetInt(this ISession session, string key, int value)
        {
            session.SetInt32(key, value);
        }

        public static int? GetInt(this ISession session, string key)
        {
            return session.GetInt32(key);
        }

        public static void RemoveKey(this ISession session, string key)
        {
            session.Remove(key);
        }
        #region user
        public static bool IsLogged(this ISession session)
        {
            return session.Get(SessionCurrentUserkey) != null;
        }
        public static LoggedUser CurrentUser(this ISession session)
        {
            return IsLogged(session) ? session.Get<LoggedUser>(SessionCurrentUserkey) : null;
        }
        #endregion
    }
}

