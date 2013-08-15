using System;
using System.IO;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.YammerTests.Factories
{
    public class YammerFactory
    {
        public static string GetJson()
        {
            var path = Environment.CurrentDirectory + @"\..\..\Synologen.Presentation.Intranet.Test\YammerTests\Factories\messages.json";
            return File.ReadAllText(path);
        }
    }
}
