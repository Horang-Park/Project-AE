using System;
using System.ComponentModel;
using System.Reflection;

namespace Utilities
{
    public static class EnumToDescription
    {
        public static string ToDescription(this Enum source)
        {
            var fi = source.GetType().GetField(source.ToString());
            var att = (DescriptionAttribute)fi.GetCustomAttribute(typeof(DescriptionAttribute));

            return att != null ? att.Description : source.ToString();
        }
    }
}