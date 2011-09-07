using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Model.Yammer;
using Spinit.Wpc.Synologen.Presentation.Site.Code.Yammer;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.Yammer;
using Spinit.Wpc.Synologen.Presentation.Site.Models.Yammer;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.Yammer
{
    public class YammerPresenter : Presenter<IYammerView>
    {
        private string _email, _password, _clientId, _network;

        private const int MaxMessagesToFetchFromYammer = 20;

        private readonly IYammerService _service;

        public YammerPresenter(IYammerView view, IYammerService service) : base(view)
        {
            _service = service;

            InitSettings();

            View.Load += View_Load;
        }

        public void View_Load(object sender, EventArgs e)
        {
            if (LivingCookiesExist())
            {
                _service.CookieContainer = View.State["YammerCookies"] as CookieContainer;
            }
            else
            {
                _service.Authenticate(_network, _clientId, _email, _password);
                if (View.State != null) { View.State["YammerCookies"] = _service.CookieContainer; }
            }

            View.Model = GetMessages();
        }

        public override void ReleaseView()
        {
            View.Load -= View_Load;
        }

        private YammerListModel GetMessages()
        {
            var objects = GetJsonObjects();

            if (objects == null || objects.messages.Count() == 0)
            {
                _service.Authenticate(_network, _clientId, _email, _password);
                objects = GetJsonObjects();
            }

            return objects == null ? new YammerListModel { Messages = Enumerable.Empty<YammerListItem>() } : new YammerListModel { Messages = YammerParserService.Convert(objects) };
        }

        private JsonMessageModel GetJsonObjects()
        {
            var json = _service.GetJson(View.NumberOfMessages, View.Threaded, View.NewerThan);
            var objects = JsonConvert.DeserializeObject<JsonMessageModel>(json);

            if (objects == null)
                return objects;

            if (View.ExcludeJoins)
            {
                int prevOldestId = 0;
                while (objects.messages.Count(x => YammerParserService.IsNotJoinMessage(x.body)) < View.NumberOfMessages)
                {
                    var oldestId = objects.messages.Where(x => x.id > View.NewerThan).Min(x => x.id);
                    if (oldestId == prevOldestId)
                    {
                        break;
                    }

                    json = _service.GetJson(MaxMessagesToFetchFromYammer, View.Threaded, View.NewerThan, oldestId);
                    var newObjects = JsonConvert.DeserializeObject<JsonMessageModel>(json);
                    objects.messages = objects.messages.Concat(newObjects.messages).ToArray();
                    objects.references = objects.references.Concat(newObjects.references).ToArray();

                    prevOldestId = oldestId;
                }
            }

            var messages = View.ExcludeJoins ? objects.messages.Where(x => YammerParserService.IsNotJoinMessage(x.body)).ToList() : objects.messages.ToList();
            objects.messages = messages.GetRange(0, Math.Min(messages.Count, View.NumberOfMessages)).ToArray();
            return objects;
        }

        private bool LivingCookiesExist()
        {
            if (View.State != null && View.State["YammerCookies"] is CookieContainer)
            {
                var cookies = View.State["YammerCookies"] as CookieContainer;
                if (cookies != null)
                {
                    var matches = cookies.GetCookies(new Uri("https://www.yammer.com"));

                    if (matches.Cast<Cookie>().Any(cookie => cookie.Expired))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private void InitSettings()
        {
            try
            {
                _email = Globals.YammerEmailAccount;
                _password = Globals.YammerPassword;
                _clientId = Globals.YammerClientId;
                _network = Globals.YammerNetwork;
            }
            catch (Exception)
            {
                _email = _password = _clientId = _network = String.Empty;
            }
        }
    }
}
