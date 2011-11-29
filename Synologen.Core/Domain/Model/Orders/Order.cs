namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
    public class Order : Entity
    {
        public virtual int ArticleId { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual int LeftBaseCurve { get; set; }
        public virtual int LeftDiameter { get; set; }
        public virtual int LeftPower { get; set; }
        public virtual int RightBaseCurve { get; set; }
        public virtual int RightDiameter { get; set; }
        public virtual int RightPower { get; set; }
        public virtual int ShipmentOption { get; set; }
        public virtual int SupplierId { get; set; }
        public virtual int TypeId { get; set; }
    }
}