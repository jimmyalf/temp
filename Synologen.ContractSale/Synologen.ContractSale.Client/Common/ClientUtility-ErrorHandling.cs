using System;
using System.Windows.Forms;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;

namespace Synologen.Client.Common {
	public static partial class ClientUtility {

		/// <summary>
		/// Tries to log an error message to Webservice
		/// (Will not throw exception if Webservice contact fails)
		/// </summary>
		public static void TryLogError(string message) {
			var client = GetWebClient();
			try {
				client.Open();
				client.LogMessage(LogType.Error, message);
			}
			catch { return; }
			finally { client.Close(); }

		}

		/// <summary>
		/// Tries to log an error message and exception message to Webservice
		/// (Will not throw exception if Webservice contact fails)
		/// </summary>
		public static void TryLogError(string message, Exception ex) {
			var client = GetWebClient();
			try {
				client.Open();
				client.LogMessage(LogType.Error, message + ": " + ex.Message);
			}
			catch { return; }
			finally { client.Close(); }

		}

		/// <summary>
		/// Tries to log an information message to Webservice
		/// (Will not throw exception if Webservice contact fails)
		/// </summary>
		public static void TryLogInformation(string message) {
			var client = GetWebClient();
			try {
				client.Open();
				client.LogMessage(LogType.Information, message);
			}
			catch { return; }
			finally { client.Close(); }

		}

		/// <summary>
		/// Displays an Error message-box on screen with given message
		/// </summary>
		public static void DisplayErrorMessage(string message) {
			MessageBox.Show(
				"Ett fel har uppstått:\n" + message,
				"Ett fel har uppstått",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
				);
		}

		/// <summary>
		/// Displays an Error message-box on screen with given exception message
		/// </summary>
		public static void DisplayErrorMessage(Exception ex) {
			MessageBox.Show(
				"Ett fel har uppstått:\n" + ex.Message,
				"Ett fel har uppstått",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
				);
		}
		
	}
}