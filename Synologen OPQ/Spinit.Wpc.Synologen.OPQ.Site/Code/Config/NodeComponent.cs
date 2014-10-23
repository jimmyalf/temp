using System.Configuration;

namespace Spinit.Wpc.Synologen.OPQ.Site.Code.Config
{
    public class NodeComponent : ConfigurationElement
    {
        [ConfigurationProperty("id", IsRequired = true)]
        public int Id
        {
            get { return (int)this["id"]; }
            set { this["id"] = value; }
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return this["name"] as string; }
            set { this["name"] = value; }
        }
    }
}