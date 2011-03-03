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
using Account=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes.Account;
using ConsentType=Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.ConsentType;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.SendConsents
{
	public class Task : TaskBase
	{
		private readonly IAutogiroFileWriter<ConsentsFile, Consent> _fileWriter;
		private readonly IBGConfigurationSettingsService _bgConfigurationSettingsService;

		public Task(
			ILoggingService loggingService, 
			IAutogiroFileWriter<ConsentsFile,  Consent> fileWriter ,
			IBGConfigurationSettingsService bgConfigurationSettingsService,
			ITaskRepositoryResolver taskRepositoryResolver) 
			: base("SendConsents", loggingService, taskRepositoryResolver, BGTaskSequenceOrder.SendTask)
		{
			_fileWriter = fileWriter;
			_bgConfigurationSettingsService = bgConfigurationSettingsService;
		}

		public override void Execute()
		{
			RunLoggedTask(repositoryResolver =>
			{
				var fileSectionToSendRepository = repositoryResolver.GetRepository<IFileSectionToSendRepository>();
				var bgConsentToSendRepository = repositoryResolver.GetRepository<IBGConsentToSendRepository>();
				var consents = bgConsentToSendRepository.FindBy(new AllNewConsentsToSendCriteria());
				var consentsFile = ToConsentFile(consents);
				var consentFileContent = _fileWriter.Write(consentsFile);
				var fileSectionToSend = ToFileSectionToSend(consentFileContent);
				fileSectionToSendRepository.Save(fileSectionToSend);
				consents.Each(consent =>
				{
					consent.SendDate = DateTime.Now;
					bgConsentToSendRepository.Save(consent);
				});
			});
		}

		protected virtual FileSectionToSend ToFileSectionToSend(string contents)
		{
			return new FileSectionToSend
			{
				CreatedDate = DateTime.Now,
                SectionData = contents,
                SentDate = null,
                Type = SectionType.ConsentsToSend
			};
		}

		protected virtual ConsentsFile ToConsentFile(IEnumerable<BGConsentToSend> consentsToSend)
		{
			return new ConsentsFile
			{
				Posts = consentsToSend.Select(consent => ToConsent(consent)),
				Reciever = new PaymentReciever
				{
					BankgiroNumber = _bgConfigurationSettingsService.GetPaymentRecieverBankGiroNumber(),
					CustomerNumber = _bgConfigurationSettingsService.GetPaymentRevieverCustomerNumber()
				},
				WriteDate = DateTime.Now
			};
		}

		protected virtual Consent ToConsent(BGConsentToSend consentsToSend)
		{
			return new Consent
			{
				Account = new Account
				{
					AccountNumber = consentsToSend.Account.AccountNumber,
					ClearingNumber = consentsToSend.Account.ClearingNumber
				},
				OrgNumber = consentsToSend.OrgNumber,
				PersonalIdNumber = consentsToSend.PersonalIdNumber,
				RecieverBankgiroNumber = _bgConfigurationSettingsService.GetPaymentRecieverBankGiroNumber(),
				Transmitter = new Payer
				{
					CustomerNumber = consentsToSend.Payer.Id.ToString()
				},
				Type = ToConsentType(consentsToSend.Type)
			};
		}

		protected virtual ConsentType ToConsentType(Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.ConsentType inputType)
		{
			switch (inputType)
			{
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.ConsentType.New:
					return ConsentType.New;
				case Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.ConsentType.Cancellation:
					return ConsentType.Cancellation;
				default:
					throw new ArgumentOutOfRangeException("inputType");
			}
		}
	}
}