using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Commerce.Business.FillObject;
using Spinit.Wpc.Commerce.Business;
using System.Collections.Generic;
using Spinit.Wpc.Commerce.Core;
using Spinit.Wpc.Commerce.Presentation.Site.SessionContext;

namespace Spinit.Wpc.Synologen.Presentation.Ogonapoteket.wpc.Ogonapoteket {

	/// <summary>
	/// Control with name, description and image for a product
	/// </summary>
	public partial class ProductDetail : UserControl{

		#region Parameters and Initialization

		// Internal data
		private bool _showProductAttributes;
		private bool _categoriesAddedToTitle;
		private Dictionary<int, ProductCategory> _categoryDictionary = new Dictionary<int, ProductCategory>();
		private Queue<string> _selectedUrlCategoryPath = new Queue<string>();
		//private string _lastSelectedCategoryId = String.Empty;
		private Collection<int> _selectedParents = new Collection<int>();
		private string _keyWords = string.Empty;


		protected string _parentCategories = string.Empty;
		

		protected void Page_Load(object sender, EventArgs e){

			InitializeControl();
		}


		private void InitializeControl(){

			if (Page.IsPostBack) return;

			int productId = GetProductIdFromQuery();			

			if (productId >= 0)
			{
				PopulateProduct(productId);

			}
		
		}

		private void PopulateProduct(int productId)
		{
			
			var products = new List<Product>();
			Product product = null;
			var bProduct = new BProduct(UserContext.CurrentCommerce);
			var bProductFill = new ProductFill();
			bProductFill.FillPicture = true;

			Error err = bProduct.GetProduct(productId, bProductFill, out product);

			if (err.HasErrors || product == null)
			{
				return;
			}			
			products.Add(product);
			
			rptProducts.DataSource = null;
			rptProducts.DataSource = products;
			rptProducts.DataBind();

		}

		private int GetProductIdFromQuery()
		{
			if (!string.IsNullOrEmpty(Request.QueryString["PrdId"]))
			{
				return int.Parse(Request.QueryString["PrdId"]);
			}
			return 0;
		}

		#endregion


		#region Getter/Setters

		public bool ShowSelectedCategoriesInTitle { get; set; }

		public bool UseUrlRewriting { get; set; }

		#endregion


		#region Item Command Methods

		private void AddToCart(RepeaterCommandEventArgs e, int productId) {
			var bProduct = new BProduct(UserContext.CurrentCommerce);

			decimal sum = 0;
			if (UserContext.CurrentCommerce.OrderItems.Count > 0) {

				foreach (var item in UserContext.CurrentCommerce.OrderItems) {
					sum += item.Price * (decimal)item.NoOfProducts;
				}
			}

			var productFill = new ProductFill {
          		FillProductPrice = true,
          		ProductPriceFill = new ProductPriceFill {FillCurrency = true}
			};

			var product = new Product();
			var err = bProduct.GetProduct(productId, productFill, out product);
			var context = UserContext.CurrentCommerce;

			var found = false;
			if ((context.OrderItems.Count > 0)) {

				foreach (var item in UserContext.CurrentCommerce.OrderItems) {
					if (item.PrdId != product.Id) continue;
					item.NoOfProducts += 1;
					found = true;

					break;
				}
			}

			if (!found)
			{
				var orderItem = new OrderItem(
					0, 
					OrderStatusEnum.New, 
					0, 
					product.Id, 
					1,
					product.ProductPrices[0].Price, 
					product.ProductPrices[0].Price, 
					product.ProductPrices[0].CrnCde,
					UserContext.CurrentCommerce
					) {ProductName = product.Name};

				context.OrderItems.Add(orderItem);
			}

			UserContext.CurrentCommerce = context;
		}

		//private void GoToArticles(RepeaterCommandEventArgs e, int productId)
		//{
		//    throw new NotImplementedException();

		//    //TODO: Databind articles
		//    //BProduct bProduct = new BProduct(SessionContext.UserContext.CurrentCommerce);
		//    //ProductCategoryFill productCategoryFill = GetProductCategoryFill();
		//    //List<Core.Product> articles = new List<Core.Product>();

		//    //if (productId > 0) {
		//    //    Core.Product product;
		//    //    Error err = bProduct.GetProduct(productId, productCategoryFill.ProductFill, out product);

		//    //    if (err.HasErrors || product == null) {
		//    //        return;
		//    //    }

		//    //    articles.AddRange(product.Articles);
		//    //}

		//    //if (articles.Count > 0) {
		//    //    //Databind articles
		//    //}
		//}

		#endregion


		#region Helpers

		protected void AddSelectedCategoryNameToPageTitle(string format, string message, bool keepOriginalPageTitleFirst) {
			if (!_categoriesAddedToTitle && !keepOriginalPageTitleFirst) {
				Page.Title = message;
				_categoriesAddedToTitle = true;
				return;
			}
			Page.Title += String.Format(format, message);
		}

		private string GetCategoryUrl(string productPageUrl, ProductCategory category) {
			TryAddCategoryToDictionary(category);
			var returnUrl = GetProductUrl(productPageUrl);
			var listOfNodeAndParents = GetCategoryAndParentList(category);
			foreach (var categoryItem in listOfNodeAndParents) {
				returnUrl += String.Concat(EncodeStringToUrl(categoryItem.Name),'/');
			}
			return returnUrl;
		}

		private string GetProductUrl(string productPageUrl) {
			var pageUrlWithoutQuerystring = GetUrlWithoutQuerystring(productPageUrl);
			return UseUrlRewriting ? GetUrlPageRoot(pageUrlWithoutQuerystring) : String.Concat(pageUrlWithoutQuerystring,"?selectedCategories=");
		}

		private static string GetUrlWithoutQuerystring(string urlWithQuerystring) {
			var questionmarkIndex = urlWithQuerystring.LastIndexOf("?");
			if (questionmarkIndex>0) urlWithQuerystring = urlWithQuerystring.Substring(0, questionmarkIndex);
			return urlWithQuerystring;
		}

		/// <summary>
		/// Returns page root-folder with slash in end position
		/// </summary>
		private static string GetUrlPageRoot(string url) {
			var slashIndex = url.LastIndexOf('/');
			if (slashIndex >= 0) url = url.Substring(0, slashIndex + 1);
			return url;
		}

		private static string EncodeStringToUrl(string value) {
			value = value.ToLower();
			value = value.Replace('å', 'a');
			value = value.Replace('ä', 'a');
			value = value.Replace('ö', 'o');
			value = value.Replace(' ', '-');
			value = Regex.Replace(value,"[^a-z0-9-]", "");
			value = Regex.Replace(value,"[-]+", "-");
			return value;
		}

		private void TryAddCategoryToDictionary(ProductCategory category) {
			if (!_categoryDictionary.ContainsKey(category.Id)) {
				_categoryDictionary.Add(category.Id, category);
			}
		}

		private List<ProductCategory> GetCategoryAndParentList(ProductCategory category) {
			var categories = new List<ProductCategory>();
			categories.Insert(0,category);
			ProductCategory parent;
			var currentParent = category.Parent;
			while (_categoryDictionary.TryGetValue(currentParent, out parent)) {
				categories.Insert(0,parent);
				currentParent = parent.Parent;
				if(currentParent==0) break;
			}
			return categories;
		}

		

		private static bool KeywordTagExistsInHeader(string input, string regexMatch) {
			return Regex.IsMatch(input, regexMatch, RegexOptions.IgnoreCase);
		}

		private Content.Presentation.Site.Code.Header GetHeaderObject() {			
			foreach (var headercontrol in Page.Header.Controls) {
				if(headercontrol.GetType() == typeof(Content.Presentation.Site.Code.Header)){
					return (Content.Presentation.Site.Code.Header)headercontrol;
				}
			}
			return null;
		}
        
		private void ApplyRepeaterLogic(Product product, RepeaterItemEventArgs e) {
			var phProduct = (PlaceHolder)e.Item.FindControl("phProduct");
			var picProduct = (Image)e.Item.FindControl("picProduct");
            
			var ltProductNumber = (Literal)e.Item.FindControl("ltProductNumber");
			var ltTitle = (Literal)e.Item.FindControl("ltTitle");
			var ltDescription = (Literal)e.Item.FindControl("ltDescription");
			var ltPrice = (Literal)e.Item.FindControl("ltprice");
			var ltArtNr = (Literal)e.Item.FindControl("ltArtNr");
			var ltArtName = (Literal)e.Item.FindControl("ltArtName");
			var ltArtDesc = (Literal)e.Item.FindControl("ltArtDesc");


			//Product level price
			if ((ltPrice != null) && (product.ProductPrices != null) && (product.ProductPrices.Count > 0)) {
				decimal price = 0;
				decimal.TryParse(product.ProductPrices[0].Price.ToString("#,##"), out price);

				ltPrice.Visible = false;
				if (price > 0) {
					ltPrice.Visible = true;
					ltPrice.Text = string.Concat(
						price.ToString(),
						" ",
						product.ProductPrices[0].Currency.CurrencyCode);
				}
			}

			ltTitle.Text = product.Name;
			ltDescription.Text = product.Description;

			if (product.PictureFile != null) {
				picProduct.ImageUrl = Base.Business.Globals.CommonFileUrl + product.PictureFile.Name;
				picProduct.Visible = true;
			}

			// Keywords
			if (!string.IsNullOrEmpty(product.KeyWords)) {
				_keyWords = string.Concat(_keyWords, " ", product.KeyWords);
			}

			var bProduct = new BProduct(UserContext.CurrentCommerce);
			List<ProductAttribute> productAttributeList;
			var err = bProduct.GetProductAttributes(
				product,
				null, //attribute
				new ProductAttributeFill(),
				out productAttributeList);

			if (!err.HasErrors && productAttributeList.Count > 0) { 
			

				/* piva 2009-06-09 Commented out. List is already sorted. And string sort may be totally wrong!

				//Sort productAttributeList by attribute order value
				productAttributeList.Sort(delegate(ProductAttribute x, ProductAttribute y) {
					return string.Compare(x.Order.ToString(), y.Order.ToString());
				});
				*/

				var rpt = (Repeater)e.Item.FindControl("rptProductAttributeValue");
				if (rpt != null) {
					rpt.DataSource = productAttributeList;
					rpt.DataBind();
				}
				if (_showProductAttributes) {

					rpt = (Repeater) e.Item.FindControl("rptProductAttributeName");
					if (rpt != null) {
						rpt.DataSource = productAttributeList;
						rpt.DataBind();
					}
				}
			}

			

			//Product has articles?
			if ((product.Articles != null) && (product.Articles.Count > 0)) {			
				var articles = new List<Product>();
				articles.AddRange(product.Articles);
                
				var rpt = (Repeater)e.Item.FindControl("rptArticleAttributeHeading");
				if (rpt != null) {
					rpt.DataSource = productAttributeList;
					rpt.DataBind();
				}

				rpt = (Repeater)e.Item.FindControl("rptProductArticles");
				if (rpt != null) {
					rpt.DataSource = articles;
					rpt.DataBind();
				}
              
			}
			else { //No articles available
				if ((ltArtNr != null) && (ltArtName != null) && (ltArtDesc != null)) {
					ltArtNr.Visible = false;
					ltArtName.Visible = false;
					ltArtDesc.Visible = false;
				}
				
			}
		}

		protected void UpdateParentCategoriesAttribute(int parentId){

			var separator = new[] {','};
			var newParentsCategories = string.Empty;

			if (string.IsNullOrEmpty(_parentCategories)) return;
			var arrParents = _parentCategories.Split(separator);

			var i = 0;
			var found = false;
			while (i < arrParents.Length && !found){
				if (arrParents[i] == parentId.ToString()) {
					found = true;
				}
				newParentsCategories += arrParents[i] + ",";
				i++;
			}

			if (!found)
				newParentsCategories += parentId;
		
			// Remove last","
			if (newParentsCategories.LastIndexOf(",") == newParentsCategories.Length - 1)
				newParentsCategories = newParentsCategories.Remove(newParentsCategories.LastIndexOf(","));	
		
			_parentCategories = newParentsCategories;
		}

		protected bool IsParentChanged(int parentId){
			var separator = new[] { ',' };
			var arrParents = _parentCategories.Split(separator);
			return (arrParents[arrParents.GetUpperBound(0)] != parentId.ToString());

		}

		protected bool IsCategoryUnfolded(ProductCategory category) {
			if (category.Parent > 0 && !_selectedParents.Contains(category.Parent)) return false;
			if (_selectedUrlCategoryPath.Count <= 0) return false;
			var compareValue = EncodeStringToUrl(category.Name);
			var firstValueInQuery = _selectedUrlCategoryPath.Peek();
			if (firstValueInQuery.Equals(compareValue)) {
				_selectedUrlCategoryPath.Dequeue();
				_selectedParents.Add(category.Id);
				return true;
			}
			return false;
		}

		#endregion


		#region Events


		//Products
		protected void rptProducts_ItemDataBound(object sender, RepeaterItemEventArgs e) {
			if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
			var product = (Product)e.Item.DataItem;
			if (product != null) {
				ApplyRepeaterLogic(product, e);
			}
		}

		//Articles
		protected void rptProductArticles_ItemDataBound(object sender, RepeaterItemEventArgs e) {
			if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
			var article = (Product)e.Item.DataItem;
			if (article != null) {
				ApplyRepeaterLogic(article, e);
			}
		}

		//Table Product Attribute Headings
		protected void rptProductAttributeName_ItemDataBound(object sender, RepeaterItemEventArgs e) {			
			var bAttribute = new BAttribute(UserContext.CurrentCommerce);
			var productAttribute = (ProductAttribute)e.Item.DataItem;

			Commerce.Core.Attribute attribute;
			var err = bAttribute.GetAttribute(productAttribute.AttId, new AttributeFill(), out attribute);

			if ((err.HasErrors) || (attribute == null)) return;
			var ltAttributeName = (Literal)e.Item.FindControl("ltAttributeName");
			ltAttributeName.Text = attribute.Name;
		}

		//Table Article Attribute Headings
		protected void rptProductAttributeHeading_ItemDataBound(object sender, RepeaterItemEventArgs e) 
		{
			var bAttribute = new BAttribute(UserContext.CurrentCommerce);
			var productAttribute = (ProductAttribute)e.Item.DataItem;

			Commerce.Core.Attribute attribute;
			var err = bAttribute.GetAttribute(productAttribute.AttId, new AttributeFill(), out attribute);

			if ((err.HasErrors) || (attribute == null)) return;
			var ltAttributeName = (Literal)e.Item.FindControl("ltAtricleAttributeName");
				
			ltAttributeName.Text = attribute.Name;
		}
        
		//Table Attribute Values
		protected void rptProductAttributeValue_ItemDataBound(object sender, RepeaterItemEventArgs e) {
			var bAttribute = new BAttribute(UserContext.CurrentCommerce);
			var productAttribute = (ProductAttribute)e.Item.DataItem;

			Commerce.Core.Attribute attribute;
			var err = bAttribute.GetAttribute(productAttribute.AttId, new AttributeFill(), out attribute);

			if ((err.HasErrors) || (attribute == null)) return;
			var ltAttributeValue = (Literal)e.Item.FindControl("ltAttributeValue");
			ltAttributeValue.Text = productAttribute.Value;
		}

		protected void rptProducts_ItemCommand(object sender, RepeaterCommandEventArgs e) {
			if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
			int productId;
			if (!int.TryParse(e.CommandArgument.ToString(), out productId)) return;
			switch (e.CommandName) {
				case "AddToCart":
					AddToCart(e, productId);
					break;
			}
		}

		protected void lnkAddToShop_Click(object sender, EventArgs e) { }

		#endregion

	}
}