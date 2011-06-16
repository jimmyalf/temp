using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.Autogiro.Helpers;

namespace Synologen.LensSubscription.Autogiro.Writers
{
	public class PaymentItemWriter : BaseWriter, IItemWriter<Payment>
	{
		public string Write(Payment item)
		{
			return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                     item.Type.ToInteger().ToString().PadLeft(2, '0'),
                     item.PaymentDate.ToString("yyyyMMdd"),
                     item.PaymentPeriodCode.ToInteger(),
                     Pad(4),
                     item.Transmitter.CustomerNumber.PadLeft(16, '0'),
                     item.Amount.ParseAmount().PadLeft(12, '0'),
                     item.RecieverBankgiroNumber.PadLeft(10, '0'),
					 (item.Reference ?? String.Empty).PadRight(16), //Reference, see comment below
                     Pad(11));
		}

		/*
		 * Comment about Reference:
		 * In order to use reference, it must be padded correctly.
		 * å,ä,ö etc consumes more than one character when output to a file (depending on encoding)
		 * This must be properly tested before Reference is used. 
		 * (An option could be to parse text into a byte array for the selected encoding and count the number of bytes 
		 * to determine the padding to be use. However, this should be tested before used.
		 */
	}
}