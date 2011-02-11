using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
    public class ReceivedFileSection : Entity
    {
        public virtual string SectionData { get; set; }
        public virtual SectionType Type { get; set; }
        public virtual string TypeName { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? HandledDate { get; set; }
    }
}
