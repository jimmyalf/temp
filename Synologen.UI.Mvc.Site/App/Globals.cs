using System;
using System.Configuration;

namespace Spinit.Wpc.Synologen.UI.Mvc.Site.App
{
    public class Globals
    {
        private const string Section = "synologen";

        public static string GoogleGeocode
        {
            get
            {
                var section = (AppSettingsSection)ConfigurationManager.GetSection(Section);
                var setting = section.Settings["GoogleGeocode"];
                if (setting != null)
                {
                    return setting.Value;
                }
                throw new ArgumentNullException("Parameter GoogleGeocode could not be found");
            }
        }
    }
}