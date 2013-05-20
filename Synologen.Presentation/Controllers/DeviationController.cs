using System.IO;
using System.Text;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Data.Commands.Deviations;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.Deviation;
using Spinit.Wpc.Synologen.Data.Queries.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
    public partial class DeviationController : BaseController
    {
        public DeviationController(ISession session)
            : base(session)
        {
        }

        #region Deviation

        public ActionResult Index()
        {
            return RedirectToAction("Deviations");
        }

        [HttpPost]
        public ActionResult Deviations(DeviationListView inModel)
        {
            var routeValues = ControllerContext.HttpContext.Request.QueryString
                .ToRouteValueDictionary()
                .BlackList("controller", "action");
            if (String.IsNullOrEmpty(inModel.SearchTerm))
            {
                routeValues.TryRemoveRouteValue("search");
            }
            else
            {
                routeValues.AddOrReplaceRouteValue("search", inModel.SearchTerm.UrlEncode());
            }
            return RedirectToAction("Deviations", routeValues);
        }

        public ActionResult Deviations(string search, GridPageSortParameters gridPageSortParameters)
        {
            var decodedSearchTerm = search.UrlDecode();
            var criteria = new PagedSortedCriteria<Deviation>
            {
                Page = gridPageSortParameters.Page,
                PageSize = gridPageSortParameters.PageSize ?? DefaultPageSize,
                OrderBy = gridPageSortParameters.Column,
                SortAscending = gridPageSortParameters.Direction == SortDirection.Ascending
            };
            var list = Query(new DeviationsQuery { PagedSortedCriteria = criteria, SearchTerms = decodedSearchTerm });

            var viewList = list.ToDeviationViewList();
            var viewModel = new DeviationListView { List = viewList, SearchTerm = decodedSearchTerm };
            return View(viewModel);
        }

        public ActionResult ViewDeviation(int id)
        {
            var model = Query(new DeviationsQuery()).FirstOrDefault(x => x.Id == id);
            return View(model);
        }

        public ActionResult EditDeviation(int id)
        {
            var model = Query(new DeviationsQuery()).FirstOrDefault(x => x.Id == id);
            return View(model);
        }

        #endregion


        #region DeviationCategory

        [HttpPost]
        public ActionResult Categories(CategoryListView inModel)
        {
            var routeValues = ControllerContext.HttpContext.Request.QueryString
                .ToRouteValueDictionary()
                .BlackList("controller", "action");
            if (String.IsNullOrEmpty(inModel.SearchTerm))
            {
                routeValues.TryRemoveRouteValue("search");
            }
            else
            {
                routeValues.AddOrReplaceRouteValue("search", inModel.SearchTerm.UrlEncode());
            }
            return RedirectToAction("Categories", routeValues);
        }

        [HttpGet]
        public ActionResult Categories(string search, GridPageSortParameters gridPageSortParameters)
        {
            var decodedSearchTerm = search.UrlDecode();
            var criteria = new PagedSortedCriteria<DeviationCategory>
            {
                Page = gridPageSortParameters.Page,
                PageSize = gridPageSortParameters.PageSize ?? DefaultPageSize,
                OrderBy = gridPageSortParameters.Column,
                SortAscending = gridPageSortParameters.Direction == SortDirection.Ascending
            } as PagedSortedCriteria;

            var list = Query(new CategoriesQuery { PagedSortedCriteria = criteria, SearchTerms = decodedSearchTerm });
            var viewList = list.ToDeviationCategoryViewList();
            var viewModel = new CategoryListView{ List = viewList, SearchTerm = decodedSearchTerm };
            return View(viewModel);
        }

        public ActionResult AddCategory()
        {
            var model = new CategoryFormView();
            model.FormLegend = "Lägg till Kategori";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(CategoryFormView model)
        {
            if (ModelState.IsValid)
            {
                IList<DeviationDefect> selectedDefects = null;
                if (model.Defects != null)
                {
                    var defects = Query(new DefectsQuery());
                    selectedDefects = (from d in model.Defects where d.IsSelected select defects.FirstOrDefault(x => x.Id == d.Id)).ToList();
                }

                var category = new DeviationCategory
                {
                    Active = model.Active,
                    Defects = selectedDefects,
                    Name = model.Name
                };

                Execute(new CreateDeviationCategoryCommand(category));

                this.AddSuccessMessage("Kategorin har sparats");
                return RedirectToAction("EditCategory", new { id = category.Id });
            }
            model.FormLegend = "Lägg till Kategori";
            return View(model);
        }

        public ActionResult EditCategory(int id)
        {
            var category = Query(new CategoriesQuery()).FirstOrDefault(x => x.Id == id);
            var model = new CategoryFormView
            {
                Active = category.Active,
                Id = category.Id,
                Name = category.Name
            };

            model.SetDefects(category.Defects);
            model.FormLegend = "Redigera Kategori";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(CategoryFormView model)
        {
            if (ModelState.IsValid)
            {
                var deviationCategory = Query(new CategoriesQuery()).FirstOrDefault(x => x.Id == model.Id);
                if (deviationCategory != null)
                {
                    deviationCategory.Name = model.Name;
                    deviationCategory.Active = model.Active;
                }

                if (deviationCategory != null)
                {
                    if (model.Defects != null)
                    {
                        var defects = Query(new DefectsQuery());

                        IList<DeviationDefect> addDefects = (from d in model.Defects where d.IsSelected select defects.FirstOrDefault(x => x.Id == d.Id)).ToList();
                        foreach (var defect in addDefects)
                        {
                            deviationCategory.Defects.Add(defect);
                        }

                        IList<DeviationDefect> removeDefects = (from d in model.Defects where d.IsSelected == false select defects.FirstOrDefault(x => x.Id == d.Id)).ToList();
                        foreach (var defect in removeDefects)
                        {
                            deviationCategory.Defects.Remove(defect);
                        }
                    }
                }

                Execute(new CreateDeviationCategoryCommand(deviationCategory));
                this.AddSuccessMessage("Kategorin har sparats");
                return RedirectToAction("Categories");
            }
            model.FormLegend = "Redigera Kategori";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id)
        {
            var deviation = Query(new DeviationsQuery { SelectedCategory = id });

            if (!deviation.Any())
            {
                var deviationCategory = Query(new CategoriesQuery()).FirstOrDefault(x => x.Id == id);
                Execute(new DeleteDeviationCategoryCommand(deviationCategory));
                this.AddSuccessMessage("Kategorin har raderats");
            }
            else
            {
                this.AddErrorMessage("Kategorin kunde inte raderas då det är knutet till en eller fler avvikelser");
            }

            return RedirectToAction("Categories");
        }

        public ActionResult AddDefect(CategoryFormView model)
        {
            try
            {
                var category = Query(new CategoriesQuery()).FirstOrDefault(x => x.Id == model.Id);
                var defect = new DeviationDefect { Name = model.DefectName, Category = category };

                Execute(new CreateDefefectCommand(defect));
                model.SetDefects(Query(new DefectsQuery { SelectedCategory = model.Id }));

                return Json(new { success = true, partial = RenderViewToString("_DefectsForm", model) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }

        [NonAction]
        protected string RenderViewToString(string viewName, object model)
        {
            string result = null;
            var view = ViewEngines.Engines.FindView(this.ControllerContext, viewName, null).View;
            if (view != null)
            {
                var sb = new StringBuilder();
                using (var writer = new StringWriter(sb))
                {
                    var viewContext = new ViewContext(this.ControllerContext, view,
                          new ViewDataDictionary(model), new TempDataDictionary(), writer);
                    view.Render(viewContext, writer);
                    writer.Flush();
                }
                result = sb.ToString();
            }
            return result;
        }

        #endregion


        #region DeviationSupplier

        [HttpPost]
        public ActionResult Suppliers(SupplierListView inModel)
        {
            var routeValues = ControllerContext.HttpContext.Request.QueryString
                .ToRouteValueDictionary()
                .BlackList("controller", "action");
            if (String.IsNullOrEmpty(inModel.SearchTerm))
            {
                routeValues.TryRemoveRouteValue("search");
            }
            else
            {
                routeValues.AddOrReplaceRouteValue("search", inModel.SearchTerm.UrlEncode());
            }
            return RedirectToAction("Suppliers", routeValues);
        }

        [HttpGet]
        public ActionResult Suppliers(string search, GridPageSortParameters gridPageSortParameters)
        {
            var decodedSearchTerm = search.UrlDecode();
            var criteria = new PagedSortedCriteria<DeviationSupplier>
            {
                Page = gridPageSortParameters.Page,
                PageSize = gridPageSortParameters.PageSize ?? DefaultPageSize,
                OrderBy = gridPageSortParameters.Column,
                SortAscending = gridPageSortParameters.Direction == SortDirection.Ascending
            } as PagedSortedCriteria;

            var list = Query(new SuppliersQuery{ PagedSortedCriteria = criteria, SearchTerms = decodedSearchTerm });
            var viewList = list.ToDeviationSupplierViewList();
            var viewModel = new SupplierListView { List = viewList, SearchTerm = decodedSearchTerm };
            return View(viewModel);
        }


        public ActionResult EditSupplier(int id)
        {
            var supplier = Query(new SuppliersQuery()).FirstOrDefault(x => x.Id == id);

            if (supplier == null)
                return RedirectToAction("Suppliers");

            var categories = Query(new CategoriesQuery());

            var model = new SupplierFormView
            {
                Active = supplier.Active,
                Email = supplier.Email,
                Id = supplier.Id,
                Name = supplier.Name,
                Phone = supplier.Phone
            };

            model.SetCategories(categories, id);

            model.FormLegend = "Redigera Leverantör";
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSupplier(SupplierFormView model)
        {

            if (ModelState.IsValid)
            {
                var categories = Query(new CategoriesQuery());
                IList<DeviationCategory> selectedCategories = (from c in model.Categories where c.IsSelected select categories.FirstOrDefault(x => x.Id == c.Id)).ToList();

                var deviationSupplier = Query(new SuppliersQuery()).FirstOrDefault(x => x.Id == model.Id);
                if (deviationSupplier != null)
                {
                    deviationSupplier.Categories = selectedCategories;
                    deviationSupplier.Name = model.Name;
                    deviationSupplier.Email = model.Email;
                    deviationSupplier.Phone = model.Phone;
                    deviationSupplier.Active = model.Active;
                }
                Execute(new CreateDeviationSupplierCommand(deviationSupplier));
                this.AddSuccessMessage("Leverantören har sparats");
                return RedirectToAction("Suppliers");
            }

            model.SetCategories(Query(new CategoriesQuery()), model.Id);
            model.FormLegend = "Redigera till Leverantör";
            return View(model);
        }

        public ActionResult AddSupplier()
        {
            var model = new SupplierFormView();
            model.SetCategories(Query(new CategoriesQuery()), null);
            model.FormLegend = "Lägg till Leverantör";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSupplier(SupplierFormView model)
        {
            if (ModelState.IsValid)
            {
                var categories = Query(new CategoriesQuery());
                IList<DeviationCategory> selectedCategories = (from c in model.Categories where c.IsSelected select categories.FirstOrDefault(x => x.Id == c.Id)).ToList();

                var supplier = new DeviationSupplier
                {
                    Active = model.Active,
                    Categories = selectedCategories,
                    Email = model.Email,
                    Name = model.Name,
                    Phone = model.Phone
                };

                Execute(new CreateDeviationSupplierCommand(supplier));
                this.AddSuccessMessage("Leverantören har sparats");
                return RedirectToAction("Suppliers");
            }

            model.SetCategories(Query(new CategoriesQuery()), model.Id);
            model.FormLegend = "Lägg till Leverantör";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSupplier(int id)
        {
            var deviation = Query(new DeviationsQuery { SelectedSupplier = id });

            if (!deviation.Any())
            {
                var deviationSupplier = Query(new SuppliersQuery()).FirstOrDefault(x => x.Id == id);
                Execute(new DeleteDeviationSupplierCommand(deviationSupplier));
                this.AddSuccessMessage("Leverantören har raderats");
            }
            else
            {
                this.AddErrorMessage("Leverantören kunde inte raderas då det är knutet till en eller fler avvikelser.");
            }

            return RedirectToAction("Suppliers");

        }

        #endregion

    }
}