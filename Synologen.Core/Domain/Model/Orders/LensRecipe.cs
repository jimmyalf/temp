namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class LensRecipe : Entity
	{
		public virtual Order Order { get; set; }
		public virtual EyeParameter BaseCurve { get; set; }
		public virtual EyeParameter Diameter { get; set; }
		public virtual EyeParameter Power { get; set; }
		public virtual EyeParameter Axis { get; set; }
		public virtual EyeParameter Cylinder { get; set; }
		public virtual EyeParameter Addition { get; set; }
	}
}