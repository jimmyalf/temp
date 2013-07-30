using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
namespace Spinit.Wpc.Synologen.Data.Commands.Deviations
{
	public class DeleteDeviationSupplierCommand : Command
	{
        private DeviationSupplier _deviationSupplier;

        public DeleteDeviationSupplierCommand(DeviationSupplier deviationSupplier)
		{
            _deviationSupplier = deviationSupplier;
		}

		public override void Execute()
		{
            Session.Delete(_deviationSupplier);
		}
	}
}