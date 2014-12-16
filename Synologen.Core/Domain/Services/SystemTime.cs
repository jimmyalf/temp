using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public static class SystemTime
	{
		private static Func<DateTime> Time = () => DateTime.Now;

		private static void Reset()
		{
			Time = () => DateTime.Now;
		}

		public static DateTime Now
		{
			get { return Time(); }
		}

		public static void InvokeWhileTimeIs(DateTime time, Action action)
		{
			lock (Time)
			{
				Time = () => time;
				try
				{
					action.Invoke();
				}
				catch
				{
					Reset();
					throw;
				}
				finally
				{
					Reset();	
				}
			}
		}

		public static TReturnType ReturnWhileTimeIs<TReturnType>(DateTime time, Func<TReturnType> action)
		{
			lock (Time)
			{
				Time = () => time;
				TReturnType returnValue;
				try
				{
					returnValue = action.Invoke();
				}
				catch
				{
					Reset();
					throw;
				}
				finally
				{
					Reset();
				}
				return returnValue;
			}
		}
	}
}