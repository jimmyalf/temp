namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class Settlement 
	{
		public Settlement() {}
		public Settlement(int id) { Id = id; }
		public virtual int Id { get; private set; }
		public override bool Equals(object obj)
		{
			var entity = obj as Settlement;
			return Equals(entity);
		}

		public virtual bool Equals(Settlement other)
		{
			return other != null && other.Id.Equals(Id);
		}

		public override int GetHashCode()
		{
			return Id;
		}
	}
}