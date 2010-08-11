using System;

namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public static class WpcDisabled
	{
		public static string DisableIf<TViewModel>(this TViewModel viewModel, Func<TViewModel,bool> disabledCondition)
		{
			var disable = disabledCondition.Invoke(viewModel);
			return disable ? "disabled" : null;
		}
	}
}