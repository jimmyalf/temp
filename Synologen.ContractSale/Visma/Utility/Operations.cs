using System;
using AdkNetWrapper;

namespace Spinit.Wpc.Synologen.Visma.Utility {
	public class Operations {

		public static void OpenCompany(ref string commonFilesPath, ref string companyName) {
			var err = Api.AdkOpen(ref commonFilesPath, ref companyName);
			HandleError(err);
		}

		public static void CloseCompany() {Api.AdkClose();}

		public static void SetString(int dataPointer, int fieldId, ref string value) {
			var err = Api.AdkSetStr(dataPointer, fieldId, ref value);
			HandleError(err);
		}

		public static void SetDouble(int dataPointer, int fieldId, double value) {
			var err = Api.AdkSetDouble(dataPointer, fieldId, value);
			HandleError(err);
		}

		public static void SetData(int dataPointer, int fieldId, int value) {
			var err = Api.AdkSetData(dataPointer, fieldId, value);
			HandleError(err);
		}

		public static void SetBool(int dataPointer, int fieldId, bool value) {
			var intValue = value ? 1 : 0;
			var err = Api.AdkSetBool(dataPointer, fieldId, intValue);
			HandleError(err);
		}

		public static void GetDouble(int dataPointer, int fieldId, ref double value) {
			var err = Api.AdkGetDouble(dataPointer, fieldId, ref value);
			HandleError(err);
		}

		public static void GetDateTime(int dataPointer, int fieldId, ref DateTime dateReference) {
			var value = 0;
			var err = Api.AdkGetDate(dataPointer, fieldId, ref value);
			HandleError(err);
			var dateString = new String(' ', 11);
			err = Api.AdkLongToDate(value, ref dateString, 11);
			HandleError(err);
			dateReference = DateTime.Parse(dateString);
		}

		public static void GetBool(int dataPointer, int fieldId, ref bool value) {
			var localValue = 0;
			var err = Api.AdkGetBool(dataPointer, fieldId, ref localValue);
			HandleError(err);
			value = (localValue > 0);

		}

		public static void GetString(int dataPointer, int fieldId, ref string value, int stringLength) {
			value = new String(' ', stringLength);
			var err = Api.AdkGetStr(dataPointer, fieldId, ref value, stringLength);
			HandleError(err);

		}

		public static DateTime SafeGetDateTime(int dataPointer, int fieldId) {
			var returnValue = DateTime.MinValue;
			try {
				GetDateTime(dataPointer, fieldId, ref returnValue);
				return returnValue;
			}
			catch { return DateTime.MinValue; }
		}

		public static void FetchDataIntoPointer(int dataPointer, bool fetchHeadOnly) {
			var err = fetchHeadOnly ? Api.AdkFindEx(dataPointer, 0) : Api.AdkFind(dataPointer);
			HandleError(err);
		}

		public static void CommitData(int dataPointer) {
			var err = Api.AdkAdd(dataPointer);
			HandleError(err);
		}

		public static void UpdateData(int dataPointer) {
			var err = Api.AdkUpdate(dataPointer);
			HandleError(err);
		}

		public static void HandleError(Api.ADKERROR vismaError) {
			if(vismaError.lRc == Api.ADKE_OK) return;
			var errorText = new String(' ', 200);
			const int errType = (int) Api.ADK_ERROR_TEXT_TYPE.elRc;
			Api.AdkGetErrorText(ref vismaError, errType, ref errorText, 200);

			throw (new VismaException(errorText));
		}
	}
}