using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using StructureMap;

namespace Synologen.Service.Web.External.App.IoC
{
	public class InstanceProvider : IInstanceProvider
	{
		private readonly Type _serviceType;
		private readonly ILoggingService _loggingService;
		private readonly UnitOfWorkManager _unitOfWorkManager;

		public InstanceProvider(Type serviceType)
		{
			_serviceType = serviceType;
			_loggingService = ObjectFactory.GetInstance<ILoggingService>();
			_unitOfWorkManager = new UnitOfWorkManager();
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
					_loggingService.LogDebug("InstanceProvider {0}: CreateInstance called", instanceContext.GetHashCode());
					var unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>() as NHibernateUnitOfWork;
					_unitOfWorkManager.Add(instanceContext, unitOfWork);
				}
				catch(Exception ex)
				{
					_loggingService.LogError(string.Format("InstanceProvider {0}: Got exception while getting instance.", instanceContext.GetHashCode()), ex);
					throw;
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
					CommitUnitOfWork(unitOfWork, instanceContext);
				}
				catch(Exception ex)
				{
					_loggingService.LogError(string.Format("InstanceProvider {0}: Got exception while releasing instance.", instanceContext.GetHashCode()), ex);
					throw;
				}	
			}
		}

		protected virtual void CommitUnitOfWork(IUnitOfWork unitOfWork,  InstanceContext instanceContext)
		{
			if (unitOfWork == null || unitOfWork.IsDisposed) return;
			try
			{
				unitOfWork.Commit();
				_loggingService.LogDebug("InstanceProvider {0}: Unit of work was comitted.", instanceContext.GetHashCode());
			}
			catch
			{
				unitOfWork.Rollback();
				_loggingService.LogDebug("InstanceProvider {0}: Unit of work was rolled back.", instanceContext.GetHashCode());
			}
			finally
			{
				unitOfWork.Dispose();
				_loggingService.LogDebug("InstanceProvider {0}: Unit of work was disposed.", instanceContext.GetHashCode());
			}
		}
	}
}