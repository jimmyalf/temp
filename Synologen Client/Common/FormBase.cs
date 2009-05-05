using System;
using System.Windows.Forms;

namespace Synologen.Client.Common {
	class FormBase : Form {
		private const string TabCharacter = "\t";

		public void SetFormStatus(Enumeration.ConnectionStatus status, string message) {
			if(ParentForm == null)
				return;
			((MDIMain)ParentForm).SetStatus(status, message);
		}

		public void CopyListViewToClipboard(ListView listView) {
			var buffer = new System.Text.StringBuilder();
			for(var i = 0; i < listView.Columns.Count; i++) {
				buffer.Append((string)listView.Columns[i].Text);
				buffer.Append(TabCharacter);
			}
			buffer.Append(Environment.NewLine);

			for(var i = 0; i < listView.Items.Count; i++) {
				for(var j = 0; j < listView.Columns.Count; j++) {
					buffer.Append((string)listView.Items[i].SubItems[j].Text);
					buffer.Append(TabCharacter);
				}
				buffer.Append(Environment.NewLine);
			}

			Clipboard.SetText(buffer.ToString());
		}
	}
}
