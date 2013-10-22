using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
namespace Spinit.Wpc.Synologen.Data.Commands.Deviations
{
	public class CreateDeviationSupplierCommand : Command
	{
        private DeviationSupplier _deviationSupplier;

        public CreateDeviationSupplierCommand(DeviationSupplier deviationSupplier)
		{
            _deviationSupplier = deviationSupplier;
		}

		public override void Execute()
		{
            Session.Save(_deviationSupplier);
		}
	}
}