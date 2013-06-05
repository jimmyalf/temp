using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Spinit.Wpc.Synologen.OPQ.Site.Code
{
	/// <summary>
	/// Message manager for user feedback.
	/// Positive and Negative.
	/// </summary>
	public class UserMessageManager : Control
	{

		private string _message;
		private string _negativeCssClass = "negative";
		private string _positiveCssClass = "positive";
		private string _controlId = "User-Message-Manager";
		
		
		protected override void Render(HtmlTextWriter writer)
		{
			const string container = "<div id=\"{0}\" class=\"{1}\">{2}</div>";
			writer.Write(
				string.Format(container, ControlId, IsNegative ? NegativeCssClass : PositiveCssClass, _message ));
		}


		/// <summary>
		/// Sets the message as positive feedback to the user
		/// </summary>
		public string PositiveMessage 
		{
			set 
			{
				_message = value;
				IsNegative = false;
			}
		}

		/// <summary>
		/// Sets the message as negative feedback to the user
		/// </summary>
		public string NegativeMessage
		{
			set
			{
				_message = value;
				IsNegative = true;
			}
		}

		private bool IsNegative { get; set; }

		/// <summary>
		/// Sets the negative class attribute
		/// </summary>
		public string NegativeCssClass
		{ 
			private get { return _negativeCssClass;}
			set { _negativeCssClass = value; }
		}
		
		/// <summary>
		/// Sets the positive class attribute
		/// </summary>
		public string PositiveCssClass
		{
			private get{ return _positiveCssClass;}
			set { _positiveCssClass = value; }
		}

		/// <summary>
		/// Sets the control identifier
		/// </summary>
		public string ControlId
		{
			private get { return _controlId; }
			set { _controlId = value; }
		}
		
	}
}
