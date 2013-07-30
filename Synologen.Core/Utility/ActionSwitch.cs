using System;

namespace Spinit.Wpc.Synologen.Core.Utility
{
	public class ActionSwitch<TType> : ReturnSwitch<TType, Action>
	{
		public ActionSwitch(TType value) : base(value) { }

		public virtual new ActionSwitch<TType> Case(Func<TType,bool> evaluate, Action returnValue)
		{
			base.Case(evaluate, returnValue);
			return this;
		}

		public virtual new ActionSwitch<TType> Default(Action defaultValue)
		{
			base.Default(defaultValue);
			return this;
		}

		public new void Evaluate()
		{
			base.Evaluate()();
		}
	}
}