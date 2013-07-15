using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
    public class ArticleSuppliersBySelectedArticleType : IActionCriteria
    {
        public int SelectedArticleTypeId { get; set; }

        public ArticleSuppliersBySelectedArticleType(int selectedArticleTypeId)
        {
            SelectedArticleTypeId = selectedArticleTypeId;
        }
    }
}