namespace Synologen.Migration.AutoGiro2.Queries
{
	public class GetById<TType> : Query<TType>
	{
		private readonly int _id;

		public GetById(int id)
		{
			_id = id;
		}

		public override TType Execute()
		{
			return Session.Get<TType>(_id);
		}
	}
}