using System;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.UI.Mvc.Site.App
{
    public class Globals : ConfigurationFetch
    {
        private const string Section = "synologen";

        public static string GoogleGeocode
        {
            get { return SafeConfigString(Section, "GoogleGeocode", String.Empty); }
        }

        public static int ViewShopsWithCategoryId
        {
            get { return SafeConfigNumber(Section, "ViewShopsWithCategoryId", 0); }
        }
    }
}