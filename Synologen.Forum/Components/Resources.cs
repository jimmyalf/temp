using System;
using System.IO;
using Spinit.Wpc.Forum.Components;
using System.Drawing;
using System.Drawing.Imaging;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum {

    public class Resources {

        public static Avatar GetAvatar (int userID) {

            // write the avatar to storage
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            return dp.GetUserAvatar (userID);
        }

        public static void UpdateAvatar (int userID, Stream postedFile) {
            // TODO : allow default image format to be controlled through admin configuration
			//
			ImageFormat format = ImageFormat.Jpeg;

            // validate stream
            if (postedFile.Length <= 0)
                return;

            // resize image
            MemoryStream image = ResizeImage (postedFile, Globals.GetSiteSettings().AvatarHeight, Globals.GetSiteSettings().AvatarWidth, format);

            // write the avatar to storage
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            Avatar avatar = new Avatar(image);
            avatar.ContentType = "image/" + format.ToString();

            dp.CreateUpdateDeleteImage(userID, avatar, DataProviderAction.Update);

        }

        public static void DeleteAvatar (int userID) {
            // write the avatar to storage
            ForumsDataProvider dp = ForumsDataProvider.Instance();

            dp.CreateUpdateDeleteImage(userID, null, DataProviderAction.Delete);
        }

        public static MemoryStream ResizeImage (Stream stream, int height, int width, ImageFormat format) {

            Bitmap bitmapOriginal = new Bitmap(stream);
            Bitmap bitmapResize = bitmapOriginal;
            MemoryStream returnStream = new MemoryStream();

            float scale;

            if ((bitmapOriginal.Height > height) || (bitmapOriginal.Width > width)) {

                // Get the scale
                //
                if (bitmapOriginal.Width < bitmapOriginal.Height)
                    scale = (float) height / (float) bitmapOriginal.Height;
                else
                    scale = (float) width / (float) bitmapOriginal.Width;

                bitmapResize = new Bitmap(bitmapOriginal, (int) (scale * bitmapOriginal.Width), (int) (scale * bitmapOriginal.Height));
            }

            bitmapResize.Save(returnStream, format);

            return returnStream;

        }


    }

}
