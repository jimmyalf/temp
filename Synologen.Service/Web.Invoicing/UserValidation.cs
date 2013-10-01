using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace Synologen.Service.Web.Invoicing 
{
	public class UserValidation : UserNamePasswordValidator 
    {
		public override void Validate ( string userName, string password ) 
        {
			const string exceptionMessage = "Validation Failed!";
			var lowerCaseUserName = userName.ToLower();
			var lowercaseClientCredential = Spinit.Wpc.Synologen.Business.Utility.Configuration.Common.ClientCredentialUserName.ToLower();

		    if (!lowerCaseUserName.Equals(lowercaseClientCredential))
		    {
		        throw new SecurityTokenException(exceptionMessage);
		    }

		    if (!password.Equals(Spinit.Wpc.Synologen.Business.Utility.Configuration.Common.ClientCredentialPassword))
		    {
		        throw new SecurityTokenException(exceptionMessage);
		    }
        }
	}
}
