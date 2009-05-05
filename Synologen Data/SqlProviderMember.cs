// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: SqlProviderMember.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/SqlProviderMember.cs $
//
//  VERSION
//	$Revision: 6 $
//
//  DATES
//	Last check in: $Date: 09-03-04 14:18 $
//	Last modified: $Modtime: 09-03-02 12:32 $
//
//  AUTHOR(S)
//	$Author: Cber $
// 	
//
//  COPYRIGHT
// 	Copyright (c) 2008 Spinit AB --- ALL RIGHTS RESERVED
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Data/SqlProviderMember.cs $
//
//6     09-03-04 14:18 Cber
//
//5     09-01-21 17:50 Cber
//
//4     09-01-16 18:15 Cber
//
//3     09-01-09 17:44 Cber
//
//2     09-01-08 18:08 Cber
//
//1     08-12-16 17:01 Cber
// 
// ==========================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Data {
	public partial class SqlProvider {

		public DataSet GetSynologenMembersByPage(int type,
			int locationId,
			int languageId,
			int categoryId,
			int shopId,
			string searchString,
			string orderBy,
			int currentPage,
			int pageSize,
			ref int totalSize) {

			try {
				int counter = 0;
				DataSet retSet = null;

				SqlParameter[] parameters = {
                    new SqlParameter ("@type", SqlDbType.Int, 4),
                    new SqlParameter ("@LocationId", SqlDbType.Int, 4),
                    new SqlParameter ("@LanguageId", SqlDbType.Int, 4),
                    new SqlParameter ("@CategoryId", SqlDbType.Int, 4),
					new SqlParameter ("@shopId", SqlDbType.Int, 4),
                    new SqlParameter ("@SearchString", SqlDbType.NVarChar, 255),
                    new SqlParameter ("@OrderBy", SqlDbType.NVarChar, 255),
                    new SqlParameter ("@CurrentPage", SqlDbType.Int, 4),
                    new SqlParameter ("@PageSize", SqlDbType.Int, 4)
                };
				parameters[counter++].Value = type;
				parameters[counter++].Value = locationId;
				parameters[counter++].Value = languageId;
				parameters[counter++].Value = categoryId;
				parameters[counter++].Value = shopId;
				parameters[counter++].Value = searchString ?? SqlString.Null;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				parameters[counter++].Value = currentPage;
				parameters[counter++].Value = pageSize;
				retSet = RunProcedure("spSynologenGetMembersByPage",parameters,_TblMembers);
				if (retSet.Tables.Count > 1)
					totalSize = Convert.ToInt32(retSet.Tables[1].Rows[0][0]);
				return retSet;

			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: "+ e);
			}

		}

		public DataSet GetSynologenMembers(int memberId, int shopId, int categoryId, string orderBy) {
			try {
				int counter = 0;
				SqlParameter[] parameters = {
						new SqlParameter ("@memberId", SqlDbType.Int, 4),
						new SqlParameter ("@categoryId", SqlDbType.Int, 4),
						new SqlParameter ("@shopId", SqlDbType.Int, 4),
						new SqlParameter ("@orderBy", SqlDbType.NVarChar, 255)
					};
				parameters[counter++].Value = memberId;
				parameters[counter++].Value = categoryId;
				parameters[counter++].Value = shopId;
				parameters[counter++].Value = orderBy ?? SqlString.Null;
				DataSet retSet = RunProcedure("spSynologenGetmembers", parameters, "tblMembers");
				//TODO: Read status: parameters[counter]
				return retSet;
			}
			catch (SqlException e) {
				throw new GeneralData.DatabaseInterface.DataException("SqlException: " + e);
			}
		}


		/// <summary>
		/// An alternative GetMember method that is not tightly bound to location and language
		/// </summary>
		/// <param name="memberId"></param>
		/// <param name="currentLocationId">Users Logged in location</param>
		/// <param name="currentLanguageId">Users Logged in language</param>
		/// <returns>MemberRow object</returns>
		public MemberRow GetSynologenMember(int memberId, int currentLocationId, int currentLanguageId) {
			DataSet memberDataSet = GetSynologenMembers(memberId, 0, 0, null);
			DataRow memberDataRow = memberDataSet.Tables[0].Rows[0];
			MemberRow memberRow = new MemberRow();
			memberRow.OrgName = memberDataRow["cOrgName"].ToString();
			memberRow.Description = Util.CheckNullString(memberDataRow, "cDescription");
			memberRow.ContactFirst = Util.CheckNullString(memberDataRow, "cContactFirst");
			memberRow.ContactLast = Util.CheckNullString(memberDataRow, "cContactLast");
			memberRow.Address = Util.CheckNullString(memberDataRow, "cAddress");
			memberRow.Zip = Util.CheckNullString(memberDataRow, "cZipcode");
			memberRow.City = Util.CheckNullString(memberDataRow, "cCity");
			memberRow.Phone = Util.CheckNullString(memberDataRow, "cPhone");
			memberRow.Fax = Util.CheckNullString(memberDataRow, "cFax");
			memberRow.Mobile = Util.CheckNullString(memberDataRow, "cMobile");
			memberRow.Email = Util.CheckNullString(memberDataRow, "cEmail");
			memberRow.Web = Util.CheckNullString(memberDataRow, "cWww");
			memberRow.Voip = Util.CheckNullString(memberDataRow, "cVoip");
			memberRow.Skype = Util.CheckNullString(memberDataRow, "cSkype");
			memberRow.Cordless = Util.CheckNullString(memberDataRow, "cCordless");
			memberRow.Active = Convert.ToBoolean(memberDataRow["cActive"]);
			memberRow.Body = Util.CheckNullString(memberDataRow, "cBody");
			memberRow.Id = (int)memberDataRow["cId"];
			memberRow.Other1 = Util.CheckNullString(memberDataRow, "cOther1");
			memberRow.Other2 = Util.CheckNullString(memberDataRow, "cOther2");
			memberRow.Other3 = Util.CheckNullString(memberDataRow, "cOther3");
			memberRow.ProfilePictureId = Util.CheckNullInt(memberDataRow, "cProfilePictureId");
			memberRow.DefaultDirectoryId = Util.CheckNullInt(memberDataRow, "cDefaultDirectoryId");

			memberRow.CreatedBy = Util.CheckNullString(memberDataRow, "cCreatedBy");
			if (!memberDataRow.IsNull("cCreatedDate")) {
				memberRow.CreatedDate = Convert.ToDateTime(memberDataRow["cCreatedDate"]);
			}
			memberRow.EditedBy = Util.CheckNullString(memberDataRow, "cEditedBy");
			if (!memberDataRow.IsNull("cEditedDate")) {
				memberRow.EditedDate = Convert.ToDateTime(memberDataRow["cEditedDate"]);
			}
			memberRow.ApprovedBy = Util.CheckNullString(memberDataRow, "cApprovedBy");
			if (!memberDataRow.IsNull("cApprovedDate")) {
				memberRow.ApprovedDate = Convert.ToDateTime(memberDataRow["cApprovedDate"]);
			}
			memberRow.LockedBy = Util.CheckNullString(memberDataRow, "cLockedBy");
			if (!memberDataRow.IsNull("cLockedDate")) {
				memberRow.CreatedDate = Convert.ToDateTime(memberDataRow["cLockedDate"]);
			}

			memberRow.LocationList = GetConnectedLocationList(memberRow.Id);

			memberRow.CategoryList = GetConnectedCategoriesList(memberRow.Id, currentLocationId, currentLanguageId);

			return memberRow;
		}

		public bool CategoryHasConnectedMembers(int categoryId) {
			DataSet memberDataSet = GetSynologenMembers(0, 0, categoryId, null);
			return DataSetHasRows(memberDataSet);
		}

		public bool MemberHasConnectedOrders(int memberId) {
			DataSet orderDataSet = GetOrders(0, 0, 0, memberId, 0, 0, 0, null);
			return DataSetHasRows(orderDataSet);
		}

		public void UpdateMemberUserDetails(int memberId,string newPassword,string email, bool active, string updatingUser) {
			int userId = GetUserId(memberId);
			User dbUser = new User(Base.Business.Globals.ConnectionString);
			UserRow user = dbUser.GetUser(userId);
			dbUser.Update(userId,
			  newPassword,
			  user.FirstName,
			  user.LastName,
			  email,
			  user.DefaultLocation,
			  active,
			  updatingUser);
		}

		public UserRow GetUserRow(int memberId) {
			User dbUser = new User(Base.Business.Globals.ConnectionString);
			int userId = GetUserId(memberId);
			return dbUser.GetUser(userId);
		}

	}

}