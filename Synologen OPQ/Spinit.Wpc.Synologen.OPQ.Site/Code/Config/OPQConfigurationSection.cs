using System.Configuration;

namespace Spinit.Wpc.Synologen.OPQ.Site.Code.Config
{
    public class OPQConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("nodes")]
        public NodeComponents Nodes
        {
            get { return this["nodes"] as NodeComponents; }
            set { this["nodes"] = value; }
        }

        public static OPQConfigurationSection GetInstance()
        {
            return ConfigurationManager.GetSection("OPQConfiguration") as OPQConfigurationSection;
        }
    }
}