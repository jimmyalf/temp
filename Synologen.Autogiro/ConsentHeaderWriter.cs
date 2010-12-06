using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;

namespace Spinit.Wp.Synologen.Autogiro
{
	public class ConsentHeaderWriter : BaseWriter, IHeaderWriter<ConsentsFile>
	{
		public string Write(ConsentsFile item) 
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