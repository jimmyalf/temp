using System.ComponentModel;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameOrderView
	{
		[DisplayName("Skapad")] public string Created { get; set; }
		[DisplayName("Skickad")] public string Sent { get; set; }
		[DisplayName("Båge")] public string Frame { get; set; }
		[DisplayName("Artnr.")] public string FrameArticleNumber { get; set; }
		[DisplayName("Glastyp")] public string GlassType { get; set; }
		[DisplayName("Id")]public int Id { get; set; }
		[DisplayName("Butik")]public string Shop { get; set; }
		[DisplayName("Butiksort")]public string ShopCity { get; set; }
		[DisplayName("Addition")]public EyeParameterViewModel Addition { get; set; }
		[DisplayName("Axel")]public EyeParameterViewModel Axis { get; set; }
		[DisplayName("Cylinder")]public EyeParameterViewModel Cylinder { get; set; }
		[DisplayName("Höjd")]public EyeParameterViewModel Height { get; set; }
		[DisplayName("PD")]public EyeParameterViewModel PupillaryDistance { get; set; }
		[DisplayName("Sfär")]public EyeParameterViewModel Sphere { get; set; }
	}
}