using System;
using FreeTextBoxControls;

namespace Spinit.Wpc.Forum.Controls {
	/// <summary>
	/// A FreeTextBox button to surround text with [code language="c#"] tags
	/// </summary>
	public class CSharpButton : ToolbarButton {
		public CSharpButton() : base("CSharpButton","FTB_CsharpCodeBlock","button_cs") {
			ScriptBlock = @"function FTB_CsharpCodeBlock(ftbName) {	
	FTB_SurroundText(ftbName,'[code language=""c#""]','[/code]');
}";
		}
	}
}
