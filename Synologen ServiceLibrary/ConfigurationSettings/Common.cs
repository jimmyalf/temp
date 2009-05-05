using System;

namespace Spinit.Wpc.Synologen.ServiceLibrary.ConfigurationSettings {
	public class Common : BaseConfiguration {
		public static string ClientCredentialUserName {
			get { return GetSafeValue("ClientCredentialUserName", String.Empty); }
		}
		public static string ClientCredentialPassword {
			get { return GetSafeValue("ClientCredentialPassword", String.Empty); }
		}
	}
}