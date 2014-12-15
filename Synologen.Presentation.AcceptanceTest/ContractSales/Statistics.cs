using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using Spinit.Wpc.Synologen.Test.Data;
using Spinit.Wpc.Utility.Business;
using StoryQ.sv_SE;
using Shop = Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales.Shop;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.ContractSales
{
	[TestFixture, Category("Skapa statistik")]
    public class Statistics : SpecTestbase
    {
	    private ContractSalesController _controller;
	    private ActionResult _actionResult;
	    private SqlProvider _sqlProvider;
	    private List<Contract> _contracts;
	    private List<Company> _companies;
	    private User _userRepo;
	    private List<Shop> _shops;
	    private List<CreatedMemberInfo> _members;
	    private StatisticsView _viewModel;
	    private Contract _selectedContract;
	    private Article _article;
	    private List<ContractArticleConnection> _contractArticleConnections;

	    public Statistics()
	    {
            Context = () =>
            {
                _controller = GetController<ContractSalesController>();
                _sqlProvider = (SqlProvider)WithSqlProvider<ISqlProvider>();
                _userRepo = DataManager.GetUserRepository();
            };

            Story = () => new Berättelse("Skapa statistik")
                .FörAtt("Få statistik över försäljningar")
                .Som("administratör")
                .VillJag("generera en statistik-fil");
	    }

        [Test]
        public void NärSidanLaddas()
        {
            SetupScenario(scenario => scenario
                .Givet(Avtal)
                    .Och(AvtalsFöretag)
                .När(SidanVisas)
                .Så(ListasAvtal)
                    .Och(ListasAvtalsFöretag));
        }

	    [Test]
        public void NärFilGenereras()
        {
            SetupScenario(scenario => scenario
                .Givet(Butiker)
                    .Och(ButikAnvändare)
                    .Och(Avtal)
                    .Och(AvtalsFöretag)
                    .Och(Artikel)
                    .Och(Försäljning)
                    .Och(AvtalÄrValt)
                .När(FilGenereras)
                .Så(ReturnerasEnExcelFil));
        }

	    [Test]
        public void NärFilGenererasUtanFörsäljning()
        {
            SetupScenario(scenario => scenario
                .Givet(Butiker)
                    .Och(ButikAnvändare)
                    .Och(Avtal)
                    .Och(AvtalsFöretag)
                    .Och(AvtalÄrValt)
                .När(FilGenereras)
                .Så(VisasEttFelmeddelande));
        }

	    #region Arrange

        private void Avtal()
        {
            _contracts = new List<Contract>(new[]
            {
                DataManager.CreateContract(_sqlProvider, "SAAB"),
                DataManager.CreateContract(_sqlProvider, "SRT")
            });
        }

        private void AvtalsFöretag()
        {
            _companies = _contracts.Select((contract, index) =>
            {
                var companyName = string.Format("{0} {1}", contract.Name, index.GetEnglishChar());
                return DataManager.CreateCompany(_sqlProvider, contract, companyName);
            }).ToList();
        }

        private void Butiker()
        {
            _shops = new List<Shop>(new[]
            {
				CreateShop<Shop>("Testbutik 1"),
				CreateShop<Shop>("Testbutik 2")                
            });
        }

        private void ButikAnvändare()
        {
            _members = _shops.Select((shop, index) =>
            {
                var userName = string.Format("test_user_{0}", index);
                return DataManager.CreateMemberForShop(_userRepo, _sqlProvider, userName, shop.Id, 2 /* location id */);
            }).ToList();
        }

        private void Artikel()
        {
            _article = ContractSalesOrderFactory.GetArticle();
            _sqlProvider.AddUpdateDeleteArticle(Enumerations.Action.Create, ref _article);
            _contractArticleConnections = new List<ContractArticleConnection>();
            foreach (var contract in _contracts)
            {
                var connection = new ContractArticleConnection
                {
                    Active = true,
                    ArticleId = _article.Id,
                    ArticleDescription = _article.Description,
                    ArticleName = _article.Name,
                    ArticleNumber = _article.Number,
                    ContractCustomerId = contract.Id,
                    EnableManualPriceOverride = false,
                    NoVAT = false,
                    Price = 123.5F,
                    SPCSAccountNumber = "123"
                };
                _sqlProvider.AddUpdateDeleteContractArticleConnection(Enumerations.Action.Create, ref connection);
                _contractArticleConnections.Add(connection);
            }
        }

        private void Försäljning()
        {
            foreach (var company in _companies)
            {
                foreach (var member in _members)
                {
                    var shopId = _sqlProvider.GetAllShopIdsPerMember(member.MemberId).Single();
                    var order = ContractSalesOrderFactory.GetOrder(company.Id, member.MemberId, shopId, 5 /* invoiced status */);
                    _sqlProvider.AddUpdateDeleteOrder(Enumerations.Action.Create, ref order);
                    var matchingConnection = _contractArticleConnections.First(x => x.ArticleId == _article.Id && x.ContractCustomerId == company.ContractId);
                    IOrderItem orderItem = ContractSalesOrderFactory.GetOrderItem(order.Id, matchingConnection);
                    _sqlProvider.AddUpdateDeleteOrderItem(Enumerations.Action.Create, ref orderItem);
                }
            }
        }

        private void AvtalÄrValt()
        {
            _selectedContract = _contracts.First();
        }

        #endregion

        #region Act

        private void SidanVisas()
        {
            _actionResult = _controller.Statistics();
        }

        private void FilGenereras()
        {
            _viewModel = new StatisticsView();

            if (_selectedContract != null)
            {
                _viewModel.SelectedContractId = _selectedContract.Id;
                _viewModel.SelectedContractName = _selectedContract.Name;
            }

            _actionResult = _controller.Statistics(_viewModel);
        }

        #endregion

        #region Assert

        private void ListasAvtalsFöretag()
        {
            var viewModel = GetViewModel<StatisticsView>(_actionResult);
            foreach (var company in _companies)
            {
                viewModel.Companies.ShouldContain(x => x.Id == company.Id);
            }
        }

        private void ListasAvtal()
        {
            var viewModel = GetViewModel<StatisticsView>(_actionResult);
            foreach (var contract in _contracts)
            {
                viewModel.Contracts.ShouldContain(x => x.Id == contract.Id);
            }
        }

        private void ReturnerasEnExcelFil()
        {
            var fileContentResult = (FileContentResult)_actionResult;
            fileContentResult.FileDownloadName.ShouldBe(string.Format("Statistik {0}.xlsx", _selectedContract.Name));
            fileContentResult.ContentType.ShouldBe("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            fileContentResult.FileContents.ShouldNotBe(null);
        }

        private void VisasEttFelmeddelande()
        {
            var actionMessage = GetActionMessages(_controller).Single();
            actionMessage.Type.ShouldBe(WpcActionMessageType.Error);
        }

        #endregion
    }
}