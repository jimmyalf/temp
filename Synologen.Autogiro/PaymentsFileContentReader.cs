using System.Linq;
using Spinit.Wp.Synologen.Autogiro.Helpers;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wp.Synologen.Autogiro
{
	public class PaymentsFileContentReader : IFileReader<PaymentsFile, Payment>
	{
		public PaymentsFile Read(string fileContents, IItemReader<Payment> itemReader) 
		{ 
			var fileRows = fileContents.SplitIntoRows();

			return new PaymentsFile
			{
				Reciever = new PaymentReciever
				{
					BankgiroNumber =  fileRows.First().ReadFrom(69).To(78).TrimStart('0'),
					CustomerNumber =  fileRows.First().ReadFrom(63).To(68).TrimStart('0')
				},
				WriteDate = fileRows.First().ReadFrom(3).To(10).ParseDate(),
				Posts = fileRows.Except(ListExtensions.IgnoreType.FirstAndLast).Select(line => itemReader.Read(line)),
				NumberOfCreditsInFile = fileRows.Last().ReadFrom(41).To(46).ToInt(),
				NumberOfDebitsInFile = fileRows.Last().ReadFrom(47).To(52).ToInt(),
				TotalCreditAmountInFile = fileRows.Last().ReadFrom(29).To(40).ParseAmount(),
				TotalDebitAmountInFile = fileRows.Last().ReadFrom(57).To(68).ParseAmount()
			};
		}
	}
}