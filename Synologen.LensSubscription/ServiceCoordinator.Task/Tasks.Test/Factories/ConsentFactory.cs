using System;
using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.Test.Factories
{
	public static class ConsentFactory
	{
		private static readonly ConsentInformationCode InformationCode = ConsentInformationCode.InitiatedByPayer;
		private static readonly ConsentCommentCode CommentCode = ConsentCommentCode.ConsentTurnedDownByBank;

		public static IEnumerable<ReceivedConsent> GetList(int subscriptionId)
		{
			Func<int,ReceivedConsent> generateItem = id => Get(id, subscriptionId);
			return generateItem.GenerateRange(1,18);
		}

		public static ReceivedConsent Get(int id, int subscriptionId)
		{
			var consent = new ReceivedConsent
			{
				PayerNumber = subscriptionId,
				ConsentId = id,
				ActionDate = DateTime.Now.AddDays(-2),
				ConsentValidForDate = DateTime.Now.AddDays(1),
				InformationCode = InformationCode.SkipItems(id),
				CommentCode = CommentCode.SkipItems(id)
			};
			return consent;
		}

		public static ReceivedConsent Get(int id, ConsentCommentCode commentCode)
		{
			var consent = new ReceivedConsent
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