using System.Diagnostics;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Events.Synologen;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public class TestEventListener : IListener<ShopWasConnectedToShopGroupEvent>
	{
		public void Handle(ShopWasConnectedToShopGroupEvent message)
		{
			Debug.WriteLine(message);
		}
	}
}