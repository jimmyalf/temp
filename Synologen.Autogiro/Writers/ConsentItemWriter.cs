using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wp.Synologen.Autogiro.Writers
{
	public class ConsentItemWriter : BaseWriter, IItemWriter<Consent>
	{
		public string Write(Consent item) 
		{ 
			return String.Format("{0}{1}{2}{3}{4}{5}{6}", 
				item.Type.ToInteger().ToString().PadLeft(2, '0'), 
				item.RecieverBankgiroNumber.PadLeft(10, '0'), 
				item.Transmitter.CustomerNumber.PadLeft(16, '0'), 
				(item.Type == ConsentType.New) 
					? item.With(x => x.Account).Return(x => x.ClearingNumber, Pad(4)) 
					: Pad(4),
				(item.Type == ConsentType.New) 
					? item.With(x => x.Account).Return(x => x.AccountNumber.PadLeft(12, '0'), Pad(12)) 
					: Pad(12),
				(item.Type == ConsentType.New) 
					? item.With(x => x.PersonalIdNumber) ??	item.With(x => x.OrgNumber).Return(x => x.PadLeft(12, '0'), Pad(12)) 
					: Pad(12),
				Pad(24));
		}
	}
}