using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Enum.Extensions;

namespace Enums.App
{
    class Program
    {
        public enum EnumDataTest
        {
            [Description("undefined value")]
            undefined = 0,

            [Description("unset value")]
            unset = 1,

            [Description("value was set")]
            set = 2
        }
        static void Main(string[] args)
        {
            List<string> enumDescription = new List<string>();

            for (int i = 0; i < 3; i++)
            {
                var enumData = (EnumDataTest)i;
                enumDescription.Add(enumData.ToDescription<EnumDataTest>());
            }

            List<EnumDataTest> enumValue = new List<EnumDataTest>();

            foreach (string enumDataTest in enumDescription)
            {
                EnumDataTest value = EnumExtension.EnumFromDescription<EnumDataTest>(enumDataTest);
                enumValue.Add(value);
            }

            for (int i = 0; i < 3; i++)
            {
                  var enumData = (EnumDataTest)i;
                  Console.WriteLine($"{enumData.ToDescription()} == {enumValue[i]}");
            }

        }


    }

}
