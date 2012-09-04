using System;
using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public class RenameFileCommand : PersistenceBase
	{
		private readonly FileEntity _fileEntity;
		private readonly Func<string, string> _rename;

		public RenameFileCommand(FileEntity fileEntity, Func<string,string> rename)
		{
			_fileEntity = fileEntity;
			_rename = rename;
		}

		public FileEntityRenamingResult Execute()
		 {
		 	var newName = _rename(_fileEntity.Name);
			var command = CommandBuilder
				.Build(@"UPDATE tblBaseFile 
					SET cName = @NewName, cPreviousName = @PreviousName 
					WHERE cId = @Id")
				.AddParameters(new { NewName = newName, Id = _fileEntity.Id, PreviousName = _fileEntity.Name });
			Execute(command);
			return new FileEntityRenamingResult {Id = _fileEntity.Id, NewPath = newName, OldPath = _fileEntity.Name};
		 }
	}
}