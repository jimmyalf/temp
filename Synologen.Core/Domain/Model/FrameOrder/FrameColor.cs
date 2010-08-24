namespace Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder
{
	public class FrameColor
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual int NumberOfFramesWithThisColor { get; private set; }
	}
}