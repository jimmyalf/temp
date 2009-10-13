using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Synologen.Business.Enumeration;
using Spinit.Wpc.Utility.Business;
namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public DataSet SynologenGetFileCategories(FileCategoryGetAction type, int categoryId) {
			try {
				DataSet retSet = null;

				SqlParameter[] parameters = {
					 new SqlParameter ("@type", SqlDbType.Int, 4),
					 new SqlParameter ("@categoryid", SqlDbType.Int, 4),
					 new SqlParameter ("@status", SqlDbType.Int, 4)
											 };
				parameters[0].Value = type;
				parameters[1].Value = categoryId;
				parameters[2].Direction = ParameterDirection.Output;

				retSet = RunProcedure("spSynologenGetFileCategories",parameters,_TblFileCategory);

				if (((int)parameters[2].Value) == 0) {return retSet;}
				throw new GeneralData.DatabaseInterface.DataException
					("Select failed. Error: "+ (int)parameters[2].Value,(int)parameters[2].Value);
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: "+ e);
			}
		}

		public FileCategoryRow GetSynologenFileCategory(int categoryId) {
			DataSet dsCategory = SynologenGetFileCategories(FileCategoryGetAction.Specific, categoryId);
			if (dsCategory.Tables[_TblFileCategory].Equals(null)) {
				return null;
			}
			if (dsCategory.Tables[_TblFileCategory].Rows.Count <= 0) {
				return null;
			}
			DataRow tblRow = dsCategory.Tables[_TblFileCategory].Rows[0];
			return GetFileCategoryRow(tblRow);
			//FileCategoryRow categoryRow = new FileCategoryRow();
			//categoryRow.Id = (int)tblRow["cId"];
			//categoryRow.Name = (string)tblRow["cDescription"];
			//return categoryRow;
		}

		public bool AddUpdateDeleteSynologenFileCategory(Enumerations.Action action,FileCategoryRow category) {
			try {
				int numAffected;

				SqlParameter[] parameters = {
					 new SqlParameter ("@type", SqlDbType.Int, 4),
					 new SqlParameter ("@name", SqlDbType.NVarChar, 255),
					 new SqlParameter ("@status", SqlDbType.Int, 4),											
					 new SqlParameter ("@id", SqlDbType.Int, 4)
				 };
				parameters[0].Value = action;
				if ((action == Enumerations.Action.Create)|| (action == Enumerations.Action.Update)) {
					parameters[1].Value = category.Name;
				}
				parameters[2].Direction = ParameterDirection.Output;
				if (action == Enumerations.Action.Create) {
					parameters[3].Direction = ParameterDirection.Output;
				}
				else {
					parameters[3].Value = category.Id;
				}

				RunProcedure("spSynologenAddUpdateDeleteFileCategory",parameters,out numAffected);
				if (((int)parameters[2].Value) == 0) {
					category.Id = (int)parameters[3].Value;
					return true;
				}

				throw new GeneralData.DatabaseInterface.DataException
					("Insert failed. Error: "+ (int)parameters[2].Value,(int)parameters[2].Value);
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: "+ e);
			}
		}

		public List<FileCategoryRow> GetAllSynologenFileCategoriesList() {
			List<FileCategoryRow> catList = new List<FileCategoryRow>();
			DataSet ds = SynologenGetFileCategories(FileCategoryGetAction.All, 0);
			if (ds.Tables[_TblFileCategory].Equals(null)) {
				return catList;
			}
			if (ds.Tables[_TblFileCategory].Rows.Count <= 0) {
				return catList;
			}
			foreach (DataRow tblRow in ds.Tables[_TblFileCategory].Rows) {
				catList.Add(GetFileCategoryRow(tblRow));
			}
			return catList;
		}

		protected static FileCategoryRow GetFileCategoryRow(DataRow tblRow) {
			FileCategoryRow row = new FileCategoryRow();
			row.Id = (int)tblRow["cId"];
			row.Name = Convert.ToString(tblRow["cDescription"]);
			return row;
		}
	}
}