using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Application.AutomapperMappings
{
	public class SubscriptionErrorValueResolver : ValueResolver<IEnumerable<SubscriptionError>, IEnumerable<ErrorListItemView>>
	{
		protected override IEnumerable<ErrorListItemView> ResolveCore(IEnumerable<SubscriptionError> source)
		{
			Func<SubscriptionError, ErrorListItemView> converter = transaction => new ErrorListItemView
          	{
				Type = transaction.Type.GetEnumDisplayName(),
				CreatedDate = transaction.CreatedDate.ToString("yyyy-MM-dd"),
				HandledDate = transaction.HandledDate.HasValue ? transaction.HandledDate.Value.ToString("yyyy-MM-dd") : String.Empty
			};
			return source.Select(converter);
		}

	}
}
