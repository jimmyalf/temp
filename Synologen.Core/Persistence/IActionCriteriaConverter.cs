namespace Spinit.Wpc.Synologen.Core.Persistence
{
	public interface IActionCriteriaConverter {}

	public interface IActionCriteriaConverter<TCriteriaSource, TCriteriaDestination> : IActionCriteriaConverter where TCriteriaSource : IActionCriteria
	{
		TCriteriaDestination Convert(TCriteriaSource source);
	}
}