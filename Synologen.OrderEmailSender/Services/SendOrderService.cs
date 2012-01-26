using System;
using System.Configuration;
using Spinit.Extensions;
using Spinit.Security.Password;
using Spinit.Services.Client;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.OrderEmailSender.Services
{
    public class SendOrderService : ISendOrderService
    {
        private readonly EmailClient2 _client;

        public SendOrderService()
        {
            var user = ConfigurationSettings.AppSettings["SpinitSendUser"];
            var password = ConfigurationSettings.AppSettings["Password"];
            var passwordEncoding = ConfigurationSettings.AppSettings["PasswordEncoding"];
            var server = ConfigurationSettings.AppSettings["ServerAddress"];

            ClientFactory.SetConfigurtion(ClientFactory.CreateConfiguration(
                    server,
                    user,
                    password,
                    PasswordEncryptionType.Sha1,
                    passwordEncoding
                    )
            );
            _client = ClientFactory.CreateEmail2Client();
        }
        
        public int SendOrderByEmail(Order order)
        {
            return ParseAndSend(order);
        }

        private int ParseAndSend(Order order)
        {
            
            var to = order.Article.ArticleSupplier.OrderEmailAddress;
            const string from = "order@synologen.nu";
            var body = GetEmailBody(order);
            var subject = "Beställning, " + order.Article.Name;

            return _client.SendMail(to, from, subject, body, EmailPriority.Medium);
        }

        private static string GetEmailBody(Order order)
        {
            var receiver = order.ShippingType.Equals(OrderShippingOption.ToCustomer) ? String.Format("{0} {1}", order.Customer.FirstName, order.Customer.LastName) : order.Shop.Name;
            var addressLineOne = order.ShippingType.Equals(OrderShippingOption.ToCustomer)
                                     ? order.Customer.AddressLineOne
                                     : order.Shop.AddressLineOne;
            var addressLineTwo = order.ShippingType.Equals(OrderShippingOption.ToCustomer)
                                     ? order.Customer.AddressLineTwo ?? ""
                                     : order.Shop.AddressLineTwo ?? "";
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
                               
                               LeftAddition = order.LensRecipe.Addition.Left.HasValue ? order.LensRecipe.Addition.Left.Value.ToString() : "",
                               LeftAxis = order.LensRecipe.Axis.Left.HasValue ? order.LensRecipe.Axis.Left.Value.ToString() : "",
                               LeftPower = order.LensRecipe.Power.Left.HasValue ? order.LensRecipe.Power.Left.Value.ToString() : "",
                               LeftCylinder = order.LensRecipe.Cylinder.Left.HasValue ? order.LensRecipe.Cylinder.Left.Value.ToString() : "",
                               LeftDiameter = order.LensRecipe.Diameter.Left.HasValue ? order.LensRecipe.Diameter.Left.Value.ToString() : "",
                               LeftBaseCurve = order.LensRecipe.BaseCurve.Left.HasValue ? order.LensRecipe.BaseCurve.Left.Value.ToString() : "",
                               RightAddition = order.LensRecipe.Addition.Right.HasValue ? order.LensRecipe.Addition.Right.Value.ToString() : "",
                               RightAxis = order.LensRecipe.Axis.Right.HasValue ? order.LensRecipe.Axis.Right.Value.ToString() : "",
                               RightPower = order.LensRecipe.Power.Right.HasValue ? order.LensRecipe.Power.Right.Value.ToString() : "",
                               RightCylinder = order.LensRecipe.Cylinder.Right.HasValue ? order.LensRecipe.Cylinder.Right.Value.ToString() : "",
                               RightDiameter = order.LensRecipe.Diameter.Right.HasValue ? order.LensRecipe.Diameter.Right.Value.ToString() : "",
                               RightBaseCurve = order.LensRecipe.BaseCurve.Right.HasValue ? order.LensRecipe.BaseCurve.Right.Value.ToString() : "",
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