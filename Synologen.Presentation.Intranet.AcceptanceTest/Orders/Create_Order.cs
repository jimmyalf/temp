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
        private IEnumerable<ArticleCategory> _allCategories;
        private IEnumerable<ArticleType> _allArticleTypes;
        private IEnumerable<Article> _allArticles;
        private IEnumerable<ArticleSupplier> _allSuppliers;
        private Article _expectedArticle;
        private Order _order;
    	private Shop _shop;
    	private ArticleCategory _selectedCategory;
    	private ArticleType _selectedArticleType;
    	private ArticleSupplier _selectedSupplier;

    	public When_creating_an_order()
        {
            Context = () =>
            {
                _testRedirectSubmitUrl = "/test/page";
                _testRedirectAbortUrl = "/test/page/abort";
                _testRedirectPreviousUrl = "/test/page/previous";
            	_shop = CreateShop<Shop>();
            	A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
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
        public void VisaBeställningsFormulärNärBeställningRedanSkapats()
        {
            SetupScenario(scenario => scenario
                .Givet(AttEnOrderSkapats)
                .När(AttAnvändarenVisarBeställningsformuläret)
                .Så(VisasKundensNamn)
                    .Och(FormuläretFyllsMedData));
        }

        [Test]                                                                            
        public void ArtikeltyperLaddas()
        {
            SetupScenario(scenario => scenario
				.Givet(DetFinnsArtikelkategorierAttLadda)
					.Och(AttDetFinnsArtikeltyperAttLaddaFrånValdKategori)
                .När(AnvändarenVäljerEnKategori)
                .Så(LaddasAktivaArtikeltyper));
        }

        [Test] 
        public void LeverantörerLaddas()
        {
            SetupScenario(scenario => scenario
				.Givet(AttDetFinnsLeverantörerMedArtiklarAvValdArtikeltyp)
                .När(AnvändarenVäljerEnArtikeltyp)
                .Så(LaddasAktivaLeverantörer)
			);
        }

        [Test]                                                                            
        public void ArtiklarLaddas()
        {
            SetupScenario(scenario => scenario
				.Givet(DetFinnsArtikelkategorierAttLadda)
					.Och(AttDetFinnsArtikeltyperAttLaddaFrånValdKategori)
					.Och(AttDetFinnsLeverantörerMedArtiklarAvValdArtikeltyp)
					.Och(AttDetFinnsArtiklarAttLaddaFrånValdLeverantörOchArtikeltyp)
                .När(AnvändarenVäljerEnLeverantör)
                .Så(LaddasAktivaArtiklar));
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

        [Test]
        public void AvbrytBeställningMedExisterandeOrder()
        {
            SetupScenario(scenario => scenario
                .Givet(AttEnOrderSkapats)
                    .När(AnvändarenAvbryterBeställningen)
                .Så(TasOrdernBort)
                    .Och(FlyttasAnvändarenTillIntranätsidan));
        }

       

        #region Arrange

        private static void Ingenting()
        {
        }

        private void AttDetFinnsEnSparadArtikelAttVälja()
        {
			_expectedArticle = CreateArticle();
        }

        private void DetFinnsArtikelkategorierAttLadda()
        {
        	_allCategories = CreateItemsWithRepository<IArticleCategoryRepository, ArticleCategory>(OrderFactory.GetCategories);
        }

        private void AttDetFinnsArtikeltyperAttLaddaFrånValdKategori()
        {
        	_selectedCategory = _allCategories.First();
        	_allArticleTypes = CreateItemsWithRepository<IArticleTypeRepository, ArticleType>(() => OrderFactory.GetArticleTypes(_selectedCategory));
        }

        private void AttDetFinnsArtiklarAttLaddaFrånValdLeverantörOchArtikeltyp()
        {
        	_selectedArticleType = _allArticleTypes.First();
        	_selectedSupplier = _allSuppliers.First();
        	_allArticles = CreateItemsWithRepository<IArticleRepository,Article>(() =>OrderFactory.GetArticles(_selectedArticleType, _selectedSupplier));
        }

        private void AttDetFinnsLeverantörerMedArtiklarAvValdArtikeltyp()
        {
        	_selectedCategory = CreateWithRepository<IArticleCategoryRepository,ArticleCategory>(() =>OrderFactory.GetCategory());
        	_selectedArticleType = CreateWithRepository<IArticleTypeRepository, ArticleType>(() => OrderFactory.GetArticleType(_selectedCategory));
        	_allSuppliers = CreateItemsWithRepository<IArticleSupplierRepository, ArticleSupplier>(OrderFactory.GetSuppliers).ToList();
			//Create an article for each supplier
        	_allSuppliers.Each(supplier => CreateWithRepository<IArticleRepository, Article>(() => OrderFactory.GetArticle(_selectedArticleType, supplier)));
        }

    	private void AttEnKundÄrVald()
    	{
            _customer = OrderFactory.GetCustomer(_shop);
            WithRepository<IOrderCustomerRepository>().Save(_customer);
            HttpContext.SetupRequestParameter("customer", _customer.Id.ToString());
    	}

        private void AttEnOrderSkapats()
        {
            var category = OrderFactory.GetCategory();
            WithRepository<IArticleCategoryRepository>().Save(category);

            var articleType = OrderFactory.GetArticleType(category);
            WithRepository<IArticleTypeRepository>().Save(articleType);

            var articleSupplier = OrderFactory.GetSupplier();
            WithRepository<IArticleSupplierRepository>().Save(articleSupplier);

            var article = OrderFactory.GetArticle(articleType, articleSupplier);
            WithRepository<IArticleRepository>().Save(article);

            var lensRecipe = OrderFactory.GetLensRecipe();
            WithRepository<ILensRecipeRepository>().Save(lensRecipe);

            _customer = OrderFactory.GetCustomer(_shop);
            WithRepository<IOrderCustomerRepository>().Save(_customer);

            _order = OrderFactory.GetOrder(_shop, article, _customer, lensRecipe);
            WithRepository<IOrderRepository>().Save(_order);
            HttpContext.SetupRequestParameter("order", _order.Id.ToString());
        }

        private void ValdArtikelFinnsSparad()
        {
        	_article = CreateArticle();
        }

		private void AttAnvändarenFylltIBeställningsformuläret()
		{
            _form = OrderFactory.GetOrderEventArgs(_article.Id);
        }

        private void AnvändarenAvbryterBeställningen()
        {
            _createOrderPresenter.View_Abort(null, new EventArgs());
        }

		#endregion

		#region Act

        private void AnvändarenVäljerArtikeln()
        {
        	_createOrderPresenter.FillModel(null, new SelectedSomethingEventArgs {SelectedArticleId = _expectedArticle.Id});
        }

        private void AnvändarenVäljerEnArtikeltyp()
        {
        	_createOrderPresenter.FillModel(null, new SelectedSomethingEventArgs {SelectedArticleTypeId = _selectedArticleType.Id});
        }

        private void AnvändarenVäljerEnLeverantör()
        {
        	_createOrderPresenter.FillModel(null, new SelectedSomethingEventArgs {SelectedArticleTypeId = _selectedArticleType.Id, SelectedSupplierId = _selectedSupplier.Id});
        }

        private void AnvändarenVäljerEnKategori()
        {
        	_createOrderPresenter.FillModel(null, new SelectedSomethingEventArgs {SelectedCategoryId = _selectedCategory.Id});
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

        private void FormuläretFyllsMedData()
        {
            View.Model.SelectedArticleId.ShouldBe(_order.Article.Id);
            View.Model.SelectedArticleTypeId.ShouldBe(_order.Article.ArticleType.Id);
            View.Model.SelectedCategoryId.ShouldBe(_order.Article.ArticleType.Category.Id);
            View.Model.SelectedSupplierId.ShouldBe(_order.Article.ArticleSupplier.Id);
            View.Model.SelectedShippingOption.ShouldBe((int)_order.ShippingType);

            View.Model.SelectedAddition.Left.ShouldBe(_order.LensRecipe.Addition.Left);
            View.Model.SelectedAddition.Right.ShouldBe(_order.LensRecipe.Addition.Right);

            View.Model.SelectedAxis.Left.ShouldBe(_order.LensRecipe.Axis.Left);
            View.Model.SelectedAxis.Right.ShouldBe(_order.LensRecipe.Axis.Right);

            View.Model.SelectedBaseCurve.Left.ShouldBe(_order.LensRecipe.BaseCurve.Left ?? -9999);
            View.Model.SelectedBaseCurve.Right.ShouldBe(_order.LensRecipe.BaseCurve.Right ?? -9999);

            View.Model.SelectedCylinder.Left.ShouldBe(_order.LensRecipe.Cylinder.Left);
            View.Model.SelectedCylinder.Right.ShouldBe(_order.LensRecipe.Cylinder.Right);

            View.Model.SelectedDiameter.Left.ShouldBe(_order.LensRecipe.Diameter.Left ?? -9999);
            View.Model.SelectedDiameter.Right.ShouldBe(_order.LensRecipe.Diameter.Right ?? -9999);

            View.Model.SelectedPower.Left.ShouldBe(_order.LensRecipe.Power.Left);
            View.Model.SelectedPower.Right.ShouldBe(_order.LensRecipe.Power.Right);

        }

        private void LaddasArtikelnsAlternativ()
        {
            View.Model.BaseCurveOptions.And(OrderFactory.FillWithIncrementalValues(_expectedArticle.Options.BaseCurve)).Do((viewModelItem, domainItem) =>
            {
                viewModelItem.Value.ShouldBe(domainItem.Value.ToString());
                viewModelItem.Text.ShouldBe(domainItem.Text);
            });

            View.Model.DiameterOptions.And(OrderFactory.FillWithIncrementalValues(_expectedArticle.Options.Diameter)).Do((viewModelItem, domainItem) =>
            {
                viewModelItem.Value.ShouldBe(domainItem.Value.ToString());
                viewModelItem.Text.ShouldBe(domainItem.Text);
            });

            View.Model.AdditionOptionsEnabled.ShouldBe(_expectedArticle.Options.EnableAddition);
            View.Model.AxisOptionsEnabled.ShouldBe(_expectedArticle.Options.EnableAxis);
            View.Model.CylinderOptionsEnabled.ShouldBe(_expectedArticle.Options.EnableCylinder);
        }

        private void LaddasAktivaLeverantörer()
        {
            var viewModelSuppliers = View.Model.Suppliers.ToList().Skip(1);
        	var activeSuppliers = _allSuppliers.Where(x => x.Active);

            viewModelSuppliers.And(activeSuppliers).Do((viewModelItem, domainItem) =>
            {
                viewModelItem.Value.ShouldBe(domainItem.Id.ToString());
                viewModelItem.Text.ShouldBe(domainItem.Name);
            });
        }

        private void LaddasAktivaArtiklar()
        {
            var viewModelArticles = View.Model.OrderArticles.ToList().Skip(1);
        	var activeArticles = _allArticles.Where(x => x.Active);
            viewModelArticles.And(activeArticles).Do((viewModelItem, domainItem) =>
            {
                viewModelItem.Value.ShouldBe(domainItem.Id.ToString());
                viewModelItem.Text.ShouldBe(domainItem.Name);
            });
        }
		
        private void LaddasAktivaArtikeltyper()
        {
            var viewModelArticleTypes = View.Model.ArticleTypes.ToList().Skip(1);
        	var activeArticleTypes = _allArticleTypes.Where(x => x.Active);
            viewModelArticleTypes.And(activeArticleTypes).Do((viewModelItem, domainItem) =>
            {
                viewModelItem.Value.ShouldBe(domainItem.Id.ToString());
                viewModelItem.Text.ShouldBe(domainItem.Name);
            });
        }

        private void ArtikelkategorierLaddas()
        {
            var viewModelCategories = View.Model.Categories.ToList().Skip(1);
        	var activeCategories = _allCategories.Where(x => x.Active);
            viewModelCategories.And(activeCategories).Do((viewModelItem, domainItem) =>
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
            _order.LensRecipe.BaseCurve.Left.ShouldBe(_form.BaseCurve.Left);
            _order.LensRecipe.BaseCurve.Right.ShouldBe(_form.BaseCurve.Right);
            _order.LensRecipe.Diameter.Left.ShouldBe(_form.Diameter.Left);
            _order.LensRecipe.Diameter.Right.ShouldBe(_form.Diameter.Right);
            _order.LensRecipe.Power.Left.ShouldBe(_form.Power.Left);
            _order.LensRecipe.Power.Right.ShouldBe(_form.Power.Right);
            _order.LensRecipe.Axis.Left.ShouldBe(_form.Axis.Left);
            _order.LensRecipe.Axis.Right.ShouldBe(_form.Axis.Right);
            _order.LensRecipe.Cylinder.Left.ShouldBe(_form.Cylinder.Left);
            _order.LensRecipe.Cylinder.Right.ShouldBe(_form.Cylinder.Right);

            _order.LensRecipe.Addition.Left.ShouldBe(_form.Addition.Left);
            _order.LensRecipe.Addition.Right.ShouldBe(_form.Addition.Right);
            
            _order.ShippingType.ToInteger().ShouldBe(_form.ShipmentOption);
			_order.Status.ShouldBe(OrderStatus.Created);
			_order.Shop.Id.ShouldBe(_shop.Id);

        }

        private void AnvändarenFörflyttasTillVynFörNästaSteg()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_testRedirectSubmitUrl + "?order=" + _order.Id);
        }

        private void FlyttasAnvändarenTillIntranätsidan()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_testRedirectAbortUrl);
        }

        private void TasOrdernBort()
        {
            WithRepository<IOrderRepository>().Get(_order.Id).ShouldBe(null);
        }

		#endregion

    }
}
