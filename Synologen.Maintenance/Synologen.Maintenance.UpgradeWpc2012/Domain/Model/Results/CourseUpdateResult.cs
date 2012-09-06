using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Results
{
	public class CourseUpdateResult : IUrlReplacingResult
	{
		private readonly CourseEntity _course;

		public CourseUpdateResult(CourseEntity course)
		{
			_course = course;
		}

		public string OldUrl { get; set; }
		public string NewUrl { get; set; }

		public RenameEventArgs ToRenameEvent()
		{
			return new RenameEventArgs(OldUrl, NewUrl, GetDescription());
		}

		public string GetDescription()
		{
			return string.Format("Course[{0}] had url \"{1}\" replaced with \"{2}\"", _course.Id, OldUrl, NewUrl);
		}
	}
}