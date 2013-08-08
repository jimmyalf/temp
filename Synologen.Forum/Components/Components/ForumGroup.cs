using System;
using System.Collections;
using Spinit.Wpc.Forum;

namespace Spinit.Wpc.Forum.Components {

    /// <summary>
	/// This class defines the properties that makeup a forum.
	/// </summary>
	public class ForumGroup : IComparable {
		int forumGroupID;
        string name;
        ArrayList forums;
        int sortOrder = 0;
        bool hideForums = false;

        public ForumGroup() {}

        public ForumGroup(string name) {
            this.name = name;
        }

        public int CompareTo(object value) {

            if (value == null) return 1;

            int compareOrder = ((ForumGroup)value).SortOrder;

            if (this.SortOrder == compareOrder) return 0;
            if (this.SortOrder < compareOrder) return -1;
            if (this.SortOrder > compareOrder) return 1;
            return 0;
        }

		/*************************** PROPERTY STATEMENTS *****************************/
		/// <summary>
		/// Specifies the unique identifier for the each forum.
		/// </summary>

        public int ForumGroupID {
            get { 
                return forumGroupID; 
            }

            set {
                if (value < 0)
                    forumGroupID = 0;
                else
                    forumGroupID = value;
            }
        }

        public ArrayList Forums {
            get { 
                if (forums == null) {
                    forums = Spinit.Wpc.Forum.Forums.GetForumsByForumGroupID(forumGroupID);
                }

                return forums; 
            }
            set { forums = value; }
        }

        public String Name {
            get { return name; }
            set { name = value; }
        }

        
        string newsgroupName;
        public string NewsgroupName {
            get { return newsgroupName.ToLower(); }
            set { newsgroupName = value; }
             
        }

        public int SortOrder {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        public bool HideForums {
            get { return hideForums; }
            set { hideForums = value; }
        }
	}
}
