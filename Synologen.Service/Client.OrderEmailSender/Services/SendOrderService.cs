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
			
			var to = order.LensRecipe.Article.Left.ArticleSupplier.OrderEmailAddress;
			var from = _configurationSettings.OrderSenderEmailAddress;
			var body = GetEmailBody(order);
			var subject = _configurationSettings.OrderEmailSubjectFormat;
			//var subject = _configurationSettings.OrderEmailSubjectFormat.ReplaceWith(new
			//{
			//    OrderArticleName = order.Article.Name
			//});

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
				LeftArticle = order.LensRecipe.Article.Right.Name,
				RightArticle = order.LensRecipe.Article.Right.Name,
				LeftQuantity = order.LensRecipe.Quantity.Left,
				RightQuantity = order.LensRecipe.Quantity.Right,
				order.Reference,
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
						<thead>
							<tr><th colspan=""8"">Best�llnings-id: {OrderId}</th></tr>
							<tr><th colspan=""8"">Fr�n butik: {ShopName}</th></tr>
							<tr><th colspan=""8"">Referens: {Reference}</th></tr>
						</thead>
						<tbody>
							<tr>
								<td>Artikel H�ger: {RightArticle}</td>
								<td>Antal H�ger: {RightQuantity}</td>
								<td>Styrka H�ger: {RightPower}</td>
								<td>Addition H�ger: {RightAddition}</td>
								<td>Baskurva H�ger: {RightBaseCurve}</td>
								<td>Diameter H�ger: {RightDiameter}</td>
								<td>Cylinder H�ger: {RightCylinder}</td>
								<td>Axel H�ger: {RightAxis}</td>
							</tr>
							<tr>
								<td>Artikel V�nster: {LeftArticle}</td>
								<td>Antal V�nster: {LeftQuantity}</td>
								<td>Styrka V�nster: {LeftPower}</td>
								<td>Addition V�nster: {LeftAddition}</td>
								<td>Baskurva V�nster: {LeftBaseCurve}</td>
								<td>Diameter V�nster: {LeftDiameter}</td>
								<td>Cylinder V�nster: {LeftCylinder}</td>
								<td>Axel V�nster: {LeftAxis}</td>
							</tr>
							<tr><td colspan=""8"">Leveransadress:</td></tr>
							<tr><td colspan=""8"">{Receiver}</td></tr>
							<tr><td colspan=""8"">{AddressLineOne}</td></tr>
							<tr><td colspan=""8"">{AddressLineTwo}</td></tr>
							<tr><td colspan=""8"">{PostalCode}</td></tr>
							<tr><td colspan=""8"">{City}</td></tr>
						</tbody>
					</table>
				</body>
			</html>";
		}

	}
}