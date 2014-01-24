using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;

using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Core.Domain.Model.Synologen;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Data.Commands.Deviations;
using Spinit.Wpc.Synologen.Data.Queries.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Helpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations
{
    public class CreateDeviationPresenter : DeviationPresenter<ICreateDeviationView>
    {

        private readonly DeviationCategoryListItem _defaultCategory = new DeviationCategoryListItem { Id = 0, Name = "-- Välj kategori --" };
        private readonly DeviationTypeListItem _defaultType = new DeviationTypeListItem { Id = 0, Name = "-- Välj typ --" };
        private readonly DeviationSupplierListItem _defaultSupplier = new DeviationSupplierListItem { Id = 0, Name = "-- Välj leverantör --" };
        private readonly ISynologenMemberService _synologenMemberService;
        private readonly IEmailService _emailService;
        private readonly ISynologenSettingsService _settingService;

        public CreateDeviationPresenter(ICreateDeviationView view, ISession session, ISynologenMemberService sessionProviderService, IEmailService emailService, ISynologenSettingsService settingService)
            : base(view, session)
        {
            _synologenMemberService = sessionProviderService;
            _emailService = emailService;
            _settingService = settingService;

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
                View.Model.Suppliers = Query(new SuppliersQuery { SelectedCategory = e.SelectedCategory, Active = true }).ToDeviationSupplierList().InsertFirst(_defaultSupplier);

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

            if (e.SendEmailSupplier)
            {
                try
                {
                    _emailService.SendEmail(_settingService.EmailOrderFrom, supplier.Email, "Synologen extern avvikelse", CreateSupplierEmailBody(deviation));
                    View.Model.Status = string.Format("Avvikelsen är skickad till leverantörens e-postadress '{0}'.", supplier.Email);
                }
                catch (Exception ex)
                {
                    View.Model.Status = string.Format("Något fel inträffade när avvikelsen skulle skickas till leverantörens e-postadress '{0}'.", supplier.Email);
                }
            }

            View.Model.Success = true;
        }

        private string CreateSupplierEmailBody(Deviation deviation)
        {
            var shop = Session.Get<Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails.Shop>(deviation.ShopId);

            var sb = new StringBuilder();
            sb.AppendLine("Hej,");
            sb.AppendLine();
            sb.AppendLine("Här kommer ny extern avvikelse.");
            sb.AppendLine();
            sb.AppendFormat("Kategori: {0}", deviation.Category.Name);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Fel:");

            foreach (var d in deviation.Defects)
            {
                sb.AppendLine(d.Name);
            }
            
            sb.AppendLine();
            sb.AppendFormat("Beskrivning: {0}", deviation.DefectDescription);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Med vänlig hälsning");
            sb.AppendLine();
            sb.AppendFormat("{0}, {1}", shop.Name, shop.Address.City);
            sb.AppendLine();
            sb.AppendLine(shop.Email);

            return sb.ToString();
        }

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