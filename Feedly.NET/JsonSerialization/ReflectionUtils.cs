using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedly.NET.JsonSerialization
{
    internal static class ReflectionUtils
    {
        public static bool IsNullableType(Type t)
        {
            return (t.IsGenericType && (t.GetGenericTypeDefinition() == typeof(Nullable<>)));
        }
    }
}
