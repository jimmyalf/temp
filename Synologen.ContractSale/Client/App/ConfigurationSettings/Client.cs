using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using Spinit.Wpc.Synologen.Business.Utility.Configuration;

namespace Synologen.Client.App.ConfigurationSettings 
{
	public class Client : BaseConfiguration 
	{

		public static string SelectedServiceEndPointName {
			get { return GetSafeValue("SelectedServiceEndPointName", String.Empty); }
		}

		/// <summary>
		/// Returns the endpoint of a given endpoint name (specified in web.config)
		/// </summary>
		/// <param name="endpointName"></param>
		/// <returns></returns>
		public static string GetEndpointAddress(string endpointName) {
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			var section = config.GetSectionGroup("system.serviceModel") as ServiceModelSectionGroup;
			if (section != null) {
				var endpoint = FindEndpointByName(endpointName, section.Client.Endpoints);
				return endpoint.Address.OriginalString;
			}
			return string.Empty;
		}

		/// <summary>
		/// Returns default endpoint address (using the SelectedServiceEndPointName property specified in web.config)
		/// </summary>
		/// <returns></returns>
		public static string GetEndpointAddress() {
			var endpointName = SelectedServiceEndPointName;
			return GetEndpointAddress(endpointName);
		}

		private static ChannelEndpointElement FindEndpointByName(string endpointName, ChannelEndpointElementCollection endpointElementCollection) {
			foreach (ChannelEndpointElement endpointElement in endpointElementCollection) {
				if (endpointElement.Name.Equals(endpointName)) {
					return endpointElement;
				}
			}
			return new ChannelEndpointElement();
		}
	}
}