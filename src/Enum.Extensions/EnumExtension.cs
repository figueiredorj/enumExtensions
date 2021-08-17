using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;


namespace Enum.Extensions
{
    public static class EnumExtension
    {
        public static string ToDescription<T>(this T enumerationValue)
            where T : struct
        {

            string description = enumerationValue.ToString();

            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return description;
        }

        public static T EnumFromDescription<T>(string description) where T : System.Enum
        {
            T enumValue = default(T);
            bool found = false;

            foreach (var field in typeof(T).GetFields().Where(hasDescriptionAttribute).AsEnumerable())
            {
                if (!found && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                    {
                        enumValue = (T)field.GetValue(null);
                        found = true;
                    }

                }
                else
                {
                    if (field.Name == description)
                    {
                        enumValue = (T)field.GetValue(null);
                        found = true;
                    }

                }
            }

            if (enumValue == null) throw new Exception($"No value in defined enum of type {typeof(T).FullName}");

            return enumValue;
        }

        private static bool hasDescriptionAttribute(FieldInfo arg)
        {
            return arg.GetCustomAttributes(typeof(DescriptionAttribute)).Any();
        }
    }
}
