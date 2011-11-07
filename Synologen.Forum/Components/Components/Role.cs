using System;
using System.Collections;

namespace Spinit.Wpc.Forum.Components {

    /// <summary>
    /// Summary description for Role.
    /// </summary>
    public class Role :	IComparable {

        int roleID;
        string name;
        string description;

        public Role() {
        }

        public Role (int roleID, string name) {
            this.roleID = roleID;
            this.name = name;
        }

        public int RoleID {
            get {
                return roleID;
            }
            set {
                roleID = value;
            }
        }

        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }


        public string Description {
            get {
                return description;
            }
            set {
                description = value;
            }
        }    

		public override bool Equals(object obj) {
			Role rhs = obj as Role;

			if( rhs != null &&
				rhs.RoleID	== this.RoleID &&
				rhs.Name	== this.Name ) {

				return true;
			}
			else {
				return false;
			}
		}

    
		#region IComparable Members

		public int CompareTo(object obj)
		{
			Role rhs = obj as Role;
			if( rhs != null ) {
				if( this.RoleID == rhs.RoleID )
					return 0;
				if( this.RoleID < rhs.RoleID )
					return -1;

				return 1;
			}
			else
				return -1;
		}

		#endregion
	}
}
