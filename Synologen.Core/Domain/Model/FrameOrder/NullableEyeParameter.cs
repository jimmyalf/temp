namespace Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder
{
	public class NullableEyeParameter
	{
		public virtual decimal? Left { get; set; }
		public virtual decimal? Right { get; set; }
	}

	public class NullableEyeParameter<TType>
	{
		public virtual TType Left { get; set; }
		public virtual TType Right { get; set; }
	}
}
