using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public class BaseGridSortPropertyMappingService : IGridSortPropertyMappingService
	{
		private List<PropertyMapItem> _mapItems;

		/// <summary>
		/// Creates a mapping for given properties
		/// </summary>
		/// <typeparam name="TController">Controller</typeparam>
		/// <typeparam name="TViewModel">View model type</typeparam>
		/// <typeparam name="TDomainModel">Domain model type</typeparam>
		/// <param name="viewModel">ViewModel property to map</param>
		/// <param name="domainModel">DomainModel property to map</param>
		protected void Map<TController, TViewModel, TDomainModel>(Expression<Func<TViewModel, string>> viewModel, Expression<Func<TDomainModel, string>> domainModel)
			where TViewModel : class
			where TDomainModel : class
			where TController : IController
		{
			if (_mapItems == null)
				_mapItems = new List<PropertyMapItem>();
			_mapItems.Add(PropertyMapItem.CreateItem(viewModel, domainModel, typeof (TController).Name));
		}

		/// <summary>
		/// Creates a mapping for given properties
		/// </summary>
		/// <typeparam name="TController">Controller</typeparam>
		/// <typeparam name="TViewModel">View model type</typeparam>
		/// <typeparam name="TDomainModel">Domain model type</typeparam>
		/// <typeparam name="TType">Common property type</typeparam>
		/// <param name="viewModel">ViewModel property to map</param>
		/// <param name="domainModel">DomainModel property to map</param>
		protected void Map<TController, TViewModel, TDomainModel, TType>(Expression<Func<TViewModel, TType>> viewModel, Expression<Func<TDomainModel, TType>> domainModel)
			where TViewModel : class
			where TDomainModel : class
			where TType : struct
			where TController : IController
		{
			if (_mapItems == null)
				_mapItems = new List<PropertyMapItem>();
			_mapItems.Add(PropertyMapItem.CreateItem(viewModel, domainModel, typeof (TController).Name));
		}

		public string TryFindMapping(string propertyName, string controllerName) 
		{ 
			if(_mapItems == null) return propertyName;
			foreach (var item in _mapItems)
			{
				if(item.ViewModelPropertyName.Equals(propertyName) && item.ControllerName.Equals(controllerName))
				{
					return item.DomainModelPropertyName;
				}
			}
			return propertyName;
		}

		internal class PropertyMapItem
		{
			public Type ViewModelType { get; private set; }
			public Type DomainModelType { get; private set; }
			public string ViewModelPropertyName { get; private set; }
			public string DomainModelPropertyName { get; private set; }
			public string ControllerName { get; private set; }
			public static PropertyMapItem CreateItem<TViewModel,TDomainModel,TProperty>(Expression<Func<TViewModel,TProperty>> viewModel, Expression<Func<TDomainModel,TProperty>> domainModel, string controllerName) where TViewModel : class where TDomainModel : class
			{
				return new PropertyMapItem
				{
					ViewModelType = typeof (TViewModel),
					DomainModelType = typeof (TDomainModel),
					ViewModelPropertyName = viewModel.GetName(),
					DomainModelPropertyName = domainModel.GetName(),
					ControllerName = controllerName
				};
			}
		}
	}
}