using System;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public class RenameFileCommand : PersistenceBase
	{
		private readonly FileEntry _fileEntry;
		private readonly Func<string, string> _rename;

		public RenameFileCommand(FileEntry fileEntry, Func<string,string> rename)
		{
			_fileEntry = fileEntry;
			_rename = rename;
		}

		public RenamedFileEntity Execute()
		 {
		 	var newName = _rename(_fileEntry.Name);
		 	var command = CommandBuilder
				.Build(@"UPDATE tblBaseFile 
					SET cName = @NewName 
					WHERE cId = @Id")
				.AddParameters(new {NewName = newName, Id = _fileEntry.Id});
			 Execute(command);
			return new RenamedFileEntity {Id = _fileEntry.Id, NewName = newName, OldName = _fileEntry.Name};
		 }
	}
}