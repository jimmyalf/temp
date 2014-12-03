using System;
using System.Linq;

namespace Spinit.Wpc.Synologen.Core.Domain.Testing
{
	public static class TestRunnerDetector
	{
		private static readonly bool RunningFromNUnit = false;
		private static bool _disabled = false;

		static TestRunnerDetector()
		{
			RunningFromNUnit = AppDomain.CurrentDomain
				.GetAssemblies()
				.Any(assem => assem.FullName.ToLowerInvariant().StartsWith("nunit.framework"));
		}

		public static bool IsRunningFromNunit
		{
			get
			{
				return !_disabled && RunningFromNUnit;
			}
		}

		public static void Disable()
		{
			_disabled = true;
		}

		public static void Enable()
		{
			_disabled = false;
		}
	}
}