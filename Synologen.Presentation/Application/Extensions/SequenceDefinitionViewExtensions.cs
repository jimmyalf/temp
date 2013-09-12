using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.Order;

namespace Spinit.Wpc.Synologen.Presentation.Application.Extensions
{
	public static class SequenceDefinitionViewExtensions
	{
		public static IEnumerable<ValidationError> GetValidationErrors<TType>(this TType input, Expression<Func<TType,SequenceDefinitionView>> propertyExpression) where TType : class
		{
			var sequence = propertyExpression.Compile()(input);
			if(sequence == null) throw new ApplicationException("Sequence was not valid");
			var propertyName = propertyExpression.GetName();
			return sequence.GetValidationErrors(propertyName);
		}
	}
}