using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.Orders
{
    public static class OrderFactory
    {
        public static IEnumerable<ArticleCategory> GetCategories()
        {
            return Sequence.Generate(GetCategory, 15);
        }

        public static ArticleCategory GetCategory()
        {
            return new ArticleCategory { Name = "Artikel 1" };
        }

        public static OrderCustomer GetCustomer()
        {
            return new OrderCustomer { AddressLineOne = "Box 1234", AddressLineTwo = "Datavägen 23", City = "Mölndal", Email = "adam.b@testbolaget.se", FirstName = "Bertil", LastName = "Adamsson", MobilePhone = "0701-987654", Notes = "Anteckningar ABC DEF", PersonalIdNumber = "197001013239", Phone = "031123456", PostalCode = "41300", };
        }

        public static IEnumerable<ArticleSupplier> GetSuppliers()
        {
            return Sequence.Generate(() => GetSupplier(), 20);
        }

        public static ArticleSupplier GetSupplier(int id = 6, OrderShippingOption shippingOptions = OrderShippingOption.DeliveredInStore | OrderShippingOption.ToCustomer | OrderShippingOption.ToStore)
        {
            var fakeSupplier = A.Fake<ArticleSupplier>();
            A.CallTo(() => fakeSupplier.Id).Returns(id);
            fakeSupplier.Name = "Leverantör ABC";
            fakeSupplier.ShippingOptions = shippingOptions;
            return fakeSupplier;
        }

        public static IEnumerable<ArticleType> GetArticleTypes()
        {
            return Sequence.Generate(GetArticleType, 15);
        }

        public static ArticleType GetArticleType()
        {
            return new ArticleType { Name = "Endagslinser" };
        }

        public static IEnumerable<Article> GetArticles()
        {
            return Sequence.Generate(GetArticle, 15);
        }
        public static Article GetArticle(int id = 2)
        {
            var fakeOrderArticle = A.Fake<Article>();
            A.CallTo(() => fakeOrderArticle.Id).Returns(id);
            fakeOrderArticle.Name = "Lins 1337";
            fakeOrderArticle.Options = new ArticleOptions
            {

                Axis =
                    {
                        Increment = 0.25F,
                        Max = 2.25F,
                        Min = -1.25F,
                    },
                BaseCurve =
                    {
                        Increment = 0.5F,
                        Max = 2.5F,
                        Min = -1.5F,
                    },
                Cylinder =
                    {
                        Increment = 0.25F,
                        Max = 8.25F,
                        Min = -5.25F,
                    },
                Diameter =
                    {
                        Increment = 0.25F,
                        Max = 2.25F,
                        Min = -1.25F,
                    },
                Power =
                    {
                        Increment = 0.25F,
                        Max = 7F,
                        Min = -7F,
                    }
            };
            
            return fakeOrderArticle;
        }

        public static IEnumerable<float> GetOptionsList(float min, float max, float increment)
        {
            for (float value = min; value <= max; value += increment)
            {
                yield return value;
            }
            yield break;
        }
    }
}