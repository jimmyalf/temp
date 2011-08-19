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
		}

		public override string AreaName
		{
			get { return "SynologenAdmin"; }
		}
	}
}