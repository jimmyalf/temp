
using Spinit.Wp.Synologen.Autogiro.Helpers;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wp.Synologen.Autogiro.Readers
{
	public class ErrorItemReader : IItemReader<Error> 
	{
		public Error Read(string line)
		{
			return new Error
					{
						Amount = line.ReadFrom(31).To(42).TrimStart('0').ParseAmount(),
						CommentCode = line.ReadFrom(59).To(60).ParseEnum<ErrorCommentCode>(),
						NumberOfReoccuringTransactionsLeft = line.ReadFrom(12).To(14).ParseNullable<int>(StringExtensions.ToInt),
						PaymentDate = line.ReadFrom(3).To(10).ParseDate(),
						PeriodCode = line.ReadFrom(11).To(11).ParseEnum<PeriodCode>(),
						Reference = line.ReadFrom(43).To(58).TrimEnd(),
						Transmitter = new Payer { CustomerNumber = line.ReadFrom(15).To(30).TrimStart('0') },
						Type = line.ReadFrom(1).To(2).ParseEnum<PaymentType>()
					};
		}
	}
}
