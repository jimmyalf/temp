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
				.AppendLine("Beställnings-id: {OrderId}")
				.AppendLine("Butik: {ShopName}")
				.AppendLine("Butiksort: {ShopCity}")
				.AppendLine("Båge: {FrameName}")
				.AppendLine("Båge Artnr: {ArticleNumber}")
				.AppendLine("PD Vänster: {PDLeft}")
				.AppendLine("PD Höger: {PDRight}")
				.AppendLine("Glastyp: {GlassTypeName}")
				.AppendLine("Sfär Vänster: {SphereLeft}")
				.AppendLine("Sfär Höger: {SphereRight}")
				.AppendLine("Cylinder Vänster: {CylinderLeft}")
				.AppendLine("Cylinder Höger: {CylinderRight}")
				.AppendLine("Axel Vänster: {AxisLeft}")
				.AppendLine("Axel Höger: {AxisRight}")
				.AppendLine("Addition Vänster: {AdditionLeft}")
				.AppendLine("Addition Höger: {AdditionRight}")
				.AppendLine("Höjd Vänster: {HeightLeft}")
				.AppendLine("Höjd Höger: {HeightRight}")
				.AppendLine("Anteckningar: \r\n{Reference}");
			return builder.ToString();
		
		}

		private static string GetExpectedFrameOrderEmailBody(FrameOrder frameOrder)
		{
			return new StringBuilder()
				.AppendFormatLine("Beställnings-id: {0}", frameOrder.Id)
				.AppendFormatLine("Butik: {0}", frameOrder.OrderingShop.Name)
				.AppendFormatLine("Butiksort: {0}", frameOrder.OrderingShop.Address.City)
				.AppendFormatLine("Båge: {0}", frameOrder.Frame.Name)
				.AppendFormatLine("Båge Artnr: {0}", frameOrder.Frame.ArticleNumber)
				.AppendFormatLine("PD Vänster: {0}", frameOrder.PupillaryDistance.Left)
				.AppendFormatLine("PD Höger: {0}", frameOrder.PupillaryDistance.Right)
				.AppendFormatLine("Glastyp: {0}", frameOrder.GlassType.Name)
				.AppendFormatLine("Sfär Vänster: {0}", frameOrder.Sphere.Left)
				.AppendFormatLine("Sfär Höger: {0}", frameOrder.Sphere.Right)
				.AppendFormatLine("Cylinder Vänster: {0}", frameOrder.Cylinder.Left)
				.AppendFormatLine("Cylinder Höger: {0}", frameOrder.Cylinder.Right)
				.AppendFormatLine("Axel Vänster: {0}", frameOrder.Axis.Left)
				.AppendFormatLine("Axel Höger: {0}", frameOrder.Axis.Right)
				.AppendFormatLine("Addition Vänster: {0}", (frameOrder.Addition != null) ? frameOrder.Addition.Left : null)
				.AppendFormatLine("Addition Höger: {0}", (frameOrder.Addition != null) ? frameOrder.Addition.Right : null)
				.AppendFormatLine("Höjd Vänster: {0}", (frameOrder.Height != null) ? frameOrder.Height.Left : null)
				.AppendFormatLine("Höjd Höger: {0}", (frameOrder.Height != null) ? frameOrder.Height.Right : null)
				.AppendFormatLine("Anteckningar: \r\n{0}", frameOrder.Reference)
				.ToString();
		}
		
	}
}