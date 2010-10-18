namespace Spinit.Wpc.Synologen.Core.Domain.Model
{
	public abstract class Entity
	{
		public virtual int Id { get; private set; }
		public override bool Equals(object obj)
		{
			var entity = obj as Entity;
			return entity != null && entity.Id.Equals(Id);
		}
	}
}