using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test
{
	[TestFixture, Category("SendConsentsTaskTests")]
	public class When_sending_consents : SendConsentsTaskTestBase
	{
		private string fileData;
		private IList<BGConsentToSend> consentsToSend;
		private string paymentRecieverBankGiroNumber;
		private string paymentRecieverCustomerNumber;
		private AutogiroPayer payer;

		public When_sending_consents()
		{
			Context = () =>
			{
				paymentRecieverBankGiroNumber = "555555";
				paymentRecieverCustomerNumber = "123456";
				payer = PayerFactory.Get();
				consentsToSend = ConsentsFactory.GetList(payer);
				fileData = ConsentsFactory.GetTestConsentFileData();
				A.CallTo(() => BGConsentToSendRepository.FindBy(A<AllNewConsentsToSendCriteria>.Ignored.Argument)).Returns(consentsToSend);
				A.CallTo(() => ConsentFileWriter.Write(A<ConsentsFile>.Ignored)).Returns(fileData);
				A.CallTo(() => BgConfigurationSettingsService.GetPaymentRecieverBankGiroNumber()).Returns(paymentRecieverBankGiroNumber);
				A.CallTo(() => BgConfigurationSettingsService.GetPaymentRevieverCustomerNumber()).Returns(paymentRecieverCustomerNumber);
				A.CallTo(() => AutogiroPayerRepository.Get(A<int>.Ignored)).Returns(payer);
			};
			Because = task => task.Execute();
		}

		[Test]
		public void Task_has_send_task_ordering()
		{
			Task.TaskOrder.ShouldBe(BGTaskSequenceOrder.SendTask.ToInteger());
		}

		[Test]
		public void Task_loggs_start_and_stop_messages()
		{
			A.CallTo(() => Log.Info(A<string>.That.Contains("Started"))).MustHaveHappened();
			A.CallTo(() => Log.Info(A<string>.That.Contains("Finished"))).MustHaveHappened();
		}

		[Test]
		public void Task_fetches_new_consents_from_repository()
		{
			A.CallTo(() => BGConsentToSendRepository.FindBy(
				A<AllNewConsentsToSendCriteria>.Ignored.Argument)
			).MustHaveHappened();
		}

		[Test]
		public void Task_parses_fetched_consents_into_consentFile()
		{

			A.CallTo(() => ConsentFileWriter.Write(
				A<ConsentsFile>
				.That.Matches(x => MatchConsents(x.Posts, consentsToSend, paymentRecieverBankGiroNumber))
				.And.Matches(x => x.Reciever.BankgiroNumber.Equals(paymentRecieverBankGiroNumber))
				.And.Matches(x => x.Reciever.CustomerNumber.Equals(paymentRecieverCustomerNumber))
				.And.Matches(x => x.WriteDate.Date.Equals(DateTime.Now.Date))
			)).MustHaveHappened();
		}

		[Test]
		public void Task_stores_consents_as_file_sections_to_send()
		{
			A.CallTo(() => FileSectionToSendRepository.Save(
				A<FileSectionToSend>
				.That.Matches(x => x.CreatedDate.Date.Equals(DateTime.Now.Date))
				.And.Matches(x => x.HasBeenSent.Equals(false))
				.And.Matches(x => x.SectionData.Equals(fileData))
				.And.Matches(x => Equals(x.SentDate, null))
				.And.Matches(x => x.Type.Equals(SectionType.ConsentsToSend))
				.And.Matches(x => x.TypeName.Equals("ConsentsToSend"))
			)).MustHaveHappened();
		}

		[Test]
		public void Task_updates_consents_as_sent()
		{
			consentsToSend.Each(consentToSend => A.CallTo(() => BGConsentToSendRepository.Save(
        		A<BGConsentToSend>
        			.That.Matches(x => x.Id.Equals(consentToSend.Id))
        			.And.Matches(x => Equals(x.SendDate.Value.Date, DateTime.Now.Date))
            )).MustHaveHappened());
		}
	}
}