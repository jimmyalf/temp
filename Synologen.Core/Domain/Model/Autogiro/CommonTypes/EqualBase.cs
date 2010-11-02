namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
	public abstract class EqualBase<T> where T : class
	{
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (T)) return false;
			return EqualityImplementation(obj as T);
		}

		protected abstract bool EqualityImplementation(T other);
	}
}