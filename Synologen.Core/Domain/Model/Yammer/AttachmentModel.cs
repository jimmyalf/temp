using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Yammer
{
    public class AttachmentModel
    {
        public string content_type { get; set; }
        public string created_at { get; set; }
        public int creator_id { get; set; }
        public string download_url { get; set; }
        public Int64 id { get; set; }
        public ImageModel image { get; set; }
        public string large_icon_url { get; set; }
        public string name { get; set; }
        public string overlay_url { get; set; }
        public string path { get; set; }
        public string preview_url { get; set; }
        public int size { get; set; }
        public string small_icon_url { get; set; }
        public string thumbnail_url { get; set; }
        public string transcoded { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public int? uuid { get; set; }
        public string web_url { get; set; }
        public int y_id { get; set; }
        public string inline_html { get; set; }
        public string inline_url { get; set; }
        public YModuleModel ymodule { get; set; }
    }
}
