using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Application.Extensions;
using Spinit.Wpc.Synologen.Presentation.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Models.Order
{

	public class ArticleFormView : CommonFormView
	{
		public ArticleFormView()
		{
			Addition = new SequenceDefinitionView();
			Axis = new SequenceDefinitionView();
			BaseCurve = new SequenceDefinitionView();
			Cylinder = new SequenceDefinitionView();
			Diameter = new SequenceDefinitionView();
			Power = new SequenceDefinitionView();
		}

		public ArticleFormView(IEnumerable<ArticleSupplier> suppliers, IEnumerable<ArticleType> types, int? id = null, string name = null, ArticleOptions options = null) : base(id)
		{
			Name = name;
			SetSuppliers(suppliers);
			SetTypes(types);
			Addition = options.GetDefinitionView(x => x.Addition);
			Axis = options.GetDefinitionView(x => x.Axis);
			BaseCurve = options.GetDefinitionView(x => x.BaseCurve);
			Cylinder = options.GetDefinitionView(x => x.Cylinder);
			Diameter = options.GetDefinitionView(x => x.Diameter);
			Power = options.GetDefinitionView(x => x.Power);
		}

		public ArticleFormView SetSuppliers(IEnumerable<ArticleSupplier> suppliers)
		{
			Suppliers = suppliers.ToSelectList(x => x.Id, x => x.Name);
			return this;
		}

		public ArticleFormView SetTypes(IEnumerable<ArticleType> types)
		{
			Types = types.ToSelectList(x => x.Id, x => x.Name);
			return this;
		}

		[DisplayName("Namn")]
		public string Name { get; set; }

		[DisplayName("Styrka")]
		public SequenceDefinitionView Power { get; set; }

		[DisplayName("Diameter")]
		public SequenceDefinitionView Diameter { get; set; }

		[DisplayName("Baskurva")]
		public SequenceDefinitionView BaseCurve { get; set; }

		[DisplayName("Axel")]
		public SequenceDefinitionView Axis { get; set; }

		[DisplayName("Cylinder")]
		public SequenceDefinitionView Cylinder { get; set; }

		[DisplayName("Addition")]
		public SequenceDefinitionView Addition { get; set; }

		public int SupplierId { get; set; }
		public int TypeId { get; set; }

		public SelectList Suppliers { get; set; }
		public SelectList Types { get; set; }

		public bool HasCustomValidationErrors
		{
			get { return GetCustomValidationErrors().Any(); }
		}

		public IEnumerable<ValidationError> GetCustomValidationErrors()
		{
			foreach (var error in this.GetValidationErrors(x => x.Power))
			{
				yield return error;
			}
			foreach (var error in this.GetValidationErrors(x => x.Diameter))
			{
				yield return error;
			}
			foreach (var error in this.GetValidationErrors(x => x.BaseCurve))
			{
				yield return error;
			}
			foreach (var error in this.GetValidationErrors(x => x.Axis))
			{
				yield return error;
			}
			foreach (var error in this.GetValidationErrors(x => x.Cylinder))
			{
				yield return error;
			}
			foreach (var error in this.GetValidationErrors(x => x.Addition))
			{
				yield return error;
			}
		}

		public ArticleOptions GetArticleOptions(float defaultValue = default(float))
		{
			return new ArticleOptions
			{
				Addition = Addition.GetSequenceDefinition(defaultValue),
				Axis = Axis.GetSequenceDefinition(defaultValue),
				BaseCurve = BaseCurve.GetSequenceDefinition(defaultValue),
				Cylinder = Cylinder.GetSequenceDefinition(defaultValue),
				Diameter = Diameter.GetSequenceDefinition(defaultValue),
				Power = Power.GetSequenceDefinition(defaultValue)
			};
		}
	}

	public class SequenceDefinitionView
	{
		public SequenceDefinitionView() { }
		public SequenceDefinitionView(SequenceDefinition definition)
		{
			if(definition == null) return;
			Min = definition.Min;
			Max = definition.Max;
			Increment = definition.Increment;
		}
		[DisplayName("Min"), RegularExpression("^(-)?[0-9]+(,[0-9]+)?$", ErrorMessage = "Angivet värde måste vara numeriskt")]
		public float? Min { get; set; }
		[DisplayName("Max"), RegularExpression("^(-)?[0-9]+(,[0-9]+)?$", ErrorMessage = "Angivet värde måste vara numeriskt")]
		public float? Max { get; set; }
		[DisplayName("Inkrement"), RegularExpression("^(-)?[0-9]+(,[0-9]+)?$", ErrorMessage = "Angivet värde måste vara numeriskt")]
		public float? Increment { get; set; }

		public bool NoParametersSet
		{
			get { return Min.HasValue == false && Max.HasValue == false && Increment.HasValue == false; }
		}

		public IEnumerable<ValidationError> GetValidationErrors(string propertyName, bool allowAllNull = false)
		{
			if (allowAllNull && NoParametersSet) yield break;
			if (!Min.HasValue) yield return new ValidationError(propertyName + ".Min", "Min saknar värde");
			if (!Max.HasValue) yield return new ValidationError(propertyName + ".Max", "Max saknar värde");
			if (!Increment.HasValue) yield return new ValidationError(propertyName + ".Increment", "Inkrement saknar värde");
			if (Increment.HasValue && Increment <= 0) yield return new ValidationError(propertyName + ".Increment", "Inkrement måste anges större än 0");
			if(Min.HasValue && Max.HasValue && Min.Value >= Max.Value)
			{
			    yield return new ValidationError(propertyName + ".Min", "Min måste vara mindre än Max");
			}
		}

		public SequenceDefinition GetSequenceDefinition(float defaultValue = default(float))
		{
			return new SequenceDefinition()
			{
				Min =  Min?? defaultValue,
				Max = Max ?? defaultValue,
				Increment = Increment ?? defaultValue
			};
		}

	}

	public class ValidationError
	{
		public ValidationError(string propertyName, string errorMessage)
		{
			PropertyName = propertyName;
			ErrorMessage = errorMessage;
		}
		public string PropertyName { get; set; }
		public string ErrorMessage { get; set; }

		public override string ToString()
		{
			return "{ PropertyName: " + PropertyName + ", ErrorMessage: " + ErrorMessage + " }";
		}
	}
}