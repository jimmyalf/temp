using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence
{
	public interface IQuery<out TResult> : IQuery
	{
		TResult Execute();
	}

	public interface IQuery
	{
		Type ResultType { get; set; }
	}
}