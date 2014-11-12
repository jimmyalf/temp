using System;
using System.Web;   

namespace Spinit.Wpc.Forum.Components {
    /// <summary>
    /// Summary description for PostAttachment.
    /// </summary>
    public class PostAttachment {
        int length = 0;
        int userID = 0;
        int postID = 0;
        int forumID =0;
        DateTime created;
        string contentType;
        string fileName;
        byte[] content;

        public PostAttachment() {
        }

        public PostAttachment (HttpPostedFile postedFile) {

            // Get the file length and content type
            //
            length = postedFile.ContentLength;
            contentType = postedFile.ContentType;

            // Get the filename
            //
            fileName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("\\") + 1);

            // Setup the byte array
            //
            content = new Byte[length];

            // Read in the attachment into a byte array
            //
            postedFile.InputStream.Read(content, 0, length);

        }

        public int Length {
            get {
                return length;
            }
            set {
                length = value;
            }
        }

        public string ContentType {
            get {
                return contentType;
            }
            set {
                contentType = value;
            }
        }

        public string FileName {
            get {
                return fileName;
            }
            set {
                fileName = value;
            }

        }

        public Byte[] Content {
            get {
                return content;
            }
            set {
                content = value;
            }
        }

        public int PostID {
            get { return postID; }
            set { postID = value; }
        }

        public int ForumID {
            get { return forumID; }
            set { forumID = value; }
        }

        public int UserID {
            get { return userID; }
            set { userID = value; }
        }

        public DateTime DateCreated {
            get {
                return created;
            }
            set {
                created = value;
            }
        }

    }
}
