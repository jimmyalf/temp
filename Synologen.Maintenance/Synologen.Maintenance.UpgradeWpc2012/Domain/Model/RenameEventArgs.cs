using System;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model
{
	public class RenameEventArgs : EventArgs
	{
		public string PreviousName { get; set; }
		public string NewName { get; set; }
		public string Description { get; set; }

		public RenameEventArgs(string previousName, string newName, string description)
		{
			PreviousName = previousName;
			NewName = newName;
			Description = description;
		}

		public override string ToString()
		{
			return Description;
		}
	}
}