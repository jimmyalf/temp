using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.Core.Extensions;
using PaymentType=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes.PaymentType;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.SendPayments
{
	public class Task : TaskBase
	{
		private readonly IBGServiceCoordinatorSettingsService _bgServiceCoordinatorSettingsService;
		private readonly IAutogiroFileWriter<PaymentsFile, Payment> _paymentFileWriter;

		public Task(
			ILoggingService loggingService, 
			IBGServiceCoordinatorSettingsService bgServiceCoordinatorSettingsService,
			IAutogiroFileWriter<PaymentsFile,Payment> paymentFileWriter,
			ITaskRepositoryResolver taskRepositoryResolver) 
			: base("SendPayments", loggingService, taskRepositoryResolver, BGTaskSequenceOrder.SendTask)
		{
			_bgServiceCoordinatorSettingsService = bgServiceCoordinatorSettingsService;
			_paymentFileWriter = paymentFileWriter;
		}

		public override void Execute()
		{
			RunLoggedTask(repositoryResolver =>
			{
				var bgPaymentToSendRepository = repositoryResolver.GetRepository<IBGPaymentToSendRepository>();
				var fileSectionToSendRepository = repositoryResolver.GetRepository<IFileSectionToSendRepository>();
				var payments = bgPaymentToSendRepository.FindBy(new AllNewPaymentsToSendCriteria());
				if(payments == null || payments.Count() == 0)
				{
					LogInfo("Found no new payments to send.");
					return;
				}
				LogDebug("Found {0} new payments to send", payments.Count());

				var paymentFile = ToPaymentFile(payments);
				var fileData = _paymentFileWriter.Write(paymentFile);
				var section = ToFileSectionToSend(fileData);
				fileSectionToSendRepository.Save(section);
				payments.Each(payment =>
				{
					payment.SendDate = DateTime.Now;
					bgPaymentToSendRepository.Save(payment);
				});
			});
		}

		protected virtual FileSectionToSend ToFileSectionToSend(string contents)
		{
			return new FileSectionToSend
			{
                SectionData = contents,
                SentDate = null,
                Type = SectionType.PaymentsToSend,
			};
		}

		protected virtual PaymentsFile ToPaymentFile(IEnumerable<BGPaymentToSend> consentsToSend)
		{
			return new PaymentsFile
			{
				Posts = consentsToSend.Select(consent => ToPayment(consent)),
				Reciever = new PaymentReciever
				{
					BankgiroNumber = _bgServiceCoordinatorSettingsService.GetPaymentRecieverBankGiroNumber(),
					CustomerNumber = _bgServiceCoordinatorSettingsService.GetPaymentRevieverCustomerNumber()
				},
				WriteDate = DateTime.Now
			};
		}

		protected virtual Payment ToPayment(BGPaymentToSend paymentToSend)
		{
			return new Payment
			{
				Amount = paymentToSend.Amount,
				PaymentDate = paymentToSend.PaymentDate,
				PeriodCode = paymentToSend.PeriodCode.ToInteger().ToEnum<PeriodCode>(),
				RecieverBankgiroNumber = _bgServiceCoordinatorSettingsService.GetPaymentRecieverBankGiroNumber(),
				Reference = paymentToSend.Reference,
				Transmitter = new Payer
				{
					CustomerNumber = paymentToSend.Payer.Id.ToString()
				},
				Type = paymentToSend.Type.ToInteger().ToEnum<PaymentType>()
			};
		}
	}
}