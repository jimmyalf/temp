using System;
using System.Collections.Generic;
using System.Data;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class CreateShopOwners : Code.SynologenPage {

		protected void Page_Load(object sender, EventArgs e) {
			if(Page.IsPostBack) return;
			PopulateCategories();
		}

		private void PopulateCategories() {
			DataSet categories = Provider.GetCategories(LocationId, LanguageId);
			drpSelectionMemberCategories.DataSource = categories;
			drpnewMemberCategories.DataSource = categories;
			drpSelectionMemberCategories.DataBind();
			drpnewMemberCategories.DataBind();
		}

		protected void btnAction_Click(object sender, EventArgs e) {
			int selectionMemberCategory = Int32.Parse(drpSelectionMemberCategories.SelectedValue);
			int newMemberCategory = Int32.Parse(drpnewMemberCategories.SelectedValue);
			int newBaseGroupId = GetBaseCategoryFromMemberCategory(newMemberCategory);
			if(newBaseGroupId<=0 || newMemberCategory <=0 ||selectionMemberCategory<=0) {
				Response.Write("Kunde inte parsa kategorier. Avbryter!");
				return;
			}
			List<MemberRow> synologer = GetMembers(selectionMemberCategory);
			//Dictionary<int, int> newMemberUsers = new Dictionary<int, int>();
			List<MemberUserPair> newMemberUsers = new List<MemberUserPair>();
			Random randomObject = new Random();
			foreach(MemberRow member in synologer) {
				int originalMemberId = member.Id;
				//Create user
				int newUserId = AddUser(originalMemberId, "Butikchef", member.OrgName, randomObject);

				//Connect to new Base Category
				ConnectBaseCategory(newUserId, newBaseGroupId);

				//Create member
				int newMemberId = AddMember(member);

				//Create User-Member Connection
				Provider.AddBaseUserConnection(newMemberId, newUserId);

				//Connect to new Member Category
				Provider.ConnectToCategory(newMemberId, newMemberCategory); 

				//Connect shop(s)
				foreach (int shopId in Provider.GetAllShopIdsPerMember(originalMemberId)) {
					Provider.ConnectShopToMember(shopId, newMemberId);
				}
				newMemberUsers.Add(new MemberUserPair(newMemberId,newUserId));
			}
			DisplayResult(newMemberUsers);
		}

		private void DisplayResult(ICollection<MemberUserPair> users) {
			Response.Write("<br/>");
			Response.Write("Added " + users.Count + " members:");
			Response.Write("<br/>");
			Response.Write("<table><tr><th>MemberId</th><th>UserId</th></tr>");
			foreach (MemberUserPair memberUserPair in users) {
				Response.Write("<tr><td>" + memberUserPair.MemberId + "</td><td>" + memberUserPair.UserId + "</td></tr>");
			}
			Response.Write("</table>");
		}

		private int GetBaseCategoryFromMemberCategory(int memberCategory) {
			CategoryRow catrow = Provider.GetCategory(memberCategory, LocationId, LanguageId);
			return catrow.GroupId;
		}

		private int AddMember(MemberRow oldMember) {
			MemberRow memberCopy = oldMember;
			memberCopy.CreatedBy = "Admin";
			memberCopy.ContactFirst = "ButikChef";
			memberCopy.ContactLast = memberCopy.OrgName;
			Provider.AddUpdateDeleteMember(Enumerations.Action.Create, LanguageId, ref memberCopy);
			return memberCopy.Id;
		}

		private List<MemberRow> GetMembers(int memberCategory) {
			DataSet memberData = Provider.GetSynologenMembers(0, 0, memberCategory, null);
			List<MemberRow> listOfMembers = new List<MemberRow>();
			foreach(DataRow row in memberData.Tables[0].Rows) {
				int memberId = Util.CheckNullInt(row,"cId");
				MemberRow member = Provider.GetSynologenMember(memberId, LocationId, LanguageId);
				listOfMembers.Add(member);
			}
			return listOfMembers;
		}

		//TODO: Move GeneralData (User) dependancy out of method to get rid of reference
		private int AddUser(int oldMemberId, string newFirstName, string newLastName,Random random) {
			UserRow user = Provider.GetUserRow(oldMemberId);
			user.FirstName = newFirstName;
			user.LastName = newLastName;
			user.UserName = newLastName.Replace(" ", "") + "-" + random.Next(100, 999);
			User userProvider =  new User(Base.Business.Globals.ConnectionString);
			string password = GeneratePassword(7);
			int userId = userProvider.Add(user.UserName, password, user.FirstName, user.LastName, user.Email, user.DefaultLocation, "Admin");
			if(userId <= 0) {
				user.UserName = newLastName.Replace(" ", "") + "-" + random.Next(100, 999);
				userId = userProvider.Add(user.UserName, password, user.FirstName, user.LastName, user.Email, user.DefaultLocation, "Admin");
			}
			userProvider.Update(userId, null, user.FirstName, user.LastName, user.Email, user.DefaultLocation, user.Active, "Admin");
			return userId;
		}

		private static void ConnectBaseCategory(int userId, int baseCategory) {
			User userProvider = new User(Base.Business.Globals.ConnectionString);
			userProvider.ConnectGroup(userId, baseCategory);
		}

		public struct MemberUserPair {
			public MemberUserPair(int memberId, int userId) {
				MemberId = memberId;
				UserId = userId;
			}
			public int MemberId;
			public int UserId;
		}

		public string GeneratePassword(int length) {
			string guidResult = Guid.NewGuid().ToString();
			guidResult += Guid.NewGuid().ToString();
			guidResult = guidResult.Replace("-", string.Empty);

			// Make sure length is valid
			if (length <= 0 || length > guidResult.Length)
				throw new ArgumentException("Length must be between 1 and " + guidResult.Length);

			return guidResult.Substring(0, length);

		}

	}
}
