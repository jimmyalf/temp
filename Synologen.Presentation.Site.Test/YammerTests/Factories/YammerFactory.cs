using System;
using System.IO;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.YammerTests.Factories
{
    public class YammerFactory
    {
        public static string GetJson()
        {
            var path = Environment.CurrentDirectory + @"\..\..\Synologen.Presentation.Site.Test\YammerTests\Factories\messages.json";
            return File.ReadAllText(path);
        }
    }
}
