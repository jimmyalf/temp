using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Presentation.Models;

namespace Spinit.Wpc.Synologen.Presentation.Helpers.Extensions
{
	public static class ViewModelExtensions
	{
		public static Frame ToFrame(this FrameEditView viewModel, FrameBrand brand, FrameColor color)
		{
			return UpdateFrame(new Frame(), viewModel, brand, color);
		}

		public static Frame FillFrame(this FrameEditView viewModel, Frame entity, FrameBrand brand, FrameColor color)
		{
			return UpdateFrame(entity, viewModel, brand, color);
		}

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
				CylinderIncrementation = entity.Cylinder.Increment,
				CylinderMaxValue = entity.Cylinder.Max,
				CylinderMinValue = entity.Cylinder.Min,
				Id = entity.Id,
				IndexIncrementation = entity.Index.Increment,
				IndexMaxValue = entity.Index.Max,
				IndexMinValue = entity.Index.Min,
				Name = entity.Name,
				PupillaryDistanceIncrementation = entity.PupillaryDistance.Increment,
				PupillaryDistanceMaxValue = entity.PupillaryDistance.Max,
				PupillaryDistanceMinValue = entity.PupillaryDistance.Min,
				SphereIncrementation = entity.Sphere.Increment,
				SphereMaxValue = entity.Sphere.Max,
				SphereMinValue = entity.Sphere.Min,
				FormLegend = formLegend
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

		public static FrameColor ToFrameColor(this FrameColorEditView viewModel)
		{
			return UpdateFrameColor(new FrameColor(), viewModel);
		}

		public static FrameColor FillFrameColor(this FrameColorEditView viewModel, FrameColor entity)
		{

			return UpdateFrameColor(entity, viewModel);
		}

		public static ISortedPagedList<FrameListItemView> ToFrameViewList(this ISortedPagedList<Frame> entityList)
		{
			Func<Frame,FrameListItemView> typeConverter = x => new FrameListItemView {
			                                                                         	AllowOrders = x.AllowOrders,
			                                                                         	ArticleNumber = x.ArticleNumber,
			                                                                         	Brand = x.Brand.Name,
			                                                                         	Color = x.Color.Name,
			                                                                         	Id = x.Id,
			                                                                         	Name = x.Name,
			                                                                         };
			return entityList.ConvertSortedPagedList(new Converter<Frame, FrameListItemView>(typeConverter));
		}

		public static ISortedPagedList<FrameColorListItemView> ToFrameColorViewList(this ISortedPagedList<FrameColor> entityList)
		{
			Func<FrameColor, FrameColorListItemView> typeConverter = x => new FrameColorListItemView {
			                                                                                         	Id = x.Id,
			                                                                                         	Name = x.Name
			                                                                                         };
			return entityList.ConvertSortedPagedList(new Converter<FrameColor, FrameColorListItemView>(typeConverter));			
		}

		public static string GetTranslatedPropertyNameOrDefault<TViewModel,TDomainModel>(string viewModelPropertyName)
		{
			//TODO: Handle mapping an a better and more generic way
			var _propertyMapItems = new []
			{
				PropertyMapItem.CreateItem<FrameListItemView, Frame, string>(x => x.Brand, x => x.Brand.Name),
				PropertyMapItem.CreateItem<FrameListItemView, Frame, string>(x => x.Color, x => x.Color.Name),
			};
			foreach (var item in _propertyMapItems)
			{
				if(
					item.ViewModelType.Equals(typeof(TViewModel)) && 
					item.DomainModelType.Equals(typeof(TDomainModel)) && 
					item.ViewModelPropertyName.Equals(viewModelPropertyName))
				{
					return item.DomainModelPropertyName;
				}
			}
			return viewModelPropertyName;

		}

		internal class PropertyMapItem
		{
			public Type ViewModelType { get; private set; }
			public Type DomainModelType { get; private set; }
			public string ViewModelPropertyName { get; private set; }
			public string DomainModelPropertyName { get; private set; }
			public static PropertyMapItem CreateItem<TViewModel,TDomainModel,TProperty>(Expression<Func<TViewModel,TProperty>> viewModel, Expression<Func<TDomainModel,TProperty>> domainModel) where TViewModel : class where TDomainModel : class
			{

				return new PropertyMapItem
				{
					ViewModelType = typeof (TViewModel),
					DomainModelType = typeof (TDomainModel),
					ViewModelPropertyName = viewModel.GetName(),
					DomainModelPropertyName = domainModel.GetName()
				};
			}
		}

		private static Frame UpdateFrame(Frame entity, FrameEditView viewModel, FrameBrand brand, FrameColor color)
		{
			entity.AllowOrders = viewModel.AllowOrders;
			entity.ArticleNumber = viewModel.ArticleNumber;
			entity.Brand = brand;
			entity.Color = color;
			entity.Id = viewModel.Id;
			entity.Name = viewModel.Name;

			entity
				.SetInterval(x => x.Index, viewModel.IndexMinValue, viewModel.IndexMaxValue, viewModel.IndexIncrementation)
				.SetInterval(x => x.Sphere, viewModel.SphereMinValue, viewModel.SphereMaxValue, viewModel.SphereIncrementation)
				.SetInterval(x => x.Cylinder, viewModel.CylinderMinValue, viewModel.CylinderMaxValue, viewModel.CylinderIncrementation)
				.SetInterval(x => x.PupillaryDistance, viewModel.PupillaryDistanceMinValue, viewModel.PupillaryDistanceMaxValue, viewModel.PupillaryDistanceIncrementation)
				;

			return entity;
		}

		private static FrameColor UpdateFrameColor(FrameColor entity, FrameColorEditView viewModel)
		{
			entity.Name = viewModel.Name;
			return entity;
		}

	}
}