using System;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
	public class FileSectionToSend : Entity
	{
		public FileSectionToSend()
		{
			CreatedDate = DateTime.Now;
		}
		public virtual string SectionData { get; set; }
		public virtual SectionType Type { get; set; }
		public virtual string TypeName { get { return Type.GetEnumDisplayName(); } private set{ return;} }
		public virtual DateTime CreatedDate { get; set; }
		public virtual DateTime? SentDate { get; set; }
		public virtual bool HasBeenSent { get { return SentDate.HasValue; } }
	}
}