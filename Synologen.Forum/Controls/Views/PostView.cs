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
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum.Controls.PostDisplay;

namespace Spinit.Wpc.Forum.Controls {

    // *********************************************************************
    //  PostView
    //
    /// <summary>
    /// This server control is used to display a Post
    /// </summary>
    /// 
    // ********************************************************************/
    public class PostView : Control, INamingContainer {
        Post post;

        // *********************************************************************
        //  CreateChildControls
        //
        /// <summary>
        /// Initializes the user control loaded in CreateChildControls. Initialization
        /// consists of finding well known control names and wiring up any necessary events.
        /// </summary>
        /// 
        // ********************************************************************/ 
        protected override void CreateChildControls() {
            TextPost textPost;

            // What kind of post are we working with
            //
            switch (post.PostType) {

                // Post is formatted with BBCode
                //
                case PostType.BBCode:
                    textPost = new TextPost();
                    textPost.Post = Post;
                    Controls.Add(textPost);
                    break;

                // Post is formatted with HTML
                //
                case PostType.HTML:
                    textPost = new TextPost();
                    textPost.Post = Post;
                    Controls.Add(textPost);
                    break;

                // Post is a vote
                //
                case PostType.Poll:
                    PollPost pollPost = new PollPost(post);
                    Controls.Add(pollPost);
                    break;

            }

        }

        public Post Post {
            get {
                return post;
            }
            set {
                post = value;
            }
        }

    }
}