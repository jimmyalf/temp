namespace Spinit.Wpc.Synologen.Core.Domain.Model
{
	public abstract class Entity
	{
		public virtual int Id { get; private set; }
		public override bool Equals(object obj)
		{
			var entity = obj as Entity;
			return Equals(entity);
		}

		public virtual bool Equals(Entity other)
		{
			if(other == null) return false;
			return other.GetHashCode() == GetHashCode();
		}

		public override int GetHashCode()
		{
			return Id;
		}
	}
}