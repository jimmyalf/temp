using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace Spinit.Wpc.Synologen.ServiceLibrary {
	public class CertificateValidator : X509CertificateValidator {

		public override void Validate ( X509Certificate2 certificate ) {
			var exceptionMessage = "Certificated was not issued by trusted issuer";
			var argumentName = "certificate";

			if ( certificate == null )
				throw new ArgumentNullException(argumentName);

			if(!CheckCertificateIsValid(certificate))
				throw new SecurityTokenValidationException(exceptionMessage);
		}

		/// <summary>
		/// Check if the name of the certifcate matches
		/// </summary>
		private static bool CheckCertificateIsValid(X509Certificate2 certificate) {
			return certificate.SubjectName.Name.Equals( ConfigurationSettings.Client.CertificateSubjectName );
		}
	}
}
