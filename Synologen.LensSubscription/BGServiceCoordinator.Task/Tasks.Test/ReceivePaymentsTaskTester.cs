﻿using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Factories;
using Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test
{
    [TestFixture, Category("ReceivePaymentsTaskTests")]
    public class When_receiveing_payments : ReceivePaymentsTaskTestBase
    {
        private IEnumerable<ReceivedFileSection> _receivedSections;
        private Payment _savedPayment;
    	private AutogiroPayer payer;

    	public When_receiveing_payments()
        {
            Context = () =>
            {
            	payer = PayerFactory.Get();
            	_receivedSections = RecievedFileSectionFactory.GetList(SectionType.ReceivedPayments);
                var paymentsFileSection = ReceivedPaymentsFactory.GetReceivedPaymentsFileSection(payer.Id);
                _savedPayment = ReceivedPaymentsFactory.GetPayment(payer.Id);

                A.CallTo(() => ReceivedFileRepository.FindBy(A<AllUnhandledReceivedPaymentFileSectionsCriteria>.Ignored)).Returns(_receivedSections);
                A.CallTo(() => PaymentFileReader.Read(A<string>.Ignored)).Returns(paymentsFileSection);
				A.CallTo(() => AutogiroPayerRepository.Get(payer.Id)).Returns(payer);
            };
            Because = task => task.Execute(ExecutingTaskContext);    
        }

        [Test]
        public void Task_has_receive_task_ordering()
        {
            Task.TaskOrder.ShouldBe(BGTaskSequenceOrder.ReadTask.ToInteger());
        }

        [Test]
        public void Task_loggs_start_and_stop_messages()
        {
            LoggingService.AssertInfo("Started");
            LoggingService.AssertInfo("Finished");
        }

        [Test]
        public void Task_logs_number_of_received_sections()
        {
            LoggingService.AssertDebug("Fetched 15 payment file sections from repository");
        }

        [Test]
        public void Task_logs_after_each_handled_section()
        {
            LoggingService.AssertDebug("Saved 10 payments to repository");
        }


        [Test]
        public void Task_fetches_new_paymentsections_from_repository()
        {
            A.CallTo(() => ReceivedFileRepository.FindBy(A<AllUnhandledReceivedPaymentFileSectionsCriteria>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void Task_saves_fetched_fileposts_as_payments()
        {
            A.CallTo(() => BGReceivedPaymentRepository.Save(A<BGReceivedPayment>.That.Matches(x => 
				x.Amount.Equals(_savedPayment.Amount)
                && x.CreatedDate.Date.Equals(DateTime.Now.Date)
                && x.Payer.Id.ToString().Equals(_savedPayment.Transmitter.CustomerNumber)
                && x.PaymentDate.Date.Equals(_savedPayment.PaymentDate.Date)
                && x.Reference.Equals(_savedPayment.Reference)
                && x.ResultType.Equals(_savedPayment.Result)
			))).MustHaveHappened();
        }

    	[Test]
        public void Task_updates_paymentsection_as_handled()
        {
            _receivedSections.Each(receivedSection => A.CallTo(() => ReceivedFileRepository.Save(A<ReceivedFileSection>.That.Matches(x => 
				Equals(x.HasBeenHandled, true) && x.HandledDate.Value.Date.Equals(DateTime.Now.Date))
			)));
        }
    }
}
