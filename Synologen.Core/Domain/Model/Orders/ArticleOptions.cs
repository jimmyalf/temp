namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
    public class ArticleOptions
    {
        public ArticleOptions()
        {
            Power = new SequenceDefinition();
            Diameter = new SequenceDefinition();
            BaseCurve = new SequenceDefinition();
            Axis = new OptionalSequenceDefinition();
            Cylinder = new OptionalSequenceDefinition();
            Addition = new OptionalSequenceDefinition();
        }

        public virtual SequenceDefinition Power { get; set; }
        public virtual SequenceDefinition Diameter { get; set; }
        public virtual SequenceDefinition BaseCurve { get; set; }
        public virtual OptionalSequenceDefinition Axis { get; set; }
        public virtual OptionalSequenceDefinition Cylinder { get; set; }
        public virtual OptionalSequenceDefinition Addition { get; set; }
    }
}