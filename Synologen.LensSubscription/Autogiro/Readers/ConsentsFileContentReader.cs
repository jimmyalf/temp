using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Synologen.LensSubscription.Autogiro.Helpers;
using Synologen.LensSubscription.Autogiro.Readers;

namespace Spinit.Wp.Synologen.Autogiro.Readers
{
	public class ConsentsFileContentReader : BaseReader, IFileReader<ConsentsFile, Consent> 
	{
		public ConsentsFile Read(string fileContent, IItemReader<Consent> itemReader)
		{
			SetupBase(fileContent);
			return new ConsentsFile
			{
				PaymentRecieverBankgiroNumber = FirstRow.ReadFrom(15).To(24).TrimStart('0'),
				WriteDate = FirstRow.ReadFrom(3).To(10).ParseDate(),
				NumberOfItemsInFile = LastRow.ReadFrom(15).To(21).ToInt(),
				Posts = AllRowsButFirstAndLast.Select(line => itemReader.Read(line)),
			};
		}

		
	}
}