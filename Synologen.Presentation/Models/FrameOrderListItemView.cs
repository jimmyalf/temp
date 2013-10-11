namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameOrderListItemView
	{
		public int Id { get; set; }
        public string Supplier { get; set; }
		public string Frame { get; set;}
		public string GlassType { get; set; }
		public string Shop { get; set; }
		public bool Sent { get; set; }
		public string Created { get; set; }
	}
}