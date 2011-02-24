using System.Text;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Services;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.FrameOrderTests
{
	[TestFixture, Category("SynologenSettingsServiceTests")]
	public class Given_A_SynologenSettingsService : AssertionHelper
	{
		private ISynologenSettingsService service;

		[SetUp]
		public void Context()
		{
			service = new SynologenSettingsService();
		}

		public void Test()
		{
			//Arrange
			var expectedTemplate = new StringBuilder()
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
				.AppendLine("Anteckningar: \r\n{Reference}")
				.ToString();
			//Act
			var template = service.GetFrameOrderEmailBodyTemplate();

			//Assert
			Expect(template, Is.EqualTo(expectedTemplate));
		}
	}
}