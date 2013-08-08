namespace Synologen.Service.Client.BGCTaskRunner.App
{
	public static class Mode
	{
		private static RunningMode _currentRunningMode = RunningMode.Debug;
		public static RunningMode Current
		{
			get { return _currentRunningMode; }
			set { _currentRunningMode = value; }
		}
	}

	public enum RunningMode
	{
		Debug,
		Test,
		InProduction
	}
}