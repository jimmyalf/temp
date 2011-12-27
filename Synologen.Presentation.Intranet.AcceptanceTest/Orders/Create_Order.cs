using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Create_Order")]
    public class When_creating_an_order : OrderSpecTestbase<CreateOrderPresenter, ICreateOrderView>
    {
        private CreateOrderPresenter _createOrderPresenter;
        private CreateOrderEventArgs _form;
        private string _testRedirectSubmitUrl;
        private string _testRedirectAbortUrl;
        private string _testRedirectPreviousUrl;
        private OrderCustomer _customer;
        private Article _article;
		private int _articleId;
        private IEnumerable<ArticleCategory> _expectedCategories;
        private IEnumerable<ArticleType> _expectedArticleTypes;
        private IEnumerable<Article> _expectedArticles;
        private IEnumerable<ArticleSupplier> _expectedSuppliers;
        private Article _expectedArticle;
        private int _selectedCategoryId;
        private int _selectedArticleTypeId;
        private int _selectedSupplierId;
        private Order _order;

        public When_creating_an_order()
        {
            Context = () =>
            {
                _testRedirectSubmitUrl = "/test/page";
                _testRedirectAbortUrl = "/test/page/abort";
                _testRedirectPreviousUrl = "/test/page/previous";
                SetupNavigationEvents(_testRedirectPreviousUrl, _testRedirectAbortUrl, _testRedirectSubmitUrl);
                _createOrderPresenter = GetPresenter();
            };                  
                                
            Story = () => new Berättelse("Spara beställning")
                .FörAtt("skapa en ny beställning")
                .Som("inloggad användare på intranätet")
                .VillJag("spara innehållet i den nya beställningen");
        }                       
            
		[Test]                  
		public void VisaBeställningsFormulär()
		{                                                                                 
		    SetupScenario(scenario => scenario                                            
		        .Givet(AttEnKundÄrVald)
                    .Och(DetFinnsArtikelkategorierAttLadda)                                   
		        .När(AttAnvändarenVisarBeställningsformuläret)                            
		        .Så(VisasKundensNamn)                                                     
                    .Och(ArtikelkategorierLaddas));                                                                            
		}

        [Test]                                                                            
        public void ArtikeltyperLaddas()                                                  
        {                                                                                 
            SetupScenario(scenario => scenario
                .Givet(AttDetFinnsArtikeltyperAttLaddaFrånValdKategori)
                .När(AnvändarenVäljerEnKategori)                                          
                .Så(LaddasArtikeltyper));
        }

        [Test] 
        public void LeverantörerLaddas()
        {
            SetupScenario(scenario => scenario
                .Givet(AttDetFinnsLeverantörerMedArtiklarAvValdArtikeltyp)
                .När(AnvändarenVäljerEnArtikeltyp)
                .Så(LaddasLeverantörer));
        }

        [Test]                                                                            
        public void ArtiklarLaddas()
        {
            SetupScenario(scenario => scenario
                .Givet(AttDetFinnsArtiklarAttLaddaFrånValdLeverantörOchArtikeltyp)
                .När(AnvändarenVäljerEnLeverantör)
                .Så(LaddasArtiklar));
        }

        [Test]
        public void ArtikelalternativLaddas()
        {
            SetupScenario(scenario => scenario
                .Givet(AttDetFinnsEnSparadArtikelAttVälja)
                .När(AnvändarenVäljerArtikeln)
                .Så(LaddasArtikelnsAlternativ));
        }

        [Test]                                                                            
		public void FöregåendeSteg()                                                      
		{                                                                                 
		    SetupScenario(scenario => scenario                                            
				.Givet(AttEnKundÄrVald)                     
		        .När(AnvändarenKlickarPåFöregåendeSteg)                                   
		        .Så(FörflyttasAnvändarenTillFöregåendeSteg)                                                           
			);                                                                            
		}                                                                                 

        [Test]
        public void NästaSteg()
        {
            SetupScenario(scenario => scenario
				.Givet(AttEnKundÄrVald)
                    .Och(ValdArtikelFinnsSparad)
					.Och(AttAnvändarenFylltIBeställningsformuläret)
                .När(AnvändarenKlickarPåNästaSteg)
                 .Så(SparasBeställningen)
                    .Och(AnvändarenFörflyttasTillVynFörNästaSteg)
			);
        }

        [Test]
        public void AvbrytBeställning()
        {
            SetupScenario(scenario => scenario
                .Givet(Ingenting)
                .När(AnvändarenAvbryterBeställningen)
                .Så(FlyttasAnvändarenTillIntranätsidan));
        }

        #region Arrange

        private static void Ingenting()
        {
        }

        private void AttDetFinnsEnSparadArtikelAttVälja()
        {
            _expectedArticle = OrderFactory.GetArticle(null, null);
            WithRepository<IArticleRepository>().Save(_expectedArticle);
        }

        private void DetFinnsArtikelkategorierAttLadda()
        {
            _expectedCategories = OrderFactory.GetCategories();

            var newCategories = new List<ArticleCategory>();

            foreach (var expectedCategory in _expectedCategories)
            {
                WithRepository<IArticleCategoryRepository>().Save(expectedCategory);
                newCategories.Add(expectedCategory);
            }
            _expectedCategories = newCategories;
        }

        private void AttDetFinnsArtikeltyperAttLaddaFrånValdKategori()
        {
            var category = OrderFactory.GetCategory();
            WithRepository<IArticleCategoryRepository>().Save(category);
            _selectedCategoryId = category.Id;

            _expectedArticleTypes = OrderFactory.GetArticleTypes(category);
            var newArticleTypes = new List<ArticleType>();

            foreach (var expectedArticleType in _expectedArticleTypes)
            {
                WithRepository<IArticleTypeRepository>().Save(expectedArticleType);
                newArticleTypes.Add(expectedArticleType);
            }

            _expectedArticleTypes = newArticleTypes;
        }

        private void AttDetFinnsArtiklarAttLaddaFrånValdLeverantörOchArtikeltyp()
        {
            var articleType = OrderFactory.GetArticleType(null);
            WithRepository<IArticleTypeRepository>().Save(articleType);
            _selectedArticleTypeId = articleType.Id;

            var articleSupplier = OrderFactory.GetSupplier();
            WithRepository<IArticleSupplierRepository>().Save(articleSupplier);
            _selectedSupplierId = articleSupplier.Id;

            _expectedArticles = OrderFactory.GetArticles(articleType, articleSupplier);
            var newArticles = new List<Article>();

            foreach (var expectedArticle in _expectedArticles)
            {
                WithRepository<IArticleRepository>().Save(expectedArticle);
                newArticles.Add(expectedArticle);
            }

            _expectedArticles = newArticles;
        }

        //TODO: add more data
        private void AttDetFinnsLeverantörerMedArtiklarAvValdArtikeltyp()
        {
            var articleType = OrderFactory.GetArticleType(null);
            WithRepository<IArticleTypeRepository>().Save(articleType);
            _selectedArticleTypeId = articleType.Id;

            var articleSupplier = OrderFactory.GetSupplier();
            WithRepository<IArticleSupplierRepository>().Save(articleSupplier);
            _expectedSuppliers = new List<ArticleSupplier> { articleSupplier };

            _expectedArticles =
                CreateItemsWithRepository<IArticleRepository, Article>(
                    () => OrderFactory.GetArticles(articleType, articleSupplier));

        }

    	private void AttEnKundÄrVald()
    	{
            _customer = OrderFactory.GetCustomer();
            WithRepository<IOrderCustomerRepository>().Save(_customer);
            HttpContext.SetupRequestParameter("customer", _customer.Id.ToString());
    	}

        private void ValdArtikelFinnsSparad()
        {
            _article = OrderFactory.GetArticle(null, null);
            WithRepository<IArticleRepository>().Save(_article);
            _articleId = _article.Id;
        }

		private void AttAnvändarenFylltIBeställningsformuläret()
		{
            _form = OrderFactory.GetOrderEventArgs(_articleId);
        }

        private void AnvändarenAvbryterBeställningen()
        {
            _createOrderPresenter.View_Abort(null, new EventArgs());
        }

		#endregion

		#region Act

        private void AnvändarenVäljerArtikeln()
        {
            _createOrderPresenter.FillModel(null, new SelectedSomethingEventArgs(0, 0, 0, _expectedArticle.Id));
        }

        private void AnvändarenVäljerEnArtikeltyp()
        {
            _createOrderPresenter.FillModel(null, new SelectedSomethingEventArgs(0, _selectedArticleTypeId, 0, 0));
        }

        private void AnvändarenVäljerEnLeverantör()
        {
            _createOrderPresenter.FillModel(null, new SelectedSomethingEventArgs(0, _selectedArticleTypeId, _selectedSupplierId, 0));
        }

        private void AnvändarenVäljerEnKategori()
        {
            _createOrderPresenter.FillModel(null, new SelectedSomethingEventArgs(_selectedCategoryId, 0, 0, 0));
        }

    	private void AttAnvändarenVisarBeställningsformuläret()
    	{
            _createOrderPresenter.View_Load(null, new EventArgs());
    	}

		private void AnvändarenKlickarPåFöregåendeSteg()
		{
            _createOrderPresenter.View_Previous(null, new EventArgs());
		}

        private void AnvändarenKlickarPåNästaSteg()
        {
            _createOrderPresenter.View_Submit(null, _form);
        }

		#endregion

		#region Assert

        private void LaddasArtikelnsAlternativ()
        {
            View.Model.AxisOptions.And(OrderFactory.FillWithIncrementalValues(_expectedArticle.Options.Axis)).Do((viewModelItem, domainItem) =>
            {
                viewModelItem.Value.ShouldBe(domainItem.Value.ToString());
                viewModelItem.Text.ShouldBe(domainItem.Text);
            });
            View.Model.BaseCurveOptions.And(OrderFactory.FillWithIncrementalValues(_expectedArticle.Options.BaseCurve)).Do((viewModelItem, domainItem) =>
            {
                viewModelItem.Value.ShouldBe(domainItem.Value.ToString());
                viewModelItem.Text.ShouldBe(domainItem.Text);
            });
            View.Model.CylinderOptions.And(OrderFactory.FillWithIncrementalValues(_expectedArticle.Options.Cylinder)).Do((viewModelItem, domainItem) =>
            {
                viewModelItem.Value.ShouldBe(domainItem.Value.ToString());
                viewModelItem.Text.ShouldBe(domainItem.Text);
            });
            View.Model.DiameterOptions.And(OrderFactory.FillWithIncrementalValues(_expectedArticle.Options.Diameter)).Do((viewModelItem, domainItem) =>
            {
                viewModelItem.Value.ShouldBe(domainItem.Value.ToString());
                viewModelItem.Text.ShouldBe(domainItem.Text);
            });
            View.Model.PowerOptions.And(OrderFactory.FillWithIncrementalValues(_expectedArticle.Options.Power)).Do((viewModelItem, domainItem) =>
            {
                viewModelItem.Value.ShouldBe(domainItem.Value.ToString());
                viewModelItem.Text.ShouldBe(domainItem.Text);
            });
        }

        private void LaddasLeverantörer()
        {
            var viewModelSuppliers = View.Model.Suppliers.ToList();
            viewModelSuppliers.RemoveAt(0);

            viewModelSuppliers.And(_expectedSuppliers).Do((viewModelItem, domainItem) =>
            {
                viewModelItem.Value.ShouldBe(domainItem.Id.ToString());
                viewModelItem.Text.ShouldBe(domainItem.Name);
            });
        }

        private void LaddasArtiklar()
        {
            var viewModelArticles = View.Model.OrderArticles.ToList();
            viewModelArticles.RemoveAt(0);

            viewModelArticles.And(_expectedArticles).Do((viewModelItem, domainItem) =>
            {
                viewModelItem.Value.ShouldBe(domainItem.Id.ToString());
                viewModelItem.Text.ShouldBe(domainItem.Name);
            });
        }

        private void LaddasArtikeltyper()
        {
            var viewModelArticleTypes = View.Model.ArticleTypes.ToList();
            viewModelArticleTypes.RemoveAt(0);

            viewModelArticleTypes.And(_expectedArticleTypes).Do((viewModelItem, domainItem) =>
            {
                viewModelItem.Value.ShouldBe(domainItem.Id.ToString());
                viewModelItem.Text.ShouldBe(domainItem.Name);
            });
        }

        private void ArtikelkategorierLaddas()
        {
            var viewModelCategories = View.Model.Categories.ToList();
            viewModelCategories.RemoveAt(0);
            viewModelCategories.And(_expectedCategories).Do((viewModelItem, domainItem) =>
            {
                viewModelItem.Value.ShouldBe(domainItem.Id.ToString());
                viewModelItem.Text.ShouldBe(domainItem.Name);
            });
        }

		private void VisasKundensNamn()
    	{
    		View.Model.CustomerName.ShouldBe(String.Format("{0} {1}", _customer.FirstName, _customer.LastName));
    	}

		private void FörflyttasAnvändarenTillFöregåendeSteg()
		{
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_testRedirectPreviousUrl + "?customer=" + _customer.Id);
		}

        private void SparasBeställningen()
        {
            _order = WithRepository<IOrderRepository>().GetAll().First();
            _order.Article.Id.ShouldBe(_form.ArticleId);
            _order.Customer.Id.ShouldBe(_customer.Id);
            _order.LensRecipe.BaseCurve.Left.ShouldBe(_form.LeftBaseCurve);
            _order.LensRecipe.BaseCurve.Right.ShouldBe(_form.RightBaseCurve);
            _order.LensRecipe.Diameter.Left.ShouldBe(_form.LeftDiameter);
            _order.LensRecipe.Diameter.Right.ShouldBe(_form.RightDiameter);
            _order.LensRecipe.Power.Left.ShouldBe(_form.LeftPower);
            _order.LensRecipe.Power.Right.ShouldBe(_form.RightPower);
            _order.LensRecipe.Axis.Left.ShouldBe(_form.LeftAxis);
            _order.LensRecipe.Axis.Right.ShouldBe(_form.RightAxis);
            _order.LensRecipe.Cylinder.Left.ShouldBe(_form.LeftCylinder);
            _order.LensRecipe.Cylinder.Right.ShouldBe(_form.RightCylinder);
            _order.ShippingType.ToInteger().ShouldBe(_form.ShipmentOption);

        }

        private void AnvändarenFörflyttasTillVynFörNästaSteg()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_testRedirectSubmitUrl + "?order=" + _order.Id);
        }

        private void FlyttasAnvändarenTillIntranätsidan()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_testRedirectAbortUrl);
        }

		#endregion

    }
}
