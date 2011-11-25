using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Data.Test.CommonDataTestHelpers
{
	public static  class DataHelper
	{
		//public static void ExecuteStatement(IDbConnection sqlConnection, string sqlStatement)
		//{
		//    var transaction = sqlConnection.BeginTransaction();
		//    using (var cmd = sqlConnection.CreateCommand()) {
		//        cmd.Connection = sqlConnection;
		//        cmd.Transaction = transaction;

		//        cmd.CommandText = sqlStatement;
		//        cmd.CommandType = CommandType.Text;
		//        cmd.ExecuteNonQuery();
		//    }
		//    transaction.Commit();
		//}

		//public static void DeleteAndResetIndexForTable(IDbConnection sqlConnection, string tableName)
		//{
		//    ExecuteStatement(sqlConnection, String.Format("DELETE FROM {0}", tableName));
		//    ExecuteStatement(sqlConnection, String.Format("DBCC CHECKIDENT ({0}, reseed, 0)", tableName));
		//}

		//public static void DeleteForTable(IDbConnection sqlConnection, string tableName)
		//{
		//    ExecuteStatement(sqlConnection, String.Format("DELETE FROM {0}", tableName));
		//}


		//public static Business.Domain.Entities.Shop CreateShop(ISqlProvider sqlProvider, string shopName)
		//{
		//    var shop = new Business.Domain.Entities.Shop
		//    {
		//        Access = ShopAccess.LensSubscription | ShopAccess.SlimJim,
		//        Active = true,
		//        Address = null,
		//        Address2 = "Datavägen 2",
		//        CategoryId = 1,
		//        City = "Askim",
		//        Concern = null,
		//        ContactFirstName = "Adam",
		//        ContactLastName = "Bertil",
		//        Description = "Testbutik",
		//        Email = "info@spinit.se",
		//        Equipment = null,
		//        Fax = "031-684630",
		//        GiroId = 1,
		//        GiroNumber = "555",
		//        GiroSupplier = null,
		//        MapUrl = "http://maps.google.com",
		//        Name = shopName,
		//        Number = "0000",
		//        OrganizationNumber = "5551234-5678",
		//        Phone = "031-7483000",
		//        Phone2 = null,
		//        ShopId = 0,
		//        Url = "",
		//        Zip = "43632"
		//    };
		//    sqlProvider.AddUpdateDeleteShop(Enumerations.Action.Create, ref shop);
		//    return shop;
		//}

		//public static int CreateMemberForShop(SqlProvider sqlProvider, string userName, int shopId, int locationId, string password = "test")
		//{
		//    var userId = CreateUser(userName, password, locationId);
		//    if (userId < 1) throw new ArgumentException("Cannot create user with username {0}. Make sure username is unique.", userName);
		//    var member = new MemberRow
		//    {
		//        Active = true,
		//        ContactFirst = "Adam",
		//        ContactLast = "Bertil",
		//        Email = "a.b@foretaget.se",
		//        CreatedBy = "TestUser",
		//        ApprovedBy = "TestUser",
		//        ApprovedDate = DateTime.Now,
		//        CreatedDate = DateTime.Now,
		//    };
		//    sqlProvider.AddUpdateDeleteMember(Enumerations.Action.Create, 1, ref member);
		//    sqlProvider.AddBaseUserConnection(member.Id, userId);
		//    sqlProvider.ConnectShopToMember(shopId, member.Id);
		//    return member.Id;
		//}

		//public static void CreateAdminUsers()
		//{
		//    var repo = new User(ConnectionString);
		//    var superadminId = repo.Add("SuperAdmin", "g@nd@lf", "SuperAdmin", "Spinit", "info@spinit.se", 1, "Admin");
		//    var adminId = repo.Add("Admin", "g@llum", "Admin", "Spinit", "info@spinit.se", 1, "Admin");
		//    repo.ConnectGroup(superadminId, 1 /*superadmin group*/);
		//    repo.ConnectGroup(adminId, 2 /*global admin group*/);
		//}

		//public static int CreateUser(string userName, string password, int locationId)
		//{
		//    var repo = new User(ConnectionString);
		//    return repo.Add(userName, password, "Adam", "Bertil", "a.b@foretaget.se", locationId, "TestUser");
		//}

		//public static Company CreateCompany(ISqlProvider provider)
		//{
		//    var contract = new Contract {Name = "Testavtal", Active = true};
		//    provider.AddUpdateDeleteContract(Enumerations.Action.Create, ref contract);
		//    var company = new Company
		//    {
		//        Active = true,
		//        Name = "Test Företag AB",
		//        BankCode = null,
		//        City = "Askim",
		//        ContractId = contract.Id,
		//        Country = new Country{Id = 1},
		//        EDIRecipientId = null,
		//        InvoiceCompanyName = "Test Företag AktieBolag",
		//        InvoiceFreeTextFormat = null,
		//        InvoicingMethodId = 2,
		//        OrganizationNumber = "555555-5555",
		//        PaymentDuePeriod = 30,
		//        PostBox = null,
		//        SPCSCompanyCode = "000",
		//        StreetName = "Datavägen 2",
		//        TaxAccountingCode = "SE213456789",
		//        Zip = "43632",
		//        //CompanyValidationRules = null
		//    };
		//    provider.AddUpdateDeleteCompany(Enumerations.Action.Create, ref company);
		//    return company;
		//}

		//public static string ConnectionString{
		//    get
		//    {
		//        const string connectionStringname = "WpcServer";
		//        return ConfigurationManager.ConnectionStrings[connectionStringname].ConnectionString;
		//    }
		//}


		//public static void DeleteShopsAndConnections(IDbConnection sqlConnection)
		//{
		//    DeleteForTable(sqlConnection, "tblSynologenShopMemberConnection");
		//    DeleteForTable(sqlConnection, "tblSynologenShopContractConnection");
		//    DeleteForTable(sqlConnection, "tblSynologenShopEquipmentConnection");
			
		//    DeleteAndResetIndexForTable(sqlConnection, "SynologenOpqFiles");
		//    DeleteForTable(sqlConnection, "SynologenOpqDocumentHistories");
		//    DeleteAndResetIndexForTable(sqlConnection, "SynologenOpqDocuments");
		//    DeleteAndResetIndexForTable(sqlConnection, "tblSynologenOrderItems");
		//    DeleteAndResetIndexForTable(sqlConnection, "tblSynologenOrderHistory");
		//    DeleteForTable(sqlConnection, "tblSynologenSettlementOrderConnection");
		//    DeleteAndResetIndexForTable(sqlConnection, "tblSynologenOrder");

		//    DeleteAndResetIndexForTable(sqlConnection, "SynologenFrameOrder");


		//    DeleteAndResetIndexForTable(sqlConnection, "SynologenLensSubscriptionTransaction");
		//    DeleteAndResetIndexForTable(sqlConnection, "SynologenLensSubscriptionError");
		//    DeleteAndResetIndexForTable(sqlConnection, "SynologenLensSubscription");
		//    DeleteAndResetIndexForTable(sqlConnection, "SynologenLensSubscriptionCustomer");
			
			
		//    DeleteAndResetIndexForTable(sqlConnection, "tblSynologenShop");
		//}

		//public static void DeleteMembersAndConnections(IDbConnection connection)
		//{
		//    try{
		//        //OPQ
		//        DeleteAndResetIndexForTable(connection, "SynologenOpqFileCategories");
		//        DeleteAndResetIndexForTable(connection, "SynologenOpqNodes");
		//        //Synologen
		//        DeleteAndResetIndexForTable(connection, "SynologenLensSubscriptionTransaction");
		//        DeleteAndResetIndexForTable(connection, "SynologenLensSubscriptionError");
		//        DeleteAndResetIndexForTable(connection, "SynologenLensSubscription");
		//        DeleteAndResetIndexForTable(connection, "tblSynologenOrder");
		//        //Member
		//        DeleteForTable(connection, "tblMemberLanguageConnection");
		//        DeleteForTable(connection, "tblMemberLocationConnection");
		//        DeleteForTable(connection, "tblMemberUserConnection");
		//        DeleteForTable(connection, "tblMemberCategoryConnection");
		//        DeleteForTable(connection, "tblMemberPageConnection");
		//        DeleteAndResetIndexForTable(connection, "tblMemberClassifiedAds");
		//        DeleteAndResetIndexForTable(connection, "tblMembersContent");
		//        DeleteAndResetIndexForTable(connection, "tblMembers");
		//        //Base
		//        DeleteForTable(connection, "tblBaseUsersGroups");
		//        DeleteAndResetIndexForTable(connection, "tblBaseUsers");
		//    }
		//    catch(Exception ex)
		//    {
		//        Debug.WriteLine(ex);
		//        throw;
		//    }
		//    Debug.WriteLine("Cleaned Members");
		//}

		public static TType Parse<TType>(this DataRow row, string columnName)
		{
			return (TType) row[columnName];
		}

		public static TType? ParseNullable<TType>(this DataRow row, string columnName)
			where TType:struct
		{
			if(row.IsNull(columnName)) return null;
			return (TType?) row[columnName];
		}


	}
}