using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Routing;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Helpers.Extensions
{
	public static class ViewModelExtensions {
		#region To Domain Entities
		public static Frame ToFrame(this FrameEditView viewModel, FrameBrand brand, FrameColor color)
		{
			return UpdateFrame(new Frame(), viewModel, brand, color);
		}

		public static Frame FillFrame(this FrameEditView viewModel, Frame entity, FrameBrand brand, FrameColor color)
		{
			return UpdateFrame(entity, viewModel, brand, color);
		}

		public static FrameColor ToFrameColor(this FrameColorEditView viewModel)
		{
			return UpdateFrameColor(new FrameColor(), viewModel);
		}

		public static FrameColor FillFrameColor(this FrameColorEditView viewModel, FrameColor entity)
		{

			return UpdateFrameColor(entity, viewModel);
		}

		public static FrameBrand ToFrameBrand(this FrameBrandEditView viewModel)
		{
			return UpdateFrameBrand(new FrameBrand(), viewModel);
		}

		public static FrameBrand FillFrameBrand(this FrameBrandEditView viewModel, FrameBrand entity)
		{
			return UpdateFrameBrand(entity, viewModel);
		}


		public static FrameGlassType ToFrameGlassType(this FrameGlassTypeEditView viewModel)
		{
			return UpdateFrameGlassType(new FrameGlassType(), viewModel);
		}

		public static FrameGlassType FillFrameGlassType(this FrameGlassTypeEditView viewModel, FrameGlassType entity)
		{
			return UpdateFrameGlassType(entity, viewModel);
		}

		public static Customer FillCustomer(this SubscriptionView viewModel, Customer entity)
		{
			return UpdateCustomer(entity, viewModel);	
		}


		public static SubscriptionView FillCustomer(this SubscriptionView viewModel, SubscriptionView customerModel)
		{
			viewModel.AddressLineOne = customerModel.AddressLineOne;
			viewModel.AddressLineTwo = customerModel.AddressLineTwo;
			viewModel.City = customerModel.City;
			viewModel.Country = customerModel.Country;
			viewModel.CustomerId = customerModel.CustomerId;
			viewModel.CustomerName = customerModel.CustomerName;
			viewModel.CustomerNotes = customerModel.CustomerNotes;
			viewModel.Email = customerModel.Email;
			viewModel.FirstName = customerModel.FirstName;
			viewModel.LastName = customerModel.LastName;
			viewModel.MobilePhone = customerModel.MobilePhone;
			viewModel.PersonalIdNumber = customerModel.PersonalIdNumber;
			viewModel.PostalCode = customerModel.PostalCode;
			return viewModel;
		}

		public static Subscription FillSubscription(this SubscriptionView viewModel, Subscription entity)
		{
			return UpdateSubscription(entity, viewModel);
		}

		public static SubscriptionView FillSubscription(this SubscriptionView viewModel, SubscriptionView subscriptionModel)
		{
			viewModel.AddressLineOne = subscriptionModel.AddressLineOne;
			viewModel.AddressLineTwo = subscriptionModel.AddressLineTwo;
			viewModel.City = subscriptionModel.City;
			viewModel.PostalCode = subscriptionModel.PostalCode;
			viewModel.Email = subscriptionModel.Email;
			viewModel.MobilePhone = subscriptionModel.MobilePhone;
			viewModel.Phone = subscriptionModel.Phone;
			viewModel.PersonalIdNumber = subscriptionModel.PersonalIdNumber;
			viewModel.AccountNumber = subscriptionModel.AccountNumber;
			viewModel.ClearingNumber = subscriptionModel.ClearingNumber;
			viewModel.MonthlyAmount = subscriptionModel.MonthlyAmount;
			viewModel.CustomerNotes = subscriptionModel.CustomerNotes;
			viewModel.SubscriptionNotes = subscriptionModel.SubscriptionNotes;
			viewModel.FirstName = subscriptionModel.FirstName;
			viewModel.LastName = subscriptionModel.LastName;

			return viewModel;
		}

		#endregion

		#region To Edit Views
		public static FrameEditView ToFrameEditView(this Frame entity, IEnumerable<FrameBrand> availableFrameBrands, IEnumerable<FrameColor> availableFrameColors, string formLegend)
		{
			return new FrameEditView
			{
				AllowOrders = entity.AllowOrders,
				ArticleNumber = entity.ArticleNumber,
				AvailableFrameBrands = availableFrameBrands,
				AvailableFrameColors = availableFrameColors,
				BrandId = entity.Brand.Id,
				ColorId = entity.Color.Id,
				Id = entity.Id,
				Name = entity.Name,
				PupillaryDistanceIncrementation = entity.PupillaryDistance.Increment,
				PupillaryDistanceMaxValue = entity.PupillaryDistance.Max,
				PupillaryDistanceMinValue = entity.PupillaryDistance.Min,

				SphereIncrementation = entity.Sphere.Increment,
				SphereMaxValue = entity.Sphere.Max,
				SphereMinValue = entity.Sphere.Min,

				CylinderIncrementation = entity.Cylinder.Increment,
				CylinderMaxValue = entity.Cylinder.Max,
				CylinderMinValue = entity.Cylinder.Min,

				FormLegend = formLegend,
            	CurrentStock = entity.Stock.CurrentStock, 
				StockAtStockDate = entity.Stock.StockAtStockDate, 
				StockDate = entity.Stock.StockDate.ToString("yyyy-MM-dd")

			};
		}

		public static FrameColorEditView ToFrameColorEditView(this FrameColor frameColor, string legend)
		{
			return new FrameColorEditView
			{
				Id = frameColor.Id,
				Name = frameColor.Name,
				FormLegend = legend
			};
		}

		public static FrameBrandEditView ToFrameBrandEditView(this FrameBrand frameBrand, string legend)
		{
			return new FrameBrandEditView
			{
				Id = frameBrand.Id,
				Name = frameBrand.Name,
				FormLegend = legend
			};
		}

		public static FrameGlassTypeEditView ToFrameGlassTypeEditView(this FrameGlassType frameGlassType, string legend)
		{
			return new FrameGlassTypeEditView
			{
				Id = frameGlassType.Id,
				Name = frameGlassType.Name,
				FormLegend = legend,
                IncludeAdditionParametersInOrder = frameGlassType.IncludeAdditionParametersInOrder,
                IncludeHeightParametersInOrder = frameGlassType.IncludeHeightParametersInOrder
			};
		}

		public static FrameOrderView ToFrameOrderView(this FrameOrder frameOrder)
		{
			return new FrameOrderView
			{
                Addition = (frameOrder.Addition != null) ? new EyeParameterViewModel(frameOrder.Addition) : null,
                Axis = new EyeParameterViewModel(frameOrder.Axis){ Format="N0" },
                Created = frameOrder.Created.ToString("yyyy-MM-dd HH:mm"),
                Cylinder = new EyeParameterViewModel(frameOrder.Cylinder),
                Frame = frameOrder.Frame.Name,
                FrameArticleNumber = frameOrder.Frame.ArticleNumber,
                GlassType = frameOrder.GlassType.Name,
                Height = (frameOrder.Height != null) ? new EyeParameterViewModel(frameOrder.Height) : null,
                Id = frameOrder.Id,
                PupillaryDistance = new EyeParameterViewModel(frameOrder.PupillaryDistance),
                Sent = frameOrder.Sent.HasValue ? frameOrder.Sent.Value.ToString("yyyy-MM-dd HH:mm") : null,
                Shop = frameOrder.OrderingShop.Name,
                ShopCity = frameOrder.OrderingShop.Address.City,
                Sphere = new EyeParameterViewModel(frameOrder.Sphere),
                Notes = frameOrder.Reference,
			};
		}
		#endregion

		#region To View Lists
		public static IEnumerable<FrameListItemView> ToFrameViewList(this IEnumerable<Frame> entityList)
		{
			Func<Frame, FrameListItemView> typeConverter = x => new FrameListItemView {
				AllowOrders = x.AllowOrders,
				ArticleNumber = x.ArticleNumber,
				Brand = x.Brand.Name,
				Color = x.Color.Name,
				Id = x.Id,
				Name = x.Name,
                NumberOfOrdersWithThisFrame = x.NumberOfConnectedOrdersWithThisFrame
			};
			return entityList.ConvertSortedPagedList(typeConverter);
		}

		public static IEnumerable<FrameColorListItemView> ToFrameColorViewList(this IEnumerable<FrameColor> entityList)
		{
			Func<FrameColor, FrameColorListItemView> typeConverter = x => new FrameColorListItemView {
			                                                                                         	Id = x.Id,
			                                                                                         	Name = x.Name,
                                                                                                        NumberOfFramesWithThisColor = x.NumberOfFramesWithThisColor
			                                                                                         };
			return entityList.ConvertSortedPagedList(typeConverter);
		}

		public static IEnumerable<FrameBrandListItemView> ToFrameBrandViewList(this IEnumerable<FrameBrand> entityList)
		{
			Func<FrameBrand, FrameBrandListItemView> typeConverter = x => new FrameBrandListItemView {
			                                                                                         	Id = x.Id,
			                                                                                         	Name = x.Name,
																										NumberOfFramesWithThisBrand = x.NumberOfFramesWithThisBrand
			                                                                                         };
			return entityList.ConvertSortedPagedList(typeConverter);
		}

		public static IEnumerable<FrameGlassTypeListItemView> ToFrameGlassTypeViewList(this IEnumerable<FrameGlassType> entityList)
		{
			Func<FrameGlassType, FrameGlassTypeListItemView> typeConverter = x => new FrameGlassTypeListItemView
			{
				Id = x.Id,
				Name = x.Name,
				IncludeAddition = x.IncludeAdditionParametersInOrder,
				IncludeHeight = x.IncludeHeightParametersInOrder,
                NumberOfOrdersWithThisGlassType = x.NumberOfConnectedOrdersWithThisGlassType
			};
			return entityList.ConvertSortedPagedList(typeConverter);
		}

		public static IEnumerable<FrameOrderListItemView> ToFrameOrderViewList(this IEnumerable<FrameOrder> entityList)
		{
			Func<FrameOrder, FrameOrderListItemView> typeConverter = x => new FrameOrderListItemView
			{
				Id = x.Id,
                Frame = x.Frame.Name,
                GlassType = x.GlassType.Name,
                Sent = x.IsSent,
                Shop = x.OrderingShop.Name,
                Created = x.Created.ToString("yyyy-MM-dd"),
			};
			return entityList.ConvertSortedPagedList(typeConverter);
		}

		#endregion

		public static RouteValueDictionary AddOrReplaceRouteValue<TViewModel>(this TViewModel viewViewModel, Expression<Func<TViewModel,string>> viewModelProperty, RouteValueDictionary dictionary) where TViewModel : class
		{
			var key = viewModelProperty.GetName().ToLower();
			var value = viewModelProperty.Compile().Invoke(viewViewModel);
			var encodedValue = HttpUtility.UrlEncode(value);
			return dictionary.AddOrReplaceRouteValue(key, encodedValue);
		}

		public static RouteValueDictionary TryRemoveRouteValue<TViewModel>(this TViewModel viewModel,  Expression<Func<TViewModel,string>> propertyExpression, RouteValueDictionary dictionary) where TViewModel : class
		{
			var key = propertyExpression.GetName().ToLower();
			return dictionary.TryRemoveRouteValue(key);
		}

		public static string UrlEncode(this string value)
		{
			return string.IsNullOrEmpty(value) ? value : HttpUtility.UrlEncode(value);
		}

		public static string UrlDecode(this string value)
		{
			return string.IsNullOrEmpty(value) ? value : HttpUtility.UrlDecode(value);
		}

		private static Frame UpdateFrame(Frame entity, FrameEditView viewModel, FrameBrand brand, FrameColor color)
		{
			entity.AllowOrders = viewModel.AllowOrders;
			entity.ArticleNumber = viewModel.ArticleNumber;
			entity.Brand = brand;
			entity.Color = color;
			entity.Id = viewModel.Id;
			entity.Name = viewModel.Name;
			if(entity.Stock == null) entity.Stock = new FrameStock();
			if(entity.Stock.StockAtStockDate != viewModel.StockAtStockDate)
			{
				entity.Stock.StockAtStockDate = viewModel.StockAtStockDate;
				entity.Stock.StockDate = DateTime.Now;
			}

			entity.SetInterval(x => x.PupillaryDistance, viewModel.PupillaryDistanceMinValue, viewModel.PupillaryDistanceMaxValue, viewModel.PupillaryDistanceIncrementation);
			entity.SetInterval(x => x.Sphere, viewModel.SphereMinValue, viewModel.SphereMaxValue, viewModel.SphereIncrementation);
			entity.SetInterval(x => x.Cylinder, viewModel.CylinderMinValue, viewModel.CylinderMaxValue, viewModel.CylinderIncrementation);

			return entity;
		}

		private static FrameColor UpdateFrameColor(FrameColor entity, FrameColorEditView viewModel)
		{
			entity.Name = viewModel.Name;
			return entity;
		}

		private static FrameBrand UpdateFrameBrand(FrameBrand entity, FrameBrandEditView viewModel)
		{
			entity.Name = viewModel.Name;
			return entity;
		}

		private static FrameGlassType UpdateFrameGlassType(FrameGlassType entity, FrameGlassTypeEditView viewModel)
		{
			entity.Name = viewModel.Name;
			entity.IncludeAdditionParametersInOrder = viewModel.IncludeAdditionParametersInOrder;
			entity.IncludeHeightParametersInOrder = viewModel.IncludeHeightParametersInOrder;
			return entity;
		}


		private static Customer UpdateCustomer(Customer entity, SubscriptionView viewModel)
		{
			entity.Address.AddressLineOne = viewModel.AddressLineOne;
			entity.Address.AddressLineTwo = viewModel.AddressLineTwo;
			entity.Address.City = viewModel.City;
			entity.Address.PostalCode = viewModel.PostalCode;
			entity.FirstName = viewModel.FirstName;
			entity.LastName = viewModel.LastName;
			entity.Notes = viewModel.CustomerNotes;
			entity.PersonalIdNumber = viewModel.PersonalIdNumber;
			entity.Contact.Email = viewModel.Email;
			entity.Contact.MobilePhone = viewModel.MobilePhone;
			entity.Contact.Phone = viewModel.Phone;
			return entity;
		}


		private static Subscription UpdateSubscription(Subscription entity, SubscriptionView viewModel)
		{
			entity.Notes = viewModel.SubscriptionNotes;
			entity.PaymentInfo.AccountNumber = viewModel.AccountNumber;
			entity.PaymentInfo.ClearingNumber = viewModel.ClearingNumber;
			entity.PaymentInfo.MonthlyAmount = viewModel.MonthlyAmount.ToDecimalOrDefault();
			var entityCustomer = entity.Customer;
			entity.Customer = UpdateCustomer(entityCustomer, viewModel);
			return entity;
		}

		public static IEnumerable<TOutputModel> ConvertSortedPagedList<TModel, TOutputModel>(this IEnumerable<TModel> enumerable, Func<TModel, TOutputModel> converter) where TOutputModel : class where TModel : class {
			return enumerable.ToExtendedEnumerable().ConvertSortedPagedList(converter);
		}

		public static IEnumerable<TOutputModel> ConvertSortedPagedList<TModel, TOutputModel>(this IExtendedEnumerable<TModel> enumerable, Func<TModel, TOutputModel> converter) where TOutputModel : class {
			return enumerable.ConvertAll(converter);
		}
	}
}