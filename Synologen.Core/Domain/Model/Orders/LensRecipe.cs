namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class LensRecipe : Entity
	{
		public virtual Order Order { get; set; }
		public virtual EyeParameter<Article> Article { get; set; }
		public virtual EyeParameter<decimal?> BaseCurve { get; set; }
		public virtual EyeParameter<decimal?> Diameter { get; set; }
		public virtual EyeParameter<string> Power { get; set; }
		public virtual EyeParameter<string> Axis { get; set; }
		public virtual EyeParameter<string> Cylinder { get; set; }
		public virtual EyeParameter<string> Addition { get; set; }
		public virtual EyeParameter<string> Quantity { get; set; }
	}
}