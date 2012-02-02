using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class CategoryFormView : CommonFormView
	{
		public CategoryFormView()
		{
			Active = true;
		}
		public CategoryFormView(int? id, ArticleCategory category) : base(id)
		{
			Name = category.Name;
			Active = category.Active;
		}

		[DisplayName("Namn"), Required]
		public string Name { get; set; }

		[DisplayName("Aktiv")]
		public bool Active { get; set; }
	}
}