using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagement.API.Helpers
{
    public static class DBHelper
    {
        public static object NullHandler(object instance)
        {
            if (instance != null)
                return instance;
            return DBNull.Value;
        }
    }
}
