namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
	public class PaymentReciever : EqualBase<PaymentReciever>
	{
		public string CustomerNumber { get; set; }
		public string BankgiroNumber { get; set; }


		protected override bool EqualityImplementation(PaymentReciever other)
		{
			return other.BankgiroNumber == BankgiroNumber && other.CustomerNumber == other.CustomerNumber;
		}
	}
}