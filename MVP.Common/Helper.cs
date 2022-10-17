using System;
using System.Collections.Generic;
using System.Text;

namespace MVP.Common
{
    public class Helper
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
