using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;

namespace Synologen.LensSubscription.Autogiro.Readers
{
    public class ReceivedFileSplitter
    {
        public static IEnumerable<FileSection> GetSections(string file)
        {
            // Todo        

            List<FileSection> fileSections = new List<FileSection>();

            // Loopa igenom rad för rad och kolla TK01
                // Spara rad till StringBuilder

                // Leta efter ny TK01 eller slutsumma post 09

                // Skapa FileSection, lägg till listan
            // end Loop
            return fileSections;
        }
    }
}
