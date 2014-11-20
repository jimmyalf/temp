using System;

namespace Spinit.Wpc.Synologen.Core.Attributes
{
    public class EnumDisplayNameAttribute : Attribute {

        public string DisplayName { get; protected set; }
        public EnumDisplayNameAttribute(string value) {
            DisplayName = value;
        }
    }
}