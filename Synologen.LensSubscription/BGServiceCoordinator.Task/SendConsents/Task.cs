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
		private readonly IBGConsentToSendRepository _bgConsentToSendRepository;
		private readonly IFileSectionToSendRepository _fileSectionToSendRepository;
		private readonly IAutogiroFileWriter<ConsentsFile, Consent> _fileWriter;
		private readonly IBGConfigurationSettings _bgConfigurationSettings;

		public Task(
			ILoggingService loggingService, 
			IBGConsentToSendRepository bgConsentToSendRepository, 
			IFileSectionToSendRepository fileSectionToSendRepository, 
			IAutogiroFileWriter<ConsentsFile,  Consent> fileWriter ,
			IBGConfigurationSettings bgConfigurationSettings
		) 
			: base("SendConsents", loggingService, BGTaskSequenceOrder.SendTask)
		{
			_bgConsentToSendRepository = bgConsentToSendRepository;
			_fileSectionToSendRepository = fileSectionToSendRepository;
			_fileWriter = fileWriter;
			_bgConfigurationSettings = bgConfigurationSettings;
		}

		public override void Execute()
		{
			RunLoggedTask(() =>
			{
				var consents = _bgConsentToSendRepository.FindBy(new AllNewConsentsToSendCriteria());
				var consentsFile = ToConsentFile(consents);
				var consentFileContent = _fileWriter.Write(consentsFile);
				var fileSectionToSend = ToFileSectionToSend(consentFileContent);
				_fileSectionToSendRepository.Save(fileSectionToSend);
				consents.Each(consent =>
				{
					consent.SendDate = DateTime.Now;
					_bgConsentToSendRepository.Save(consent);
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
					BankgiroNumber = _bgConfigurationSettings.GetPaymentRecieverBankGiroNumber(),
					CustomerNumber = _bgConfigurationSettings.GetPaymentRevieverCustomerNumber()
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
				RecieverBankgiroNumber = _bgConfigurationSettings.GetPaymentRecieverBankGiroNumber(),
				Transmitter = new Payer
				{
					CustomerNumber = consentsToSend.CustomerNumber
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