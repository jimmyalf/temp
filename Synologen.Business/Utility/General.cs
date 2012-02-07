using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;

namespace Spinit.Wpc.Synologen.Business.Utility {
	public class General {
		public static bool ValidatePersonalIDNumber(string number) {
			try {
				number = number.Replace("-", "");
				if (number.Length != 12) throw new ArgumentOutOfRangeException();
				number = number.Remove(0, 2); //remove first two year characters
				int sum = 0;
				for (int i = 0; i < 10; i++) {
					int temp = Convert.ToInt32(number.Substring(i, 1)) * (((i + 1) % 2) + 1);
					if (temp > 9) temp = temp - 9;
					sum += temp;
				}
				return (sum % 10).Equals(0);
			}
			catch {
				return false;
			}
		}

		/// <summary>
		/// Workaround since regular .net method is incorrect.
		/// Reference: http://blogs.msdn.com/shawnste/archive/2006/01/24/iso-8601-week-of-year-format-in-microsoft-net.aspx
		/// </summary>
		/// <param name="dateTime">Input date</param>
		/// <returns>Weeknumber</returns>
		private static int GetIso8601WeekOfYear(DateTime dateTime) {
			var currentCulture = CultureInfo.CurrentCulture;
			var day = currentCulture.Calendar.GetDayOfWeek(dateTime);
			if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday) {
				dateTime = dateTime.AddDays(3);
			}
			return currentCulture.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
		}

		public static string GetSettlementPeriodNumber(DateTime date) {
			string weekNumber = GetIso8601WeekOfYear(date).ToString().PadLeft(2,'0');
			return date.ToString("yy") + weekNumber;
		}

		public static float ParseIDataItemContainerToFloat(IDataItemContainer dataItem, string identifier, RoundDecimals roundSetting) {
			float returnValue;
			string stringValue = ((DataRowView)dataItem.DataItem).Row[identifier].ToString();
			stringValue = stringValue.Replace(',', '.');
			Single.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out returnValue);
			return Numerical.RoundValue(returnValue,roundSetting);
		}


		public static bool IsOrderEditable(int orderStatusId) {
			List<int> listOfEditableStatuses = Session.GetSessionIntList("ListOfEditableStatuses");
			if (listOfEditableStatuses.Count == 0) {
				listOfEditableStatuses = Globals.EditableOrderStatusList;
				Session.SetSessionValue("ListOfEditableStatuses", listOfEditableStatuses);
			}
			return listOfEditableStatuses.Contains(orderStatusId);
		}
		public static bool IsOrderHalted(int orderStatusId) {
			int haltedStatus = Session.GetSessionValue("HaltedStatus", -1);
			if (haltedStatus == -1) {
				haltedStatus = Globals.HaltedStatusId;
				Session.SetSessionValue("HaltedStatus", haltedStatus);
			}
			return (haltedStatus == orderStatusId);
		}

		public static IList<CartOrderItem> ParseList(IEnumerable<OrderItem> orderItems){
			var internalList = new List<OrderItem>(orderItems);
			var returnList = new List<CartOrderItem>();
			internalList.ForEach(x => returnList.Add(new CartOrderItem(x)));
			return returnList;
		}
	}
}