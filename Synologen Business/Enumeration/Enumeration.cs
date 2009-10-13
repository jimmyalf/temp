namespace Spinit.Wpc.Synologen.Business.Enumeration {
	public enum FetchShop {
		All = 1,
		AllPerContractCustomer = 2,
		Specific = 3,
		AllPerShopCategory = 4,
		AllPerMember = 5
	}

	public enum FetchCustomerContract {
		All = 1,
		AllPerShop = 2,
		Specific = 3
	}
	public enum ConnectionAction {
		Connect = 1,
		Delete = 2
	}
	public enum ShopMemberConnectionAction {
		ConnectShopAndMember = 1,
		DeleteAllPerMember = 2
	}

	public enum FileCategoryGetAction {
		Specific = 1,
		All = 2
	}

	public enum FileCategoryType {
		Member,
		Synologen
	}
	public enum LogType {
		Error = 1,
		Information = 2,
		Other = 3
	}
	public enum ActiveFilter {
		Active = 1,
		Inactive = 2,
		Both = 3
	}

	public enum RoundDecimals {
		DoNotRound=1,
		RoundUp=2,
		RoundDown=3,
		RoundWithTwoDecimals=4
	}
}