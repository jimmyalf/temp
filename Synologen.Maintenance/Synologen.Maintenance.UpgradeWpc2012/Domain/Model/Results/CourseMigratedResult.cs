using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public class CourseMigratedResult : IEntityMigratedResult
	{
		private readonly CourseEntity _course;

		public CourseMigratedResult(CourseEntity course)
		{
			_course = course;
		}

		public string OldUrl { get; set; }
		public string NewUrl { get; set; }

		public RenameEventArgs ToRenameEvent()
		{
			return new RenameEventArgs(OldUrl, NewUrl, ToString());
		}

		public override string ToString()
		{
			return string.Format("Course[{0}] had url \"{1}\" replaced with \"{2}\"", _course.Id, OldUrl, NewUrl);
		}
	}
}