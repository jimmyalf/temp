namespace Spinit.Wpc.Synologen.Presentation.Models.Deviation
{
	public abstract class CommonFormView
	{
		public CommonFormView() { }
		public CommonFormView(int? id)
		{
			Id = id;
		}

		public int? Id { get; set; }
		public bool IsCreate { get { return !IsUpdate; } }
		public bool IsUpdate { get { return Id.HasValue; } }
	}
}