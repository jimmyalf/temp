namespace Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder
{
	public class FrameGlassType
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual bool IncludeAdditionParametersInOrder { get; set; }
		public virtual bool IncludeHeightParametersInOrder { get; set; }
	}
}