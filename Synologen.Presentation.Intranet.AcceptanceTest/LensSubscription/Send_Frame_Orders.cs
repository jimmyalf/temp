using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spinit.Security.Password;
using Spinit.Services.Client;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data.Repositories.FrameOrderRepositories;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.LensSubscription
{
	[TestFixture, Explicit]
	public class Manual_Test
	{
		private IEmailService _emailService;
		private SynologenFrameOrderService _orderService;
		private ISynologenSettingsService _settingsService;
		private IFrameOrderRepository _framOrderRepository;

		public Manual_Test()
		{
			_emailService = new CustomEmailService();
			_settingsService = new CustomSettingsService();
			_orderService = new SynologenFrameOrderService(_emailService, _settingsService);
			NHibernateFactory.MappingAssemblies.Add(typeof(FrameOrderRepository).Assembly);
			var sessionFactory = NHibernateFactory.Instance.GetSessionFactory();
			var session = sessionFactory.OpenSession();
			_framOrderRepository = new FrameOrderRepository(session);
		}
        /*
		[Test]
		public void TestSingleOrder()
		{
			var orderList = new List<FrameOrder>();
			for(var i=1740;i<=1847;i++)
			{
				var order = _framOrderRepository.GetOld(i);
				if(!order.Sent.HasValue) continue;
				orderList.Add(order);
			}

			foreach (var frameOrder in orderList.Take(5))
			{
				Console.Write("Sending order: {0}", frameOrder.Id);
				_orderService.SendOrder(frameOrder);
			}
			Console.WriteLine("Done!");
		}
         * */
	}

	public class CustomSettingsService : ISynologenSettingsService
	{
		public Interval Addition
		{
			get
			{	
				return new Interval  { Increment = 0.25M, Max = 3, Min = 1 };
			}
		}
		public Interval Height
		{
			get
			{	
				return new Interval 
				{
					Increment = 1, Max = 28, Min = 10
				};
			}
		}

		public string EmailOrderSupplierEmail
		{
			get { return "carl.berg@spinit.se"; }
		}

		public string EmailOrderFrom
		{
			get { return "info@synologen.se"; }
		}

		public string EmailOrderSubject
		{
			get { return "Glasögonbeställning från synologen"; }
		}

		public int SubscriptionCutoffDate
		{
			get { throw new NotImplementedException(); }
		}

		public int SubscriptionWithdrawalDate
		{
			get { throw new NotImplementedException(); }
		}

		public string GetFrameOrderEmailBodyTemplate() {
			 return @"
<html> 
  <head>
    <style type=""text/css"">
		td { padding-right: 1em; }
		body { padding:0; margin:0; font: 0.7em Verdana, Arial, Helvetica, sans-serif; } 
		table {font-size: 1.0em;}
	</style>
  </head>
	<body>
	<table>
		<tr><td colspan=""3"">Beställnings-id: {OrderId}</td></tr>
		<tr><td colspan=""3"">Butik: {ShopName}</td></tr>
		<tr><td colspan=""3"">Butiksort: {ShopCity}</td></tr>
		<tr><td colspan=""3"">Båge: {FrameName}</td></tr>
		<tr><td colspan=""3"">Båge Artnr: {ArticleNumber}</td></tr>
		<tr><td colspan=""3"">Glastyp: {GlassTypeName}</td></tr>
		<tr class=""spacer-row""><td colspan=""3""/></tr>
		<tr><td>Sfär Höger: {SphereRight}</td><td>Cylinder Höger: {CylinderRight}</td><td>Axel Höger: {AxisRight}</td></tr>
		<tr><td>Sfär Vänster: {SphereLeft}</td><td>Cylinder Vänster: {CylinderLeft}</td><td>Axel Vänster: {AxisLeft}</td></tr>
		<tr class=""spacer-row""><td colspan=""3""/></tr>
		<tr><td>PD Höger: {PDRight}</td><td>Höjd Höger: {HeightRight}</td><td>Addition Höger: {AdditionRight}</td></tr>
		<tr><td>PD Vänster: {PDLeft}</td><td>Höjd Vänster: {HeightLeft}</td><td>Addition Vänster: {AdditionLeft}</td></tr>
		<tr class=""spacer-row""><td colspan=""3""/></tr>
		<tr><td colspan=""3"">Referens:<br/>{Reference}</td></tr>
	</table>
	</body>
</html>";
		}
	}

	public class CustomEmailService : IEmailService
	{
		private readonly EmailClient2 _client;

		public CustomEmailService()
		{
					
			ClientFactory.SetConfigurtion (ClientFactory.CreateConfiguration(
					"http://services.spinit.se",
					"SynologenSendUser",
					"yM-28iB",
					PasswordEncryptionType.Sha1,
					"Utf-8")
			);
			_client = ClientFactory.CreateEmail2Client();
		}

		public void SendEmail(string @from, string to, string subject, string body)
		{
			var response = _client.SendMail(to, @from, subject, body, EmailPriority.Medium);
			Console.Write("\tMail service id: {0}\r\n", response);
		}
	}
}
