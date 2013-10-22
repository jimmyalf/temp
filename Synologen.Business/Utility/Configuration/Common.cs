using System;

namespace Spinit.Wpc.Synologen.Business.Utility.Configuration 
{
	public class Common : BaseConfiguration 
	{
		public static string ClientCredentialUserName 
		{
			get { return GetSafeValue("ClientCredentialUserName", String.Empty); }
		}

		public static string ClientCredentialPassword
		{
			get { return GetSafeValue("ClientCredentialPassword", String.Empty); }
		}

		public static string CertificateSubjectName 
		{
			get { return GetSafeValue("CertificateSubjectName", String.Empty); }
		}
	}
}