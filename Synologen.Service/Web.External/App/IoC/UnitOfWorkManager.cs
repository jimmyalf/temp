using System.Collections.Generic;
using System.ServiceModel;
using Spinit.Data;

namespace Synologen.Service.Web.External.App.IoC
{
	public class UnitOfWorkManager
	{
		private readonly IDictionary<int, IUnitOfWork> _unitOfWorks;

		public UnitOfWorkManager()
		{
			_unitOfWorks = new Dictionary<int, IUnitOfWork>();
			SyncRoot = new object();
		}

		public void Add(InstanceContext context, IUnitOfWork unitOfWork)
		{
			_unitOfWorks.Add(context.GetHashCode(), unitOfWork);
		}
		public IUnitOfWork Get(InstanceContext context)
		{
			var key = context.GetHashCode();
			if(_unitOfWorks.ContainsKey(key))
			{
				var unitOfWork = _unitOfWorks[key];
				_unitOfWorks.Remove(key);
				return unitOfWork;
			}
			return null;
		}

		public object SyncRoot { get; set; }
	}
}