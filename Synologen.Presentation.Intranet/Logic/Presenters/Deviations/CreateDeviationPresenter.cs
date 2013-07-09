﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Commands.Deviations;
using Spinit.Wpc.Synologen.Data.Queries.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Helpers;
//using Spinit.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations
{
    public class CreateDeviationPresenter : DeviationPresenter<ICreateDeviationView>
    {

        private readonly DeviationCategoryListItem _defaultCategory = new DeviationCategoryListItem { Id = 0, Name = "-- Välj kategori --" };
        private readonly DeviationTypeListItem _defaultType = new DeviationTypeListItem { Id = 0, Name = "-- Välj typ --" };
        private readonly DeviationSupplierListItem _defaultSupplier = new DeviationSupplierListItem { Id = 0, Name = "-- Välj leverantör --" };
        private readonly ISynologenMemberService _synologenMemberService;
        private readonly IEmailService _emailService;

        public CreateDeviationPresenter(ICreateDeviationView view, ISession session, ISynologenMemberService sessionProviderService, IEmailService emailService)
            : base(view, session)
        {
            _synologenMemberService = sessionProviderService;
            _emailService = emailService;

            InitiateEventHandlers();
        }

        private void InitiateEventHandlers()
        {
            View.Load += View_Load;
            View.TypeSelected += View_TypeSelected;
            View.CategorySelected += View_CategorySelected;
            View.Submit += View_Submit;
        }

        void View_TypeSelected(object sender, CreateDeviationEventArgs e)
        {
            if (e.SelectedType > 0)
            {
                View.Model.SelectedType = (int)e.SelectedType;
                SetDisplayView(e.SelectedType);
            }
        }

        public void View_Load(object sender, EventArgs e)
        {
            InitializeModel();
        }

        private void SetDisplayView(DeviationType type)
        {
            if (type == DeviationType.Internal)
                View.Model.DisplayInternalDeviation = true;
            else
                View.Model.DisplayExternalDeviation = true;
        }

        public void View_CategorySelected(object sender, CreateDeviationEventArgs e)
        {
            if (e.SelectedCategory > 0)
            {
                View.Model.SelectedType = (int)e.SelectedType;
                View.Model.SelectedCategoryId = e.SelectedCategory;

                View.Model.Defects = Query(new DefectsQuery { SelectedCategory = e.SelectedCategory }).ToDeviationDefectList();
                View.Model.Suppliers = Query(new SuppliersQuery { SelectedCategory = e.SelectedCategory }).ToDeviationSupplierList().InsertFirst(_defaultSupplier);

                SetDisplayView(e.SelectedType);
            }
        }

        public void View_Submit(object sender, CreateDeviationEventArgs e)
        {
            var category = Session.Get<DeviationCategory>(e.SelectedCategory);
            var supplier = Session.Get<DeviationSupplier>(e.SelectedSupplier);
            var shopId = _synologenMemberService.GetCurrentShopId();

            var deviation = new Deviation
            {
                Type = e.SelectedType,
                ShopId = shopId,
                Status = DeviationStatus.NotStarted,
                Supplier = supplier,
                Title = e.Title,
                DefectDescription = e.DefectDescription,
                Category = category,
            };

            if (e.SelectedType == DeviationType.External)
            {
                foreach (var d in e.SelectedDefects)
                {
                    deviation.Defects.Add(new DeviationDefect { Name = d.Name });
                }
            }

            Execute(new CreateDeviationCommand(deviation));
            //_emailService.SendEmail("roger.edvardsson@spinit.se", "roger.edvardsson@spinit.se", "Extern avvikelse", ReportEmailBody(deviation));
            View.Model.Success = true;
        }

        //private string ReportEmailBody(Deviation deviation)
        //{
        //    var sb = new StringBuilder();
        //    sb.AppendLine("Hej,");
        //    sb.AppendLine();
        //    sb.AppendLine();
        //    sb.AppendLine("Här kommer extern avvikelserapport.");
        //    sb.AppendLine();
        //    sb.AppendLine();
        //    sb.AppendFormat("Category: {0}", deviation.Category.Name);
        //    sb.AppendLine();
        //    sb.AppendLine("Fel:");
        //    sb.AppendLine();
        //    foreach (var d in deviation.Defects)
        //    {
        //        sb.AppendLine(d.Name);
        //    }
        //    sb.AppendLine();
        //    sb.AppendLine();
        //    sb.AppendLine("Synologen");

        //    return sb.ToString();
        //}

        public void InitializeModel()
        {
            View.Model.Types = GetDeviationTypes();
            var categories = Query(new CategoriesQuery { Active = true });
            View.Model.Categories = categories.ToDeviationCategoryList().InsertFirst(_defaultCategory);
        }

        public override void ReleaseView()
        {
            View.Load -= View_Load;
            View.CategorySelected -= View_CategorySelected;
            View.TypeSelected -= View_TypeSelected;
            View.Submit -= View_Submit;
        }

        private IEnumerable<DeviationTypeListItem> GetDeviationTypes()
        {
            return EnumExtensions.Enumerate<DeviationType>()
                .Select(item => new DeviationTypeListItem { Id = (int)item, Name = item.GetEnumDisplayName() })
                .ToList()
                .InsertFirst(_defaultType);
        }
    }
}