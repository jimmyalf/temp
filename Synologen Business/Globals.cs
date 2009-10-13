using System;
using System.Collections.Generic;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Business {
	public class Globals : ConfigurationFetch {
		private const String _setting = "synologen";
		/// <summary>
		/// Specifies the component Name used in the component db
		/// </summary>

		static public String ComponentName {
			get { return SafeConfigString(_setting, "name", string.Empty); }
		}


		/// <summary>
		/// Returns the current application Path for the component
		/// </summary>
		static public string ComponentApplicationPath {
			get { return SafeConfigString(_setting, "componentApplicationPath", "/"); }
		}

		/// <summary>
		/// Master member category Id
		/// (Connects a store to all ContractCustomers by default)
		/// </summary>
		static public int MasterShopCategoryId {
			get { return SafeConfigNumber(_setting, "masterShopCategoryId", 1); }
		}


		static public int DefaultNewOrderStatus {
			get { return SafeConfigNumber(_setting, "defaultNewOrderStatus", 1); }
		}

		static public int DefaultOrderStatusAfterSPCSInvoice {
			get { return SafeConfigNumber(_setting, "defaultOrderStatusAfterSPCSInvoice", 2); }
		}

		
		/// <summary>
		/// Master member category Id
		/// (Connects a store to all ContractCustomers by default)
		/// </summary>
		static public List<int> CategoriesWithoutShops {
			get {
				string configString = SafeConfigString(_setting, "categoriesWithoutShops", String.Empty);
				List<string> list =  ParseCommaSeparatedList(configString);
				return ParseStringListToIntList(list);
			}
		}

		/// <summary>
		/// Returns a list of status id's which orders are editable
		/// </summary>
		static public List<int> EditableOrderStatusList {
			get {
				string configString = SafeConfigString(_setting, "editableOrderStatusList", String.Empty);
				List<string> list =  ParseCommaSeparatedList(configString);
				return ParseStringListToIntList(list);
			}
		}

		/// <summary>
		/// Returns an abort status id
		/// </summary>
		static public int AbortStatusId {
			get { return SafeConfigNumber(_setting, "abortStatusId", -1); }
		}

		/// <summary>
		/// Returns an abort status id
		/// </summary>
		static public int HaltedStatusId {
			get { return SafeConfigNumber(_setting, "haltedStatusId", -1); }
		}

		/// <summary>
		/// Returns an id that should be used for filtering orders ready for settlement
		/// </summary>
		static public int ReadyForSettlementStatusId {
			get { return SafeConfigNumber(_setting, "readyForSettlementStatusId", -1); }
		}

		/// <summary>
		/// Returns an id that should be used to set status id after a settlement
		/// </summary>
		static public int DefaultOrderIdAfterSettlement {
			get { return SafeConfigNumber(_setting, "defaultOrderIdAfterSettlement", -1); }
		}

		private static List<string> ParseCommaSeparatedList(string inputString) {
			if (String.IsNullOrEmpty(inputString)) return new List<string>();
			return  new List<string>(inputString.Split(",".ToCharArray()));
		}

		private static List<int> ParseStringListToIntList(IEnumerable<string> inputList) {
			List<int> returnList = new List<int>();
			foreach (string parameter in inputList) {
				int value;
				if (Int32.TryParse(parameter, out value)) {
					returnList.Add(value);
				}
			}
			return returnList;
		}
		static public string EditOrderPage {
			get { return SafeConfigString(_setting, "editOrderPage", "/"); }
		}
		static public string ViewOrderPage {
			get { return SafeConfigString(_setting, "viewOrderPage", "/"); }
		}
		static public string EditMemberPage {
			get { return SafeConfigString(_setting, "editMemberPage", "/"); }
		}
		static public string ViewSettlementPage {
			get { return SafeConfigString(_setting, "viewSettlementPage", "/"); }
		}

		static public int CacheTimeout {
			get { return SafeConfigNumber(_setting, "cacheTimeout", 0); }
		}

		static public TimeSpan SecurityLogoutTimeout {
			get {
				int timeoutInMinutes = SafeConfigNumber(_setting, "securityLogoutTimeout", 0);
				return TimeSpan.FromMinutes(timeoutInMinutes);
			}
		}
		
	}
}