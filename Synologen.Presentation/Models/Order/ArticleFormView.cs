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
			BaseCurve = new SequenceDefinitionView();
			Diameter = new SequenceDefinitionView();
			Active = true;
			EnableAddition = true;
			EnableAxis = true;
			EnableCylinder = true;
		}

		public ArticleFormView(IEnumerable<ArticleSupplier> suppliers, IEnumerable<ArticleType> types, int? id, Article article) : base(id)
		{
			Name = article.Name;
			SetSuppliers(suppliers);
			SetTypes(types);
			BaseCurve = article.Options.GetDefinitionView(x => x.BaseCurve);
			Diameter = article.Options.GetDefinitionView(x => x.Diameter);
			SupplierId = article.With(x => x.ArticleSupplier).Return(x => x.Id, default(int));
			TypeId = article.With(x => x.ArticleType).Return(x => x.Id, default(int));
			Active = article.Active;
			EnableAddition = article.With(x => x.Options).Return(x => x.EnableAddition, true);
			EnableAxis = article.With(x => x.Options).Return(x => x.EnableAxis, true);
			EnableCylinder = article.With(x => x.Options).Return(x => x.EnableCylinder, true);
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

		[DisplayName("Använd Addition")]
		public bool EnableAddition { get; set; }

		[DisplayName("Använd Axel")]
		public bool EnableAxis { get; set; }

		[DisplayName("Använd Cylinder")]
		public bool EnableCylinder { get; set; }


		[DisplayName("Diameter")]
		public SequenceDefinitionView Diameter { get; set; }

		[DisplayName("Baskurva")]
		public SequenceDefinitionView BaseCurve { get; set; }


		[DisplayName("Leverantör"), Required]
		public int SupplierId { get; set; }

		[DisplayName("Artikeltyp"), Required]
		public int TypeId { get; set; }

		public SelectList Suppliers { get; set; }
		public SelectList Types { get; set; }

		public bool HasCustomValidationErrors
		{
			get { return GetCustomValidationErrors().Any(); }
		}

		public IEnumerable<ValidationError> GetCustomValidationErrors()
		{
			foreach (var error in this.GetValidationErrors(x => x.Diameter))
			{
				yield return error;
			}
			foreach (var error in this.GetValidationErrors(x => x.BaseCurve))
			{
				yield return error;
			}
		}

		public ArticleOptions GetArticleOptions()
		{
			return new ArticleOptions
			{
				BaseCurve = BaseCurve.GetSequenceDefinition(),
				Diameter = Diameter.GetSequenceDefinition(),
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
		public decimal? Min { get; set; }
		[DisplayName("Max"), RegularExpression("^(-)?[0-9]+(,[0-9]+)?$", ErrorMessage = "Angivet värde måste vara numeriskt")]
		public decimal? Max { get; set; }
		[DisplayName("Inkrement"), RegularExpression("^(-)?[0-9]+(,[0-9]+)?$", ErrorMessage = "Angivet värde måste vara numeriskt")]
		public decimal? Increment { get; set; }

		public IEnumerable<ValidationError> GetValidationErrors(string propertyName)
		{
			if (!Min.HasValue) yield return new ValidationError(propertyName + ".Min", "Min saknar värde");
			if (!Max.HasValue) yield return new ValidationError(propertyName + ".Max", "Max saknar värde");
			if (!Increment.HasValue) yield return new ValidationError(propertyName + ".Increment", "Inkrement saknar värde");
			if (Increment.HasValue && Increment < 0) yield return new ValidationError(propertyName + ".Increment", "Inkrement måste anges som noll eller större");
			if(Min.HasValue && Max.HasValue && Min.Value > Max.Value)
			{
			    yield return new ValidationError(propertyName + ".Min", "Min måste vara mindre eller lika med Max");
			}
			if(Min.HasValue && Max.HasValue && Min.Value == Max.Value && Increment.HasValue && Increment.Value != 0)
			{
			    yield return new ValidationError(propertyName + ".Increment", "Om Min = Max, måste Inkrement anges som 0");
			}
		}

		public SequenceDefinition GetSequenceDefinition(decimal defaultValue = default(decimal))
		{
			return new SequenceDefinition 
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

	public class ValidationError<TType> : ValidationError where TType : class
	{
		public ValidationError(Expression<Func<TType,object>> expression, string errorMessage) : base(expression.GetName(), errorMessage){}
	}
}