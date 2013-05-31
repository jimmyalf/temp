using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Commands.Deviations;
using Spinit.Wpc.Synologen.Data.Queries.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Helpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;
using EnumExtensions = Spinit.Wpc.Synologen.Core.Extensions.EnumExtensions;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations
{
	public class ViewDeviationPresenter : DeviationPresenter<IViewDeviationView>
	{
        private readonly DeviationStatusListItem _defaultStatus = new DeviationStatusListItem { Id = 0, Name = "-- Välj status --" };
        private int _deviationId;

        public ViewDeviationPresenter(IViewDeviationView view, ISession session)
            : base(view, session)
		{
			View.Load += View_Load;
            View.Submit += View_Submit;
            View.StatusSubmit += View_StatusSubmit;
		}

        public void View_StatusSubmit(object sender, ViewDeviationEventArgs e)
        {
            if (e.SelectedStatus.ToInteger() > 0)
            {
                var deviation = GetCurrentDeviation();
                
                if (deviation.Status != e.SelectedStatus)
                {
                    deviation.Status = e.SelectedStatus;
                    var comment = new DeviationComment { Description = string.Format("Status ändrad till {0}.", e.SelectedStatus.GetEnumDisplayName()), CreatedDate = DateTime.Now };
                    deviation.Comments.Add(comment);

                    Execute(new CreateDeviationCommand(deviation));
                    View.Model.SelectedStatus = e.SelectedStatus.ToInteger();
                }
                
            }
        }

        private Deviation GetCurrentDeviation()
        {
            _deviationId = int.Parse(Request.Params["id"]);
            var deviation = Query(new DeviationsQuery { SelectedDeviation = _deviationId }).FirstOrDefault();
            return deviation;
        }

		public void View_Load(object sender, EventArgs e)
		{
            InitializeModel();
		}

        public void InitializeModel()
        {
            var deviation = GetCurrentDeviation();

            if (deviation == null)
                return;

            View.Model.Id = deviation.Id;
            View.Model.Category = deviation.Category;
            View.Model.CreatedDate = deviation.CreatedDate;
            View.Model.Title = deviation.Title;
            View.Model.DefectDescription = deviation.DefectDescription;
            View.Model.Defects = deviation.Defects;
            View.Model.ShopId = deviation.ShopId;
            View.Model.Supplier = deviation.Supplier;
            View.Model.Type = deviation.Type;
            View.Model.Comments = deviation.Comments.OrderByDescending(x => x.CreatedDate);
            View.Model.Statuses = GetDeviationStatuses();
            View.Model.SelectedStatus = deviation.Status.ToInteger();
        }

        private IEnumerable<DeviationStatusListItem> GetDeviationStatuses()
        {
            return EnumExtensions.Enumerate<DeviationStatus>()
                .Select(item => new DeviationStatusListItem { Id = (int)item, Name = item.GetEnumDisplayName() })
                .ToList()
                .InsertFirst(_defaultStatus);
        }

        void View_Submit(object sender, ViewDeviationEventArgs e)
        {
            var deviation = Query(new DeviationsQuery()).FirstOrDefault(x => x.Id == _deviationId);
            if (deviation == null)
                return;

            var comment = new DeviationComment { Description = e.Comment, CreatedDate = DateTime.Now };
            deviation.Comments.Add(comment);
            Execute(new CreateDeviationCommand(deviation));
        }

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}
	}
}