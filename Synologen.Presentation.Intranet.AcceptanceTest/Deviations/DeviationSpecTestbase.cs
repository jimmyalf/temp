using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Deviations
{
    public abstract class DeviationSpecTestbase<TPresenter, TView> : SpecTestbase<TPresenter, TView>
        where TPresenter : Presenter<TView>
        where TView : class, IView
    {
        //protected Order CreateOrder(Shop shop, OrderCustomer customer = null)
        //{
        //    customer = customer ?? CreateCustomer(shop);
        //    return CreateWithRepository<IOrderRepository, Order>(() => OrderFactory.GetOrder(shop, customer));
        //}

        //protected Order CreateOrderWithSubscription(Shop shop, Article article = null, OrderCustomer customer = null, PaymentOptionType paymentOptionType = PaymentOptionType.Subscription_Autogiro_New, bool useOngoingSubscription = false)
        //{
        //    article = article ?? CreateArticle();
        //    customer = customer ?? CreateCustomer(shop);
        //    var lensRecipe = CreateLensRecipe(article);
        //    var subscription = CreateSubscription(shop, active: paymentOptionType == PaymentOptionType.Subscription_Autogiro_Existing);
        //    var subscriptionItem = CreateSubscriptionItem(subscription, useOngoingSubscription);

        //    return CreateWithRepository<IOrderRepository, Order>(() => OrderFactory.GetOrder(shop, customer, lensRecipe, subscriptionItem, paymentOptionType));
        //}

        //private LensRecipe CreateLensRecipe(Article article, ArticleCategory category = null, ArticleType articleType = null, ArticleSupplier supplier = null)
        //{
        //    category = category ?? CreateWithRepository<IArticleCategoryRepository, ArticleCategory>(() => OrderFactory.GetCategory());
        //    articleType = articleType ?? CreateWithRepository<IArticleTypeRepository, ArticleType>(() => OrderFactory.GetArticleType(category));
        //    supplier = supplier ?? CreateWithRepository<IArticleSupplierRepository, ArticleSupplier>(() => OrderFactory.GetSupplier());
        //    return CreateWithRepository<ILensRecipeRepository, LensRecipe>(() => OrderFactory.GetLensRecipe(article, category, articleType, supplier));
        //}

        //protected SubscriptionItem CreateSubscriptionItem(Subscription subscription, bool useOngoingSubscription = false)
        //{
        //    return
        //        CreateWithRepository<ISubscriptionItemRepository, SubscriptionItem>(
        //            () => OrderFactory.GetSubscriptionItem(subscription, useOngoingSubscription));
        //}

        //protected OrderCustomer CreateCustomer(Shop shop, Func<Shop, OrderCustomer> factoryMethod = null)
        //{
        //    var customer = (factoryMethod == null) ? OrderFactory.GetCustomer(shop) : factoryMethod(shop);
        //    return CreateWithRepository<IOrderCustomerRepository, OrderCustomer>(() => customer);
        //}

        //protected Article CreateArticle(ArticleCategory category = null, ArticleType articleType = null, ArticleSupplier supplier = null)
        //{
        //    category = category ?? CreateWithRepository<IArticleCategoryRepository, ArticleCategory>(() => OrderFactory.GetCategory());
        //    articleType = articleType ?? CreateWithRepository<IArticleTypeRepository, ArticleType>(() => OrderFactory.GetArticleType(category));
        //    supplier = supplier ?? CreateWithRepository<IArticleSupplierRepository, ArticleSupplier>(() => OrderFactory.GetSupplier());
        //    return CreateWithRepository<IArticleRepository, Article>(() => OrderFactory.GetArticle(articleType, supplier));
        //}

        //protected IEnumerable<Subscription> CreateSubscriptions(Shop shop, OrderCustomer customer = null, Func<Shop, OrderCustomer, IEnumerable<Subscription>> factoryMethod = null)
        //{
        //    customer = customer ?? CreateCustomer(shop);
        //    var getSubscriptions = factoryMethod ?? OrderFactory.GetSubscriptions;
        //    return CreateItemsWithRepository<ISubscriptionRepository, Subscription>(() => getSubscriptions(shop, customer));
        //}

        //protected Subscription CreateSubscription(Shop shop, OrderCustomer customer = null, bool active = false, DateTime? consentedDate = null, SubscriptionConsentStatus? consentStatus = null)
        //{
        //    customer = customer ?? CreateCustomer(shop);
        //    return CreateWithRepository<ISubscriptionRepository, Subscription>(() => OrderFactory.GetSubscription(shop, customer, active: active, consentedDate: consentedDate, consentStatus: consentStatus));
        //}

        //protected decimal GetExpectedCurrentBalance(IList<SubscriptionTransaction> transactions)
        //{
        //    Func<SubscriptionTransaction, bool> isWithdrawal = transaction => (transaction.Reason == TransactionReason.Withdrawal || transaction.Reason == TransactionReason.Correction) && transaction.Type == TransactionType.Withdrawal;
        //    Func<SubscriptionTransaction, bool> isDeposit = transaction => (transaction.Reason == TransactionReason.Payment || transaction.Reason == TransactionReason.Correction) && transaction.Type == TransactionType.Deposit;
        //    var withdrawals = transactions.Where(isWithdrawal).Sum(x => x.GetAmount().Total);
        //    var deposits = transactions.Where(isDeposit).Sum(x => x.GetAmount().Total);
        //    return deposits - withdrawals;
        //}

        //protected string GetStatusMessage(Subscription subscription)
        //{
        //    if (!subscription.Active) return "Vilande autogiro";
        //    if (subscription.Errors != null && subscription.Errors.Any(e => !e.IsHandled)) return "Transaktion ej genomförd";
        //    return subscription.ConsentStatus.GetEnumDisplayName();
        //}
    }
}