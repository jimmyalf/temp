using System;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.Supplier {
    public partial class ViewMemberImage : System.Web.UI.Page {
        private string _filename = "";
        private int _maxHeight = -1;
        private int _maxWidth = -1;
        private string _contentInfo = null;

        protected void Page_Load(object sender, EventArgs e) {
            if (Request.Params["filename"] != null)
                _filename = Request.Params["filename"];

            if (Request.Params["height"] != null)
                _maxHeight = int.Parse(Request.Params["height"]);

            if (Request.Params["width"] != null)
                _maxWidth = int.Parse(Request.Params["width"]);

            if (Request.Params["ext"] != null)
                _contentInfo = Request.Params["ext"];


            if (_filename == "" || _contentInfo == null)
                return;

            string filePath = Spinit.Wpc.Base
                                .Business.Globals
                                .CommonFilePath;

            string fileUrl = Spinit.Wpc.Base
                                .Business.Globals
                                .CommonFileUrl;

            System.Drawing.Image image = null;
            System.Drawing.Image thumb = null;
            if (_filename != "") {
                //image = System.Drawing.Image.FromFile(Server.MapPath(fileUrl + _filename));
            	var path = filePath + _filename.Replace("/", "\\");
				image = System.Drawing.Image.FromFile(path);
            }

            float ratio = 0f;

            //Check if scaling needed
            if (_maxHeight > 0 && _maxWidth > 0 && (image.Height > _maxHeight || image.Width > _maxWidth)) {

                if (image.Height > _maxHeight && image.Width < _maxWidth) {
                    //Height larger than max, width smaller than max
                    ratio = (float)image.Width / (float)image.Height;

                    thumb = image.GetThumbnailImage((int)(ratio * _maxHeight), _maxHeight, null, IntPtr.Zero);

                }

                else if (image.Height < _maxHeight && image.Width > _maxWidth) {
                    //Width larger than max, height smaller than max
                    ratio = (float)image.Height / (float)image.Width;

                    thumb = image.GetThumbnailImage(_maxWidth, (int)(ratio * _maxWidth), null, IntPtr.Zero);

                }

                else if (image.Height > _maxHeight && image.Width > _maxWidth) {
                    //Width larger than max, height larger than max



                    float maxvalueration = (float)_maxHeight / (float)_maxWidth;

                    ratio = (float)image.Height / (float)image.Width;

                    thumb = image.GetThumbnailImage(_maxWidth, (int)(ratio * _maxWidth), null, IntPtr.Zero);


                    if (thumb.Height > _maxHeight) {
                        ratio = (float)image.Width / (float)image.Height;
                        thumb = image.GetThumbnailImage((int)(ratio * _maxHeight), _maxHeight, null, IntPtr.Zero);
                    }
                }
                else {
                    thumb = (System.Drawing.Image)image.Clone();
                }


            }
            else {
                thumb = (System.Drawing.Image)image.Clone();
            }
            image.Dispose();


            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            switch (_contentInfo) {
                case "jpg":
                    thumb.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Response.ContentType = "image/jpeg";
                    break;
                case "jpeg":
                    thumb.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Response.ContentType = "image/jpeg";
                    break;
                case "gif":
                    thumb.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);
                    Response.ContentType = "image/gif";
                    break;
                case "png":
                    thumb.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    Response.ContentType = "image/png";
                    break;
                case "bmp":
                    thumb.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    Response.ContentType = "image/bmp";
                    break;
            }

            Response.Clear();
            Response.ClearContent();

            Response.BufferOutput = true;

            byte[] bytes = stream.ToArray();

            //Response.AppendHeader("Content-Disposition"," attachment; filename=" + file.Name.Trim());

            Response.BinaryWrite(bytes);

            Response.Flush();
            Response.End();
        }
    }
}
