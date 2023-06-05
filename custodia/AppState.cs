using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custodia
{
    public static class AppState
    {
        private static Dictionary<string, object> _values =
                   new Dictionary<string, object>();

        public static void SetValue(string key, object value)
        {
            _values.Add(key, value);
        }

        public static dynamic GetValue<T>(string key)
        {
            return _values[key];
        }
    }
}
