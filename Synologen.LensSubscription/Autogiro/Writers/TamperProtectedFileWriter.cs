using System;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.Autogiro.Writers
{

	public class TamperProtectedFileWriter : BaseWriter, ITamperProtectedFileWriter
	{
		private readonly IHashService _hashService;
		private readonly DateTime _writeDate;
		private const string KeyVerificationTokenValue = "00000000";

		public TamperProtectedFileWriter(IHashService hashService, DateTime writeDate)
		{
			_hashService = hashService;
			_writeDate = writeDate;
		}

		public string Write(string fileContents)
		{
			var openingSealRecord = GetOpeningSealRecord();
			var tamperProtectionRecord = GetTamperProtectionRecord(openingSealRecord, fileContents);
			return new StringBuilder()
				.AppendLine(openingSealRecord)
				.AppendLine(fileContents)
				.AppendLine(tamperProtectionRecord).ToString();

		}

		public static string KeyVerificationToken
		{
			get { return KeyVerificationTokenValue; }
		}

		protected virtual string GetOpeningSealRecord()
		{
			return String.Format("00{0}{1}{2}",
                 _writeDate.ToString("yyyyMMdd"),
                 _hashService.CondensateTypeName,
				 Pad(68));
		}

		protected virtual string GetTamperProtectionRecord(string openingSealRecord, string fileContents)
		{
			return String.Format("99{0}{1}{2}{3}",
                 _writeDate.ToString("yyyyMMdd"),
                 GetKeyVerificationValue(),
				 GetMessageAuthenticationCode(new StringBuilder()
					 .AppendLine(openingSealRecord)
					 .AppendLine(fileContents)
					 .ToString()
					 .TrimEnd('\r','\n')),
				 Pad(8));
		}

		protected virtual string GetMessageAuthenticationCode(string fileContents)
		{
			return _hashService.GetHash(fileContents);
		}

		protected virtual string GetKeyVerificationValue()
		{
			return _hashService.GetHash(KeyVerificationToken);
		}

		
	}
}