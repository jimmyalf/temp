using System.ServiceModel;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService
{
	public interface IBGWebServiceClient : IBGWebService
	{
		void Open();
		void Close();
		CommunicationState State { get; }
	}
}