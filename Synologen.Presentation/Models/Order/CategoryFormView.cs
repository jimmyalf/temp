using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class CategoryFormView : CommonFormView
	{
		public CategoryFormView() { }
		public CategoryFormView(int? id = null, string name = null) : base(id)
		{
			Name = name;
		}

		[DisplayName("Namn"), Required]
		public string Name { get; set; }
	}
}