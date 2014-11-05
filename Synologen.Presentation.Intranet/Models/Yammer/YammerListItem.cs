using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Yammer
{
    public class YammerListItem
    {
        public string Created { get; set; }
        public string AuthorImageUrl { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public IEnumerable<YammerImage> Images { get; set; }
        public IEnumerable<YammerModule> YammerModules { get; set; }
    }
}
