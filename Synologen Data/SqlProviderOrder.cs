using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Utility.Business;
namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public DataSet GetOrdersByPage(int contractId, int statusId, int settlementId, DateTime intervalStart, DateTime intervalEnd, string searchString, string orderBy, int currentPage, int pageSize, ref int totalSize) {

			try {
				int counter = 0;

				SqlParameter[] parameters = {
					new SqlParameter ("@contractId", SqlDbType.Int, 4),
					new SqlParameter ("@statusId", SqlDbType.Int, 4),
					new SqlParameter ("@settlementId", SqlDbType.Int, 4),
					new SqlParameter ("@createDateStartLimit", SqlDbType.SmallDateTime),
					new SqlParameter ("@createDateStopLimit", SqlDbType.SmallDateTime),
					new SqlParameter ("@SearchString", SqlDbType.NVarChar, 255),
					new SqlParameter ("@OrderBy", SqlDbType.NVarChar, 255),
					new SqlParameter ("@CurrentPage", SqlDbType.Int, 4),
					new SqlParameter ("@PageSize", SqlDbType.Int, 4)
				};
				parameters[counter++].Value = contractId;
				parameters[counter++].Value = statusId;
				parameters[counter++].Value = settlementId;
				parameters[counter++].Value = intervalStart == DateTime.MinValue ? SqlDateTime.Null : intervalStart;
				parameters[counter++].Value = intervalEnd == DateTime.MinValue ? SqlDateTime.Null : intervalEnd;
				parameters[counter++].Value = searchString ?? SqlString.Null;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter++].Value = currentPage;
				parameters[counter].Value = pageSize;
				DataSet retSet = RunProcedure("spSynologenGetOrdersByPage", parameters, "tblSynologenOrder");
				if (retSet.Tables.Count > 1) totalSize = Convert.ToInt32(retSet.Tables[1].Rows[0][0]);
				return retSet;

			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}

		}

		public DataSet GetOrders(int orderId, int shopId, int contractId, int salesPersonMemberId, int companyId, long invoiceNumberId, int statusId, string orderBy) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
					new SqlParameter ("@orderId", SqlDbType.Int, 4),
					new SqlParameter ("@shopId", SqlDbType.Int, 4),
					new SqlParameter ("@contractId", SqlDbType.Int, 4),
					new SqlParameter ("@salesPersonMemberId", SqlDbType.Int, 4),
					new SqlParameter ("@companyId", SqlDbType.Int, 4),
					new SqlParameter ("@invoiceNumberId", SqlDbType.BigInt),
					new SqlParameter ("@statusId", SqlDbType.Int, 4),
					new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
					new SqlParameter ("@status", SqlDbType.Int, 4)
				};
				parameters[counter++].Value = orderId;
				parameters[counter++].Value = shopId;
				parameters[counter++].Value = contractId;
				parameters[counter++].Value = salesPersonMemberId;
				parameters[counter++].Value = companyId;
				parameters[counter++].Value = invoiceNumberId;
				parameters[counter++].Value = statusId;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter].Direction = ParameterDirection.Output;
				DataSet retSet = RunProcedure("spSynologenGetOrders", parameters, "tblSynologenOrder");
				//TODO: Read status: parameters[counter]
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		private DataSet GetOrdersForInvoicingDataSet(int statusId, int? invoicingMethodId, string orderBy) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
					new SqlParameter ("@statusId", SqlDbType.Int, 4),
					new SqlParameter ("@invoicingMethodIdFilter", SqlDbType.Int, 4),
					new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
					new SqlParameter ("@status", SqlDbType.Int, 4)
				};
				parameters[counter++].Value = statusId;
				parameters[counter++].Value = GetNullableSqlType(invoicingMethodId);
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter].Direction = ParameterDirection.Output;
				DataSet retSet = RunProcedure("spSynologenGetForInvoicing", parameters, "tblSynologenOrder");
				//TODO: Read status: parameters[counter]
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		private DataSet GetOrdersForStatusCheckDataSet(int statusId, string orderBy) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
					new SqlParameter ("@statusId", SqlDbType.Int, 4),
					new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
					new SqlParameter ("@status", SqlDbType.Int, 4)
				};
				parameters[counter++].Value = statusId;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter].Direction = ParameterDirection.Output;
				DataSet retSet = RunProcedure("spSynologenGetOrdersForStatusCheck", parameters, "tblSynologenOrder");
				//TODO: Read status: parameters[counter]
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public Order GetOrder(int orderId) {
			DataSet orderDataSet = GetOrders(orderId, 0, 0, 0, 0, 0, 0, null);
			DataRow orderDataRow = orderDataSet.Tables[0].Rows[0];
			return ParseOrderRow(orderDataRow);
		}

		public int GetNumberOfOrderWithSpecificStatus(int orderStatus) {
			DataSet orderDataSet = GetOrders(0, 0, 0, 0, 0, 0, orderStatus, null);
			if (orderDataSet == null) return 0;
			if (orderDataSet.Tables[0] == null) return 0;
			return orderDataSet.Tables[0].Rows.Count;
		}



		/// <summary>
		/// Fetches order all orders which have no invoice number filtered by
		/// status id if <param name="statusId"/> > 0
		/// </summary>
		public List<IOrder> GetOrdersForInvoicing(int statusId, int? invoicingMethodIdFilter,  string orderBy) {
			var listOfOrders = new List<IOrder>();
			try {
				var orderDataSet = GetOrdersForInvoicingDataSet(statusId, invoicingMethodIdFilter, orderBy);
				foreach (DataRow orderDataRow in orderDataSet.Tables[0].Rows) {
					listOfOrders.Add(ParseOrderRow(orderDataRow));
				}
				return listOfOrders;
			}
			catch { return listOfOrders; }

		}

		/// <summary>
		/// Fetches order all order invoice numbers filtered by status id
		/// if <param name="statusId"/> > 0
		/// </summary>
		public List<long> GetOrderInvoiceNumbers(int statusId, string orderBy) {
			List<long> listOfOrderIds = new List<long>();
			try {
				DataSet orderDataSet = GetOrdersForStatusCheckDataSet(statusId,orderBy);
				foreach (DataRow orderDataRow in orderDataSet.Tables[0].Rows) {
					if (!String.IsNullOrEmpty(orderDataRow["cInvoiceNumber"].ToString())) {
						listOfOrderIds.Add(long.Parse(orderDataRow["cInvoiceNumber"].ToString()));
					}
				}
				return listOfOrderIds;
			}
			catch { return listOfOrderIds; }
		}

		public void ChangeOrderStatus(int orderId, int newStatusId) {
			Order order = GetOrder(orderId);
			order.StatusId = newStatusId;
			AddUpdateDeleteOrder(Enumerations.Action.Update, ref order);
		}

		public bool AddUpdateDeleteOrder(Enumerations.Action action, ref Order order) {
			try {
				int numAffected;
				SqlParameter[] parameters = {
            		new SqlParameter("@type", SqlDbType.Int, 4),
					new SqlParameter("@companyId", SqlDbType.Int, 4),
            		//new SqlParameter("@rstId", SqlDbType.Int, 4),
					new SqlParameter("@rstText", SqlDbType.NVarChar, 50),
					new SqlParameter("@statusId", SqlDbType.Int, 4),
					new SqlParameter("@salesPersonMemberId", SqlDbType.Int, 4),
					new SqlParameter("@salesPersonShopId", SqlDbType.Int, 4),
            		new SqlParameter("@companyUnit", SqlDbType.NVarChar, 255),
            		new SqlParameter("@customerFirstName", SqlDbType.NVarChar, 50),
            		new SqlParameter("@customerLastName", SqlDbType.NVarChar, 50),
					new SqlParameter("@personalIdNumber", SqlDbType.NVarChar, 50),
            		new SqlParameter("@email", SqlDbType.NVarChar, 50),
            		new SqlParameter("@phone", SqlDbType.NVarChar, 50),
					new SqlParameter("@orderPayedToShop", SqlDbType.Bit),
					new SqlParameter("@invoiceNumber", SqlDbType.BigInt),
					new SqlParameter("@invoiceSumIncludingVAT", SqlDbType.Float),
					new SqlParameter("@invoiceSumExcludingVAT", SqlDbType.Float),
					new SqlParameter("@customerOrderNumber", SqlDbType.NVarChar, 50),
            		new SqlParameter("@status", SqlDbType.Int, 4),
            		new SqlParameter("@id", SqlDbType.Int, 4)
				};

				int counter = 0;
				parameters[counter++].Value = (int)action;
				if (action == Enumerations.Action.Create || action == Enumerations.Action.Update) {
					parameters[counter++].Value = order.CompanyId;
					//parameters[counter++].Value = order.RSTId <= 0 ? SqlInt32.Null : order.RSTId;
					parameters[counter++].Value = order.RstText ?? SqlString.Null;
					parameters[counter++].Value = order.StatusId;
					parameters[counter++].Value = order.SalesPersonMemberId;
					parameters[counter++].Value = order.SalesPersonShopId;
					parameters[counter++].Value = order.CompanyUnit ?? SqlString.Null;
					parameters[counter++].Value = order.CustomerFirstName ?? SqlString.Null;
					parameters[counter++].Value = order.CustomerLastName ?? SqlString.Null;
					parameters[counter++].Value = order.PersonalIdNumber ?? SqlString.Null;
					parameters[counter++].Value = order.Email ?? SqlString.Null;
					parameters[counter++].Value = order.Phone ?? SqlString.Null;
					parameters[counter++].Value = order.MarkedAsPayedByShop;
					parameters[counter++].Value = order.InvoiceNumber<=0 ? SqlInt64.Null : order.InvoiceNumber;
					parameters[counter++].Value = order.InvoiceSumIncludingVAT <= 0 ? SqlDouble.Null : order.InvoiceSumIncludingVAT;
					parameters[counter++].Value = order.InvoiceSumExcludingVAT <= 0 ? SqlDouble.Null : order.InvoiceSumExcludingVAT;
					parameters[counter++].Value = GetNullableSqlType(order.CustomerOrderNumber);
				}
				parameters[parameters.Length - 2].Direction = ParameterDirection.Output;
				if (action == Enumerations.Action.Create) {
					parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
				}
				else {
					parameters[parameters.Length - 1].Value = order.Id;
				}

				RunProcedure("spSynologenAddUpdateDeleteOrder", parameters, out numAffected);

				if (((int)parameters[parameters.Length - 2].Value) == 0) {
					order.Id = (int)parameters[parameters.Length - 1].Value;
					return true;
				}
				throw new GeneralData.DatabaseInterface.DataException("Insert failed. Error: " + (int)parameters[parameters.Length - 2].Value, (int)parameters[parameters.Length - 2].Value);
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public void UpdateOrderStatus(int newOrderStatusId, int filterOrderId, int filterShopId, int filterContractId, int filterSalesPersonMemberId, int filterCompanyId, long filterInvoiceNumberId) {
			try {
				int counter = 0;
				int numAffected;
				SqlParameter[] parameters = {
					new SqlParameter ("@newStatusId", SqlDbType.Int, 4),
					new SqlParameter ("@orderId", SqlDbType.Int, 4),
					new SqlParameter ("@shopId", SqlDbType.Int, 4),
					new SqlParameter ("@contractId", SqlDbType.Int, 4),
					new SqlParameter ("@salesPersonMemberId", SqlDbType.Int, 4),
					new SqlParameter ("@companyId", SqlDbType.Int, 4),
					new SqlParameter ("@invoiceNumberId", SqlDbType.BigInt),
					new SqlParameter ("@status", SqlDbType.Int, 4)
				};

				parameters[counter++].Value = newOrderStatusId;
				parameters[counter++].Value = filterOrderId;
				parameters[counter++].Value = filterShopId;
				parameters[counter++].Value = filterContractId;
				parameters[counter++].Value = filterSalesPersonMemberId;
				parameters[counter++].Value = filterCompanyId;
				parameters[counter++].Value = filterInvoiceNumberId;
				parameters[counter].Direction = ParameterDirection.Output;
				RunProcedure("spSynologenUpdateOrderStatus", parameters, out numAffected);
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public bool SetOrderInvoiceNumber(int orderId, long invoiceNumber, int newOrderStatusId, double invoiceSumIncludingVAT, double invoiceSumExcludingVAT) {
			if (orderId<=0 || invoiceNumber<=0) {
				return false;
			}
			Order order = GetOrder(orderId);
			if (order.InvoiceNumber > 0) throw new Exception("SqlProvider.SetOrderInvoiceNumber: Order already has an Invoice number");
			order.InvoiceNumber = invoiceNumber;
			order.StatusId = newOrderStatusId;
			order.InvoiceSumIncludingVAT = invoiceSumIncludingVAT;
			order.InvoiceSumExcludingVAT = invoiceSumExcludingVAT;
			return AddUpdateDeleteOrder(Enumerations.Action.Update, ref order);
		}

		//public bool SetOrderStatus(long invoiceNumber, int newOrderStatusId) {
		//    if (invoiceNumber <= 0) { return false;}
		//    Order order = GetOrder(orderId);
		//    if (order.InvoiceNumber > 0) throw new Exception("SqlProvider.SetOrderInvoiceNumber: Order already has an Invoice number");
		//    order.InvoiceNumber = invoiceNumber;
		//    order.StatusId = newOrderStatusId;
		//    return AddUpdateDeleteOrder(Enumerations.Action.Update, ref order);
		//}

		private static Order ParseOrderRow(DataRow orderDataRow) {
			try {
				var orderRow = new Order {Id = Util.CheckNullInt(orderDataRow, "cId"), RstText = Util.CheckNullString(orderDataRow, "cRstText"), StatusId = Util.CheckNullInt(orderDataRow, "cStatusId"), CompanyId = Util.CheckNullInt(orderDataRow, "cCompanyId"), SalesPersonMemberId = Util.CheckNullInt(orderDataRow, "cSalesPersonMemberId"), SalesPersonShopId = Util.CheckNullInt(orderDataRow, "cSalesPersonShopId"), CompanyUnit = Util.CheckNullString(orderDataRow, "cCompanyUnit"), CustomerFirstName = Util.CheckNullString(orderDataRow, "cCustomerFirstName"), CustomerLastName = Util.CheckNullString(orderDataRow, "cCustomerLastName"), PersonalIdNumber = Util.CheckNullString(orderDataRow, "cPersonalIdNumber"), Email = Util.CheckNullString(orderDataRow, "cEmail"), Phone = Util.CheckNullString(orderDataRow, "cPhone")};
				//orderRow.RSTId = Util.CheckNullInt(orderDataRow, "cRstId");
				if (!String.IsNullOrEmpty(orderDataRow["cInvoiceNumber"].ToString())){
					orderRow.InvoiceNumber = long.Parse(orderDataRow["cInvoiceNumber"].ToString());
				}
				if (Util.CheckDateTimeInput(orderDataRow["cCreatedDate"].ToString())) {
					orderRow.CreatedDate = (DateTime)orderDataRow["cCreatedDate"];
				}
				if (Util.CheckDateTimeInput(orderDataRow["cUpdatedDate"].ToString())) {
					orderRow.UpdateDate = (DateTime)orderDataRow["cUpdatedDate"];
				}
				if (!String.IsNullOrEmpty(orderDataRow["cInvoiceSumIncludingVAT"].ToString())) {
					orderRow.InvoiceSumIncludingVAT = float.Parse(orderDataRow["cInvoiceSumIncludingVAT"].ToString());
				}
				if (!String.IsNullOrEmpty(orderDataRow["cInvoiceSumExcludingVAT"].ToString())) {
					orderRow.InvoiceSumExcludingVAT = float.Parse(orderDataRow["cInvoiceSumExcludingVAT"].ToString());
				}
				orderRow.CustomerOrderNumber = Util.CheckNullString(orderDataRow, "cCustomerOrderNumber");

				orderRow.MarkedAsPayedByShop = (bool)orderDataRow["cOrderMarkedAsPayed"];

				return orderRow;
			}
			catch (Exception ex) {
				throw new Exception("Exception found while parsing a Order object: " + ex.Message);
			}
		}
	}
}