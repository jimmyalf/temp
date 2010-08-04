using System;
using System.Collections.Generic;
using System.ComponentModel;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Presentation.Models;

namespace Spinit.Wpc.Synologen.Presentation.App.Extensions
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

		public static IPagedList<FrameListItemView> ToFrameViewList(this IPagedList<Frame> entityList)
		{
			Func<Frame,FrameListItemView> typeConverter = x => new FrameListItemView {
				AllowOrders = x.AllowOrders,
                ArticleNumber = x.ArticleNumber,
                Brand = x.Brand.Name,
                Color = x.Color.Name,
                Id = x.Id,
                Name = x.Name
			};
			return entityList.ConvertList(new Converter<Frame, FrameListItemView>(typeConverter));
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

		public static string GetDomainPropertyName(this Type type, string viewModelPropertyName)
		{
			if(viewModelPropertyName == null) return null;
			var attributes = TypeDescriptor.GetProperties(type)[viewModelPropertyName].Attributes;
			var modelDomainMappingAttribute = (ModelDomainMappingAttribute)attributes[typeof(ModelDomainMappingAttribute)];
			return modelDomainMappingAttribute == null ? viewModelPropertyName : modelDomainMappingAttribute.DomainPropertyName;
		}
	}
}