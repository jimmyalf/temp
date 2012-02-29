using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Models.Order;

namespace Spinit.Wpc.Synologen.Presentation.Application.Extensions
{
	public static class ArticleOptionsExtensions
	{
		public static SequenceDefinitionView GetDefinitionView(this ArticleOptions options, Func<ArticleOptions, SequenceDefinition> sequenceProperty)
		{
			if(options == null) return new SequenceDefinitionView();
			var sequence = sequenceProperty(options);
			return sequence == null ? new SequenceDefinitionView() : new SequenceDefinitionView(sequence);
		}
	}
}