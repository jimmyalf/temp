using System;
using System.Text;
using System.Web;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Presentation.Helpers.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public class WpcPager 
	{
		private readonly IPagedList _pagedList;
		private readonly HttpRequestBase _request;

		private string _paginationContainerClass = "wpcPager";
		private string _paginationFormat = Resources.WpcPager.CenterText;
		private string _paginationFirst = Resources.WpcPager.FirstText;
		private string _paginationPrev = Resources.WpcPager.PreviousText;
		private string _paginationNext = Resources.WpcPager.Next;
		private string _paginationLast = Resources.WpcPager.LastText;
		private string _pageQueryName = "page";
		private Func<int, string> _urlBuilder;
		//private NameValueCollection _extraQueryParameters;

		/// <summary>
		/// Creates a new instance of the Pager class.
		/// </summary>
		/// <param name="pagedList">The IPagedList datasource</param>
		/// <param name="request">The current HTTP Request</param>
		public WpcPager(IPagedList pagedList, HttpRequestBase request) 
		{
			_pagedList = pagedList;
			_request = request;

			_urlBuilder = CreateDefaultUrl;
		}

		/// <summary>
		/// Specifies the query string parameter to use when generating pager links. The default is 'page'
		/// </summary>
		public WpcPager QueryParam(string queryStringParam) 
		{
			_pageQueryName = queryStringParam;
			return this;
		}

		//public WpcPager ExtraQueryParameters(NameValueCollection extraQueryParameters) 
		//{
		//    _extraQueryParameters = extraQueryParameters;
		//    return this;
		//}

		public WpcPager ContainerClass(string className) 
		{
			_paginationContainerClass = className;
			return this;
		}


		/// <summary>
		/// Specifies the format to use when rendering a pagination containing multiple pages. 
		/// The default is 'Page {0} of {1} ({2} items)' (eg 'Page 1 of 2 (60 items)')
		/// </summary>
		public WpcPager Format(string format) 
		{
			_paginationFormat = format;
			return this;
		}

		/// <summary>
		/// Text for the 'first' link.
		/// </summary>
		public WpcPager First(string first) 
		{
			_paginationFirst = first;
			return this;
		}

		/// <summary>
		/// Text for the 'prev' link
		/// </summary>
		public WpcPager Previous(string previous) 
		{
			_paginationPrev = previous;
			return this;
		}

		/// <summary>
		/// Text for the 'next' link
		/// </summary>
		public WpcPager Next(string next) 
		{
			_paginationNext = next;
			return this;
		}

		/// <summary>
		/// Text for the 'last' link
		/// </summary>
		public WpcPager Last(string last) 
		{
			_paginationLast = last;
			return this;
		}


		/// <summary>
		/// Uses a lambda expression to generate the URL for the page links.
		/// </summary>
		/// <param name="urlBuilder">Lambda expression for generating the URL used in the page links</param>
		public WpcPager Link(Func<int, string> urlBuilder) 
		{
			_urlBuilder = urlBuilder;
			return this;
		}

		public override string ToString() 
		{
			if (_pagedList.Total == 0) return null;

			var builder = new StringBuilder();

			if(!String.IsNullOrEmpty(_paginationContainerClass))
			{
				builder.AppendFormat("<div class=\"{0}\">", _paginationContainerClass);
			}
			else
			{
				builder.Append("<div>");
			}
			
			RenderPager(builder);

			builder.Append(@"</div>");

			return builder.ToString();
		}

		protected virtual void RenderPager(StringBuilder builder) 
		{ 
			//If we're on page 1 then there's no need to render a link to the first page. 
			if (_pagedList.Page == 1) 
			{
				builder.Append(_paginationFirst);
			}
			else {
				builder.Append(CreatePageLink(1, _paginationFirst));
			}

			builder.Append(" | ");

			//If we're on page 2 or later, then render a link to the previous page. 
			//If we're on the first page, then there is no need to render a link to the previous page. 
			if (_pagedList.HasPreviousPage) 
			{
				builder.Append(CreatePageLink(_pagedList.Page - 1, _paginationPrev));
			}
			else {
				builder.Append(_paginationPrev);
			}

			builder.Append(" | ");

			builder.AppendFormat(_paginationFormat, _pagedList.Page, _pagedList.NumberOfPages, _pagedList.Total);

			builder.Append(" | ");

			//Only render a link to the next page if there is another page after the current page.
			if (_pagedList.HasNextPage) 
			{
				builder.Append(CreatePageLink(_pagedList.Page + 1, _paginationNext));
			}
			else 
			{
				builder.Append(_paginationNext);
			}

			builder.Append(" | ");

			var lastPage = _pagedList.NumberOfPages;

			//Only render a link to the last page if we're not on the last page already.
			if (_pagedList.Page < lastPage) 
			{
				builder.Append(CreatePageLink(lastPage, _paginationLast));
			}
			else 
			{
				builder.Append(_paginationLast);
			}
		
		}

		private string CreatePageLink(int pageNumber, string text) {
			const string link = "<a href=\"{0}\">{1}</a>";
			return string.Format(link, _urlBuilder(pageNumber), text);
		}

		private string CreateDefaultUrl(int pageNumber) {
			var parameterCollection = _request.QueryString.AddReplaceItem(_pageQueryName, pageNumber.ToString());
			var queryString = parameterCollection.ToQueryString();
			var filePath = _request.FilePath;
			var url = string.Format("{0}?{1}", filePath, queryString);
			return url;
		}
	}
}