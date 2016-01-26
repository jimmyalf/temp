using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
namespace Spinit.Wpc.Synologen.Data.Commands.Deviations
{
	public class CreateDeviationCategoryCommand : Command
	{
        private DeviationCategory _deviationCategory;

        public CreateDeviationCategoryCommand(DeviationCategory deviationCategory)
		{
            _deviationCategory = deviationCategory;
		}

		public override void Execute()
		{
            Session.Save(_deviationCategory);
		}
	}
}