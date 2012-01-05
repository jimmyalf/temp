using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.UI.Mvc.Site.App
{
    public class Globals : ConfigurationFetch
    {
        private const string Section = "synologen";

        public static string GoogleGeocode
        {
            get { return SafeConfigString(Section, "GoogleGeocode", string.Empty); }
        }
    }
}