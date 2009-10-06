using System;
using Spinit.Wpc.Synologen.Business.Interfaces;

namespace Spinit.Wpc.Synologen.Visma.Utility {
	public static class Formatting {
		public static string BuildOrderRstDescription(IOrder order, int maxLength) {
			var rstDescription = String.IsNullOrEmpty(order.RstText) ? String.Empty : String.Concat("RST: ", order.RstText);
			var unitDescription = String.IsNullOrEmpty(order.CompanyUnit) ? String.Empty : String.Concat("Enhet: ", order.CompanyUnit);
			if(!String.IsNullOrEmpty(rstDescription) && !String.IsNullOrEmpty(unitDescription)){
				return TruncateString(rstDescription+ ", " + unitDescription, maxLength);
			}
			if(!String.IsNullOrEmpty(rstDescription)){
				return TruncateString(rstDescription, maxLength);
			}
			if(!String.IsNullOrEmpty(unitDescription)){
				return TruncateString(unitDescription, maxLength);
			}
			return null;
		}

		public static string BuildOrderCustomerDescription(IOrder order, int maxLength) {
			var personalID = String.IsNullOrEmpty(order.PersonalIdNumber) ? String.Empty : order.PersonalIdNumber;
			var firstName = String.IsNullOrEmpty(order.CustomerFirstName) ? String.Empty : order.CustomerFirstName;
			var lastName = String.IsNullOrEmpty(order.CustomerLastName) ? String.Empty : order.CustomerLastName;
			if(!String.IsNullOrEmpty(personalID) && !String.IsNullOrEmpty(firstName) && !String.IsNullOrEmpty(lastName)){
				return TruncateString("Kund: " + personalID + ", " + firstName + " " + lastName, maxLength);
			}
			if( !String.IsNullOrEmpty(firstName) && !String.IsNullOrEmpty(lastName)){
				return TruncateString("Kund: " + firstName + " " + lastName, maxLength);
			}
			if(!String.IsNullOrEmpty(personalID) &&  !String.IsNullOrEmpty(lastName)){
				return TruncateString("Kund: " + personalID + ", " + lastName, maxLength);
			}
			if(!String.IsNullOrEmpty(personalID) && !String.IsNullOrEmpty(firstName) ){
				return TruncateString("Kund: " + personalID + ", " + firstName, maxLength);
			}
			if( !String.IsNullOrEmpty(personalID)){
				return TruncateString("Kund: " + personalID, maxLength);
			}
			if(!String.IsNullOrEmpty(firstName)){
				return TruncateString("Kund: " + firstName, maxLength);
			}
			if(!String.IsNullOrEmpty(lastName)){
				return TruncateString("Kund: " + lastName, maxLength);
			}
			return null;
		}

		public static string BuildOrderIdDescription(IOrder order, int maxLength) {
			return TruncateString("WPC OrderId: " + order.Id, maxLength);
		}

		public static string TruncateString (string value, int maxLength) {
			return value.Length <= maxLength ? value : value.Substring(0, maxLength);
		}

		public static string BuildItemDescription(IOrderItem orderItem) {
			return orderItem.ArticleDisplayNumber + " " + orderItem.ArticleDisplayName;
		}
	}
}