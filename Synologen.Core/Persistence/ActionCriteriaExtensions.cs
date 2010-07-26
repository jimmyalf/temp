using System;

namespace Spinit.Wpc.Synologen.Core.Persistence
{
	public static class ActionCriteriaExtensions
	{
		private static Func<Type, object> _converterConstructor;

		public static void ConstructConvertersUsing(Func<Type, object> constructor)
		{
			_converterConstructor = constructor;
		}

		public static TCriteriaDestination Convert<TCriteriaSource, TCriteriaDestination>(this TCriteriaSource actionCriteria) where TCriteriaSource : IActionCriteria
		{
			var converter = GetConverter<TCriteriaSource, TCriteriaDestination>();
			return converter.Convert(actionCriteria);
		}

		private static IActionCriteriaConverter<TCriteriaSource, TCriteriaDestination> GetConverter<TCriteriaSource, TCriteriaDestination>() where TCriteriaSource : IActionCriteria
		{
			var converter = _converterConstructor.Invoke(typeof(IActionCriteriaConverter<TCriteriaSource, TCriteriaDestination>));
			return (IActionCriteriaConverter<TCriteriaSource, TCriteriaDestination>)converter;
		}
	}
}