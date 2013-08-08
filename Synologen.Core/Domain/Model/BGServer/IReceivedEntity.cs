namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
	public abstract class ReceivedEntity : Entity
	{
		public virtual bool Handled { get; protected set; }
		public virtual void SetHandled()
		{
			Handled = true;
		}
	}
}