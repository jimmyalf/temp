using System.Web.UI;
using Spinit.Extensions;

namespace Spinit.Wpc.Synologen.OPQ.Presentation
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
			string cssClass = " class=\"{0}\"";
			const string container = "<div id=\"{0}\"{1}>{2}</div>";
			if (HasMessage && IsNegative) cssClass = string.Format(cssClass, NegativeCssClass);
			else if (HasMessage && !IsNegative) cssClass = string.Format(cssClass, PositiveCssClass);
			else if (!HasMessage) cssClass = string.Empty;

			writer.Write(
				string.Format(container, ControlId, cssClass, _message));
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

		public bool HasMessage
		{
			get { return _message.IsNotNullOrEmpty(); }
		}
		
	}
}
