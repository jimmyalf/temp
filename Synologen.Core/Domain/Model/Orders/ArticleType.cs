namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class ArticleType : Entity
	{
		public ArticleType()
		{
			Active = true;
		}
		public virtual string Name { get; set; }
	    public virtual ArticleCategory Category { get; set; }
		public virtual bool Active { get; set; }
	}
}