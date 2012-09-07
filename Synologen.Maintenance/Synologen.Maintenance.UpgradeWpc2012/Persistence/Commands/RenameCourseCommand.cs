using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public class RenameCourseCommand : PersistenceBase
	{
		private readonly CourseEntity _course;

		public RenameCourseCommand(CourseEntity course)
		{
			_course = course;
		}

		public CourseMigratedResult Execute(string search, string replace)
		{
			RenameContent(_course.Id, search, replace, "tblCourse", "cBody");
			RenameContent(_course.Id, search, replace, "tblCourse", "cFormatedBody");
			return new CourseMigratedResult(_course) {OldUrl = search, NewUrl = replace};
		}		 
	}
}