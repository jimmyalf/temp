using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers;
using Spinit.Wpc.Synologen.Test.Data;
using StructureMap;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest
{
	//[TestFixture, Explicit]
	//public class ManualDataManagement
	//{
	//    private ISession _session;
	//    private Article _article;
	//    private OrderCustomer _customerOne;
	//    private OrderCustomer _customerTwo;
	//    private DataManager _dataManager;
	//    private Shop _shop1, _shop2;
	//    private ArticleCategory _category1, _category2;
	//    private ArticleType _articleType1, _articleType2;
	//    private ArticleSupplier _supplier1, _supplier2;

	//    [SetUp]
	//    public void Context()
	//    {
	//        _dataManager = new DataManager();
	//        _dataManager.CleanTables();
	//        _session = ObjectFactory.GetInstance<ISession>();
	//        _category1 = OrderFactory.GetCategory("Linser").StoreWith(_session);
	//        _category2 = OrderFactory.GetCategory("Bågar").StoreWith(_session);
	//        _articleType1 = OrderFactory.GetArticleType(_category1, name: "Endagslinser").StoreWith(_session);
	//        _articleType2 = OrderFactory.GetArticleType(_category2, name: "Månadslinser").StoreWith(_session);
	//        _supplier1 = OrderFactory.GetSupplier(name: "Johnsson & McBeth").StoreWith(_session);
	//        _supplier2 = OrderFactory.GetSupplier(name: "Ciba vision").StoreWith(_session);
	//        _shop1 = CreateShop(_session, "Testbutik A");
	//        _shop2 = CreateShop(_session, "Testbutik B");
	//        _article = OrderFactory.GetArticle(_articleType1, _supplier1).StoreWith(_session);
	//        _customerOne = OrderFactory.GetCustomer(_shop1).StoreWith(_session);
	//        _customerTwo = OrderFactory.GetCustomer(_shop2, firstName: "Börje", lastName: "Svensson").StoreWith(_session);
	//    }

	//    [Test]
	//    public void Add_orders()
	//    {
	//        SystemTime.InvokeWhileTimeIs(new DateTime(2012,01,10),() => 
	//            OrderFactory.GetOrders(_shop1, _article, _customerOne).StoreItemsWith(_session));
	//        SystemTime.InvokeWhileTimeIs(new DateTime(2012, 01, 15), () =>
	//            OrderFactory.GetOrders(_shop2, _article, _customerTwo).StoreItemsWith(_session));
	//    }

	//    [Test]
	//    public void Add_article_categories()
	//    {
	//        OrderFactory.GetCategories().StoreItemsWith(_session);
	//    }

	//    [Test]
	//    public void Add_article_suppliers()
	//    {
	//        OrderFactory.GetSuppliers().StoreItemsWith(_session);
	//    }

	//    [Test]
	//    public void Add_article_types()
	//    {
	//        OrderFactory.GetArticleTypes(_category1).StoreItemsWith(_session);
	//        OrderFactory.GetArticleTypes(_category2).StoreItemsWith(_session);
	//    }

	//    [Test]
	//    public void Add_articles()
	//    {
	//        OrderFactory.GetArticles(_articleType1, _supplier1).StoreItemsWith(_session);
	//        OrderFactory.GetArticles(_articleType2, _supplier2).StoreItemsWith(_session);
	//    }

	//    protected Shop CreateShop(ISession session, string shopName = "Testbutik")
	//    {
	//        var shop = _dataManager.CreateShop(shopName: shopName);
	//        return session.Get<Shop>(shop.ShopId);
	//    }

	//}

	//public static class HelperExtensions
	//{
	//    public static TType StoreWith<TType>(this TType item, ISession session)
	//    {
	//        session.Save(item);
	//        return item;
	//    }

	//    public static IEnumerable<TType> StoreItemsWith<TType>(this IEnumerable<TType> items, ISession session)
	//    {
	//        var list = items.ToList();
	//        foreach (var item in list)
	//        {
	//            session.Save(item);	
	//        }
	//        return list;
	//    }
	//}



	//public class Error
	//{
	//    public string GetErrorMessage()
	//    {
	//        return "test";
	//    }
	//}
}
