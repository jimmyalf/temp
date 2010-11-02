using System.Linq;
using System.Text;
using Spinit.Extensions;
using Spinit.Wp.Synologen.Autogiro.Helpers;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using ConsentsFile=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.ConsentsFile;
using PaymentsFile=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.PaymentsFile;

namespace Spinit.Wp.Synologen.Autogiro
{
	public class AutogiroParserService : IAutogiroParserService 
	{
		private readonly IAutogiroLineParserService _autogiroLineParserService;
		public AutogiroParserService(IAutogiroLineParserService autogiroLineParserService)
		{
			_autogiroLineParserService = autogiroLineParserService;
		}

		public string ParseConsents(ConsentsFile file)
		{
			var builder = new StringBuilder().AppendLine(_autogiroLineParserService.WriteConsentHeaderLine(file));
			file.Posts.Each(consent => builder.AppendLine(_autogiroLineParserService.WriteConsentPostLine(consent)));
			return builder.ToString().TrimEnd(new []{'\r','\n'});
		}

		public string ParsePayments(PaymentsFile file)
		{
			var builder = new StringBuilder().AppendLine(_autogiroLineParserService.WritePaymentHeaderLine(file));
			file.Posts.Each(payment => builder.AppendLine(_autogiroLineParserService.WritePaymentPostLine(payment)));
			return builder.ToString().TrimEnd(new []{'\r','\n'});
		}

		public Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.PaymentsFile ReadPayments(string fileContents)
		{
			var fileRows = fileContents.SplitIntoRows();

			return new Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.PaymentsFile
			{
				Reciever = new PaymentReciever
				{
					BankgiroNumber =  fileRows[0].ReadFrom(69).To(78).TrimStart('0'),
					CustomerNumber =  fileRows[0].ReadFrom(63).To(68).TrimStart('0')
				},
				WriteDate = fileRows[0].ReadFrom(3).To(10).ParseDate(),
				Posts = fileRows.Except(ListExtensions.IgnoreType.FirstAndLast)
					.Select(line => _autogiroLineParserService.ReadPaymentLine(line)),
			};
		}

		public Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.ConsentsFile ReadConsents(string fileContents)
		{
			return null;
		}

		public ErrorsFile ReadErrors(string fileContents)
		{
			return null;
		}
	}
}