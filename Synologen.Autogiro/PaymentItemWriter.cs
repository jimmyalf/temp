using System;
using Spinit.Wp.Synologen.Autogiro.Helpers;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wp.Synologen.Autogiro
{
	public class PaymentItemWriter : BaseWriter, IItemWriter<Payment>
	{
		public string Write(Payment item)
		{
			return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
				item.Type.ToInteger().ToString().PadLeft(2, '0'),
				item.PaymentDate.ToString("yyyyMMdd"),
				item.PeriodCode.ToInteger(),
				Pad(4),
				item.Transmitter.CustomerNumber.PadLeft(16, '0'),
				item.Amount.ParseAmount().PadLeft(12, '0'),
				item.RecieverBankgiroNumber.PadLeft(10, '0'),
				item.Reference.PadRight(16),
				Pad(11));
		}
	}
}