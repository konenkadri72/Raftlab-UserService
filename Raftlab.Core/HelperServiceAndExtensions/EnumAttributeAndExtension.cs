using System;
using System.Collections.Generic;
using System.Text;

namespace Raftlab.Core.HelperServiceAndExtensions
{
    public class CodeNameAttribute : System.Attribute
    {
        public string DisplayName { get; set; }
        public CodeNameAttribute(string name)
        {
            this.DisplayName = name;
        }
    }

    public static class EnumExtension
    {
        public static string GetCodeName<TEnum>(this TEnum enumObj)
        {
            if (enumObj != null)
            {
                var fi = enumObj.GetType().GetField(enumObj.ToString());
                var attributes = (CodeNameAttribute[])fi.GetCustomAttributes(typeof(CodeNameAttribute), false);
                if (attributes.Length > 0)
                {
                    return attributes[0].DisplayName;
                }
                else
                {
                    return enumObj.ToString();
                }
            }
            else
            {
                return null;
            }
        }
    }
}
