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

        //public static IEnumerable<string> GetFileNames(string customerNumber)
        //{
        //    string[] fileNames = new string[] {};

        //    fileNames[0] = string.Format("UAGAG.K0{0}D090101T101545", customerNumber);
        //    fileNames[1] = string.Format("UAGAG.K0{0}D100101T101545", customerNumber);
        //    fileNames[2] = string.Format("UAGAG.K0{0}D080101T101545", customerNumber);
        //    fileNames[3] = string.Format("UAGAG.K0{0}D110101T101545", customerNumber);

        //    fileNames[4] = string.Format("UAGAG.K0{0}D110803T101545", customerNumber);
        //    fileNames[5] = string.Format("UAGAG.K0{0}D110303T101545", customerNumber);
        //    fileNames[6] = string.Format("UAGAG.K0{0}D110503T101545", customerNumber);
        //    fileNames[7] = string.Format("UAGAG.K0{0}D111103T101545", customerNumber);

        //    fileNames[8] = string.Format("UAGAG.K0{0}D110512T101545", customerNumber);
        //    fileNames[9] = string.Format("UAGAG.K0{0}D110502T101545", customerNumber);
        //    fileNames[10] = string.Format("UAGAG.K0{0}D110507T101545", customerNumber);
        //    fileNames[11] = string.Format("UAGAG.K0{0}D110520T101545", customerNumber);

        //    fileNames[12] = string.Format("UAGAG.K0{0}D110512T104545", customerNumber);
        //    fileNames[13] = string.Format("UAGAG.K0{0}D110512T031545", customerNumber);
        //    fileNames[14] = string.Format("UAGAG.K0{0}D110512T111545", customerNumber);
        //    fileNames[15] = string.Format("UAGAG.K0{0}D110512T101545", customerNumber);

        //    fileNames[16] = string.Format("UAGAG.K0{0}D110512T102245", customerNumber);
        //    fileNames[17] = string.Format("UAGAG.K0{0}D110512T100145", customerNumber);
        //    fileNames[18] = string.Format("UAGAG.K0{0}D110512T101545", customerNumber);
        //    fileNames[19] = string.Format("UAGAG.K0{0}D110512T101445", customerNumber);

        //    fileNames[20] = string.Format("UAGAG.K0{0}D110512T101455", customerNumber);
        //    fileNames[21] = string.Format("UAGAG.K0{0}D110512T101423", customerNumber);
        //    fileNames[22] = string.Format("UAGAG.K0{0}D110512T101419", customerNumber);
        //    fileNames[23] = string.Format("UAGAG.K0{0}D110512T101430", customerNumber);

        //    return fileNames.ToList();
        //}

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
