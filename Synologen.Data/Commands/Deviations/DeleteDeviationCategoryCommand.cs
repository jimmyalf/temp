using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
namespace Spinit.Wpc.Synologen.Data.Commands.Deviations
{
	public class DeleteDeviationCategoryCommand : Command
	{
        private DeviationCategory _deviationCategory;

        public DeleteDeviationCategoryCommand(DeviationCategory deviationCategory)
		{
            _deviationCategory = deviationCategory;
		}

		public override void Execute()
		{
            Session.Delete(_deviationCategory);
		}
	}
}