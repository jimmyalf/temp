namespace Spinit.Wpc.Synologen.Core.Domain.Model
{
	public class FrameBrand
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual int NumberOfFramesWithThisBrand { get; private set; }
	}
}