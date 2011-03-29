using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.Autogiro.Helpers;

namespace Synologen.LensSubscription.Autogiro.Readers
{
	public class PaymentsFileContentReader : BaseReader, IFileReader<PaymentsFile, Payment>
	{
		public PaymentsFile Read(string fileContent, IItemReader<Payment> itemReader) 
		{ 
			SetupBase(fileContent);
			var paymentsFile =  new PaymentsFile
			{
				Reciever = new PaymentReciever
				{
					BankgiroNumber = FirstRow.ReadFrom(69).To(78).TrimStart('0'),
					CustomerNumber =  FirstRow.ReadFrom(63).To(68).TrimStart('0')
				},
				WriteDate = FirstRow.ReadFrom(3).To(10).ParseDate(),
				Posts = AllRowsButFirstAndLast.Select(line => itemReader.Read(line)),
				NumberOfCreditsInFile = LastRow.ReadFrom(41).To(46).ToInt(),
				NumberOfDebitsInFile = LastRow.ReadFrom(47).To(52).ToInt(),
				TotalCreditAmountInFile = LastRow.ReadFrom(29).To(40).ParseAmount(),
				TotalDebitAmountInFile = LastRow.ReadFrom(57).To(68).ParseAmount()
			};
			return paymentsFile;
		}
	}
}