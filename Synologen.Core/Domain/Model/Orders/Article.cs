﻿namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class Article : Entity
	{
		public Article()
		{
			Active = true;
		}
		public virtual string Name { get; set; }
	    public virtual ArticleOptions Options { get; set; }
		public virtual ArticleType ArticleType { get; set; }
        public virtual ArticleSupplier ArticleSupplier { get; set; }
		public virtual bool Active { get; set; }
	}
}