
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.Autogiro.Helpers;

namespace Synologen.LensSubscription.Autogiro.Readers
{
	public class ErrorFileContentReader : BaseReader, IFileReader<ErrorsFile, Error> 
	{
		public ErrorsFile Read(string fileContent, IItemReader<Error> itemReader)
		{
			SetupBase(fileContent);

			IEnumerable<string> poster = AllRowsButFirstAndLast;
			IEnumerable<Error> poster2 = poster.Select(line => itemReader.Read(line));

			return new ErrorsFile
			{
				WriteDate = FirstRow.ReadFrom(3).To(10).ParseDate(),
				Reciever = new PaymentReciever
				{
					BankgiroNumber = FirstRow.ReadFrom(69).To(78).TrimStart('0'),
					CustomerNumber = FirstRow.ReadFrom(63).To(68).TrimStart('0')
				},
				Posts = AllRowsButFirstAndLast.Select(line => itemReader.Read(line)),
				NumberOfCreditsInFile = LastRow.ReadFrom(15).To(20).ToInt(),
				NumberOfDebitsInFile = LastRow.ReadFrom(33).To(38).ToInt(),
				TotalCreditAmountInFile = LastRow.ReadFrom(21).To(32).ParseAmount(),
				TotalDebitAmountInFile = LastRow.ReadFrom(39).To(50).ParseAmount()
			};
		}
	}
}