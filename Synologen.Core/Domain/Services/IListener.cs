namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IListener<in T>
    {
        void Handle(T message);
    }

	public interface ITypeListener<in T>
    {
        void Handle();
    }
}