namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class LensRecipe : Entity
	{
		public virtual Order Order { get; set; }
		public virtual EyeParameter<decimal?> BaseCurve { get; set; }
		public virtual EyeParameter<decimal?> Diameter { get; set; }
		public virtual EyeParameter<decimal?> Power { get; set; }
		public virtual EyeParameter<decimal?> Axis { get; set; }
		public virtual EyeParameter<decimal?> Cylinder { get; set; }
		public virtual EyeParameter<decimal?> Addition { get; set; }
	}
}