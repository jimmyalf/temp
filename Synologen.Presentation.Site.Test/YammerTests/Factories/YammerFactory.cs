using System.IO;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.YammerTests.Factories
{
    public class YammerFactory
    {
        public static string GetJson()
        {
            var text = File.ReadAllText(@"C:\Develop\WPC\Customer Specific\Synologen\trunk\Synologen.Presentation.Site.Test\YammerTests\Factories\messages.json");
            return text;
        }
    }
}
