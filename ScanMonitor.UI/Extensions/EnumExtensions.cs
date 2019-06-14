using System;

namespace ScanMonitor.UI.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value)
            where T: struct
        {
            if (!typeof(T).IsEnum)
                throw new Exception($"{typeof(T).Name} is not an enum type.");

            return Enum.TryParse(value, out T result) ? result : default(T);
        }
    }
}