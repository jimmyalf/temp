using System;
using System.Collections;
using System.Text;

namespace Spinit.Wpc.Forum.Components {


    public class StringTransforms {

        // *********************************************************************
        //  ToDelimitedString
        //
        /// <summary>
        /// Private helper function to convert a collection to delimited string array
        /// </summary>
        /// 
        // ********************************************************************/
        public static string ToDelimitedString(ICollection collection, string delimiter) {

            StringBuilder delimitedString = new StringBuilder();

            // Hashtable is perfomed on Keys
            //
            if (collection is Hashtable) {

                foreach (object o in ((Hashtable) collection).Keys) {
                    delimitedString.Append( o.ToString() + delimiter);
                }
            }

            // ArrayList is performed on contained item
            //
            if (collection is ArrayList) {
                foreach (object o in (ArrayList) collection) {
                    delimitedString.Append( o.ToString() + delimiter);
                }
            }

            // String Array is performed on value
            //
            if (collection is String[]) {
                foreach (string s in (String[]) collection) {
                    delimitedString.Append( s + delimiter);
                }
            }

            return delimitedString.ToString().TrimEnd(Convert.ToChar(delimiter));
        }
    }
}
