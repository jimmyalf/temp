using System;
using System.Collections;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    /// <summary>
    /// Summary description for ForumListBox.
    /// </summary>
    public class ForumListBox : ListBox {
        ForumMode mode = ForumMode.User;

        public ForumListBox() {
            DataBind();
        }

        public override void DataBind() {
            ArrayList forumGroups;

            // Get all forum groups
            if (mode == ForumMode.User)
                forumGroups = ForumGroups.GetForumGroups(false, false);
            else
                forumGroups = ForumGroups.GetForumGroups(false);

            foreach (ForumGroup group in forumGroups) {
                
				// quick hack to keep recursive from showing
				if (Items.FindByValue("g-" + group.ForumGroupID) == null) 
				{

                // Add the forum group
                //
                Items.Add(new ListItem(group.Name, "g-" + group.ForumGroupID));

                RecursiveAddForum (0, group.Forums);

                // Add the forum group
                //
                Items.Add(new ListItem("", "s"));
            }
            }
        }

        public int SelectedForum {
            get {
                if (base.SelectedValue.StartsWith("f-"))
                    return int.Parse(base.SelectedValue.Replace("f-", ""));
                return 0;
            }
            set {
                if (value > 0) {
                    if (base.SelectedValue != "")
                        Items.FindByValue( base.SelectedValue ).Selected = false;

                    Items.FindByValue("f-" + value).Selected = true;
                }
            }
        }


        public int SelectedForumGroup {
            get {
                if (base.SelectedValue.IndexOf("f") >= 0) {
                    return Forums.GetForum(int.Parse(base.SelectedValue.Replace("f-", ""))).ForumGroupID;
                } else {
                    return int.Parse(base.SelectedValue.Replace("g-", ""));
                }
            }
            set { 
                // Deselect current item
                if (base.SelectedValue != "")
                    Items.FindByValue( base.SelectedValue ).Selected = false;

                Items.FindByValue("g-" + value).Selected = true;
            }
        }

        private void RecursiveAddForum (int depth, ArrayList forums) {


            foreach (Spinit.Wpc.Forum.Components.Forum forum in forums)
            {
                // We only go 3 deep
                //
                switch (depth) {
                    case 0:
                        Items.Add(new ListItem(ResourceManager.GetString("Navigation_JumpDropDownList_Indent1") + forum.Name, "f-" + forum.ForumID.ToString()));
                        if (forum.Forums.Count > 0)
                            RecursiveAddForum((depth + 1), forum.Forums);
                        break;

                    case 1:
                        Items.Add(new ListItem(ResourceManager.GetString("Navigation_JumpDropDownList_Indent2") + forum.Name, "f-" + forum.ForumID.ToString()));
                        if (forum.Forums.Count > 0)
                            RecursiveAddForum((depth + 1), forum.Forums);
                        break;

                    case 2:
                        Items.Add(new ListItem(ResourceManager.GetString("Navigation_JumpDropDownList_Indent3") + forum.Name, "f-" + forum.ForumID.ToString()));
                        if (forum.Forums.Count > 0)
                            RecursiveAddForum((depth + 1), forum.Forums);
                        break;

                    default:
                        return;

                }
            }
        }

        public ForumMode ForumMode {
            get {
                return mode;
            }
            set {
                mode = value;
            }
        }
    }
}
