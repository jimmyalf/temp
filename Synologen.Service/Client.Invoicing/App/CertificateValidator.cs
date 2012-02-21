using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace Synologen.Service.Client.Invoicing.App 
{
	public class CertificateValidator : X509CertificateValidator 
	{
		public override void Validate (X509Certificate2 certificate) 
		{
			if (certificate == null) throw new ArgumentNullException("certificate");
			if (!CheckCertificateIsValid(certificate)) throw new SecurityTokenValidationException("Certificated was not issued by trusted issuer");
		}

		/// <summary>
		/// Check if the name of the certifcate matches
		/// </summary>
		private static bool CheckCertificateIsValid(X509Certificate2 certificate)
		{
			if(certificate == null || certificate.SubjectName.Name == null) return false;
			return certificate.SubjectName.Name.Equals( Spinit.Wpc.Synologen.Business.Utility.Configuration.Common.CertificateSubjectName );
		}
	}
}
