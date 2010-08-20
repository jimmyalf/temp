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
		private IFrameGlassTypeRepository frameGlassTypeRepository;

		[SetUp]
		public void Context()
		{
			repository = Factories.RepositoryFactory.GetFrameRepository();
			frameGlassTypeRepository = Factories.RepositoryFactory.GetFrameGlassRepository();
			view = Factories.ViewsFactory.GetFrameOrderView();
			presenter = new FrameOrderPresenter(view, repository, frameGlassTypeRepository);
		}

		[Test]
		public void When_View_Is_Loaded_Model_Has_Expected_Values()
		{
			//Arrange

			//Act
			presenter.View_Load(null, new EventArgs());

			//Assert
			Expect(view.Model.SelectedFrameId, Is.EqualTo(0));
			Expect(view.Model.SelectedPupillaryDistanceLeft, Is.EqualTo(int.MinValue));
			Expect(view.Model.SelectedPupillaryDistanceRight, Is.EqualTo(int.MinValue));

			Expect(view.Model.FramesList.Count(), Is.EqualTo(1));
			Expect(view.Model.FramesList.ToList()[0].Id, Is.EqualTo(0));
			Expect(view.Model.FramesList.ToList()[0].Name, Is.EqualTo("-- Välj båge --"));

			Expect(view.Model.PupillaryDistanceList.Count(), Is.EqualTo(1));
			Expect(view.Model.PupillaryDistanceList.ToList()[0].Value, Is.EqualTo(int.MinValue));
			Expect(view.Model.PupillaryDistanceList.ToList()[0].Name, Is.EqualTo("-- Välj PD --"));

			Expect(view.Model.GlassTypesList.Count(), Is.EqualTo(1));
			Expect(view.Model.GlassTypesList.ToList()[0].Id, Is.EqualTo(0));
			Expect(view.Model.GlassTypesList.ToList()[0].Name, Is.EqualTo("-- Välj glastyp --"));

			Expect(view.Model.FrameRequiredErrorMessage, Is.EqualTo("Båge saknas"));
			Expect(view.Model.PupillaryDistanceRequiredErrorMessage, Is.EqualTo("PD saknas"));
			Expect(view.Model.GlassTypeRequiredErrorMessage, Is.EqualTo("Glastyp saknas"));

			Expect(view.Model.NotSelectedIntervalValue, Is.EqualTo(int.MinValue));
			
		}

		[Test]
		public void When_Model_Is_Bound_Selected_Model_Has_Expected_Values()
		{
			//Arrange
			var frameSelectedEventArgs = new FrameFormEventArgs
			{
				SelectedFrameId = 1, 
				SelectedGlassTypeId = 1,
				SelectedPupillaryDistanceLeft = 22, 
				SelectedPupillaryDistanceRight = 33
			};

			//Act
			presenter.View_Load(null, new EventArgs());
			presenter.View_BindModel(null, frameSelectedEventArgs);

			//Assert
			Expect(view.Model.SelectedFrameId, Is.EqualTo(1));
			Expect(view.Model.SelectedGlassTypeId, Is.EqualTo(1));
			Expect(view.Model.PupillaryDistanceList.Count(), Is.EqualTo(42));
			Expect(view.Model.SelectedPupillaryDistanceLeft, Is.EqualTo(22));
			Expect(view.Model.SelectedPupillaryDistanceRight, Is.EqualTo(33));
		}

		[Test]
		public void When_Model_Is_Bound_Model_With_Invalid_Parameters_Get_Default_Values()
		{
			//Arrange
			var frameSelectedEventArgs = new FrameFormEventArgs
			{
				SelectedFrameId = 1, 
				SelectedGlassTypeId = 1,
				SelectedPupillaryDistanceLeft = 200, 
				SelectedPupillaryDistanceRight = -20
			};

			//Act
			presenter.View_Load(null, new EventArgs());
			presenter.View_BindModel(null, frameSelectedEventArgs);

			//Assert
			Expect(view.Model.SelectedFrameId, Is.EqualTo(1));
			Expect(view.Model.SelectedGlassTypeId, Is.EqualTo(1));
			Expect(view.Model.PupillaryDistanceList.Count(), Is.EqualTo(42));
			Expect(view.Model.SelectedPupillaryDistanceLeft, Is.EqualTo(int.MinValue));
			Expect(view.Model.SelectedPupillaryDistanceRight, Is.EqualTo(int.MinValue));
		}
	}
}