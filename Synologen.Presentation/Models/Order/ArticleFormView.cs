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
			Addition = new OptionalSequenceDefinitionView();
			Axis = new OptionalSequenceDefinitionView();
			BaseCurve = new SequenceDefinitionView();
			Cylinder = new OptionalSequenceDefinitionView();
			Diameter = new SequenceDefinitionView();
			Power = new SequenceDefinitionView();
			Active = true;
		}

		public ArticleFormView(IEnumerable<ArticleSupplier> suppliers, IEnumerable<ArticleType> types, int? id, Article article) : base(id)
		{
			Name = article.Name;
			SetSuppliers(suppliers);
			SetTypes(types);
			Addition = article.Options.GetOptinalDefinitionView(x => x.Addition);
			Axis = article.Options.GetOptinalDefinitionView(x => x.Axis);
			BaseCurve = article.Options.GetDefinitionView(x => x.BaseCurve);
			Cylinder = article.Options.GetOptinalDefinitionView(x => x.Cylinder);
			Diameter = article.Options.GetDefinitionView(x => x.Diameter);
			Power = article.Options.GetDefinitionView(x => x.Power);
			SupplierId = article.With(x => x.ArticleSupplier).Return(x => x.Id, default(int));
			TypeId = article.With(x => x.ArticleType).Return(x => x.Id, default(int));
			Active = article.Active;
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

		[DisplayName("Aktiv")]
		public bool Active { get; set; }

		[DisplayName("Namn"), Required]
		public string Name { get; set; }

		[DisplayName("Styrka")]
		public SequenceDefinitionView Power { get; set; }

		[DisplayName("Diameter")]
		public SequenceDefinitionView Diameter { get; set; }

		[DisplayName("Baskurva")]
		public SequenceDefinitionView BaseCurve { get; set; }

		[DisplayName("Axel")]
		public OptionalSequenceDefinitionView Axis { get; set; }

		[DisplayName("Cylinder")]
		public OptionalSequenceDefinitionView Cylinder { get; set; }

		[DisplayName("Addition")]
		public OptionalSequenceDefinitionView Addition { get; set; }

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

		public ArticleOptions GetArticleOptions()
		{
			return new ArticleOptions
			{
				Addition = Addition.GetSequenceDefinition(),
				Axis = Axis.GetSequenceDefinition(),
				BaseCurve = BaseCurve.GetSequenceDefinition(),
				Cylinder = Cylinder.GetSequenceDefinition(),
				Diameter = Diameter.GetSequenceDefinition(),
				Power = Power.GetSequenceDefinition()
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

		public IEnumerable<ValidationError> GetValidationErrors(string propertyName)
		{
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
			return new SequenceDefinition 
			{
				Min =  Min?? defaultValue,
				Max = Max ?? defaultValue,
				Increment = Increment ?? defaultValue
			};
		}
	}


	public class OptionalSequenceDefinitionView
	{
		public OptionalSequenceDefinitionView() { }
		public OptionalSequenceDefinitionView(OptionalSequenceDefinition definition)
		{
			if(definition == null) return;
			Min = definition.Min;
			Max = definition.Max;
			Increment = definition.Increment;
			Disable = definition.DisableDefinition;
		}

		[DisplayName("Min"), RegularExpression("^(-)?[0-9]+(,[0-9]+)?$", ErrorMessage = "Angivet värde måste vara numeriskt")]
		public float? Min { get; set; }
		[DisplayName("Max"), RegularExpression("^(-)?[0-9]+(,[0-9]+)?$", ErrorMessage = "Angivet värde måste vara numeriskt")]
		public float? Max { get; set; }
		[DisplayName("Inkrement"), RegularExpression("^(-)?[0-9]+(,[0-9]+)?$", ErrorMessage = "Angivet värde måste vara numeriskt")]
		public float? Increment { get; set; }
		[DisplayName("Avaktivera")]
		public bool Disable { get; set; }

		public IEnumerable<ValidationError> GetValidationErrors(string propertyName, bool allowAllNull = false)
		{
			if (Disable) yield break;
			if (!Min.HasValue) yield return new ValidationError(propertyName + ".Min", "Min saknar värde");
			if (!Max.HasValue) yield return new ValidationError(propertyName + ".Max", "Max saknar värde");
			if (!Increment.HasValue) yield return new ValidationError(propertyName + ".Increment", "Inkrement saknar värde");
			if (Increment.HasValue && Increment <= 0) yield return new ValidationError(propertyName + ".Increment", "Inkrement måste anges större än 0");
			if(Min.HasValue && Max.HasValue && Min.Value >= Max.Value)
			{
			    yield return new ValidationError(propertyName + ".Min", "Min måste vara mindre än Max");
			}
		}

		public OptionalSequenceDefinition GetSequenceDefinition(float? defaultValue = null)
		{
			return new OptionalSequenceDefinition {
				Min =  Min?? defaultValue,
				Max = Max ?? defaultValue,
				Increment = Increment ?? defaultValue,
				DisableDefinition = Disable
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

	public class ValidationError<TType> : ValidationError where TType : class
	{
		public ValidationError(Expression<Func<TType,object>> expression, string errorMessage) : base(expression.GetName(), errorMessage){}
	}
}