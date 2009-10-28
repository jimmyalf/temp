using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Data{
	public partial class SqlProvider{
		private DataSet GetCountryDataSet(int? countryId, string orderBy) {
			try {
				var counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@countryId", SqlDbType.Int, 4),
						new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
						new SqlParameter ("@status", SqlDbType.Int, 4)
					};
				parameters[counter++].Value = GetNullableSqlType(countryId);
				parameters[counter++].Value = GetNullableSqlType(orderBy);
				parameters[counter].Direction = ParameterDirection.Output;
				var retSet = RunProcedure("spSynologenGetCountries", parameters, "tblSynologenConcern");
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException while fetching country data set ", e);
			}
		}
		private static CountryRow ParseCountryRow(DataRow dataRow){
			var concernRow = new CountryRow
			{
				Id = Util.CheckNullInt(dataRow, "cId"),
				Name = Util.CheckNullString(dataRow, "cName"),
				OrganizationCountryCode = (CountryIdentificationCodeContentType) Util.CheckNullInt(dataRow, "cSvefakturaCountryCode")
			};
			return concernRow;
		}
		public CountryRow GetCountryRow(int countryId) {
			try {
				var shopDataSet = GetCountryDataSet(countryId,"cId");
				var shopDataRow = shopDataSet.Tables[0].Rows[0];
				return ParseCountryRow(shopDataRow);
			}
			catch (Exception ex) {
				throw new Exception("Exception found while parsing a CountryRow object.", ex);
			}
		}
		private IList<CountryRow> GetCountryRows(){
			var dataSet = GetCountryDataSet(null, null);
			if(dataSet == null || dataSet.Tables == null || dataSet.Tables.Count <= 0) return new List<CountryRow>();
			if(dataSet.Tables[0] == null || dataSet.Tables[0].Rows == null) return new List<CountryRow>();
			var returnList = new List<CountryRow>();
			foreach (DataRow dataRow in dataSet.Tables[0].Rows){
				returnList.Add(ParseCountryRow(dataRow));
			}
			return returnList;
		}
		public IList<CountryRow> GetCountryRows(Func<CountryRow,string> orderBy){
			var returnList = GetCountryRows();
			return (orderBy == null) ? returnList : returnList.OrderBy(orderBy).ToList();
		}
	}
}