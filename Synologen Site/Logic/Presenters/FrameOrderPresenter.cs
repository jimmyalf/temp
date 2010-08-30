using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Helpers;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters
{
	public class FrameOrderPresenter : Presenter<IFrameOrderView<FrameOrderModel>>
	{
		private readonly IFrameRepository _frameRepository;
		private readonly IFrameGlassTypeRepository _frameGlassTypeRepository;
		private readonly IFrameOrderSettingsService _frameOrderSettingsService;
		private readonly IEnumerable<IntervalListItem> EmptyIntervalList = new List<IntervalListItem>();
		private readonly FrameListItem DefaultFrame = new FrameListItem {Id = 0, Name = "-- Välj båge --"};

		public FrameOrderPresenter(IFrameOrderView<FrameOrderModel> view, IFrameRepository repository, IFrameGlassTypeRepository frameGlassTypeRepository, IFrameOrderSettingsService frameOrderSettingsService) : base(view)
		{
			_frameRepository = repository;
			_frameGlassTypeRepository = frameGlassTypeRepository;
			_frameOrderSettingsService = frameOrderSettingsService;
			InitiateEventHandlers();
		}

		private void InitiateEventHandlers()
		{
			View.Load += View_Load;
			View.FrameSelected += View_BindModel;
			View.SubmitForm += View_SumbitForm;
			View.GlassTypeSelected += View_BindModel;
		}

		public void View_Load(object sender, EventArgs e)
		{
			InitializeModel();
		}

		public void View_BindModel(object sender, FrameFormEventArgs e)
		{
			UpdateModel(e);
		}

		public void View_SumbitForm(object sender, FrameFormEventArgs e) 
		{ 
			if(e.PageIsValid)
			{
				//TODO: Save frame order && redirect user to thank you page
			}
			else
			{
				View.Model.SelectedFrameId = e.SelectedFrameId;
			}
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}

		public void InitializeModel()
		{
			View.Model.FramesList = _frameRepository.GetAll().ToFrameViewList().InsertFirst(DefaultFrame);
			View.Model.PupillaryDistance = EmptyIntervalList.CreateDefaultEyeParameter("PD");
			View.Model.GlassTypesList = _frameGlassTypeRepository.GetAll().ToFrameGlassTypeViewList().InsertFirst(new FrameGlassTypeListItem {Id = 0, Name = "-- Välj glastyp --"});
			View.Model.Sphere = _frameOrderSettingsService.Sphere.GetList().CreateDefaultEyeParameter("Sfär");
			View.Model.Cylinder = _frameOrderSettingsService.Cylinder.GetList().CreateDefaultEyeParameter("Cylinder");
			View.Model.Addition = EmptyIntervalList.CreateDefaultEyeParameter("Addition");
			View.Model.Height = EmptyIntervalList.CreateDefaultEyeParameter("Höjd");
			View.Model.AxisSelection = new EyeParameter {Left = 0, Right = 0};
			View.Model.FrameRequiredErrorMessage = "Båge saknas";
			View.Model.GlassTypeRequiredErrorMessage = "Glastyp saknas";
			View.Model.PupillaryDistanceRequiredErrorMessage = "PD saknas";
			View.Model.SphereRequiredErrorMessage = "Sfär saknas";
			View.Model.CylinderRequiredErrorMessage = "Cylinder saknas";
			View.Model.AdditionRequiredErrorMessage = "Addition saknas";
			View.Model.HeightRequiredMessage = "Höjd saknas";
			
		}

		public void UpdateModel(FrameFormEventArgs e)
		{

			var frame = _frameRepository.Get(e.SelectedFrameId);
			var glassType = _frameGlassTypeRepository.Get(e.SelectedGlassTypeId);

			View.Model.SelectedFrameId = e.SelectedFrameId;
			View.Model.SelectedGlassTypeId = e.SelectedGlassTypeId;
			View.Model.HeightParametersEnabled = glassType.IncludeHeightParametersInOrder;
			View.Model.AdditionParametersEnabled = glassType.IncludeAdditionParametersInOrder;
			View.Model.PupillaryDistance = e.GetEyeParameter(x => x.SelectedPupillaryDistance, frame.PupillaryDistance.GetList(), "PD");
			View.Model.Sphere = e.GetEyeParameter(x => x.SelectedSphere, _frameOrderSettingsService.Sphere.GetList(), "Sfär");
			View.Model.Cylinder = e.GetEyeParameter(x => x.SelectedCylinder, _frameOrderSettingsService.Cylinder.GetList(), "Cylinder");
			View.Model.AxisSelection = new EyeParameter {Left = e.SelectedAxis.Left, Right = e.SelectedAxis.Right};

			if(glassType.IncludeAdditionParametersInOrder)
			{
				View.Model.Addition = e.GetEyeParameter(x => x.SelectedAddition, _frameOrderSettingsService.Addition.GetList(), "Addition");
			}
			if(glassType.IncludeHeightParametersInOrder)
			{
				View.Model.Height = e.GetEyeParameter(x => x.SelectedHeight, _frameOrderSettingsService.Height.GetList(), "Höjd");
			}
		}
	}
}