using System;
using System.Collections;

namespace Spinit.Wpc.Forum.Components {

    /// <summary>
    /// Summary description for UserSet.
    /// </summary>
    public class PostSet {

        ArrayList posts = new ArrayList();
        int totalRecords = 0;

        public int TotalRecords {
            get {
                return totalRecords;
            }
            set {
                totalRecords = value;
            }
        }

        public ArrayList Posts {
            get {
                return posts;
            }
        }

        public bool HasResults {
            get {
                if (posts.Count > 0)
                    return true;
                return false;
            }
        }
    }
}
