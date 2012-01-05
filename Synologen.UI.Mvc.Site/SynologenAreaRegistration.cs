using System.Web.Mvc;

namespace Spinit.Wpc.Synologen.UI.Mvc.Site
{
    public class SynologenAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SynologenRoute",
                "Synologen/{action}/{*actionParameters}",
                new { controller = "Synologen", action = "Index", actionParameters = "" }
                );
        }

        public override string AreaName
        {
            get { return "Synologen"; }
        }
    }
}