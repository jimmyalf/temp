using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Spinit.Data;
using Spinit.Data.NHibernate;
using StructureMap;

namespace Synologen.LensSubscription.BGWebService.App.IoC
{
	public class BGInstanceProvider : IInstanceProvider
	{
		private readonly Type _serviceType;
		private NHibernateUnitOfWork _unitOfWork;

		public BGInstanceProvider(Type serviceType)
		{
			_serviceType = serviceType;
		}

		public object GetInstance(InstanceContext instanceContext)
		{
			return GetInstance(instanceContext, null);
		}

		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			_unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>() as NHibernateUnitOfWork;
			return ObjectFactory.GetInstance(_serviceType);
		}

		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			CommitUnitOfWork();
		}


		protected virtual void CommitUnitOfWork()
		{
			if (_unitOfWork == null) return;
			try
			{
				_unitOfWork.Commit();
			}
			catch
			{
				_unitOfWork.Rollback();
			}
			finally
			{
				_unitOfWork.Dispose();
			}
		}
	}
}