using System;
using System.Collections;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum {

	public class Services {

		public static ArrayList GetServices() {
			return null;
		}

		public static Service GetService( int serviceId ) {
			return null;
		}

		public static int CreateService( Service service ) {
			return CreateUpdateDeleteService( service, DataProviderAction.Create);
		}
		public static int DeleteService( Service service ) {
			return CreateUpdateDeleteService( service, DataProviderAction.Delete);
		}
		public static int UpdateService( Service service ) {
			return CreateUpdateDeleteService( service, DataProviderAction.Update);
		}

		private static int CreateUpdateDeleteService( Service service, DataProviderAction action ) {
			ForumsDataProvider dp = ForumsDataProvider.Instance();

			return dp.CreateUpdateDeleteService( service, action );
		}
	}
}