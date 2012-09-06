using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results;

namespace Synologen.Maintenance.UpgradeWpc2012.Persistence.Commands
{
	public class RenameCourseCommand : RenameTextCommand
	{
		private readonly CourseEntity _course;

		public RenameCourseCommand(CourseEntity course)
		{
			_course = course;
		}

		public CourseUpdateResult Execute(string search, string replace)
		{
			Execute(_course.Id, search, replace, "tblCourse", "cBody");
			Execute(_course.Id, search, replace, "tblCourse", "cFormatedBody");
			return new CourseUpdateResult(_course) {OldUrl = search, NewUrl = replace};
		}		 
	}
}