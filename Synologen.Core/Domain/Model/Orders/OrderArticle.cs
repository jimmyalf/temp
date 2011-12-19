using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
    public class OrderArticle : Entity
    {
        public string Name { get; set; }
        public ArticleType ArticleType { get; set; }
    }
}