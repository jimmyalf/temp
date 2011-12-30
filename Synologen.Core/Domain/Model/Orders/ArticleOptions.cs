namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
    public class ArticleOptions
    {
        public ArticleOptions()
        {
            Power = new SequenceDefinition();
            Diameter = new SequenceDefinition();
            BaseCurve = new SequenceDefinition();
            Axis = new SequenceDefinition();
            Cylinder = new SequenceDefinition();
            Addition = new SequenceDefinition();
        }

        public virtual SequenceDefinition Power { get; set; }
        public virtual SequenceDefinition Diameter { get; set; }
        public virtual SequenceDefinition BaseCurve { get; set; }
        public virtual SequenceDefinition Axis { get; set; }
        public virtual SequenceDefinition Cylinder { get; set; }
        public virtual SequenceDefinition Addition { get; set; }
    }
}