using Spinit.Extensions;
using Spinit.Services.Client;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.Service.Client.OrderEmailSender.Application.Extensions;

namespace Synologen.Service.Client.OrderEmailSender.Services
{
	public class SendOrderService : ISendOrderService
	{
		private readonly EmailClient2 _client;
		private readonly IConfigurationSettings _configurationSettings;

		public SendOrderService(EmailClient2 client, IConfigurationSettings configurationSettings)
		{
			_client = client;
			_configurationSettings = configurationSettings;
		}

		public int SendOrderByEmail(Order order)
		{
			return ParseAndSend(order);
		}

		private int ParseAndSend(Order order)
		{
			
			var to = order.Article.ArticleSupplier.OrderEmailAddress;
			var from = _configurationSettings.OrderSenderEmailAddress;
			var body = GetEmailBody(order);
			var subject = _configurationSettings.OrderEmailSubjectFormat.ReplaceWith(new
			{
				OrderArticleName = order.Article.Name
			});

			return _client.SendMail(to, from, subject, body, EmailPriority.Medium);
		}

		private static string GetEmailBody(Order order)
		{
			var receiver = order.ShippingType.Equals(OrderShippingOption.ToCustomer) 
				? order.Customer.ParseName(x => x.FirstName, x => x.LastName)
				: order.Shop.Name;
			var addressLineOne = order.ShippingType.Equals(OrderShippingOption.ToCustomer)
				? order.Customer.AddressLineOne
				: order.Shop.AddressLineOne;
			var addressLineTwo = order.ShippingType.Equals(OrderShippingOption.ToCustomer)
				? order.Customer.AddressLineTwo
				: order.Shop.AddressLineTwo;
			var city = order.ShippingType.Equals(OrderShippingOption.ToCustomer)
				? order.Customer.City
				: order.Shop.City;
			var postalCode = order.ShippingType.Equals(OrderShippingOption.ToCustomer)
				? order.Customer.PostalCode
				: order.Shop.PostalCode;


			var data = new 
			{
				OrderId = order.Id,
				Article = order.Article.Name,
				ShopName = order.Shop.Name,

				Receiver = receiver,
				AddressLineOne = addressLineOne,
				AddressLineTwo = addressLineTwo,
				City = city,
				PostalCode = postalCode,
							   
				LeftAddition = order.LensRecipe.Addition.Left,
				LeftAxis = order.LensRecipe.Axis.Left,
				LeftPower = order.LensRecipe.Power.Left,
				LeftCylinder = order.LensRecipe.Cylinder.Left,
				RightAddition = order.LensRecipe.Addition.Left,
				RightAxis = order.LensRecipe.Axis.Right,
				RightPower = order.LensRecipe.Power.Right,
				RightCylinder = order.LensRecipe.Cylinder.Right,
				LeftDiameter = order.LensRecipe.Diameter.Return(x => x.Left.GetStringValue() ,null),
				LeftBaseCurve = order.LensRecipe.BaseCurve.Return(x => x.Left.GetStringValue() ,null),
				RightDiameter = order.LensRecipe.Diameter.Return(x => x.Right.GetStringValue() ,null),
				RightBaseCurve = order.LensRecipe.BaseCurve.Return(x => x.Right.GetStringValue() ,null),
				order.Reference
			};
			return HtmlTemplate().ReplaceWith(data);
			
		}

		private static string HtmlTemplate()
		{
			return @"<html>
			  <head>
				<style type=""text/css"">
					td { padding-right: 1em; }
					body { padding:0; margin:0; font: 0.7em Verdana, Arial, Helvetica, sans-serif; } 
					table {font-size: 1.0em;}
				</style>
			  </head>
				<body>
				<table>
					<tr><td colspan=""6"">Beställnings-id: {OrderId}</td></tr>
					<tr><td colspan=""6"">Från butik: {ShopName}</td></tr>
					<tr><td colspan=""6"">Artikel: {Article}</td></tr>
					<tr><td colspan=""6"">Referens: {Reference}</td></tr>
					<tr class=""spacer-row""><td colspan=""6""/></tr>
					<tr>
						<td>Styrka Höger: {RightPower}</td>
						<td>Addition Höger: {RightAddition}</td>
						<td>Baskurva Höger: {RightBaseCurve}</td>
						<td>Diameter Höger: {RightDiameter}</td>
						<td>Cylinder Höger: {RightCylinder}</td>
						<td>Axel Höger: {RightAxis}</td>
					</tr>
					<tr>
						<td>Styrka Vänster: {LeftPower}</td>
						<td>Addition Vänster: {LeftAddition}</td>
						<td>Baskurva Vänster: {LeftBaseCurve}</td>
						<td>Diameter Vänster: {LeftDiameter}</td>
						<td>Cylinder Vänster: {LeftCylinder}</td>
						<td>Axel Vänster: {LeftAxis}</td>
					</tr>
					<tr class=""spacer-row""><td colspan=""6""/></tr>
					<tr><td colspan=""6"">Leveransadress:</td></tr>
					<tr><td colspan=""6"">{Receiver}</td></tr>
					<tr><td colspan=""6"">{AddressLineOne}</td></tr>
					<tr><td colspan=""6"">{AddressLineTwo}</td></tr>
					<tr><td colspan=""6"">{PostalCode}</td></tr>
					<tr><td colspan=""6"">{City}</td></tr>
				</table>
				</body>
			</html>";
		}

	}
}