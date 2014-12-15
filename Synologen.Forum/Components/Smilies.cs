using System;
using System.Web;
using System.Collections;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum {

	public class Smilies {

		public static ArrayList GetSmilies() {
			ArrayList smilies;
            string cacheKey = "AspNetForums-Emoticons";

            // Attempt to fetch from cache
            smilies = (ArrayList) HttpRuntime.Cache[cacheKey];

            if (smilies == null) {

                ForumsDataProvider dp = ForumsDataProvider.Instance();

                smilies = dp.GetSmilies();

                HttpRuntime.Cache.Insert(cacheKey, smilies, null, DateTime.Now.AddMinutes(60), TimeSpan.Zero);
            } 

            return smilies;

		}

		public static Smiley GetSmiley( int smileyId ) {
			return null;
		}

		public static int CreateSmiley( Smiley smiley ) {
			return CreateUpdateDeleteSmiley( smiley, DataProviderAction.Create);
		}
		public static int DeleteSmiley( Smiley smiley ) {
			return CreateUpdateDeleteSmiley( smiley, DataProviderAction.Delete);
		}
		public static int UpdateSmiley( Smiley smiley ) {
			return CreateUpdateDeleteSmiley( smiley, DataProviderAction.Update);
		}

		private static int CreateUpdateDeleteSmiley( Smiley smiley, DataProviderAction action ) {
			ForumsDataProvider dp = ForumsDataProvider.Instance();

			return dp.CreateUpdateDeleteSmiley( smiley, action );
		}
	}
}