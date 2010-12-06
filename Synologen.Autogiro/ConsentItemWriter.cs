using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wp.Synologen.Autogiro
{
	public class ConsentItemWriter : BaseWriter, IItemWriter<Consent>
	{
		public string Write(Consent item) 
		{ 
			return String.Format("{0}{1}{2}{3}{4}{5}", 
				item.Type.ToInteger().ToString().PadLeft(2, '0'), 
				item.Reciever.BankgiroNumber.PadLeft(10, '0'), 
				item.Transmitter.CustomerNumber.PadLeft(16, '0'), 
				(item.Type == ConsentType.New) ? item.AccountAndClearingNumber.PadLeft(16, '0') : String.Empty,
				(item.Type == ConsentType.New) ? item.With(x => x.PersonalIdNumber) ??	item.With(x => x.OrgNumber).Return(x => x.PadLeft(12, '0'), String.Empty) : String.Empty,
				Pad(24));
		}
	}
}