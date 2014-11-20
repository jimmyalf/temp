using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Code {
	public class SynologenSalesUserControl : SynologenUserControl {
		private int _maxNumberOfItems = 15;
		protected List<ICompanyValidationRule> contractValidationRules = new List<ICompanyValidationRule>();

		protected void PopulateValidationRules(Company company, ControlCollection controls) {
            contractValidationRules = new List<ICompanyValidationRule>(company.CompanyValidationRules);
			SetRequiredAsteriskInLabels(controls);
		}

		protected void SetRequiredAsteriskInLabels(ControlCollection controls) {
			foreach (Control control in controls){
				if(control.GetType() != typeof (Literal)){
					if (control.HasControls()){
						SetRequiredAsteriskInLabels(control.Controls);
					}
					else{ continue;}
				}
				if (control.GetType() != typeof(Literal)) continue;
				var literalControl = (Literal) control;
				if (!literalControl.ID.ToLower().Contains("required")) continue;
				literalControl.DataBind();
			}
		}

		protected void PerformCustomValidation(object source, ServerValidateEventArgs args) {
			var validationControl = (CustomValidator) source;
			var controlToValidateID = validationControl.ControlToValidate;
			string errorMessage;
			args.IsValid = CustomOrderValidation.IsValid(controlToValidateID, args.Value, contractValidationRules, out errorMessage);
			validationControl.ErrorMessage = errorMessage;
		}

		protected void SessionValidation(object source, ServerValidateEventArgs args) {
		    args.IsValid = (MemberId > 0 && MemberShopId > 0);
		}

		protected string GetControlIsRequiredCharacter(string controlToValidate) {
			return contractValidationRules.Exists(x => x.ControlToValidate.Equals(controlToValidate) && CustomOrderValidation.IsValidationRuleRequired(x))  ? "*" : String.Empty;
		}

		protected static int GetNewTemporaryIdForCart(IList<CartOrderItem> cart) {
			if (cart == null || cart.Count == 0) return 1;
			var tempId = 0;
			foreach (var itemRow in cart){
				if (itemRow.TemporaryId > tempId) tempId = itemRow.TemporaryId;
			}
			return tempId+1;
		}

		protected static float GetTotalCartPrice(IEnumerable<CartOrderItem> cart) {
			float returnValue = 0;
			foreach(var order in cart) {
				returnValue += order.DisplayTotalPrice;
			}
			return returnValue;
		}
		
		protected List<int> GetNumberOfItemsItemList() {
			var returnList = new List<int>();
			for (var i = 1; i <= MaxNumberOfItems; i++) {
				returnList.Add(i);
			}
			return returnList;
		}

		public int MaxNumberOfItems {
			get { return _maxNumberOfItems; }
			set { _maxNumberOfItems = value; }
		}


	}
}