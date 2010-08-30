using System;
using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Services;
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
		private IFrameOrderSettingsService frameOrderSettingsService;

		[SetUp]
		public void Context()
		{
			repository = Factories.RepositoryFactory.GetFrameRepository();
			frameGlassTypeRepository = Factories.RepositoryFactory.GetFrameGlassRepository();
			frameOrderSettingsService = Factories.ServiceFactory.GetFrameOrderSettingsService();
			view = Factories.ViewsFactory.GetFrameOrderView();
			presenter = new FrameOrderPresenter(view, repository, frameGlassTypeRepository, frameOrderSettingsService);
		}

		[Test]
		public void When_View_Is_Loaded_Model_Has_Expected_Values()
		{
			//Arrange

			//Act
			presenter.View_Load(null, new EventArgs());

			//Assert
			Expect(view.Model.SelectedFrameId, Is.EqualTo(0));
			Expect(view.Model.SelectedGlassTypeId, Is.EqualTo(0));

			Expect(view.Model.SelectedPupillaryDistanceLeft, Is.EqualTo(int.MinValue));
			Expect(view.Model.SelectedPupillaryDistanceRight, Is.EqualTo(int.MinValue));

			Expect(view.Model.SelectedSphereLeft, Is.EqualTo(int.MinValue));
			Expect(view.Model.SelectedSphereRight, Is.EqualTo(int.MinValue));

			Expect(view.Model.SelectedCylinderLeft, Is.EqualTo(int.MinValue));
			Expect(view.Model.SelectedCylinderRight, Is.EqualTo(int.MinValue));

			Expect(view.Model.FramesList.Count(), Is.EqualTo(11));
			Expect(view.Model.FramesList.ToList()[0].Id, Is.EqualTo(0));
			Expect(view.Model.FramesList.ToList()[0].Name, Is.EqualTo("-- Välj båge --"));
			Expect(view.Model.FrameRequiredErrorMessage, Is.EqualTo("Båge saknas"));

			Expect(view.Model.PupillaryDistanceList.Count(), Is.EqualTo(1));
			Expect(view.Model.PupillaryDistanceList.ToList()[0].Value, Is.EqualTo(int.MinValue));
			Expect(view.Model.PupillaryDistanceList.ToList()[0].Name, Is.EqualTo("-- Välj PD --"));
			Expect(view.Model.PupillaryDistanceRequiredErrorMessage, Is.EqualTo("PD saknas"));

			Expect(view.Model.SphereList.Count(), Is.EqualTo(50));
			Expect(view.Model.SphereList.ToList()[0].Value, Is.EqualTo(int.MinValue));
			Expect(view.Model.SphereList.ToList()[0].Name, Is.EqualTo("-- Välj Sfär --"));
			Expect(view.Model.SphereRequiredErrorMessage, Is.EqualTo("Sfär saknas"));

			Expect(view.Model.CylinderList.Count(), Is.EqualTo(10));
			Expect(view.Model.CylinderList.ToList()[0].Value, Is.EqualTo(int.MinValue));
			Expect(view.Model.CylinderList.ToList()[0].Name, Is.EqualTo("-- Välj Cylinder --"));
			Expect(view.Model.CylinderRequiredErrorMessage, Is.EqualTo("Cylinder saknas"));

			Expect(view.Model.GlassTypesList.Count(), Is.EqualTo(11));
			Expect(view.Model.GlassTypesList.ToList()[0].Id, Is.EqualTo(0));
			Expect(view.Model.GlassTypesList.ToList()[0].Name, Is.EqualTo("-- Välj glastyp --"));
			Expect(view.Model.GlassTypeRequiredErrorMessage, Is.EqualTo("Glastyp saknas"));

			Expect(view.Model.NotSelectedIntervalValue, Is.EqualTo(int.MinValue));
		}

		[Test]
		public void When_Model_Is_Bound_Selected_Model_Has_Expected_Values()
		{
			//Arrange
			var frameSelectedEventArgs = new FrameFormEventArgs
			{
				SelectedFrameId = 5, 
				SelectedGlassTypeId = 5,
				SelectedPupillaryDistance = new EyeParameter{Left = 22, Right = 33},
				SelectedSphere = new EyeParameter{Left = -5, Right = 2.25M},
                SelectedCylinder = new EyeParameter{ Left = 0.25M, Right = 1.75M},
			};
			const int expectedNumberOfFramesInList = 11;
			const int expectedNumberOfGlassTypesInList = 11;
			const int expectedNumberOfPDsInList = 22;

			//Act
			presenter.View_Load(null, new EventArgs());
			presenter.View_BindModel(null, frameSelectedEventArgs);

			//Assert
			Expect(view.Model.PupillaryDistanceList.Count(), Is.EqualTo(expectedNumberOfPDsInList));
			Expect(view.Model.GlassTypesList.Count(), Is.EqualTo(expectedNumberOfGlassTypesInList));
			Expect(view.Model.FramesList.Count(), Is.EqualTo(expectedNumberOfFramesInList));

			Expect(view.Model.SelectedFrameId, Is.EqualTo(frameSelectedEventArgs.SelectedFrameId));
			Expect(view.Model.SelectedGlassTypeId, Is.EqualTo(frameSelectedEventArgs.SelectedGlassTypeId));
			Expect(view.Model.SelectedPupillaryDistanceLeft, Is.EqualTo(frameSelectedEventArgs.SelectedPupillaryDistance.Left));
			Expect(view.Model.SelectedPupillaryDistanceRight, Is.EqualTo(frameSelectedEventArgs.SelectedPupillaryDistance.Right));
			Expect(view.Model.SelectedSphereLeft, Is.EqualTo(frameSelectedEventArgs.SelectedSphere.Left));
			Expect(view.Model.SelectedSphereRight, Is.EqualTo(frameSelectedEventArgs.SelectedSphere.Right));
			Expect(view.Model.SelectedCylinderLeft, Is.EqualTo(frameSelectedEventArgs.SelectedCylinder.Left));
			Expect(view.Model.SelectedCylinderRight, Is.EqualTo(frameSelectedEventArgs.SelectedCylinder.Right));
		}

		[Test]
		public void When_Model_Is_Bound_Model_With_Invalid_Parameters_Get_Default_Values()
		{
			//Arrange
			var frameSelectedEventArgs = new FrameFormEventArgs
			{
				SelectedFrameId = 1, 
				SelectedGlassTypeId = 1,
				SelectedPupillaryDistance = new EyeParameter{Left = 200, Right = -20},
				SelectedSphere = new EyeParameter{Left =10, Right = 42},
				SelectedCylinder = new EyeParameter{Left = -0.25M, Right = 3}
			};

			//Act
			presenter.View_Load(null, new EventArgs());
			presenter.View_BindModel(null, frameSelectedEventArgs);

			//Assert
			Expect(view.Model.SelectedFrameId, Is.EqualTo(1));
			Expect(view.Model.SelectedGlassTypeId, Is.EqualTo(1));
			Expect(view.Model.SelectedPupillaryDistanceLeft, Is.EqualTo(int.MinValue));
			Expect(view.Model.SelectedPupillaryDistanceRight, Is.EqualTo(int.MinValue));
			Expect(view.Model.SelectedSphereLeft, Is.EqualTo(int.MinValue));
			Expect(view.Model.SelectedSphereRight, Is.EqualTo(int.MinValue));
			Expect(view.Model.SelectedCylinderLeft, Is.EqualTo(int.MinValue));
			Expect(view.Model.SelectedCylinderRight, Is.EqualTo(int.MinValue));
		}
	}
}