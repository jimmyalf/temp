using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{
	public class CategoryFormView
	{
		public CategoryFormView() { }
		public CategoryFormView(int? id = null, string name = null)
		{
			Id = id;
			Name = name;
		}

		public int? Id { get; set; }
		[DisplayName("Namn"), Required]
		public string Name { get; set; }
		public bool IsAdd { get { return !Id.HasValue; } }
	}
}