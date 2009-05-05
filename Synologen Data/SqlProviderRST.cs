// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: SqlProviderRST.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/SqlProviderRST.cs $
//
//  VERSION
//	$Revision: 2 $
//
//  DATES
//	Last check in: $Date: 09-01-13 17:53 $
//	Last modified: $Modtime: 09-01-13 14:31 $
//
//  AUTHOR(S)
//	$Author: Cber $
// 	
//
//  COPYRIGHT
// 	Copyright (c) 2009 Spinit AB --- ALL RIGHTS RESERVED
// 	Spinit AB, Datavägen 2, 436 32 Askim, SWEDEN
//
// ==========================================================================
// 
//  DESCRIPTION
//  
//
// ==========================================================================
//
//	History
//
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/SqlProviderRST.cs $
//
//2     09-01-13 17:53 Cber
//
//1     09-01-08 18:08 Cber
// 
// ==========================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Utility.Business;
namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public DataSet GetCompanyRSTs(int rstId, int companyId, string orderBy) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
					new SqlParameter ("@rstId", SqlDbType.Int, 4),
					new SqlParameter ("@CompanyId", SqlDbType.Int, 4),
					new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255),
					new SqlParameter ("@status", SqlDbType.Int, 4)
				};
				parameters[counter++].Value = rstId;
				parameters[counter++].Value = companyId;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter].Direction = ParameterDirection.Output;
				DataSet retSet = RunProcedure("spSynologenGetCompanyRSTs", parameters, "tblSynologenRst");
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public bool AddUpdateDeleteRST(Enumerations.Action action, ref RSTRow rst) {
			try {
				int numAffected = 0;
				SqlParameter[] parameters = {
		            new SqlParameter("@type", SqlDbType.Int, 4),
					new SqlParameter("@companyId", SqlDbType.Int, 4),
		            new SqlParameter("@name", SqlDbType.NVarChar, 50),
		            new SqlParameter("@status", SqlDbType.Int, 4),
		            new SqlParameter("@id", SqlDbType.Int, 4)
		        };

				int counter = 0;
				parameters[counter++].Value = (int)action;
				if (action == Enumerations.Action.Create || action == Enumerations.Action.Update) {
					parameters[counter++].Value = rst.CompanyId;
					parameters[counter++].Value = rst.Name ?? SqlString.Null;

				}
				parameters[parameters.Length - 2].Direction = ParameterDirection.Output;
				if (action == Enumerations.Action.Create) {
					parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
				}
				else {
					parameters[parameters.Length - 1].Value = rst.Id;
				}
				RunProcedure("spSynologenAddUpdateDeleteRST", parameters, out numAffected);

				if (((int)parameters[parameters.Length - 2].Value) == 0) {
					rst.Id = (int)parameters[parameters.Length - 1].Value;
					return true;
				}
				throw new GeneralData.DatabaseInterface.DataException("Insert failed. Error: " + (int)parameters[parameters.Length - 2].Value, (int)parameters[parameters.Length - 2].Value);
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}

		public RSTRow GetCompanyRST(int rstId) {
			try {
				DataSet rstDataSet = GetCompanyRSTs(rstId,0,null);
				DataRow rstDataRow = rstDataSet.Tables[0].Rows[0];
				RSTRow rstRow = new RSTRow();
				rstRow.Name = Util.CheckNullString(rstDataRow, "cName");
				rstRow.CompanyId = Util.CheckNullInt(rstDataRow, "cCompanyId");
				rstRow.Id = Util.CheckNullInt(rstDataRow, "cId");
				return rstRow;
			}
			catch (Exception ex) {
				throw new Exception("Exception found while parsing a RSTRow object: " + ex.Message);
			}
		}
		
	}
}