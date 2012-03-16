using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Spinit.Wpc.Synologen.Core.Domain.Model.Yammer;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Yammer;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Code.Yammer
{
    public class YammerParserService
    {
        public static IEnumerable<YammerListItem> Convert(JsonMessageModel json, Func<string, string, string, string> fetchImage)
        {
            foreach (var message in json.messages)
            {
                var author = json.references.FirstOrDefault(x => x.id == message.sender_id);
               
                DateTime created;
                DateTime.TryParse(message.created_at, out created);

                var item = new YammerListItem
                {
                    AuthorName = author.full_name,
                    Content = FormatContent(message.body),
                    AuthorImageUrl = author.mugshot_url,
                    Created = String.Format("{0:HH}:{0:mm}, {0:dd MMM yyyy}", created),
                    //Images = message.attachments.Where(IsImage).Select(x => ParseImage(x, fetchImage)).Where(x => !String.IsNullOrEmpty(x.Url) || !String.IsNullOrEmpty(x.Thumbnail)),
                    YammerModules = message.attachments.Where(IsYModule).Select(ParseYammerModule),
                    Images = Enumerable.Empty<YammerImage>()
                };

                foreach (var attachment in message.attachments.Where(IsImage))
                {
                    try
                    {
                        var image = ParseImage(attachment, fetchImage);
                        if (!String.IsNullOrEmpty(image.Url) || !String.IsNullOrEmpty(image.Thumbnail))
                        {
                            item.Images = item.Images.Concat(new[] { image });
                        }
                    }
                    catch(Exception) {}
                }

                yield return item;
            }
        }

        private static YammerImage ParseImage(AttachmentModel attachmentModel, Func<string, string, string, string> fetchImage)
        {
            var extension = Path.HasExtension(attachmentModel.image.url) ? Path.GetExtension(attachmentModel.image.url) : String.Empty;

            var fileName = attachmentModel.id.ToString();
            var thumbnailFileName = String.Format("{0}-thumbnail", fileName);

            var imageWithExt = fetchImage.Invoke(attachmentModel.image.url, fileName, extension);
            var thumbnailWithExt = fetchImage.Invoke(attachmentModel.image.thumbnail_url, thumbnailFileName, extension);

            return new YammerImage {Name = attachmentModel.name, Thumbnail = thumbnailWithExt, Url = imageWithExt};
        }

        private static YammerModule ParseYammerModule(AttachmentModel attachmentModel)
        {
            return new YammerModule
            {
                Name = attachmentModel.name ?? String.Empty,
                WebUrl = attachmentModel.web_url ?? String.Empty,
                InlineHtml = attachmentModel.inline_html ?? String.Empty
            };
        }

        private static bool IsImage(AttachmentModel attachmentModel)
        {
            return attachmentModel.type.Equals("image") && attachmentModel.image != null;
        }
        
        private static bool IsYModule(AttachmentModel attachmentModel)
        {
            return attachmentModel.type.Equals("ymodule");
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

        /// <summary>
        /// Detects wether yammer message say that someone joined the network
        /// </summary>
        public static bool IsNotJoinMessage(BodyModel body)
        {
            return !Regex.IsMatch(body.plain, "#joined", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        }
    }
}
