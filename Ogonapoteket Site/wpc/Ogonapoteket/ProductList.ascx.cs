using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Spinit.Wpc.Commerce.Business.FillObject;
using Spinit.Wpc.Commerce.Business;
using System.Collections.Generic;
using Spinit.Wpc.Commerce.Core;
using Spinit.Wpc.Commerce.Presentation.Site.SessionContext;
using Spinit.Wpc.Content.Data;


namespace Spinit.Wpc.Ogonapoteket.Presentation.Site.Wpc.Ogonapoteket {

	/// <summary>
	/// Generates list of products with name and image
	/// </summary>
	public partial class ProductList : UserControl{

		#region Parameters and Initialization

		// Internal data
		private bool _showProductAttributes;
		private bool _categoriesAddedToTitle;
		protected string _gotoPage = string.Empty;
		private int _gotoPageNo = -1;
		private Dictionary<int, ProductCategory> _categoryDictionary = new Dictionary<int, ProductCategory>();
		private Queue<string> _selectedUrlCategoryPath = new Queue<string>();
		private Collection<int> _selectedParents = new Collection<int>();
		private string _keyWords = string.Empty;


		protected string _parentCategories = string.Empty;
		

		protected void Page_Load(object sender, EventArgs e){
			GetProductDisplayPage();
			GetSelectedCategoryPath();
			InitializeControl();
		}

		private void GetProductDisplayPage() {
			var contentTree = new Tree(Base.Business.Globals.ConnectionString);
			if (_gotoPageNo > 0)
				_gotoPage = contentTree.GetFileUrlDownString(_gotoPageNo);
		}

		private void GetSelectedCategoryPath() {
			_selectedUrlCategoryPath.Clear();
			var selectedCategories = Request.QueryString["selectedCategories"];
			if(String.IsNullOrEmpty(selectedCategories)) return;
			var categories = selectedCategories.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
			//_lastSelectedCategoryId = categories[categories.Length - 1];
			foreach (var encodedCategory in categories) {
				if (String.IsNullOrEmpty(encodedCategory)) continue;
				_selectedUrlCategoryPath.Enqueue(encodedCategory);
			}
		}

		private void InitializeControl(){		
			if (Page.IsPostBack) return;
	
			var lastSelectedCategory = GetLastSelectedCategoryId(null);
			PopulateProducts(lastSelectedCategory);
			
		}

		#endregion


		#region Populaters

		private static void PopulateCategoryListChild(ProductCategory parent) {
			var bProductCategory = new BProductCategory(UserContext.CurrentCommerce);

			List<ProductCategory> productCategoryList;
			bProductCategory.GetProductCategories(
				null,   //product
				parent, //parent
				null,   //attribute
				null,   //name
				false,  //root
				new ProductCategoryFill(),
				out productCategoryList);

			if (productCategoryList == null) return;
			foreach (var productCategory in productCategoryList) {
				parent.ChildCategories.Add(productCategory);
				PopulateCategoryListChild(productCategory);
			}
		}

		private void PopulateProducts(int categoryId){
			var productCategoryFill = GetProductCategoryFill();
			var products = new List<Product>();

			if (categoryId > 0) {
				var bProductCategory =
					new BProductCategory(UserContext.CurrentCommerce);

				ProductCategory productCategory;
				var err = bProductCategory.GetProductCategory(
					categoryId, 
					productCategoryFill, 
					out productCategory);

				if (err.HasErrors) {
					return;
				}

				if (!string.IsNullOrEmpty(productCategory.Name))
				{
					ltCategoryName.Text = productCategory.Name;
				}

				products = new List<Product>();

				if (productCategory != null) {

					phCategoryName.Visible = true;
					products.AddRange(productCategory.Products);
				}
			}
			

			if (products.Count <= 0) return;
			rptProducts.DataSource = null;
			rptProducts.DataSource = products;
			rptProducts.DataBind();
		}


		#endregion


		#region Getter/Setters

		
		public int GotoPageNo
		{
			get { return _gotoPageNo;}
			set { _gotoPageNo = value; }
		}

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

		/// <summary>
		/// Recursive method deques the url arguments and attempts to find the selected category id
		/// If no category id is found, the method will throw an exception
		/// </summary>
		/// <param name="startCategory"></param>
		/// <returns></returns>
		private int GetLastSelectedCategoryId(ProductCategory startCategory) {
			if(_selectedUrlCategoryPath.Count<=0) {
				if (startCategory == null) return -1;
				if (_selectedUrlCategoryPath.Count<=0) return startCategory.Id;
				throw new Exception("Selected category could not be found");
			}
			List<ProductCategory> productCategoryList;
			var bProductCategory = new BProductCategory(UserContext.CurrentCommerce);
			bProductCategory.GetProductCategories(null,startCategory,null,null,(startCategory == null),new ProductCategoryFill(),out productCategoryList);
			foreach (var category in productCategoryList) {
				if (!_selectedUrlCategoryPath.Peek().Equals(EncodeStringToUrl(category.Name))) continue;
				_selectedUrlCategoryPath.Dequeue();
				return GetLastSelectedCategoryId(category);
			}
			throw new Exception("Selected category could not be found");
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

		protected Repeater CreateAndBindRepeaterToCategories(IEnumerable<ProductCategory> categories) {

			var rptProductCategories = new Repeater {
            	HeaderTemplate = LoadTemplate("Templates/CategoryHeader.ascx"),
            	ItemTemplate = LoadTemplate("Templates/CategoryItem.ascx"),
            	FooterTemplate = LoadTemplate("Templates/CategoryFooter.ascx")
            };

			rptProductCategories.ItemDataBound += rptProductCategories_DataBound;
			rptProductCategories.DataSource = categories;
			rptProductCategories.DataBind();
			rptProductCategories.Visible = HasVisibleItems(rptProductCategories.Items);

			return rptProductCategories;
		}

		private static bool HasVisibleItems(RepeaterItemCollection itemCollection) {
			foreach (RepeaterItem item in itemCollection) {
				if (item.Visible) {
					return true;
				}
			}
			return false;
		}
        

		private static ProductCategoryFill GetProductCategoryFill(){
			var productCategoryFill = new ProductCategoryFill {
          		FillPicture = true,
          		FillProduct = true,
          		ProductFill = new ProductFill {
          				FillPicture = true,
          				FillProductPrice = true,
          				ProductPriceFill = new ProductPriceFill { FillCurrency = true }
					}
			};
			productCategoryFill.ProductFill.FillArticles = true;
			productCategoryFill.ProductFill.ArticlesFill = new ProductFill {
           		FillPicture = true,
           		FillProductPrice = true,
           		ProductPriceFill = new ProductPriceFill { FillCurrency = true }
           };

			return productCategoryFill;
		}
        
		private void ApplyRepeaterLogic(Product product, RepeaterItemEventArgs e) {
			var phMainProduct = (PlaceHolder)e.Item.FindControl("phMainProduct");
			var phProduct = (PlaceHolder)e.Item.FindControl("phProduct");
			var picProduct = (Image)e.Item.FindControl("picProduct");
            
			var hlProductName =(HyperLink) e.Item.FindControl("hlProductName");
			var ltArtNr = (Literal)e.Item.FindControl("ltArtNr");
			var ltArtName = (Literal)e.Item.FindControl("ltArtName");
			var ltArtDesc = (Literal)e.Item.FindControl("ltArtDesc");


			hlProductName.Text = HttpUtility.HtmlEncode(product.Name); 
			
			hlProductName.NavigateUrl = CreateProductUrl(product.Id);

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


		/// <summary>
		/// Creates link for product
		/// </summary>
		/// <param name="productId">The product id</param>
		/// <returns>url for Product link</returns>
		private string CreateProductUrl(int productId)
		{
			if (!string.IsNullOrEmpty(Request.QueryString["selectedCategories"]))
			{
				string categoriesQuery = Request.QueryString["selectedCategories"];


				string url = String.IsNullOrEmpty(Request.Url.Query)
								? Request.Url.ToString()
								: Request.Url.ToString().Replace(Request.Url.Query, String.Empty);
				
				url = GetProductUrl(url);
				if (UseUrlRewriting)
					return String.Concat(url, categoriesQuery, "/" ,productId);
				else
					return String.Concat(url, categoriesQuery, "&PrdId=", productId);
			}
			return string.Empty;
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

		//Categories
		protected void rptProductCategories_DataBound(object sender, RepeaterItemEventArgs e) {
			if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
			var containerControl = e.Item.Controls[0];

			var hlCategoryName = (HyperLink)containerControl.FindControl("hlCategoryName");
			var phSubCategories = (PlaceHolder)containerControl.FindControl("phSubCategories");
			var category = (ProductCategory)e.Item.DataItem;
			var hiddenField = (HiddenField) containerControl.FindControl("hfSelectedValue");

			if (category.Parent == 0){	
				// Reset 
				_parentCategories = "0";
			}
			else{
				if (IsParentChanged(category.Parent))
					UpdateParentCategoriesAttribute(category.Parent);
			}

			if (IsCategoryUnfolded(category)) {
				hiddenField.Value = "selected";
				if (ShowSelectedCategoriesInTitle){
					AddSelectedCategoryNameToPageTitle(Globals.CategoryItemTitleFormat, category.Name, false);
				}
			}

			hlCategoryName.Text = HttpUtility.HtmlEncode(category.Name);

			string url;

			if (string.IsNullOrEmpty(_gotoPage)){
				url = String.IsNullOrEmpty(Request.Url.Query) ? Request.Url.ToString() : Request.Url.ToString().Replace(Request.Url.Query, String.Empty);
			}
			else{
				url = _gotoPage;
			}

			hlCategoryName.NavigateUrl = GetCategoryUrl(url,category);

			Repeater rptChildCategories = CreateAndBindRepeaterToCategories(category.ChildCategories);
			phSubCategories.Controls.Add(rptChildCategories);
		}

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