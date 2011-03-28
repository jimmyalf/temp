using System.ServiceModel;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService
{
	public interface IBGWebServiceClient : IBGWebService
	{
		/// <summary>
		/// Causes the System.ServiceModel.ClientBase&lt;TChannel&gt; object to transition from the created state into the opened state.
		/// </summary>
		void Open();

		/// <summary>
		/// Causes the System.ServiceModel.ClientBase&lt;TChannel&gt; object to transition from its current state into the closed state.
		/// </summary>
		void Close();

		/// <summary>
		/// Causes the System.ServiceModel.ClientBase&lt;TChannel&gt; object to transition immediately from its current state into the closed state.
		/// </summary>
		void Abort();

		/// <summary>
		/// Gets the current state of the System.ServiceModel.ClientBase&lt;TChannel&gt; object.
		/// </summary>
		/// <returns>
		/// The value of the System.ServiceModel.CommunicationState of the object.
		/// </returns>
		CommunicationState State { get; }
	}
}