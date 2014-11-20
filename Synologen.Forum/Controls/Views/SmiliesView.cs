using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Components;
using System.ComponentModel;
using System.IO;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Controls	{

	public class SmiliesView : DataList {

		public SmiliesView(){

			AlternatingItemTemplate	= Page.LoadTemplate( Globals.ApplicationPath + "/Themes/default/Skins/Admin/View-Smilies.ascx");
			EditItemTemplate		= Page.LoadTemplate( Globals.ApplicationPath + "/Themes/default/Skins/Admin/View-Smilies.ascx");
			HeaderTemplate			= Page.LoadTemplate( Globals.ApplicationPath + "/Themes/default/Skins/Admin/View-Smilies.ascx");
			FooterTemplate			= Page.LoadTemplate( Globals.ApplicationPath + "/Themes/default/Skins/Admin/View-Smilies.ascx");

			DataSource = Smilies.GetSmilies();
		}

		protected override void CreateChildControls() {
			base.CreateChildControls ();

			if( !Page.IsPostBack )
				DataBind();
		}

	}
/*
	///	<summary>
	///	This server	control	is used	to display all the members of the current forum.
	///	</summary>
	[
	ParseChildren(true)
	]
	public class SmiliesView : SkinnedForumWebControl {
		ArrayList	smiliesArray	= null;
		public DataList	smileyList;
//		TextBox		_txtSmileyCode;
//		TextBox		_txtSmileyUrl;
//		TextBox		_txtSmileyText;
//		int		_smileyToEdit;

//		TextBox	searchForUser;
//		Button searchButton; 
 
		// Define the default skin for this	control
		private	const string skinFilename =	"Admin/View-Smilies.ascx";
		
		public SmiliesView() {

			// Set the default skin
			if ( SkinFilename == null )
				SkinFilename = skinFilename;

		}

		/// <summary>
		/// 
		/// </summary>
		public override	void DataBind()	{

			if( smileyList.DataSource != null )
				return;

			smiliesArray = Smilies.GetSmilies();

			if( smiliesArray == null )
				return;

			smileyList.DataSource = smiliesArray;
			smileyList.DataBind();
		}

//		protected void BindList() {
//			smileyList.DataSource =	smiliesArray;
//
//			if( !Page.IsPostBack)
//				smileyList.DataBind();
//		}

		override protected void	InitializeSkin(Control skin) {

			// Find	the	user list repeater
			//
			smileyList = (DataList)	skin.FindControl("SmileyList");

//			smileyList.AlternatingItemTemplate	= new TemplateBuilder()

//			smileyList.AlternatingItemTemplate	= Page.LoadTemplate( Globals.ApplicationPath + "/Themes/default/Skins/" + this.SkinFilename );
//			smileyList.EditItemTemplate			= Page.LoadTemplate( Globals.ApplicationPath + "/Themes/default/Skins/" + this.SkinFilename );
//			smileyList.HeaderTemplate			= Page.LoadTemplate( Globals.ApplicationPath + "/Themes/default/Skins/" + this.SkinFilename );
//			smileyList.FooterTemplate			= Page.LoadTemplate( Globals.ApplicationPath + "/Themes/default/Skins/" + this.SkinFilename );

//			smileyList.ItemCreated	 += new DataListItemEventHandler(smileyList_ItemCreated);
//			smileyList.ItemCommand	 += new DataListCommandEventHandler(smileyList_ItemCommand);
			smileyList.EditCommand	 += new DataListCommandEventHandler(smileyList_EditCommand);
			smileyList.CancelCommand += new DataListCommandEventHandler(smileyList_CancelCommand);
			smileyList.UpdateCommand += new DataListCommandEventHandler(smileyList_UpdateCommand);
			smileyList.DeleteCommand += new DataListCommandEventHandler(smileyList_DeleteCommand);

//			smileyList.DataSource = smiliesArray;

			if( !Page.IsPostBack)
				DataBind();
		}

		private void smileyList_CancelCommand(object source, DataListCommandEventArgs e) {
			// Set the EditItemIndex property to -1 to exit editing mode. Be sure
			// to rebind the DataList to the data source to refresh the control.
			smileyList.EditItemIndex = -1;
			DataBind();
		}

		private void smileyList_UpdateCommand(object source, DataListCommandEventArgs e) {
			Smiley updatedSmiley	= this.GetCurrentSmiley( e );

			if( updatedSmiley == null ) {
				// TODO should we redirect to an error page?
				return;
			}

			updatedSmiley.SmileyCode	= ((TextBox)e.Item.FindControl("EditSmileyCode")).Text;
			updatedSmiley.SmileyText	= ((TextBox)e.Item.FindControl("EditSmileyText")).Text;
			updatedSmiley.SmileyUrl		= ((TextBox)e.Item.FindControl("EditSmileyUrl")).Text;

			// now we can update the smiley
			Smilies.UpdateSmiley( updatedSmiley );

			// Set the EditItemIndex property to -1 to exit editing mode. 
			// Be sure to rebind the DataList to the data source to refresh
			// the control.
			smileyList.EditItemIndex = -1;
			DataBind();
		}

		private void smileyList_DeleteCommand(object source, DataListCommandEventArgs e) {
			Smiley deleteSmiley = this.GetCurrentSmiley( e );

			if( deleteSmiley == null ) {
				// TODO should we redirect to an error page?
				return;
			}

			Smilies.DeleteSmiley( deleteSmiley );

			smiliesArray.Remove( deleteSmiley );

			// Set the EditItemIndex property to -1 to exit editing mode. Be sure
			// to rebind the DataList to the data source to refresh the control.
			smileyList.EditItemIndex = -1;
			DataBind();
		}

		private void smileyList_EditCommand(object source, DataListCommandEventArgs e) {
			smileyList.EditItemIndex = e.Item.ItemIndex;
			DataBind();
		}

		private void smileyList_CreateCommand(object source, DataListCommandEventArgs e) {
			
			TextBox txtSmileyCode	= (TextBox)e.Item.FindControl("CreateSmileyCode");
			TextBox	txtSmileyUrl	= (TextBox)e.Item.FindControl("CreateSmileyUrl");
			TextBox txtSmileyText	= (TextBox)e.Item.FindControl("CreateSmileyText");
			
			if( txtSmileyCode	!= null &&
				txtSmileyUrl	!= null &&
				txtSmileyText	!= null ) {

				if( txtSmileyCode.Text != String.Empty &&
					txtSmileyUrl.Text	!= String.Empty &&
					txtSmileyText.Text	!= String.Empty ) {

					int nSmileyId = Smilies.CreateSmiley( new Smiley(0, txtSmileyCode.Text, txtSmileyUrl.Text, txtSmileyText.Text ));

					smiliesArray.Add( new Smiley( nSmileyId, txtSmileyCode.Text, txtSmileyUrl.Text, txtSmileyText.Text));

					DataBind();
				}
			}
		}

		private void smileyList_ItemCommand(object source, DataListCommandEventArgs e) {
			if( e.CommandName == "create" ) 
				smileyList_CreateCommand( source, e );
		}

		private void smileyList_ItemCreated(object sender, DataListItemEventArgs e) {
			Button button = (Button)e.Item.FindControl("CreateButton");

//			if( button != null ) {
//				_txtSmileyCode	= (TextBox)e.Item.FindControl("CreateSmileyCode");
//				_txtSmileyUrl	= (TextBox)e.Item.FindControl("CreateSmileyUrl");
//				_txtSmileyText	= (TextBox)e.Item.FindControl("CreateSmileyText");
//			}
		}

		private Smiley GetCurrentSmiley( DataListCommandEventArgs e ) {
			// Retrieve the updated values from the selected item.
			TextBox	txtSmileyId		= (TextBox)e.Item.FindControl("EditSmileyId");
			TextBox	txtSmileyCode	= (TextBox)e.Item.FindControl("EditSmileyCode");
			TextBox txtSmileyUrl	= (TextBox)e.Item.FindControl("EditSmileyUrl");
			TextBox	txtSmileyText	= (TextBox)e.Item.FindControl("EditSmileyText");

			if(	txtSmileyId		== null ||
				txtSmileyCode	== null ||
				txtSmileyUrl	== null ||
				txtSmileyText	== null ) {

				// TODO should we redirect to an error page.
				return null;
			}

			int		smileyId		= Int32.Parse(txtSmileyId.Text);
			String	smileyCode		= txtSmileyCode.Text;
			String	smileyUrl		= txtSmileyUrl.Text;
			String	smileyText		= txtSmileyText.Text;

			if( (smileyId		<= 0 ) ||
				(smileyCode		== null || smileyCode	== String.Empty ) ||
				(smileyUrl		== null || smileyUrl	== String.Empty ) ||
				(smileyText		== null || smileyText	== String.Empty )) {

				// TODO : should we redirecto to an error page?
				return null;
			}

			// ok we've done all our data validation lets update the smiley
			Smiley foundSmiley = null;

			// since the smilies are not stored in a hash, I have to iterate the array to find
			// the smiley being edited
			foreach( Smiley smiley in smiliesArray ) {

				if( smiley.SmileyId	== smileyId )
					foundSmiley = smiley;
			}

			return foundSmiley;
		}

		//		private void smileyList_ItemCreated(object sender, RepeaterItemEventArgs e) {
		//
		//			_btnEdit	= (Button) e.Item.FindControl("EditSmiley");
		//			_btnDelete	= (Button) e.Item.FindControl("DeleteSmiley");
		//			_btnCreate	= (Button) e.Item.FindControl("CreateSmiley");
		//
		//			// if we couldn't find any, then we are on the header row, so get out
		//			if( _btnEdit	== null &&
		//				_btnDelete	== null &&
		//				_btnCreate	== null )
		//				return;
		//
		//			// if we have the create control then we are on the footer 
		//			if( _btnCreate != null ) {
		//
		//				_btnCreate.Click += new EventHandler(_btnCreate_Click);
		//
		//				this._txtSmileyCode = (TextBox) e.Item.FindControl("SmileyCode");
		//				this._txtSmileyText	= (TextBox) e.Item.FindControl("SmileyText");
		//				this._txtSmileyUrl	= (TextBox) e.Item.FindControl("SmileyUrl");
		//
		//			} else if(	_btnEdit	!= null &&
		//						_btnDelete	!= null ) {
		//				
		//				_btnEdit.Click		+=new EventHandler(_btnEdit_Click);
		//				_btnDelete.Click	+=new EventHandler(_btnDelete_Click);
		//			}else {
		//
		//			}
		//		}

		//		private void smileyList_ItemCommand(object source, RepeaterCommandEventArgs e) {
		//
		//			HttpContext.Current.Response.Write(source.ToString() + " " + e.CommandArgument + " " + e.CommandName + " " + e.CommandSource + " " + e.Item.ToString() );
		//		}
		//
		//		private void _btnCreate_Click(object sender, EventArgs e) {
		//			if( _txtSmileyCode	!= null &&
		//				_txtSmileyUrl	!= null &&
		//				_txtSmileyText	!= null ) {
		//
		//				if( _txtSmileyCode.Text != String.Empty &&
		//					_txtSmileyUrl.Text	!= String.Empty &&
		//					_txtSmileyText.Text	!= String.Empty ) {
		//
		//					Smilies.CreateSmiley( new Smiley(0, _txtSmileyCode.Text, _txtSmileyUrl.Text, _txtSmileyText.Text ));
		//
		//					DataBind();
		//				}
		//			}
		//
		//			HttpContext.Current.Response.Write("youclicked");
		//		}
		//
		//		private void _btnEdit_Click(object sender, EventArgs e) {
		//			HttpContext.Current.Response.Write("you edited");
		//		}
		//
		//		private void _btnDelete_Click(object sender, EventArgs e) {
		//			HttpContext.Current.Response.Write("you deleted");
		//		}

	}
	*/
}