using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;

namespace Synologen.LensSubscription.Autogiro.Writers
{
	public class PaymentHeaderWriter : BaseWriter, IHeaderWriter<PaymentsFile>
	{
		public string Write(PaymentsFile item)
		{
			return String.Format("01{0}AUTOGIRO{1}{2}{3}{4}",
			                     item.WriteDate.ToString("yyyyMMdd"),
			                     Pad(44),
			                     item.Reciever.CustomerNumber.PadLeft(6, '0'),
			                     item.Reciever.BankgiroNumber.PadLeft(10, '0'),
			                     Pad(2));
		}
	}
}