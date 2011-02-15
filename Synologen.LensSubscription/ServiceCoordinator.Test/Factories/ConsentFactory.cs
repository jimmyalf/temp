using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.ServiceCoordinator.Test.Factories
{
	public static class ConsentFactory
	{
		private static readonly ConsentInformationCode InformationCode = ConsentInformationCode.InitiatedByPayer;
		private static readonly ConsentCommentCode CommentCode = ConsentCommentCode.ConsentTurnedDownByBank;

		public static IEnumerable<RecievedConsent> GetList(int subscriptionId)
		{
			var list = TestHelper.GenerateSequence(x => Get(x, subscriptionId), 18);
			return list;
		}

		public static RecievedConsent Get(int id, int subscriptionId)
		{
			var consent = new RecievedConsent
			{
				PayerNumber = subscriptionId,
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
				PayerNumber = id,
				ActionDate = DateTime.Now.AddDays(-2),
				ConsentValidForDate = DateTime.Now.AddDays(1),
				InformationCode = InformationCode.Next(),
				CommentCode = commentCode
			};
			return consent;
		}
	}
}