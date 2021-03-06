using System.Web.Mvc;
using Spinit.Wpc.Synologen.Presentation.Application.ModelBinders;

namespace Spinit.Wpc.Synologen.Presentation
{
	public class SynologenAdminAreaRegistration : AreaRegistration
	{
		public SynologenAdminAreaRegistration()
		{
			ModelBinders.Binders.DefaultBinder = new GridSortPropertyMappingModelBinder();
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			const string urlPrefix = "components/synologen/";

			context.MapRoute(AreaName + "FrameAdd", urlPrefix + "frames/add", new { controller = "Frame", action = "Add" });
			context.MapRoute(AreaName + "FrameDelete", urlPrefix + "frames/delete/{id}", new { controller = "Frame", action = "Delete" } );
			context.MapRoute(AreaName + "FrameEdit", urlPrefix + "frames/edit/{id}", new { controller = "Frame", action = "Edit" } );
			context.MapRoute(AreaName + "FrameIndex", urlPrefix + "frames", new { controller = "Frame", action = "Index" } );


			context.MapRoute(AreaName + "FrameAddColor", urlPrefix + "frames/addcolor", new { controller = "Frame", action = "AddColor" });
			context.MapRoute(AreaName + "FrameDeleteColor", urlPrefix + "frames/deletecolor/{id}", new { controller = "Frame", action = "DeleteColor" } );
			context.MapRoute(AreaName + "FrameEditColor", urlPrefix + "frames/editcolor/{id}", new { controller = "Frame", action = "EditColor" } );
			context.MapRoute(AreaName + "FrameColors", urlPrefix + "frames/colors", new { controller = "Frame", action = "Colors" } );


			context.MapRoute(AreaName + "FrameAddBrand", urlPrefix + "frames/addbrand", new { controller = "Frame", action = "AddBrand" });
			context.MapRoute(AreaName + "FrameDeleteBrand", urlPrefix + "frames/deletebrand/{id}", new { controller = "Frame", action = "DeleteBrand" } );
			context.MapRoute(AreaName + "FrameEditBrand", urlPrefix + "frames/editbrand/{id}", new { controller = "Frame", action = "EditBrand" } );
			context.MapRoute(AreaName + "FrameBrands", urlPrefix + "frames/brands", new { controller = "Frame", action = "Brands" } );


			context.MapRoute(AreaName + "FrameAddGlassType", urlPrefix + "frames/addglasstype", new { controller = "Frame", action = "AddGlassType" });
			context.MapRoute(AreaName + "FrameDeleteGlassType", urlPrefix + "frames/deleteglasstype/{id}", new { controller = "Frame", action = "DeleteGlassType" } );
			context.MapRoute(AreaName + "FrameEditGlassType", urlPrefix + "frames/editglasstype/{id}", new { controller = "Frame", action = "EditGlassType" } );
			context.MapRoute(AreaName + "FrameGlassTypes", urlPrefix + "frames/glasstypes", new { controller = "Frame", action = "GlassTypes" } );

            context.MapRoute(AreaName + "FrameAddSupplier", urlPrefix + "frames/addsupplier", new { controller = "Frame", action = "AddSupplier" });
            context.MapRoute(AreaName + "FrameDeleteSupplier", urlPrefix + "frames/deletesupplier/{id}", new { controller = "Frame", action = "DeleteSupplier" });
            context.MapRoute(AreaName + "FrameEditSupplier", urlPrefix + "frames/editsupplier/{id}", new { controller = "Frame", action = "EditSupplier" });
            context.MapRoute(AreaName + "FrameSuppliers", urlPrefix + "frames/suppliers", new { controller = "Frame", action = "Suppliers" });

			context.MapRoute(AreaName + "FrameOrders", urlPrefix + "frames/orders", new { controller = "Frame", action = "FrameOrders" } );
			context.MapRoute(AreaName + "ViewFrameOrders", urlPrefix + "frames/orders/view/{id}", new { controller = "Frame", action = "ViewFrameOrder" } );

			context.MapRoute(AreaName + "LensSubscriptions", urlPrefix + "lens-subscriptions", new { controller = "LensSubscription", action = "Index" } );
			context.MapRoute(AreaName + "LensSubscription", urlPrefix + "lens-subscription/{id}", new { controller = "LensSubscription", action = "EditSubscription" } );
			context.MapRoute(AreaName + "LensSubscriptionEdit", urlPrefix + "lens-subscription/edit/{id}", new { controller = "LensSubscription", action = "Edit" });

			context.MapRoute(AreaName + "LensSubscriptionTransactionArticles", urlPrefix + "lens-subscriptions/transaction-articles", new { controller = "LensSubscription", action = "TransactionArticles" } );
			context.MapRoute(AreaName + "LensSubscriptionTransactionArticleEdit", urlPrefix + "lens-subscriptions/transaction-article/{id}", new { controller = "LensSubscription", action = "EditTransactionArticle" } );
			context.MapRoute(AreaName + "LensSubscriptionTransactionArticleAdd", urlPrefix + "lens-subscriptions/add-transaction-article", new { controller = "LensSubscription", action = "AddTransactionArticle" } );
			context.MapRoute(AreaName + "LensSubscriptionTransactionArticleDelete", urlPrefix + "lens-subscriptions/delete-transaction-article/{id}", new { controller = "LensSubscription", action = "DeleteTransactionArticle" } );

			context.MapRoute(AreaName + "ContractSalesSettlement", urlPrefix + "contract-sales/settlement/{id}", new { controller = "ContractSales", action = "ViewSettlement" } );
			context.MapRoute(AreaName + "ContractSalesSettlements", urlPrefix + "contract-sales/settlements/", new { controller = "ContractSales", action = "Settlements" } );
			context.MapRoute(AreaName + "ContractSalesCancelOrder", urlPrefix + "contract-sales/order/{id}/cancel", new { controller = "ContractSales", action = "CancelOrder" } );
			context.MapRoute(AreaName + "ContractSalesManageOrder", urlPrefix + "contract-sales/order/{id}", new { controller = "ContractSales", action = "ManageOrder" } );
			context.MapRoute(AreaName + "ContractSalesAddArticle", urlPrefix + "contract-sales/article/add", new { controller = "ContractSales", action = "AddArticle" } );
			context.MapRoute(AreaName + "ContractSalesEditArticle", urlPrefix + "contract-sales/article/edit/{id}", new { controller = "ContractSales", action = "EditArticle" } );
			context.MapRoute(AreaName + "ContractSalesAddContractArticle", urlPrefix + "contract-sales/contract/{contractId}/article/add", new { controller = "ContractSales", action = "AddContractArticle" } );
			context.MapRoute(AreaName + "ContractSalesEditContractArticle", urlPrefix + "contract-sales/contract/{contractId}/article/{contractArticleId}/edit", new { controller = "ContractSales", action = "EditContractArticle" } );
			context.MapRoute(AreaName + "ContractSalesGetArticle", urlPrefix + "contract-sales/article/{articleId}/{format}", new { controller = "ContractSales", action = "GetArticle", format = UrlParameter.Optional } );
            context.MapRoute(AreaName + "ContractSalesDefault", urlPrefix + "contract-sales/{action}", new { controller = "ContractSales" });

			context.MapRoute(AreaName + "Reports", urlPrefix + "reports", new { controller = "Report", action = "Index" } );
            context.MapRoute(AreaName + "ReportsInvoiceCopy", urlPrefix + "reports/invoice-copy/{id}", new { controller = "Report", action = "InvoiceCopy" });
            context.MapRoute(AreaName + "InvoiceCredit", urlPrefix + "reports/InvoiceCredit/{id}", new { controller = "Report", action = "InvoiceCredit" });

			context.MapRoute(AreaName + "OrderSubscriptions", urlPrefix + "orders/subscriptions/", new { controller = "Order", action = "Subscriptions" } );
			context.MapRoute(AreaName + "OrderSubscription", urlPrefix + "orders/subscriptions/{id}", new { controller = "Order", action = "SubscriptionView" } );
			context.MapRoute(AreaName + "Orders", urlPrefix + "orders/{action}", new { controller = "Order", action = "Orders" } );

			context.MapRoute(AreaName + "ShopGroupEdit", urlPrefix + "shop-groups/edit/{id}", new { controller = "Synologen", action = "ShopGroupForm" } );
			context.MapRoute(AreaName + "ShopGroupDelete", urlPrefix + "shop-groups/delete/{id}", new { controller = "Synologen", action = "DeleteShopGroup" } );
			context.MapRoute(AreaName + "ShopGroupAdd", urlPrefix + "shop-groups/add", new { controller = "Synologen", action = "ShopGroupForm" } );
			context.MapRoute(AreaName + "ShopGroups", urlPrefix + "shop-groups", new { controller = "Synologen", action = "ShopGroups" } );

            context.MapRoute(AreaName + "Deviations", urlPrefix + "deviations/{action}", new { controller = "Deviation", action = "Deviations" });
		}

		public override string AreaName
		{
			get { return "SynologenAdmin"; }
		}
	}
}