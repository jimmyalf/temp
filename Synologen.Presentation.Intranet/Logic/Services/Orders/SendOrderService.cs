using System;
using System.Web;
using Spinit.Extensions;
using Spinit.Security.Password;
using Spinit.Services.Client;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services.Orders
{
    public class SendOrderService : ISendOrderService
    {
        private readonly EmailClient2 _client;

        public SendOrderService()
        {            
            ClientFactory.SetConfigurtion(ClientFactory.CreateConfiguration(
                    Utility.Business.Globals.SpinitServicesAddress,
                    Utility.Business.Globals.SpinitServicesUserName,
                    Utility.Business.Globals.SpinitServicesPassword,
                    Utility.Business.Globals.SpinitServicesPasswordType,
                    Utility.Business.Globals.SpinitServicesPasswordEncoding)
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
            var from = "order@synologen.nu";
            var body = GetEmailBody(order);
            var subject = "Beställning, " + order.Article.Name;

            return _client.SendMail(to, from, subject, body, EmailPriority.Medium);
        }

        private string GetEmailBody(Order order)
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

            return HttpContext.GetGlobalResourceObject("Templates", "SynologenOrderEmailTemplate").ToString().ReplaceWith(data);
            
        }
    }
}