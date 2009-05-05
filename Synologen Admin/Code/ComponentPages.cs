// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: ComponentPages.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Admin/Code/ComponentPages.cs $
//
//  VERSION
//	$Revision: 7 $
//
//  DATES
//	Last check in: $Date: 09-04-02 17:54 $
//	Last modified: $Modtime: 09-04-02 12:51 $
//
//  AUTHOR(S)
//	$Author: Cber $
// 	
//
//  COPYRIGHT
// 	Copyright (c) 2008 Spinit AB --- ALL RIGHTS RESERVED
// 	Spinit AB, Datavägen 2, 436 32 Askim, SWEDEN
//
// ==========================================================================
// 
//  DESCRIPTION
//  
//
// ==========================================================================
//
//	History
//
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Admin/Code/ComponentPages.cs $
//
//7     09-04-02 17:54 Cber
//New Settlement functionality
//
//6     09-01-09 17:44 Cber
//
//5     09-01-08 18:08 Cber
//
//4     09-01-07 17:35 Cber
//
//3     08-12-22 17:22 Cber
//
//2     08-12-19 17:22 Cber
//
//1     08-12-16 17:01 Cber
// 
// ==========================================================================

namespace Spinit.Wpc.Synologen.Presentation.Code {
	public class ComponentPages {
		private const string  PageBase = "~/Components/Synologen/";

		public const string AddFiles = PageBase + "AddFiles.aspx";
		public const string Category = PageBase + "Category.aspx";
		public const string EditMember = PageBase + "EditMember.aspx";
		public const string EditShop = PageBase + "EditShop.aspx";
		public const string FileCategories = PageBase + "FileCategories.aspx";
		public const string Files =	PageBase + "Files.aspx";
		public const string Index =	PageBase + "Index.aspx";
		public const string Shops =	PageBase + "Shops.aspx";
		public const string Contracts = PageBase + "Contracts.aspx";
		public const string EditContractCustomer = PageBase + "EditContract.aspx";
		public const string ContractArticles = PageBase + "ContractArticles.aspx";
		public const string ContractCompanies = PageBase + "ContractCompanies.aspx";
		public const string Articles = PageBase + "Articles.aspx";
		public const string EditOrder = PageBase + "EditOrder.aspx"; 
		public const string Orders = PageBase + "Orders.aspx";
		public const string NoAccess = "~/NoAccess.aspx";
		public const string ShopCategories = PageBase + "ShopCategories.aspx";
		public const string ShopEquipment = PageBase + "ShopEquipment.aspx";
		public const string OrderStatus = PageBase + "OrderStatus.aspx";
		public const string Settlements = PageBase + "Settlements.aspx";
	}
}