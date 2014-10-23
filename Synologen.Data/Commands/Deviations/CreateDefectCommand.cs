using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
namespace Spinit.Wpc.Synologen.Data.Commands.Deviations
{
	public class CreateDefefectCommand : Command
	{
        private DeviationDefect _deviationDefect;

        public CreateDefefectCommand(DeviationDefect deviationDefect)
		{
            _deviationDefect = deviationDefect;
		}

		public override void Execute()
		{
            Session.Save(_deviationDefect);
		}
	}
}