using System.Web.Mvc;

namespace Spinit.Wpc.Synologen.UI.Mvc.Site
{
    public class SynologenAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "WpcSynologenRoute",
                "WpcSynologen/{action}/{*actionParameters}",
                new { controller = "WpcSynologen", action = "Index", actionParameters = "" }
                );
        }

        public override string AreaName
        {
            get { return "WpcSynologen"; }
        }
    }
}