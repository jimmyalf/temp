using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Spinit.Wpc.Synologen.Presentation.Helpers.Extensions
{
	public static class WpcActionMessageExtensions
	{
		private const string DefaultActionMessagesTempDataKey = "WpcActionMessages";

		public static void AddSuccessMessage(this ControllerBase controller, string message)
		{
			controller.AddActionMessage(message, WpcActionMessageType.Success);
		}

		public static void AddErrorMessage(this ControllerBase controller, string message)
		{
			controller.AddActionMessage(message, WpcActionMessageType.Error);
		}

		public static void AddInformationMessage(this ControllerBase controller, string message)
		{
			controller.AddActionMessage(message, WpcActionMessageType.Information);
		}

		private static void AddActionMessage(this ControllerBase controller, string message, WpcActionMessageType type)
		{
			AddActionMessage(controller, message, DefaultActionMessagesTempDataKey, type);
		}

		private static void AddActionMessage(this ControllerBase controller, string message, string key, WpcActionMessageType type)
		{
			var modelMessages = controller.TempData[key] as IList<IWpcActionMessage> ?? new List<IWpcActionMessage>();
			var existingMessages = modelMessages.Where(x => x.Message == message && x.Type == type);
			if (existingMessages == null || existingMessages.Count() < 1)
			{
				modelMessages.Add(new WpcActionMessage { Message = message, Type = type });
				controller.TempData[DefaultActionMessagesTempDataKey] = modelMessages;
			}
		}

		public static string Messages(this HtmlHelper htmlhelper)
		{
			return Messages(htmlhelper, DefaultActionMessagesTempDataKey);
		}

		public static string Messages(this HtmlHelper htmlhelper, string key)
		{
			var actionMessages = GetMessages(htmlhelper, key);
			if(actionMessages == null || !actionMessages.Any()) return String.Empty;
			var ul = new TagBuilder("ul")
				.SetListClasses(actionMessages);

			foreach (var message in actionMessages)
			{
				var li = new TagBuilder("li") { InnerHtml = message.Message };
				li.AddCssClass(GetMessageTypeClass(message));
				ul.InnerHtml = string.Concat(ul.InnerHtml, li);
			}
			return ul.ToString();
		}

		private static TagBuilder SetListClasses(this TagBuilder tagBuilder, IEnumerable<IWpcActionMessage> actionMessages)
		{
			if(actionMessages.MessagesHasErrors())
			{
				tagBuilder.Attributes.Add("class", "action-messages contains-errors");
			}
			else if(actionMessages.MessagesHasSuccess())
			{
				tagBuilder.Attributes.Add("class", "action-messages contains-success");
			}
			return tagBuilder;
		}

		private static string GetMessageTypeClass(IWpcActionMessage message) 
		{ 
			switch (message.Type)
			{
				case WpcActionMessageType.Error:
					return "error";
				case WpcActionMessageType.Information:
					return "info";
				case WpcActionMessageType.Success:
					return "success";
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static IList<IWpcActionMessage> GetMessages(this HtmlHelper htmlHelper, string tempDataKey)
		{
			return htmlHelper.ViewContext.TempData[tempDataKey] as IList<IWpcActionMessage>;
		}

		private static bool MessagesHasErrors(this IEnumerable<IWpcActionMessage> messages)
		{
			return messages.Any(x => x.Type.Equals(WpcActionMessageType.Error));
		}

		private static bool MessagesHasSuccess(this IEnumerable<IWpcActionMessage> messages)
		{
			return messages.Any(x => x.Type.Equals(WpcActionMessageType.Success));
		}
	}
}