using System;
using System.Collections;

namespace Spinit.Wpc.Forum.Components {
    /// <summary>
    /// Summary description for UserSet.
    /// </summary>
    public class ThreadSet {

        ArrayList threads = new ArrayList();
        ArrayList announcements = new ArrayList();
        int totalRecords = 0;

        public int TotalRecords {
            get {
                return totalRecords;
            }
            set {
                totalRecords = value;
            }
        }

        public ArrayList Threads {
            get {
                return threads;
            }
        }

        public ArrayList Announcements {
            get {
                if (announcements.Count > 0)
                    return announcements;

                // Announcements hasn't been asked for
                // Split thread and announcements
                FilterAnnouncements();

                return announcements;
            }
        }

        public bool HasResults {
            get {
                if (threads.Count > 0)
                    return true;
                return false;
            }
        }

        private void FilterAnnouncements() {
            ArrayList _threads = new ArrayList();

            foreach (Thread t in threads) {

                if (t.IsAnnouncement)
                    announcements.Add(t);
                else
                    _threads.Add(t);

            }

            // All done
            //
            threads = _threads;

        }

    }
}
