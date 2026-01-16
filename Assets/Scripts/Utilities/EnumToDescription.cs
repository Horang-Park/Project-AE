using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Utilities
{
    public static class EnumToDescription
    {
        private static readonly Dictionary<Enum, string> Cache = new();

        public static string ToDescription(this Enum source)
        {
            if (source == null)
            {
                return string.Empty;
            }

            if (Cache.TryGetValue(source, out var cachedDescription))
            {
                return cachedDescription;
            }

            var fieldInfo = source.GetType().GetField(source.ToString());

            if (fieldInfo == null)
            {
                var fallback = source.ToString();

                Cache[source] = fallback;

                return fallback;
            }

            var attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
            var description = attribute?.Description ?? source.ToString();

            Cache[source] = description;

            return description;
        }
    }
}