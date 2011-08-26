using AutoMapper;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Application.AutomapperMappings
{
	public class CustomerNameValueResolver : ValueResolver<Customer, string>
	{
		protected override string ResolveCore(Customer source) 
		{
			return source.ParseName(x => x.FirstName, x => x.LastName);
		}
	}
}