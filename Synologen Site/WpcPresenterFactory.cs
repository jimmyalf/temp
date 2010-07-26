using System;
using StructureMap;
using StructureMap.Pipeline;
using WebFormsMvp;
using WebFormsMvp.Binder;

namespace Spinit.Wpc.Synologen.Presentation.Site
{
	public class WpcPresenterFactory : IPresenterFactory, IDisposable
	{
        private readonly object _registerLock = new object();
		private readonly IContainer _container;


		/// <summary>
		/// Initializea a new instance of the StructureMapPresenterFactory class.
		/// </summary>
		/// <param name="container">The StructureMap container to use withing the presenter actory</param>
		public WpcPresenterFactory(IContainer container)
		{
		    if (container == null) throw new ArgumentNullException("container");

		    _container = container;
		}

        /// <summary>
        /// Creates the specified presenter type.
        /// </summary>
        /// <param name="presenterType">Type of the presenter.</param>
        /// <param name="viewType">Type of the view.</param>
        /// <param name="viewInstance">The view instance.</param>
        public IPresenter Create(Type presenterType, Type viewType, IView viewInstance)
        {
            if (presenterType == null) throw new ArgumentNullException("presenterType");
            if (viewType == null) throw new ArgumentNullException("viewType");
            if (viewInstance == null) throw new ArgumentNullException("viewInstance");

            if (!_container.Model.HasImplementationsFor(presenterType))
            {
                lock (_registerLock)
                {
                    if (!_container.Model.HasImplementationsFor(presenterType))
                    {
                        _container.Configure(
                            x =>
                            x.ForRequestedType(presenterType).TheDefaultIsConcreteType(presenterType).WithName(
                                presenterType.Name));
                    }
                }
            }

            var args = new ExplicitArguments();
            args.Set("view");
            args.SetArg("view", viewInstance);


            return (IPresenter) _container.GetInstance(presenterType, args);
        }

        /// <summary>
        /// Releases the specified presenter.
        /// </summary>
        /// <param name="presenter">The presenter.</param>
        public void Release(IPresenter presenter)
        {
            _container.EjectAllInstancesOf<IPresenter>();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
			//if (_container != null)
			//    _container.Dispose();
        }
	}
}