using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Utility.Business;
using Shop = Spinit.Wpc.Synologen.Business.Domain.Entities.Shop;
using SqlProvider = Spinit.Wpc.Synologen.Data.SqlProvider;

namespace Spinit.Wpc.Synogen.Test.Data
{
	public class DataManager : DataUtility
	{
		public Shop CreateShop(ISqlProvider sqlProvider, string shopName)
		{
			var shop = new Shop
			{
				Access = ShopAccess.LensSubscription | ShopAccess.SlimJim,
				Active = true,
				Address = null,
				Address2 = "Datavägen 2",
				CategoryId = 1,
				City = "Askim",
				Concern = null,
				ContactFirstName = "Adam",
				ContactLastName = "Bertil",
				Description = "Testbutik",
				Email = "info@spinit.se",
				Equipment = null,
				Fax = "031-684630",
				GiroId = 1,
				GiroNumber = "555",
				GiroSupplier = null,
				MapUrl = "http://maps.google.com",
				Name = shopName,
				Number = "0000",
				OrganizationNumber = "5551234-5678",
				Phone = "031-7483000",
				Phone2 = null,
				ShopId = 0,
				Url = "",
				Zip = "43632"
			};
			sqlProvider.AddUpdateDeleteShop(Enumerations.Action.Create, ref shop);
			return shop;
		}

		public int CreateMemberForShop(User userRepository, SqlProvider sqlProvider, string userName, int shopId, int locationId, string password = "test")
		{
			var userId = CreateUser(userRepository, userName, password, locationId);
			if (userId < 1) throw new ArgumentException("Cannot create user with username {0}. Make sure username is unique.", userName);
			var member = new MemberRow
			{
				Active = true,
				ContactFirst = "Adam",
				ContactLast = "Bertil",
				Email = "a.b@foretaget.se",
				CreatedBy = "TestUser",
				ApprovedBy = "TestUser",
				ApprovedDate = DateTime.Now,
				CreatedDate = DateTime.Now,
			};
			sqlProvider.AddUpdateDeleteMember(Enumerations.Action.Create, 1, ref member);
			sqlProvider.AddBaseUserConnection(member.Id, userId);
			sqlProvider.ConnectShopToMember(shopId, member.Id);
			return member.Id;
		}

		public int CreateMemberForShop(SqlProvider sqlProvider, string userName, int shopId, int locationId, string password = "test")
		{
			var userRepo = new User(ConnectionString);
			return CreateMemberForShop(userRepo, sqlProvider, userName, shopId, locationId, password);
		}

		private static void CreateAdminUsers(User userRepository)
		{
			var superadminId = userRepository.Add("SuperAdmin", "g@nd@lf", "SuperAdmin", "Spinit", "info@spinit.se", 1, "Admin");
			var adminId = userRepository.Add("Admin", "g@llum", "Admin", "Spinit", "info@spinit.se", 1, "Admin");
			userRepository.ConnectGroup(superadminId, 1 /*superadmin group*/);
			userRepository.ConnectGroup(adminId, 2 /*global admin group*/);
		}

		public int CreateUser(User userRepository, string userName, string password, int locationId)
		{
			return userRepository.Add(userName, password, "Adam", "Bertil", "a.b@foretaget.se", locationId, "TestUser");
		}

		public Company CreateCompany(ISqlProvider provider)
		{
			var contract = new Contract {Name = "Testavtal", Active = true};
			provider.AddUpdateDeleteContract(Enumerations.Action.Create, ref contract);
			var company = new Company
			{
				Active = true,
				Name = "Test Företag AB",
				BankCode = null,
				City = "Askim",
				ContractId = contract.Id,
				Country = new Country{Id = 1},
				EDIRecipientId = null,
				InvoiceCompanyName = "Test Företag AktieBolag",
				InvoiceFreeTextFormat = null,
				InvoicingMethodId = 2,
				OrganizationNumber = "555555-5555",
				PaymentDuePeriod = 30,
				PostBox = null,
				SPCSCompanyCode = "000",
				StreetName = "Datavägen 2",
				TaxAccountingCode = "SE213456789",
				Zip = "43632",
				//CompanyValidationRules = null
			};
			provider.AddUpdateDeleteCompany(Enumerations.Action.Create, ref company);
			return company;
		}
			 
		private void DeleteShopsAndConnections(IDbConnection connection)
		{
			DeleteForTable(connection, "tblSynologenShopMemberConnection");
			DeleteForTable(connection, "tblSynologenShopContractConnection");
			DeleteForTable(connection, "tblSynologenShopEquipmentConnection");
			DeleteForTable(connection, "tblSynologenSettlementOrderConnection");

			DeleteAndResetIndexForTable(connection, "tblSynologenShop");
			Debug.WriteLine("Cleaned Shops");
		}

		private void DeleteMembersAndConnections(IDbConnection connection)
		{
			//Member
			DeleteForTable(connection, "tblMemberLanguageConnection");
			DeleteForTable(connection, "tblMemberLocationConnection");
			DeleteForTable(connection, "tblMemberUserConnection");
			DeleteForTable(connection, "tblMemberCategoryConnection");
			DeleteForTable(connection, "tblMemberPageConnection");
			DeleteAndResetIndexForTable(connection, "tblMemberClassifiedAds");
			DeleteAndResetIndexForTable(connection, "tblMembersContent");
			DeleteAndResetIndexForTable(connection, "tblMembers");
			//Base
			DeleteForTable(connection, "tblBaseUsersGroups");
			DeleteAndResetIndexForTable(connection, "tblBaseUsers");
			Debug.WriteLine("Cleaned Members");
		} 

		private void DeleteLensSubscriptionsAndConnections(IDbConnection connection)
		{
			DeleteAndResetIndexForTable(connection, "SynologenLensSubscriptionError");
			DeleteAndResetIndexForTable(connection, "SynologenLensSubscriptionTransaction");
			DeleteAndResetIndexForTable(connection, "SynologenLensSubscription");
			DeleteAndResetIndexForTable(connection, "SynologenLensSubscriptionCustomer");
			DeleteAndResetIndexForTable(connection, "SynologenLensSubscriptionTransactionArticle");
			Debug.WriteLine("Cleaned Lens Subscriptions");
		}

		private void DeleteFrameOrdersAndConnections(IDbConnection connection)
		{
			DeleteAndResetIndexForTable(connection, "SynologenFrameOrder");
			DeleteAndResetIndexForTable(connection, "SynologenFrame");
			Debug.WriteLine("Cleaned Frame Orders");
		}

		private void DeleteOPQAndConnections(IDbConnection connection)
		{
			DeleteAndResetIndexForTable(connection, "SynologenOpqFiles");
			DeleteForTable(connection, "SynologenOpqDocumentHistories");
			DeleteAndResetIndexForTable(connection, "SynologenOpqDocuments");
			DeleteAndResetIndexForTable(connection, "SynologenOpqFileCategories");
			DeleteAndResetIndexForTable(connection, "SynologenOpqNodes");
			Debug.WriteLine("Cleaned OPQ");
		}

		private void DeleteContractSalesAndConnections(IDbConnection connection)
		{
			DeleteForTable(connection, "tblSynologenSettlementOrderConnection");
			DeleteForTable(connection, "tblSynologenContractArticleConnection");
			DeleteAndResetIndexForTable(connection, "tblSynologenSettlement");
			DeleteAndResetIndexForTable(connection, "tblSynologenOrderItems");
			DeleteAndResetIndexForTable(connection, "tblSynologenOrderHistory");
			DeleteAndResetIndexForTable(connection, "tblSynologenOrder");
			DeleteAndResetIndexForTable(connection, "tblSynologenArticle");
			Debug.WriteLine("Cleaned Contract Sales");
		}

		public virtual void ValidateConnectionIsDev(IDbConnection connection)
		{
			if(!IsDevelopmentServer(connection.ConnectionString))
			{
				throw new OperationCanceledException("Make sure you are running tests against a development database!");
			}
		}

		public void CleanTables()
		{
			var connection = new SqlConnection(ConnectionString);
			connection.Open();
			CleanTables(connection);
			connection.Close();
		}

		public void CleanTables(IDbConnection connection)
		{
			var userRepository = GetUserRepository();
			ValidateConnectionIsDev(connection);
			DeleteOPQAndConnections(connection);
			DeleteLensSubscriptionsAndConnections(connection);
			DeleteContractSalesAndConnections(connection);
			DeleteFrameOrdersAndConnections(connection);
			DeleteShopsAndConnections(connection);
			DeleteMembersAndConnections(connection);
			CreateAdminUsers(userRepository);
		}
	}
}
