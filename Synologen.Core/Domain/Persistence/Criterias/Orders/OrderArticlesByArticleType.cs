using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
    public class OrderArticlesByArticleType : IActionCriteria
    {
        public int ArticleTypeId { get; set; }

        public OrderArticlesByArticleType(int articleTypeId)
        {
            ArticleTypeId = articleTypeId;
        }
    }
}