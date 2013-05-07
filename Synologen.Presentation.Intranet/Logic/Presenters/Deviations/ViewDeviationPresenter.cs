using System;
using System.Linq;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Data.Queries.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations
{
	public class ViewDeviationPresenter : DeviationPresenter<IViewDeviationView>
	{
        public ViewDeviationPresenter(IViewDeviationView view, ISession session)
            : base(view, session)
		{
			View.Load += View_Load;
		}

		public void View_Load(object sender, EventArgs e)
		{
		    var id = int.Parse(Request.Params["id"]);
            var deviation = Query(new DeviationsQuery()).FirstOrDefault(x => x.Id == id);

            if (deviation != null)
            {
                View.Model.Id = deviation.Id;
                View.Model.Category = deviation.Category;
                View.Model.CreatedDate = deviation.CreatedDate;
                View.Model.DefectDescription = deviation.DefectDescription;
                View.Model.Defects = deviation.Defects;
                View.Model.ShopId = deviation.ShopId;
                View.Model.Supplier = deviation.Supplier;
                View.Model.Type = deviation.Type;
            }
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}
	}
}