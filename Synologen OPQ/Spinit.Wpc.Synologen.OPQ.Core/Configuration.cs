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


		/// <summary>
		/// Specifies the root url used for shop documents
		/// </summary>
		static public string DocumentShopRootUrl
		{
			get
			{
				return string.Concat(Globals.FilesUrl, SafeConfigString(SynologenOpqSetting, "DocumentShopRootUrl", string.Empty));
			}
		}

		/// <summary>
		/// Specifies the root url used for central documents
		/// </summary>
		static public string DocumentCentralRootUrl
		{
			get
			{
				return string.Concat(Globals.FilesUrl, SafeConfigString(SynologenOpqSetting, "DocumentCentralRootUrl", string.Empty));
			}
		}

		/// <summary>
		/// Allowed extensions for upload
		/// </summary>
		static public string UploadAllowedExtensions
		{
			get
			{
				return SafeConfigString(SynologenOpqSetting, "UploadAllowedExtensions", string.Empty);
			}
		}

		/// <summary>
		/// Max size for upload
		/// </summary>
		static public int UploadMaxFileSize
		{
			get
			{
				return SafeConfigNumber(SynologenOpqSetting, "UploadMaxFileSize", 102400);
			}
		}


		#endregion
	}
} 
