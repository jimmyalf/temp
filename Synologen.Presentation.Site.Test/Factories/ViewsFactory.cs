using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.Factories
{
	public static class ViewsFactory
	{
		
		public static IEditFrameOrderView<EditFrameOrderModel> GetFrameOrderView()
		{
			return new MockedEditFrameOrderView();
		}

		internal class MockedEditFrameOrderView : IEditFrameOrderView<EditFrameOrderModel>
		{
			public event EventHandler Load;
			public EditFrameOrderModel Model { get; set; }
			public event EventHandler<EditFrameFormEventArgs> FrameSelected;
			public event EventHandler<EditFrameFormEventArgs> SubmitForm;
			public event EventHandler<EditFrameFormEventArgs> GlassTypeSelected;
			public int RedirectPageId { get; set; }
		}

		public static IViewFrameOrderView<ViewFrameOrderModel> GetViewFrameOrderView() {
			return new MockedViewFrameOrderView();
		}
	}

	public class MockedViewFrameOrderView : IViewFrameOrderView<ViewFrameOrderModel> {
		public event EventHandler Load;
		public event EventHandler SendOrder;
		public ViewFrameOrderModel Model { get; set; }
		public int RedirectAfterSentOrderPageId { get; set; }
		public int EditPageId { get; set; }
	}
}