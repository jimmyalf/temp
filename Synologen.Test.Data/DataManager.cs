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

namespace Spinit.Wpc.Synologen.Test.Data
{
	public class DataManager : DataUtility
	{
		public Shop CreateShop(ISqlProvider sqlProvider = null, string shopName = "Testbutik AB", string externalAccessUserName = "test.butik", string externalAccessHashedPassword = "6250625B226DF62870AE23AF8D3FAC0760D71588" /*TestPassword*/)
		{
			var provider = sqlProvider ?? GetSqlProvider();
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
				Zip = "43632",
                Longitude = 11.97456m,
                Latitude = 57.70887m,
				ExternalAccessHashedPassword = externalAccessHashedPassword,
				ExternalAccessUsername = externalAccessUserName
			};
			provider.AddUpdateDeleteShop(Enumerations.Action.Create, ref shop);
			return shop;
		}

		public CreatedMemberInfo CreateMemberForShop(User userRepository, SqlProvider sqlProvider, string userName, int shopId, int locationId, string password = "test")
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
			return new CreatedMemberInfo(userId, member.Id);
		}

		private static void CreateAdminUsers(User userRepository)
		{
			var superadminId = userRepository.Add("SuperAdmin", "g@nd@lf", "SuperAdmin", "Spinit", "info@spinit.se", 2, "Admin");
			var adminId = userRepository.Add("Admin", "g@llum", "Admin", "Spinit", "info@spinit.se", 2, "Admin");
			userRepository.ConnectGroup(superadminId, (int) GroupTypeRow.TYPE.SUPER_ADMIN);
			userRepository.ConnectGroup(adminId, (int) GroupTypeRow.TYPE.GLOBAL);
		}

		private int CreateUser(User userRepository, string userName, string password, int locationId)
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

		private void CreateShopAndTestUsers(User userRepository, SqlProvider provider)
		{
			var shop = CreateShop(provider, "Testbutik ABC");

			var shopMember = CreateMemberForShop(userRepository, provider, "butik", shop.ShopId, 2 /* synologen.nu location id */, "butik");
			userRepository.ConnectGroup(shopMember.UserId, 5 /*Butik user group id*/);
			provider.ConnectToCategory(shopMember.MemberId, 1 /*Butik member group id*/);

			var shopOpqMember = CreateMemberForShop(userRepository, provider, "butikopq", shop.ShopId, 2 /* synologen.nu location id */, "butikopq");
			userRepository.ConnectGroup(shopOpqMember.UserId, 10 /*Butik opq user group id*/);
			provider.ConnectToCategory(shopOpqMember.MemberId, 8 /*Butik opq member group id*/);

			var ownerMember = CreateMemberForShop(userRepository, provider, "ägare", shop.ShopId, 2 /* synologen.nu location id */, "ägare");
			userRepository.ConnectGroup(ownerMember.UserId, 8 /*Owner user group id*/);
			provider.ConnectToCategory(ownerMember.MemberId, 5 /*Owner member group id*/);
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
			DeleteForTable(connection, "tblMemberLanguageConnection");
			DeleteForTable(connection, "tblMemberLocationConnection");
			DeleteForTable(connection, "tblMemberUserConnection");
			DeleteForTable(connection, "tblMemberCategoryConnection");
			DeleteForTable(connection, "tblMemberPageConnection");
			DeleteAndResetIndexForTable(connection, "tblMemberClassifiedAds");
			DeleteAndResetIndexForTable(connection, "tblMembersContent");
			DeleteAndResetIndexForTable(connection, "tblMembers");
			Debug.WriteLine("Cleaned Members");
		} 

		private void DeleteUsers(IDbConnection connection)
		{
			DeleteForTable(connection, "tblBaseUsersGroups");
			DeleteAndResetIndexForTable(connection, "tblBaseUsers");
			Debug.WriteLine("Cleaned Users");
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

        private void DeleteOrders(IDbConnection connection)
        {
			DeleteAndResetIndexForTable(connection, "SynologenOrder");
            DeleteAndResetIndexForTable(connection, "SynologenOrderLensRecipe");
			DeleteAndResetIndexForTable(connection, "SynologenOrderArticle");
			DeleteForTable(connection, "SynologenOrderSubscriptionPendingPayment_SynologenOrderSubscriptionItem");
			DeleteAndResetIndexForTable(connection, "SynologenOrderSubscriptionPendingPayment");
			DeleteAndResetIndexForTable(connection, "SynologenOrderSubscriptionItem");
			DeleteAndResetIndexForTable(connection, "SynologenOrderSubscriptionError");
			DeleteAndResetIndexForTable(connection, "SynologenOrderTransaction");
			DeleteAndResetIndexForTable(connection, "SynologenOrderSubscription");
			DeleteAndResetIndexForTable(connection, "SynologenOrderCustomer");
			DeleteAndResetIndexForTable(connection, "SynologenOrderArticleType");
            DeleteAndResetIndexForTable(connection, "SynologenOrderArticleCategory");
            DeleteAndResetIndexForTable(connection, "SynologenOrderArticleSupplier");
			Debug.WriteLine("Cleaned Orders");
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
			var sqlProvider = GetSqlProvider();
			ValidateConnectionIsDev(connection);
			DeleteOPQAndConnections(connection);
            DeleteOrders(connection);
			DeleteLensSubscriptionsAndConnections(connection);
			DeleteContractSalesAndConnections(connection);
			DeleteFrameOrdersAndConnections(connection);
			DeleteShopsAndConnections(connection);
			DeleteMembersAndConnections(connection);
			DeleteUsers(connection);
			CreateAdminUsers(userRepository);
			CreateShopAndTestUsers(userRepository, sqlProvider as SqlProvider);
		}

	}

	public sealed class CreatedMemberInfo
	{
		public int UserId { get; private set; }
		public int MemberId { get; private set; }

		public CreatedMemberInfo(int userId, int memberId)
		{
			UserId = userId;
			MemberId = memberId;
		}
	}
}
