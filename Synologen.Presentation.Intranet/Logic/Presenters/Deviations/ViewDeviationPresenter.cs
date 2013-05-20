using System;
using System.Linq;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Data.Commands.Deviations;
using Spinit.Wpc.Synologen.Data.Queries.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations
{
	public class ViewDeviationPresenter : DeviationPresenter<IViewDeviationView>
	{
        private int _deviationId;

        public ViewDeviationPresenter(IViewDeviationView view, ISession session)
            : base(view, session)
		{
			View.Load += View_Load;
            View.Submit += View_Submit;
		}

		public void View_Load(object sender, EventArgs e)
		{
            _deviationId = int.Parse(Request.Params["id"]);
            var deviation = Query(new DeviationsQuery { SelectedDeviation = _deviationId }).FirstOrDefault();

		    if (deviation == null) 
                return;

		    View.Model.Id = deviation.Id;
		    View.Model.Category = deviation.Category;
		    View.Model.CreatedDate = deviation.CreatedDate;
		    View.Model.DefectDescription = deviation.DefectDescription;
		    View.Model.Defects = deviation.Defects;
		    View.Model.ShopId = deviation.ShopId;
		    View.Model.Supplier = deviation.Supplier;
		    View.Model.Type = deviation.Type;
		    View.Model.Comments = deviation.Comments;
		}

        void View_Submit(object sender, EventArguments.Deviations.ViewDeviationEventArgs e)
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