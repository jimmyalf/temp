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
		private readonly ILoggingService _loggingService;
		private readonly UnitOfWorkManager _unitOfWorkManager;

		public BGInstanceProvider(Type serviceType)
		{
			_serviceType = serviceType;
			_loggingService = ObjectFactory.GetInstance<ILoggingService>();
			_unitOfWorkManager = new UnitOfWorkManager();
			_loggingService.LogDebug("InstanceProvider was initiated for service type \"{0}\"", serviceType.FullName);
		}

		public object GetInstance(InstanceContext instanceContext)
		{
			return GetInstance(instanceContext, null);
		}

		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			lock(_unitOfWorkManager.SyncRoot){
				try
				{
					_loggingService.LogDebug("InstanceProvider {0}: GetInstance called with message action {1}", instanceContext.GetHashCode(), GetActionUrl(message));
					var unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>() as NHibernateUnitOfWork;
					_unitOfWorkManager.Add(instanceContext, unitOfWork);
					_loggingService.LogDebug("InstanceProvider {0}: Unit of work was fetched", instanceContext.GetHashCode());
				}
				catch(Exception ex)
				{
					_loggingService.LogError(string.Format("Got exception while getting instance {0}", instanceContext.GetHashCode()), ex);
				}
				return ObjectFactory.GetInstance(_serviceType);
			}
		}

		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			lock (_unitOfWorkManager.SyncRoot)
			{
				try
				{
					_loggingService.LogDebug("InstanceProvider {0}: ReleaseInstance called", instanceContext.GetHashCode());
					var unitOfWork = _unitOfWorkManager.Get(instanceContext);
					CommitUnitOfWork(unitOfWork, instanceContext.GetHashCode());
				}
				catch(Exception ex)
				{
					_loggingService.LogError(string.Format("Got exception while releasing instance {0}", instanceContext.GetHashCode()), ex);
				}	
			}
		}

		private static string GetActionUrl(Message message)
		{
			string actionName = null;
			if(message != null && message.Headers != null && message.Headers.Action != null)
			{
				actionName = message.Headers.Action;
			}
			return actionName;
		}

		protected virtual void CommitUnitOfWork(IUnitOfWork unitOfWork, int instanceContextHash)
		{
			if (unitOfWork == null)
			{
				_loggingService.LogDebug("InstanceProvider {0}: Unit of work is null. Nothing to commit.", instanceContextHash);
				return;
			}
			if(unitOfWork.IsDisposed)
			{
				_loggingService.LogDebug("InstanceProvider {0}: Unit of work is already disposed. Nothing to commit.", instanceContextHash);
				return;
			}
			try
			{
				unitOfWork.Commit();
				_loggingService.LogDebug("InstanceProvider {0}: Unit of work was committed", instanceContextHash);
			}
			catch
			{
				unitOfWork.Rollback();
				_loggingService.LogDebug("InstanceProvider {0}: Unit of work was rolled back", instanceContextHash);
			}
			finally
			{
				unitOfWork.Dispose();
				_loggingService.LogDebug("InstanceProvider {0}: Unit of work was disposed", instanceContextHash);
			}
		}
	}
}