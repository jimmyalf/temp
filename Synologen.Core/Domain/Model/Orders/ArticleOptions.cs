namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
    public class ArticleOptions
    {
        public ArticleOptions()
        {
            Diameter = new SequenceDefinition();
            BaseCurve = new SequenceDefinition();
        	EnableAxis = true;
        	EnableCylinder = true;
			EnableAddition = true;
        }

		public virtual bool EnableAddition { get; set; }
		public virtual bool EnableAxis { get; set; }
		public virtual bool EnableCylinder { get; set; }
        public virtual SequenceDefinition Diameter { get; set; }
        public virtual SequenceDefinition BaseCurve { get; set; }
    }
}