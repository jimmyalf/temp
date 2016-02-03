using System;

namespace Spinit.Wpc.Synologen.Visma {
	/// <summary>
	/// Represents error that occurs during Visma operations
	/// </summary>
	public class VismaException : Exception{
		public VismaException(string message) : base(message) {}
		public VismaException(string message, Exception ex) : base(message, ex) {}
	}
}