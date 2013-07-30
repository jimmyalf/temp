using System;
using System.Linq.Expressions;
using System.Reflection;
using FakeItEasy;
using Spinit.Extensions;
using Spinit.Test;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.TestHelpers
{
	[TestFixture]
	public abstract class CommonTaskTestBase : BehaviorActionTestbase<ITask>
	{
		protected Mock<IBGWebServiceClient> MockedWebServiceClient;
		protected Mock<ISubscriptionRepository> MockedSubscriptionRepository;
		protected Mock<ISubscriptionErrorRepository> MockedSubscriptionErrorRepository;
		protected FakeLoggingService LoggingService;
		protected Mock<ITransactionRepository> MockedTransactionRepository;
		protected ITaskRepositoryResolver TaskRepositoryResolver;
		protected IAutogiroPaymentService AutogiroPaymentService;
		protected ISubscriptionPendingPaymentRepository SubscriptionPendingPaymentRepository;

		protected override void SetUp()
		{
			MockedWebServiceClient = new Mock<IBGWebServiceClient>();
			MockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			MockedSubscriptionErrorRepository = new Mock<ISubscriptionErrorRepository>();
			MockedTransactionRepository = new Mock<ITransactionRepository>();
			LoggingService = new FakeLoggingService();
			TaskRepositoryResolver = A.Fake<ITaskRepositoryResolver>();
			AutogiroPaymentService = A.Fake<IAutogiroPaymentService>();
			SubscriptionPendingPaymentRepository = A.Fake<ISubscriptionPendingPaymentRepository>();
			A.CallTo(() => TaskRepositoryResolver.GetRepository<ISubscriptionRepository>()).Returns(MockedSubscriptionRepository.Object);
			A.CallTo(() => TaskRepositoryResolver.GetRepository<ISubscriptionErrorRepository>()).Returns(MockedSubscriptionErrorRepository.Object);
			A.CallTo(() => TaskRepositoryResolver.GetRepository<ITransactionRepository>()).Returns(MockedTransactionRepository.Object);
			A.CallTo(() => TaskRepositoryResolver.GetRepository<IBGWebServiceClient>()).Returns(MockedWebServiceClient.Object);
			A.CallTo(() => TaskRepositoryResolver.GetRepository<ISubscriptionPendingPaymentRepository>()).Returns(SubscriptionPendingPaymentRepository);
		}

		protected abstract ITask GetTask();
		protected override ITask GetTestEntity()
		{
			return GetTask();
		}

		protected ExecutingTaskContext ExecutingTaskContext
		{
			get
			{
				return new ExecutingTaskContext(TestEntity, TaskRepositoryResolver);
			}
		}

		protected void TrySetProperty<TType>(TType value, Expression<Func<TType,object>> expression, object propertyValue) where TType : class
		{
			var propertyName = expression.GetName();
			var propertyInfo = typeof (TType).GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty);
			if (propertyInfo == null) return;
			propertyInfo.SetValue(value, propertyValue, null);
		}
	}
}