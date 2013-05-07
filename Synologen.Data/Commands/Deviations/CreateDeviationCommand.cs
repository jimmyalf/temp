using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
namespace Spinit.Wpc.Synologen.Data.Commands.Deviations
{
	public class CreateDeviationCommand : Command
	{
        private Deviation _deviation;

        public CreateDeviationCommand(Deviation deviation)
		{
            _deviation = deviation;
		}

		public override void Execute()
		{
            Session.Save(_deviation);
		}
	}
}