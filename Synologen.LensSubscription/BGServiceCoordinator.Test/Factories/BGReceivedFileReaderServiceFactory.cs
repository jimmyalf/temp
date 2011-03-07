using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synologen.LensSubscription.BGService.Test.Factories
{
    public static class BGReceivedFileReaderServiceFactory
    {
        public static IEnumerable<string> GetFileNames(string customerNumber, string productCode)
        {
            string[] fileNames = new string[4];

            fileNames[0] = string.Format("{0}.K0{1}D090101T101545", productCode, customerNumber);
            fileNames[1] = string.Format("{0}.K0{1}D100101T101545", productCode, customerNumber);
            fileNames[2] = string.Format("{0}.K0{1}D080101T101545", productCode, customerNumber);
            fileNames[3] = string.Format("{0}.K0{1}D110101T101545", productCode, customerNumber);
            return fileNames.ToList();
        }

        public static DateTime GetDate(int counter)
        {
            switch (counter)
            {
                case 0:
                    return new DateTime(2009, 01, 01, 10, 15, 45);
                    
                case 1:
                    return new DateTime(2010, 01, 01, 10, 15, 45);
                    
                case 2:
                    return new DateTime(2008, 01, 01, 10, 15, 45);

                case 3:
                    return new DateTime(2011, 01, 01, 10, 15, 45);
            }
            return DateTime.Now;
        }

        public static IEnumerable<string> GetInvalidFileNames()
        {
            string s1 = "";
            string s2 = "sdfgds";
            string s3 = "fdsgds.sgd.sert";
            string[] invalidFileNames = new [] { s1, s2, s3 };

            return invalidFileNames.ToList();
        }
    }
}
