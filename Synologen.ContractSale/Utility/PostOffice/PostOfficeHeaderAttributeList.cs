using System.Collections.Generic;
using System.Linq;

namespace Spinit.Wpc.Synologen.Invoicing.PostOffice
{
    public class PostOfficeHeaderAttributeList
    {
        protected IList<PostOfficeHeaderAttribute> Attributes { get; set; }

        public PostOfficeHeaderAttributeList()
        {
            Attributes = new List<PostOfficeHeaderAttribute>();
        }

        public void Add(string key, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            Attributes.Add(new PostOfficeHeaderAttribute { Key = key, Value = value });
        }

        public string Render()
        {
            return string.Join(" ", Attributes.Select(x => x.Render()));
        }
    }
}