using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.ServiceCoordinator.Test.Factories
{
	public static class ConsentFactory
	{
		private static ConsentInformationCode InformationCode = ConsentInformationCode.InitiatedByPayer;
		private static ConsentCommentCode CommentCode = ConsentCommentCode.ConsentTurnedDownByBank;

		public static IEnumerable<RecievedConsent> GetList(int subscriptionId)
		{
			IEnumerable<RecievedConsent> list = TestHelper.GenerateSequence(x => Get(x, subscriptionId), 18);
			return list;
		}

		public static RecievedConsent Get(int id, int subscriptionId)
		{
			var consent = new RecievedConsent
			              	{
								PayerId = subscriptionId,
			              		ConsentId = id,
			              		ActionDate = DateTime.Now.AddDays(-2),
			              		ConsentValidForDate = DateTime.Now.AddDays(1),
								InformationCode = InformationCode.SkipValues(id),
			              		CommentCode = CommentCode.SkipValues(id)
			              	};
			return consent;
		}

		public static RecievedConsent Get(int id, ConsentCommentCode commentCode)
		{
			var consent = new RecievedConsent
			{
				PayerId = id,
				ActionDate = DateTime.Now.AddDays(-2),
				ConsentValidForDate = DateTime.Now.AddDays(1),
				InformationCode = InformationCode.Next(),
				CommentCode = commentCode
			};
			return consent;
		}
	}
}
