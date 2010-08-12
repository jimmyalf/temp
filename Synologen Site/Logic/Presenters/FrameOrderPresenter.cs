using System;
using System.Collections.Generic;
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
		public FrameOrderPresenter(IFrameOrderView<FrameOrderModel> view, IFrameRepository repository) : base(view)
		{
			_repository = repository;
			InitiateEventHandlers();
		}

		private void InitiateEventHandlers()
		{
			View.Load += View_Load;
			View.FrameSelected += View_FrameSelected;
			View.SubmitForm += View_SumbitForm;
		}

		private void View_SumbitForm(object sender, FrameOrderFormSubmitEventArgs e) 
		{ 
			if(e.PageIsValid)
			{
				//TODO: Save frame order && redirect user to thank you page
				View.Model.Message = "Submit returned a valid result and should now create an order and redirect";
			}
			else
			{
				View.Model.SelectedFrameId = e.SelectedFrameId;
				View.Model.Message = "Submit did not return a valid result!";
			}
		}

		private void View_FrameSelected(object sender, FrameSelectedEventArgs e) 
		{
			View.Model.Message = "Vald båge med id: " + e.SelectedFrameId;
			View.Model.SelectedFrameId = e.SelectedFrameId;
			var frame = _repository.Get(e.SelectedFrameId);
			View.Model.IndexList = frame.GetIntervalListFor(x => x.Index).InsertDefaultValue("index", View.Model.NotSelectedIntervalValue);
			View.Model.SphereList = frame.GetIntervalListFor(x => x.Sphere).InsertDefaultValue("sfär", View.Model.NotSelectedIntervalValue);
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}

		void View_Load(object sender, EventArgs e)
		{
			View.Model.Message = "Testar Web Forms MVP!";
			var frameListItems = _repository.GetAll().ToFrameViewList().InsertFirst(new FrameListItem {Id = 0, Name = "-- Välj båge --"});
			View.Model.FramesList = frameListItems;
			View.Model.IndexList = new List<IntervalListItem>().InsertDefaultValue("index", View.Model.NotSelectedIntervalValue);
			View.Model.SphereList =  new List<IntervalListItem>().InsertDefaultValue("sfär", View.Model.NotSelectedIntervalValue);
			View.Model.FrameRequiredErrorMessage = "Båge saknas";
			View.Model.IndexRequiredErrorMessage = "Index saknas";
			View.Model.SphereRequiredErrorMessage = "Sfär saknas";
		}

	}
}