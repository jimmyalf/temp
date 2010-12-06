using System.Linq;
using Spinit.Wp.Synologen.Autogiro.Helpers;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wp.Synologen.Autogiro
{
	public class PaymentsFileContentReader : BaseContentReader, IFileReader<PaymentsFile, Payment>
	{
		public PaymentsFileContentReader(string fileContents) : base(fileContents) {}

		public PaymentsFile Read(IItemReader<Payment> itemReader) 
		{ 
			return new PaymentsFile
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
		}
	}
}