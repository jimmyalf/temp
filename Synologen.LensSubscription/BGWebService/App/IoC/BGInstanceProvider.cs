using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using StructureMap;

namespace Synologen.LensSubscription.BGWebService.App.IoC
{
	public class BGInstanceProvider : IInstanceProvider
	{
		private readonly Type _serviceType;
		private NHibernateUnitOfWork _unitOfWork;
		private ILoggingService _loggingService;

		public BGInstanceProvider(Type serviceType)
		{
			_serviceType = serviceType;
			_loggingService = ObjectFactory.GetInstance<ILoggingService>();
			_loggingService.LogDebug("InstanceProvider was initiated for service type \"{0}\"", serviceType.FullName);
		}

		public object GetInstance(InstanceContext instanceContext)
		{
			return GetInstance(instanceContext, null);
		}

		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			_loggingService.LogDebug("InstanceProvider: GetInstance called");
			_unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>() as NHibernateUnitOfWork;
			_loggingService.LogDebug("InstanceProvider: Unit of work was fetched");
			return ObjectFactory.GetInstance(_serviceType);
		}

		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			_loggingService.LogDebug("InstanceProvider: ReleaseInstance called");
			CommitUnitOfWork();
		}


		protected virtual void CommitUnitOfWork()
		{
			if (_unitOfWork == null) return;
			try
			{
				_unitOfWork.Commit();
				_loggingService.LogDebug("InstanceProvider: Unit of work was committed");
			}
			catch
			{
				_unitOfWork.Rollback();
				_loggingService.LogDebug("InstanceProvider: Unit of work was rolled back");
			}
			finally
			{
				_unitOfWork.Dispose();
				_loggingService.LogDebug("InstanceProvider: Unit of work was disposed");
			}
		}
	}
}