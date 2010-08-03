using System;
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
	}
}