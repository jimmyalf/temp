using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Helpers;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters
{
	public class FrameOrderPresenter : Presenter<IFrameOrderView<FrameOrderModel>>
	{
		private readonly IFrameRepository _repository;
		private readonly IFrameGlassTypeRepository _frameGlassTypeRepository;

		public FrameOrderPresenter(IFrameOrderView<FrameOrderModel> view, IFrameRepository repository, IFrameGlassTypeRepository frameGlassTypeRepository) : base(view)
		{
			_repository = repository;
			_frameGlassTypeRepository = frameGlassTypeRepository;
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

		public void UpdateModel(FrameFormEventArgs e)
		{
			View.Model.SelectedFrameId = e.SelectedFrameId;
			View.Model.SelectedGlassTypeId = e.SelectedGlassTypeId;

			var frame = _repository.Get(e.SelectedFrameId);

			View.Model.PupillaryDistanceList = frame.PupillaryDistance.GetList().InsertDefaultValue("PD", View.Model.NotSelectedIntervalValue);
			View.Model.SelectedPupillaryDistanceLeft = View.Model.PupillaryDistanceList.Any(x => x.Value.Equals(e.SelectedPupillaryDistanceLeft)) ? e.SelectedPupillaryDistanceLeft : View.Model.NotSelectedIntervalValue;
			View.Model.SelectedPupillaryDistanceRight = View.Model.PupillaryDistanceList.Any(x => x.Value.Equals(e.SelectedPupillaryDistanceRight)) ? e.SelectedPupillaryDistanceRight : View.Model.NotSelectedIntervalValue;
		}

		public void InitializeModel()
		{
			View.Model.FramesList = _repository.GetAll().ToFrameViewList().InsertFirst(new FrameListItem {Id = 0, Name = "-- Välj båge --"});
			View.Model.PupillaryDistanceList = new List<IntervalListItem>().InsertDefaultValue("PD", View.Model.NotSelectedIntervalValue);
			View.Model.GlassTypesList = _frameGlassTypeRepository.GetAll().ToFrameGlassTypeViewList().InsertFirst(new FrameGlassTypeListItem {Id = 0, Name = "-- Välj glastyp --"});
			View.Model.FrameRequiredErrorMessage = "Båge saknas";
			View.Model.GlassTypeRequiredErrorMessage = "Glastyp saknas";
			View.Model.PupillaryDistanceRequiredErrorMessage = "PD saknas";

			View.Model.SelectedPupillaryDistanceLeft = View.Model.NotSelectedIntervalValue;
			View.Model.SelectedPupillaryDistanceRight = View.Model.NotSelectedIntervalValue;
		}

	}
}