using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using System.IO;
using System.Web.Security;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls {

    [
    ParseChildren(true)	
    ]
    /// <summary>
    /// Summary description for Summary.
    /// </summary>
    public abstract class SkinnedForumWebControl : WebControl, INamingContainer {

        ForumContext forumContext = ForumContext.Current;
        string skinFilename = null;
        string skinName = null;
        string returnURL = null;
        ForumMode mode = ForumMode.User;

        // *********************************************************************
        //  SkinnedForumWebControl
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/
        public SkinnedForumWebControl() {

            // What skin should be used?
            //
            if (forumContext.User.IsAnonymous) {
                skinName = Globals.Skin;
            } else {
                skinName = forumContext.User.Theme;
            }

        }

        
        // *********************************************************************
        //  CreateChildControls
        //
        /// <summary>
        /// This event handler adds the children controls.
        /// </summary>
        // ***********************************************************************/
        protected override void CreateChildControls() {
            Control skin;

            // Load the skin
            skin = LoadSkin();

            // Initialize the skin
            InitializeSkin(skin);

            Controls.Add(skin);
        }

        // *********************************************************************
        //  LoadControlSkin
        //
        /// <summary>
        /// Loads the names control template from disk.
        /// </summary>
        // ***********************************************************************/
        protected Control LoadSkin() {
            Control skin;
            string skinPath = Globals.GetSkinPath() + "/Skins/" + SkinFilename.TrimStart('/');
            string defaultSkinPath = Globals.ApplicationPath + "/Themes/default/Skins/" + SkinFilename.TrimStart('/');

            // Do we have a skin?
            if (SkinFilename == null)
                throw new Exception("You must specify a skin.");

            // Attempt to load the control. If this fails, we're done
            try {
                skin = Page.LoadControl(skinPath);
            }
            catch (FileNotFoundException) {

                // Ok we couldn't find the skin, let's attempt to load the default skin instead
                try {

                    skin = Page.LoadControl(defaultSkinPath);
                } 
                catch (FileNotFoundException) {
                    throw new Exception("Critical error: The skinfile " + skinPath + " could not be found. The skin must exist for this control to render.");
                }
            }

            return skin;
        }

        // *********************************************************************
        //  InitializeSkin
        //
        /// <summary>
        /// Initialize the control template and populate the control with values
        /// </summary>
        // ***********************************************************************/
        protected abstract void InitializeSkin(Control skin);


        // *********************************************************************
        //  SkinName
        //
        /// <summary>
        /// Allows the default control template to be overridden
        /// </summary>
        // ***********************************************************************/
        public string SkinFilename {
            get { 
                return skinFilename; 
            }
            set { 
                skinFilename = value; 
            }
        }

        // *********************************************************************
        //  SkinName
        //
        /// <summary>
        /// Used to construct paths to images, etc. within controls.
        /// </summary>
        /// 
        // ********************************************************************/ 
        protected string SkinName {
            get {
                return skinName;
            }
            set {
                skinName = value;
            }
        }

        public ForumMode Mode {
            get { return mode; }
            set { mode = value; }
        }
    
    }
}
