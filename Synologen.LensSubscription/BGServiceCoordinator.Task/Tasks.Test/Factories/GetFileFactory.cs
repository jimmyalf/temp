using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories
{
    public class GetFileFactory
    {
        public static IEnumerable<string> GetFileNames()
        {
            return new List<string>
            {
                "UAGAG.K0999999.110301.T130426",
                "UAGAG.K0999999.110301.T130427",
                "UAGAG.K0999999.110301.T130428"
            };
        }

        public static string[] GetFile()
        {
            return new [] { new string('A', 255), new string('B', 255), new string('C', 255), new string('D', 255) };
        }

        public static IEnumerable<FileSection> GetSections()
        {
            var section1 = new FileSection {Posts = new string('A', 80), SectionType = SectionType.ReceivedConsents};
            var section2 = new FileSection {Posts = new string('A', 80), SectionType = SectionType.ReceivedErrors};
            var section3 = new FileSection{Posts = new string('A', 80), SectionType = SectionType.ReceivedPayments};

            return new List<FileSection> {section1, section2, section3};
        }
    }
}
