using Spinit.Data.Linq;
using Spinit.Logging;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.Opq.Data.Managers;

namespace Spinit.Wpc.Synologen.OPQ.Data
{
	public partial class WpcSynologenRepository
	{

		#region Constructors

		/// <summary>
		/// Constructor whics sets configuration and logging.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="loggingService">The logging-service.</param>
		/// <param name="webContext">The web-context.</param>

		public WpcSynologenRepository (
			Configuration configuration,
			ILoggingService loggingService,
			Context webContext)
			: base (new WpcSynologenDataContext (configuration.ConnectionString))
		{
			OnCreate ();

			Configuration = configuration;

			LoggingService = loggingService;

			WebContext = webContext;

			TrackingOn = false;
		}

		/// <summary>
		/// Constructor whics sets configuration, logging and tracking.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="loggingService">The logging-service.</param>
		/// <param name="webContext">The web-context.</param>
		/// <param name="noTracking">If true=>no tracking.</param>

		public WpcSynologenRepository (
			Configuration configuration,
			ILoggingService loggingService,
			Context webContext,
			bool noTracking)
			: base (new WpcSynologenDataContext (configuration.ConnectionString).NoTracking ())
		{
			OnCreate ();

			Configuration = configuration;

			LoggingService = loggingService;

			WebContext = webContext;

			TrackingOn = noTracking;
		}

		#endregion

		#region Create Repository

		/// <summary>
		/// Creates the repository with tracking.
		/// </summary>
		/// <param name="configuration">The configuarion.</param>
		/// <param name="loggingService">The logging service.</param>
		/// <param name="webContext">The web-context.</param>
		/// <returns>A repository</returns>

		public static WpcSynologenRepository GetWpcSynologenRepository (
			Configuration configuration,
			ILoggingService loggingService,
			Context webContext)
		{
			return new WpcSynologenRepository (
				configuration,
				loggingService,
				webContext);
		}

		/// <summary>
		/// Creates the repository without tracking.
		/// </summary>
		/// <param name="configuration">The configuarion.</param>
		/// <param name="loggingService">The logging service.</param>
		/// <param name="webContext">The web-context.</param>
		/// <returns>A repository</returns>

		public static WpcSynologenRepository GetWpcSynologenRepositoryNoTracking (
			Configuration configuration,
			ILoggingService loggingService,
			Context webContext)
		{
			return new WpcSynologenRepository (
				configuration,
				loggingService,
				webContext,
				true);
		}

		#endregion

		#region On Create

		/// <summary>
		/// The on-create overriden class
		/// </summary>

		protected override sealed void OnCreate ()
		{
			Node = new NodeManager (this);
		}
		
		#endregion
		
		#region Managers

		/// <summary>
		/// Gets the account-manager.
		/// </summary>

		public NodeManager Node { get; private set; }

		#endregion

		#region Global properties

		/// <summary>
		/// Gets or sets the web-context
		/// </summary>

		public Context WebContext { get; set; }

		#endregion
	}	
}
 
