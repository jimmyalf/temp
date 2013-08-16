namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class ArticleCategory : Entity 
	{
		public ArticleCategory()
		{
			Active = true;
		}
		public virtual string Name { get; set; }
		public virtual bool Active { get; set; }
	}
}