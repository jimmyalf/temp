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
				.AppendLine("Anteckningar: \r\n{Reference}")
				.ToString();
			//Act
			var template = service.GetFrameOrderEmailBodyTemplate();

			//Assert
			Expect(template, Is.EqualTo(expectedTemplate));
		}
	}
}