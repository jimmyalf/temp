using System;
using System.Collections;

namespace Spinit.Wpc.Forum.Components {
    

    public class SearchResult {

        ArrayList posts = new ArrayList();
        double searchDuration = 0;
        int totalRecords = 0;

        public double SearchDurationInMS {
            get {
                return searchDuration;
            }
            set {
                searchDuration = value;
            }
        }

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
