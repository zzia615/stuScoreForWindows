using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace stuScore
{
    public static class Ext
    {
        public static string AsString(this object obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }
    }
}
