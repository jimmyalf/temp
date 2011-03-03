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
    [TestFixture, Category("ReceiveConsentsTaskTests")]
    public class When_receiveing_consents : ReceiveConsentsTaskTestBase
    {
        private IEnumerable<ReceivedFileSection> _receivedSections;
        private Consent _savedConsent;
    	private AutogiroPayer payer;

    	public When_receiveing_consents()
        {
            Context = () =>
            {
            	payer = PayerFactory.Get();
                //_receivedSections = ReceivedConsentsFactory.GetList();
            	_receivedSections = RecievedFileSectionFactory.GetList(SectionType.ReceivedConsents);
                var consentFileSection = ReceivedConsentsFactory.GetReceivedConsentFileSection(payer.Id);
                _savedConsent = ReceivedConsentsFactory.GetConsent(payer.Id);

                A.CallTo(() => ReceivedFileRepository.FindBy(A<AllUnhandledReceivedConsentFileSectionsCriteria>
                                                      .Ignored.Argument)).Returns(_receivedSections);
                A.CallTo(() => ConsentFileReader.Read(A<string>.Ignored)).Returns(consentFileSection);
            	A.CallTo(() => AutogiroPayerRepository.Get(payer.Id)).Returns(payer);
            };
            Because = task => task.Execute();
        }

        [Test]
        public void Task_has_receive_task_ordering()
        {
            Task.TaskOrder.ShouldBe(BGTaskSequenceOrder.ReadTask.ToInteger());
        }

        [Test]
        public void Task_loggs_start_and_stop_messages()
        {
            A.CallTo(() => Log.Info(A<string>.That.Contains("Started"))).MustHaveHappened();
			A.CallTo(() => Log.Info(A<string>.That.Contains("Finished"))).MustHaveHappened();
        }

        [Test]
		public void Task_logs_number_of_received_sections()
        {
            A.CallTo(() => Log.Debug(A<string>.That.Contains("Fetched 15 consent file sections from repository"))).MustHaveHappened();
		}

		[Test]
		public void Task_logs_after_each_handled_sections()
        {
            A.CallTo(() => Log.Debug(A<string>.That.Contains("Saved 10 consents to repository"))).MustHaveHappened();
		}

        [Test]
        public void Task_fetches_new_consentsections_from_repository()
        {
            A.CallTo(() => ReceivedFileRepository.FindBy(
                               A<AllUnhandledReceivedConsentFileSectionsCriteria>.Ignored.Argument))
                               .MustHaveHappened();
        }

        [Test]
        public void Task_saves_fetched_fileposts_as_consents()
        {
            A.CallTo(() => BGReceivedConsentRepository.Save(
                            A<BGReceivedConsent>
                            .That.Matches(x => x.ActionDate.Equals(_savedConsent.ActionDate))
                            .And.Matches(x => x.CommentCode.Equals(_savedConsent.CommentCode))
                            .And.Matches(x => x.ConsentValidForDate.Equals(_savedConsent.ConsentValidForDate))
                            .And.Matches(x => x.InformationCode.Equals(_savedConsent.InformationCode))
                            .And.Matches(x => x.Payer.Id.ToString().Equals(_savedConsent.Transmitter.CustomerNumber))
                            .And.Matches(x => x.CreatedDate.Date.Equals(DateTime.Now.Date))
			            )).MustHaveHappened();
        }

        [Test]
        public void Task_updates_consentsection_as_handled()
        {
            _receivedSections.Each(receivedSection => A.CallTo(() => ReceivedFileRepository.Save(
                A<ReceivedFileSection>
                    .That.Matches(x => Equals(x.HasBeenHandled, true))
                    .And.Matches(x => x.HandledDate.Value.Date.Equals(DateTime.Now.Date))
                ))) ;
        }
    }
}
