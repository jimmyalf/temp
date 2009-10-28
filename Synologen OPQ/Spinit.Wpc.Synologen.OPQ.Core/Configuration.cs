using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.OPQ.Core
{
	
	/// <summary>
	/// The configuration data-container.
	/// </summary>

	public class Configuration : ConfigurationFetch
	{
		public const string SynologenOpqSetting = "SynologenOpq";

		/// <summary>
		/// The minimum constructor.
		/// </summary>
		/// <param name="connectionString">The connection-string.</param>
		/// <param name="defaultCulture">The default-culture.</param>

		public Configuration (string connectionString, string defaultCulture)
		{
			ConnectionString = connectionString;
			DefaultCulture = defaultCulture;
		}

		/// <summary>
		/// Fetches the configuration for use in repository.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>A new configuration.</returns>

		public static Configuration GetConfiguration (Context context)
		{
			return new Configuration (
				Globals.ConnectionString (ConnectionName),
				context.Culture);
		}

		#region Properties

		#region Used for Data-Context and Repository

		/// <summary>
		/// Gets the connection-string.
		/// </summary>

		public string ConnectionString { get; private set; }
		
		/// <summary>
		/// Gets the default-culture.
		/// </summary>

		public string DefaultCulture { get; private set; }

		#endregion

		/// <summary>
		/// Specifies the component Name used in the component db
		/// </summary>

		static public string ComponentName
		{
			get {
				return SafeConfigString (
					SynologenOpqSetting,
					"name",
					string.Empty);
			}
		}

		/// <summary>
		/// Specifies the component application path used in the component db
		/// </summary>

		static public string ComponentApplicationPath
		{
			get  {
				return SafeConfigString (
					SynologenOpqSetting,
					"componentApplicationPath",
					string.Empty);
			}
		}

		/// <summary>
		/// Specifies the connection name used in the component db
		/// </summary>

		static public string ConnectionName
		{
			get {
				return SafeConfigString (
					SynologenOpqSetting,
					"connectionName",
					string.Empty);
			}
		}

		#endregion
	}
} 
