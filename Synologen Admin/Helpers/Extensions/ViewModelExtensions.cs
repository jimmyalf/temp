using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Presentation.Models;

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
				FormLegend = formLegend,
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
		#endregion

		#region To View Lists
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
			                                                                                         	Name = x.Name,
                                                                                                        NumberOfFramesWithThisColor = x.NumberOfFramesWithThisColor
			                                                                                         };
			return entityList.ConvertSortedPagedList(new Converter<FrameColor, FrameColorListItemView>(typeConverter));			
		}

		public static ISortedPagedList<FrameBrandListItemView> ToFrameBrandViewList(this ISortedPagedList<FrameBrand> entityList)
		{
			Func<FrameBrand, FrameBrandListItemView> typeConverter = x => new FrameBrandListItemView {
			                                                                                         	Id = x.Id,
			                                                                                         	Name = x.Name,
																										NumberOfFramesWithThisBrand = x.NumberOfFramesWithThisBrand
			                                                                                         };
			return entityList.ConvertSortedPagedList(new Converter<FrameBrand, FrameBrandListItemView>(typeConverter));
		}

		public static ISortedPagedList<FrameGlassTypeListItemView> ToFrameGlassTypeViewList(this ISortedPagedList<FrameGlassType> entityList)
		{
			Func<FrameGlassType, FrameGlassTypeListItemView> typeConverter = x => new FrameGlassTypeListItemView
			{
				Id = x.Id,
				Name = x.Name,
				IncludeAddition = x.IncludeAdditionParametersInOrder,
				IncludeHeight = x.IncludeHeightParametersInOrder
			};
			return entityList.ConvertSortedPagedList(new Converter<FrameGlassType, FrameGlassTypeListItemView>(typeConverter));
		}

		#endregion

		private static Frame UpdateFrame(Frame entity, FrameEditView viewModel, FrameBrand brand, FrameColor color)
		{
			entity.AllowOrders = viewModel.AllowOrders;
			entity.ArticleNumber = viewModel.ArticleNumber;
			entity.Brand = brand;
			entity.Color = color;
			entity.Id = viewModel.Id;
			entity.Name = viewModel.Name;

			entity
				.SetInterval(x => x.PupillaryDistance, viewModel.PupillaryDistanceMinValue, viewModel.PupillaryDistanceMaxValue, viewModel.PupillaryDistanceIncrementation);

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

	}
}