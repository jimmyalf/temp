namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class EyeParameter<T> //where T:struct
	{
		public EyeParameter() { }
		public EyeParameter(T left, T right)
		{
			Left = left;
			Right = right;
		}
		public virtual T Left { get; set; }
		public virtual T Right { get; set; }
	}
}