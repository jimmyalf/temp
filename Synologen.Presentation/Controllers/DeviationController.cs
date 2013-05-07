using System.IO;
using System.Text;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Data.Commands.Deviations;
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

        [HttpPost]
        public ActionResult Index(DeviationListView viewModel)
        {
            var decodedSearchTerm = viewModel.SearchTerm.UrlDecode();
            var deviations = Query(new DeviationsQuery { SelectedCategory = viewModel.SelectedCategory, SelectedSupplier = viewModel.SelectedSupplier });
            var model = new DeviationListView(decodedSearchTerm, deviations);
            model.DeviationCategories = Query(new CategoriesQuery());
            model.DeviationSuppliers = Query(new SuppliersQuery());
            return View(model);
        }

        public ActionResult Index()
        {
            var deviations = Query(new DeviationsQuery());
            var model = new DeviationListView(string.Empty, deviations);
            model.DeviationCategories = Query(new CategoriesQuery());
            model.DeviationSuppliers = Query(new SuppliersQuery());
            return View(model);
        }

        [HttpPost]
        public ActionResult ListCategories(CategoryListView viewModel)
        {
            var decodedSearchTerm = viewModel.SearchTerm.UrlDecode();
            var categories = Query(new CategoriesQuery { SearchTerms = decodedSearchTerm });
            var model = new CategoryListView(decodedSearchTerm, categories);
            return View(model);
        }

        public ActionResult ListCategories()
        {
            var categories = Query(new CategoriesQuery());
            var model = new CategoryListView(string.Empty, categories);
            return View(model);
        }

        [HttpPost]
        public ActionResult ListSuppliers(SupplierListView viewModel)
        {
            var decodedSearchTerm = viewModel.SearchTerm.UrlDecode();
            var suppliers = Query(new SuppliersQuery { SearchTerms = viewModel.SearchTerm });
            var model = new SupplierListView(decodedSearchTerm, suppliers);
            return View(model);
        }

        public ActionResult ListSuppliers()
        {
            var suppliers = Query(new SuppliersQuery());
            var model = new SupplierListView(string.Empty, suppliers);
            return View(model);
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
                return RedirectToAction("ListCategories");
            }
            model.FormLegend = "Redigera Kategori";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id)
        {
            // Check if this category contains any deviation.
            var deviation = Query(new DeviationsQuery { SelectedCategory = id });

            if (deviation.Count==0)
            {
                var deviationCategory = Query(new CategoriesQuery()).FirstOrDefault(x => x.Id == id);
                Execute(new DeleteDeviationCategoryCommand(deviationCategory));
                this.AddSuccessMessage("Kategorin har raderats");
            }
            else
            {
                this.AddErrorMessage("Kategorin kunde inte raderas då det är knutet till en eller fler avvikelser");
            }

            return RedirectToAction("ListCategories");
        }

        public ActionResult EditSupplier(int id)
        {
            var supplier = Query(new SuppliersQuery()).FirstOrDefault(x => x.Id == id);

            if (supplier == null)
                return RedirectToAction("ListSuppliers");

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
                return RedirectToAction("ListSuppliers");
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
                return RedirectToAction("ListSuppliers");
            }

            model.SetCategories(Query(new CategoriesQuery()), model.Id);
            model.FormLegend = "Lägg till Leverantör";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSupplier(int id)
        {
            // Check if this supplier contains any deviation.
            var deviation = Query(new DeviationsQuery { SelectedSupplier = id });

            if (deviation.Count==0)
            {
                var deviationSupplier = Query(new SuppliersQuery()).FirstOrDefault(x => x.Id == id);
                Execute(new DeleteDeviationSupplierCommand(deviationSupplier));
                this.AddSuccessMessage("Leverantören har raderats");
            }
            else
            {
                this.AddErrorMessage("Leverantören kunde inte raderas då det är knutet till en eller fler avvikelser.");
            }
            
            return RedirectToAction("ListSuppliers");

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

    }
}