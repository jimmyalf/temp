namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class Settlement 
	{
		public Settlement() {}
		public Settlement(int id) { Id = id; }
		public virtual int Id { get; private set; }
		public override bool Equals(object obj)
		{
			var entity = obj as Entity;
			return entity != null && entity.Id.Equals(Id);
		}
	}
}