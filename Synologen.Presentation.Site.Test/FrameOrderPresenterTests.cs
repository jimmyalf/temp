using System;
using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test
{
	[TestFixture]
	public class Given_a_FrameOrderPresenter : AssertionHelper
	{
		private FrameOrderPresenter presenter;
		private IFrameOrderView<FrameOrderModel> view;
		private IFrameRepository repository;

		[SetUp]
		public void Context()
		{
			repository = Factories.RepositoryFactory.GetFrameRepository();
			view = Factories.ViewsFactory.GetFrameOrderView();
			presenter = new FrameOrderPresenter(view, repository);
		}

		[Test]
		public void When_View_Is_Loaded_Model_Has_Expected_Values()
		{
			//Arrange
			var eventArgs = new EventArgs();

			//Act
			presenter.View_Load(null, eventArgs);

			//Assert
			Expect(view.Model.FramesList.Count(), Is.EqualTo(1));
			Expect(view.Model.FramesList.ToList()[0].Id, Is.EqualTo(0));
			Expect(view.Model.FramesList.ToList()[0].Name, Is.EqualTo("-- Välj båge --"));
			//Expect(view.Model.IndexList.Count(), Is.EqualTo(1));
			//Expect(view.Model.IndexList.ToList()[0].Name, Is.EqualTo("-- Välj index --"));
			//Expect(view.Model.IndexList.ToList()[0].Value, Is.EqualTo(int.MinValue));
			//Expect(view.Model.SphereList.Count(), Is.EqualTo(1));
			//Expect(view.Model.SphereList.ToList()[0].Name, Is.EqualTo("-- Välj sfär --"));
			//Expect(view.Model.SphereList.ToList()[0].Value, Is.EqualTo(int.MinValue));

			//Expect(view.Model.IndexRequiredErrorMessage, Is.EqualTo("Index saknas"));
			Expect(view.Model.FrameRequiredErrorMessage, Is.EqualTo("Båge saknas"));
			//Expect(view.Model.SphereRequiredErrorMessage, Is.EqualTo("Sfär saknas"));
			Expect(view.Model.Message, Is.EqualTo("Testar Web Forms MVP!"));
			Expect(view.Model.NotSelectedIntervalValue, Is.EqualTo(int.MinValue));
			Expect(view.Model.SelectedFrameId, Is.EqualTo(0));
		}

		[Test]
		public void When_Frame_Is_Selected_Model_Has_Expected_Values()
		{
			//Arrange
			var frameSelectedEventArgs = new FrameSelectedEventArgs {SelectedFrameId = 1};

			//Act
			presenter.View_FrameSelected(null, frameSelectedEventArgs);

			//Assert
			Expect(view.Model.Message, Is.EqualTo("Vald båge med id: 1"));
			Expect(view.Model.SelectedFrameId, Is.EqualTo(1));
			//Expect(view.Model.IndexList.Count(), Is.EqualTo(3));
			//Expect(view.Model.IndexList.ToList()[0].Name, Is.EqualTo("-- Välj index --"));
			//Expect(view.Model.SphereList.Count(), Is.EqualTo(50));
			//Expect(view.Model.SphereList.ToList()[0].Name, Is.EqualTo("-- Välj sfär --"));
		}

		[Test]
		public void When_Form_Is_Valid_And_Submitted_Model_Has_Expected_Values()
		{
			//Arrange
			var frameOrderFormSubmitEventArgs = new FrameOrderFormSubmitEventArgs
			{
				PageIsValid = true,
				SelectedFrameId = 1,
				SelectedIndex = 1.6m,
				SelectedSphere = -5.25m
			};

			//Act
			presenter.View_SumbitForm(null, frameOrderFormSubmitEventArgs);

			//Assert
			//TODO: Save frame order && redirect user to thank you page
			Expect(view.Model.Message, Is.EqualTo("Submit returned a valid result and should now create an order and redirect"));
		}

		[Test]
		public void When_Form_Is_Invalid_And_Submitted_Model_Has_Expected_Values()
		{
			//Arrange
			var frameOrderFormSubmitEventArgs = new FrameOrderFormSubmitEventArgs
			{
				PageIsValid = false,
				SelectedFrameId = 1,
				SelectedIndex = 1.6m,
				SelectedSphere = -5.25m
			};

			//Act
			presenter.View_SumbitForm(null, frameOrderFormSubmitEventArgs);

			//Assert
			Expect(view.Model.SelectedFrameId, Is.EqualTo(1));
			Expect(view.Model.Message, Is.EqualTo("Submit did not return a valid result!"));
		}
		
	}
}