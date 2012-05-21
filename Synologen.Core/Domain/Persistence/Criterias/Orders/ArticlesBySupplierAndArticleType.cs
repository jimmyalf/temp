using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
    public class ArticlesBySupplierAndArticleType : IActionCriteria
    {
        public ArticlesBySupplierAndArticleType(int supplierId, int articleTypeId)
        {
            ArticleTypeId = articleTypeId;
            SupplierId = supplierId;
        }

        public int ArticleTypeId { get; set; }
        public int SupplierId { get; set; }
    }
}