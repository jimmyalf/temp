using Moq;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.MockHelpers
{
	public static class MvpHelpers
	{
		public static Mock<TView> GetMockedView<TView,TModel>() 
			where TView : class, IView<TModel> 
			where TModel : class, new() 
		{
			var mockedView = new Mock<TView>();
			var mockedModel = new Mock<TModel>();
			mockedView.SetupGet(x => x.Model).Returns(mockedModel.Object);
			mockedView.SetupAllProperties();
			return mockedView;
		}
	}
}