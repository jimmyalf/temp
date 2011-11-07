using System;
using FreeTextBoxControls;

namespace Spinit.Wpc.Forum.Controls {
	/// <summary>
	/// A FreeTextBox button to surround text with [code language="vb"] tags
	/// </summary>
	public class VBButton : ToolbarButton {
		public VBButton() : base("VBButton","FTB_VBCodeBlock","button_vb") {
			ScriptBlock = @"function FTB_VBCodeBlock(ftbName) {	
	FTB_SurroundText(ftbName,'[code language=""vb""]','[/code]');
}";
		}
	}
}
