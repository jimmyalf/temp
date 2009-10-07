// ==========================================================================
//
//  PROGRAM
//	
//
//  FILENAME
//	$Workfile: SynologenUtility.cs $
//
//  ARCHIVE
//  $Archive: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Admin/Code/SynologenUtility.cs $
//
//  VERSION
//	$Revision: 3 $
//
//  DATES
//	Last check in: $Date: 08-12-22 17:22 $
//	Last modified: $Modtime: 08-12-22 14:37 $
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
//	$Log: /Develop/WPC/Version 4.1/CustomerSpecific/Synologen/Synologen Admin/Code/SynologenUtility.cs $
//
//3     08-12-22 17:22 Cber
//
//2     08-12-19 17:22 Cber
//
//1     08-12-16 17:01 Cber
// 
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace Spinit.Wpc.Synologen.Presentation.Code {
	public static class Utility{
		public static bool FindMatchInLists(List<int> firstList,List<int> secondList) {
				foreach(var number in firstList) {
					if (secondList.Contains(number)) return true;
				}
				return false;
		}

		public static int GetSelectedGridViewDataKeyId(GridView gridView, GridViewDeleteEventArgs eventArgs) {
			var index = eventArgs.RowIndex;
			if (gridView == null) throw new ArgumentNullException("gridView");
			if (gridView.DataKeys == null) throw new ArgumentException("Gridview does not contain any datakeys.");
			return (int)gridView.DataKeys[index].Value;
		}

		public static int GetSelectedGridViewDataKeyId(GridView gridView, GridViewEditEventArgs eventArgs) {
			var index = eventArgs.NewEditIndex;
			if (gridView == null) throw new ArgumentNullException("gridView");
			if (gridView.DataKeys == null) throw new ArgumentException("Gridview does not contain any datakeys.");
			return (int)gridView.DataKeys[index].Value;
		}

		public static int GetSelectedGridViewDataKeyId(GridView gridView, GridViewCommandEventArgs eventArgs) {
			var index = Convert.ToInt32(eventArgs.CommandArgument);
			if (gridView == null) throw new ArgumentNullException("gridView");
			if (gridView.DataKeys == null) throw new ArgumentException("Gridview does not contain any datakeys.");
			return (int)gridView.DataKeys[index].Value;
		}

		public static void SetActiveGridViewControl(GridView gridView, DataSet ds, string columnName, string imageControlName, string descriptionOn, string descriptionOff) {
			var i = 0;
			foreach (GridViewRow row in gridView.Rows) {
				var active = Convert.ToBoolean(ds.Tables[0].Rows[i][columnName]);
				if (row.FindControl(imageControlName) != null) {
					var img = (Image)row.FindControl(imageControlName);
					if (active) {
						img.ImageUrl = "~/common/icons/True.png";
						img.AlternateText = descriptionOn;
						img.ToolTip = descriptionOn;
					}
					else {
						img.ImageUrl = "~/common/icons/False.png";
						img.AlternateText = descriptionOff;
						img.ToolTip = descriptionOff;
					}
				}
				i++;
			}
		}
	}
}