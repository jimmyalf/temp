using System.Collections.Generic;
using RazorEngine;
using Spinit.Extensions;
using Spinit.Services.Client;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.Service.Client.OrderEmailSender.Application.Extensions;
using Synologen.Service.Client.OrderEmailSender.Model;

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

		public int SendOrdersBatchedByShopAndSupplier(IEnumerable<Order> orderWithShopAndSupplierInCommon, ArticleSupplier supplier)
		{
			var to = supplier.OrderEmailAddress;
			var from = _configurationSettings.OrderSenderEmailAddress;
			var subject = _configurationSettings.OrderEmailSubjectFormat;
			var viewModel = new OrderEmailListModel(orderWithShopAndSupplierInCommon);
			var body = Razor.Parse(RazorTemplate(), viewModel);
			return _client.SendMail(to, from, subject, body, EmailPriority.Medium);
		}

		private int ParseAndSend(Order order)
		{
			var to = order.LensRecipe.Article.Left.ArticleSupplier.OrderEmailAddress;
			var from = _configurationSettings.OrderSenderEmailAddress;
			var body = GetEmailBody(order);
			var subject = _configurationSettings.OrderEmailSubjectFormat;
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
							<tr><th colspan=""8"">Beställnings-id: {OrderId}</th></tr>
							<tr><th colspan=""8"">Från butik: {ShopName}</th></tr>
							<tr><th colspan=""8"">Referens: {Reference}</th></tr>
						</thead>
						<tbody>
							<tr>
								<td>Artikel Höger: {RightArticle}</td>
								<td>Antal Höger: {RightQuantity}</td>
								<td>Styrka Höger: {RightPower}</td>
								<td>Addition Höger: {RightAddition}</td>
								<td>Baskurva Höger: {RightBaseCurve}</td>
								<td>Diameter Höger: {RightDiameter}</td>
								<td>Cylinder Höger: {RightCylinder}</td>
								<td>Axel Höger: {RightAxis}</td>
							</tr>
							<tr>
								<td>Artikel Vänster: {LeftArticle}</td>
								<td>Antal Vänster: {LeftQuantity}</td>
								<td>Styrka Vänster: {LeftPower}</td>
								<td>Addition Vänster: {LeftAddition}</td>
								<td>Baskurva Vänster: {LeftBaseCurve}</td>
								<td>Diameter Vänster: {LeftDiameter}</td>
								<td>Cylinder Vänster: {LeftCylinder}</td>
								<td>Axel Vänster: {LeftAxis}</td>
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

		private static string RazorTemplate()
		{
			return @"
<!DOCTYPE html>
<html lang=""sv"">
	<head>
		<style type=""text/css"">
			html {margin: 0; padding: 0; height: 100%;}
			body {font: 0.875em/1.5em sans-serif; line-height: 1.5em; color: black; margin: 0; padding: 0 5%;}
			p { margin: 0; padding: 0; border: 0; vertical-align: baseline; }
			table {border-collapse: collapse; border-spacing: 0;}
			table.parameters, .parameters th, .parameters td { border: solid 1px #A0A0A0; }
			tbody th { text-align:left; margin: 0px; vertical-align: text-top; }
			tr { margin: 0px; padding 0px; }
			td, th{ padding: 2px 10px; }
		</style>
		<meta charset=""utf-8"">
		<title>Beställning från Synologen</title>
	</head>
	<body>
		<hr/>
	@foreach(var order in Model.Orders){
		<div class=""order"">
			<table class=""order-information"">
				<tbody>
					<tr class=""order-id""><th>Beställnings-id</th><td>@order.OrderId</td></tr>
					<tr class=""shop""><th>Från butik</th><td>@order.ShopName, @order.ShopCity</td></tr>
					<tr class=""customer""><th>Kund</th><td>@order.Customer</td></tr>
					<tr class=""reference""><th>Referens</th><td>@order.Reference</td></tr>
					<tr class=""delivery-address"">
						<th>Leveransadress</th>
						<td>
							<p>@order.DeliveryAddress.Receiver</p>
							<p>@order.DeliveryAddress.AddressLineOne</p>
							<p>@order.DeliveryAddress.AddressLineTwo</p>
							<p>@order.DeliveryAddress.PostalCode @order.DeliveryAddress.City</p>
						</td>
					</tr>
				</tbody>
			</table>
			<br/>
			<table class=""parameters"">
				<thead>
					<tr><th>Parameter</th><th>Höger</th><th>Vänster</th></tr>
				</thead>
				<tbody>
					<tr class=""article"">
						<th>Artikel</th>
						<td class=""right"">@order.Article.Right</td>
						<td class=""left"">@order.Article.Left</td>
					</tr>
					<tr class=""quantity"">
						<th>Antal</th>
						<td class=""right"">@order.Quantity.Right</td>
						<td class=""left"">@order.Quantity.Left</td>
					</tr>
					<tr class=""power"">
						<th>Styrka</th>
						<td class=""right"">@order.Power.Right</td>
						<td class=""left"">@order.Power.Left</td>
					</tr>
					<tr class=""addition"">
						<th>Addition</th>
						<td class=""right"">@order.Addition.Right</td>
						<td class=""left"">@order.Addition.Left</td>
					</tr>
					<tr class=""basecurve"">
						<th>Baskurva</th>
						<td class=""right"">@order.BaseCurve.Right</td>
						<td class=""left"">@order.BaseCurve.Left</td>
					</tr>
					<tr class=""diameter"">
						<th>Diameter</th>
						<td class=""right"">@order.Diameter.Right</td>
						<td class=""left"">@order.Diameter.Left</td>
					</tr>
					<tr class=""diameter"">
						<th>Cylinder</th>
						<td class=""right"">@order.Cylinder.Right</td>
						<td class=""left"">@order.Cylinder.Left</td>
					</tr>
					<tr class=""axis"">
						<th>Axel</th>
						<td class=""right"">@order.Axis.Right</td>
						<td class=""left"">@order.Axis.Left</td>
					</tr>
				</tbody>
			</table>
		</div>
		<hr/>
		}
	</body>
</html>";
		}

	}
}