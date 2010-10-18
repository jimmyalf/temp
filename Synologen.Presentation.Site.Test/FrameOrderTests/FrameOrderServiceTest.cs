using System.Text;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Helpers;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Services;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.FrameOrderTests
{
	[TestFixture]
	public class Given_A_FrameOrderService
	{
		private IFrameOrderService service;
		private Mock<IEmailService> emailService;
		private Mock<ISynologenSettingsService> settingsService;
		private IFrameOrderRepository frameOrderRepository;

		[SetUp]
		public void Context()
		{
			emailService = new Mock<IEmailService>();
			settingsService = new Mock<ISynologenSettingsService>();
			service = new SynologenFrameOrderService(emailService.Object, settingsService.Object);
			frameOrderRepository = Factories.RepositoryFactory.GetFramOrderRepository();
		}

		[Test]
		public void Send_Order_Creates_expected_email()
		{
			//Arrange
			var frameOrder = frameOrderRepository.Get(10);
			const string expectedEmailFrom = "test@abc.se";
			const string expectedEmailTo = "test@abc.se";
			const string expectedSubject = "Testsubject";
			var expectedEmailBody = GetExpectedFrameOrderEmailBody(frameOrder);

			//Act
			settingsService.Setup(x => x.GetFrameOrderEmailBodyTemplate()).Returns(GetFrameOrderEmailBodyTemplate);
			settingsService.SetupGet(x => x.EmailOrderFrom).Returns(expectedEmailFrom);
			settingsService.SetupGet(x => x.EmailOrderSupplierEmail).Returns(expectedEmailTo);
			settingsService.SetupGet(x => x.EmailOrderSubject).Returns(expectedSubject);
			service.SendOrder(frameOrder);

			//Assert
			emailService.Verify(x => x.SendEmail(expectedEmailFrom, expectedEmailTo, expectedSubject, expectedEmailBody));
		}

		private static string GetFrameOrderEmailBodyTemplate() { 
			var builder = new StringBuilder()
				.AppendLine("Best�llnings-id: {OrderId}")
				.AppendLine("Butik: {ShopName}")
				.AppendLine("Butiksort: {ShopCity}")
				.AppendLine("B�ge: {FrameName}")
				.AppendLine("B�ge Artnr: {ArticleNumber}")
				.AppendLine("PD V�nster: {PDLeft}")
				.AppendLine("PD H�ger: {PDRight}")
				.AppendLine("Glastyp: {GlassTypeName}")
				.AppendLine("Sf�r V�nster: {SphereLeft}")
				.AppendLine("Sf�r H�ger: {SphereRight}")
				.AppendLine("Cylinder V�nster: {CylinderLeft}")
				.AppendLine("Cylinder H�ger: {CylinderRight}")
				.AppendLine("Axel V�nster: {AxisLeft}")
				.AppendLine("Axel H�ger: {AxisRight}")
				.AppendLine("Addition V�nster: {AdditionLeft}")
				.AppendLine("Addition H�ger: {AdditionRight}")
				.AppendLine("H�jd V�nster: {HeightLeft}")
				.AppendLine("H�jd H�ger: {HeightRight}")
				.AppendLine("Anteckningar: \r\n{Reference}");
			return builder.ToString();
		
		}

		private static string GetExpectedFrameOrderEmailBody(FrameOrder frameOrder)
		{
			return new StringBuilder()
				.AppendFormatLine("Best�llnings-id: {0}", frameOrder.Id)
				.AppendFormatLine("Butik: {0}", frameOrder.OrderingShop.Name)
				.AppendFormatLine("Butiksort: {0}", frameOrder.OrderingShop.Address.City)
				.AppendFormatLine("B�ge: {0}", frameOrder.Frame.Name)
				.AppendFormatLine("B�ge Artnr: {0}", frameOrder.Frame.ArticleNumber)
				.AppendFormatLine("PD V�nster: {0}", frameOrder.PupillaryDistance.Left)
				.AppendFormatLine("PD H�ger: {0}", frameOrder.PupillaryDistance.Right)
				.AppendFormatLine("Glastyp: {0}", frameOrder.GlassType.Name)
				.AppendFormatLine("Sf�r V�nster: {0}", frameOrder.Sphere.Left)
				.AppendFormatLine("Sf�r H�ger: {0}", frameOrder.Sphere.Right)
				.AppendFormatLine("Cylinder V�nster: {0}", frameOrder.Cylinder.Left)
				.AppendFormatLine("Cylinder H�ger: {0}", frameOrder.Cylinder.Right)
				.AppendFormatLine("Axel V�nster: {0}", frameOrder.Axis.Left)
				.AppendFormatLine("Axel H�ger: {0}", frameOrder.Axis.Right)
				.AppendFormatLine("Addition V�nster: {0}", (frameOrder.Addition != null) ? frameOrder.Addition.Left : null)
				.AppendFormatLine("Addition H�ger: {0}", (frameOrder.Addition != null) ? frameOrder.Addition.Right : null)
				.AppendFormatLine("H�jd V�nster: {0}", (frameOrder.Height != null) ? frameOrder.Height.Left : null)
				.AppendFormatLine("H�jd H�ger: {0}", (frameOrder.Height != null) ? frameOrder.Height.Right : null)
				.AppendFormatLine("Anteckningar: \r\n{0}", frameOrder.Reference)
				.ToString();
		}
		
	}
}