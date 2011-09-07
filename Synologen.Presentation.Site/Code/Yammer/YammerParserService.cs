using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Spinit.Wpc.Synologen.Core.Domain.Model.Yammer;
using Spinit.Wpc.Synologen.Presentation.Site.Models.Yammer;

namespace Spinit.Wpc.Synologen.Presentation.Site.Code.Yammer
{
    public class YammerParserService
    {
        public static IEnumerable<YammerListItem> Convert(JsonMessageModel json)
        {
            foreach (var message in json.messages)
            {
                var author = json.references.FirstOrDefault(x => x.id == message.sender_id);
               
                DateTime created;
                DateTime.TryParse(message.created_at, out created);

                yield return new YammerListItem
                {
                    AuthorName = author.full_name,
                    Content = FormatContent(message.body),
                    AuthorImageUrl = author.mugshot_url,
                    Created = String.Format("{0:HH}:{0:mm}, {0:dd MMM yyyy}", created)
                };
            }
        }

        public static string GetFormAction(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var formNode = doc.DocumentNode.SelectNodes("//form").First();
            return formNode.Attributes["action"].Value;
        }

        public static NameValueCollection GetPostCollection(string html, string email, string password)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var formNode = doc.DocumentNode.SelectNodes("//form").First();

            var collection = new NameValueCollection();

            foreach (HtmlNode hiddenInput in formNode.SelectNodes("//input[@type='hidden']"))
            {
                var nameAttribute = hiddenInput.Attributes["name"];
                var valueAttribute = hiddenInput.Attributes["value"];
                collection.Add(nameAttribute.Value, valueAttribute.Value);
            }

            var usernameInput = formNode.SelectNodes("//input[@type='text']").First();
            var passwordInput = formNode.SelectNodes("//input[@type='password']").First();

            var usernameValue = usernameInput.Attributes["name"];
            var passwordValue = passwordInput.Attributes["name"];

            collection.Add(usernameValue.Value, email);
            collection.Add(passwordValue.Value, password);

            return collection;
        }

        private static string FormatContent(BodyModel body)
        {
            if (body.urls != null)
            {
                foreach (var url in body.urls)
                {
                    string formattedUrl = url;
                    if (url.Length > 50)
                    {
                        formattedUrl = url.Substring(0, 47) + "...";
                    }
                    body.plain = body.plain.Replace(url, String.Format("<a href='{0}' target='_blank'>{1}</a>", url, formattedUrl));
                }
            }

            return body.plain;
        }

        public static bool IsNotJoinMessage(BodyModel body)
        {
            return !Regex.IsMatch(body.plain, "#joined", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        }
    }
}
