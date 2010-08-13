using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;

namespace Synologen.Presentation.Site.Test.Factories
{
	public static class ViewsFactory
	{
		
		public static IFrameOrderView<FrameOrderModel> GetFrameOrderView()
		{
			return new MockedFrameOrderView();
		}

		internal class MockedFrameOrderView : IFrameOrderView<FrameOrderModel>
		{
			public event EventHandler Load;
			public FrameOrderModel Model { get; set; }
			public event EventHandler<FrameSelectedEventArgs> FrameSelected;
			public event EventHandler<FrameOrderFormSubmitEventArgs> SubmitForm;
		}
	}
}