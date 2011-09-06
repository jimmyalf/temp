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
        private int _numberOfMessages;

        private readonly IYammerView _view;
        private readonly IYammerService _service;

        public YammerPresenter(IYammerView view, IYammerService service) : base(view)
        {
            _view = view;
            _service = service;

            InitSettings();

            View.Load += View_Load;
        }

        public void View_Load(object sender, EventArgs e)
        {
            if (LivingCookiesExist())
            {
                _service.CookieContainer = _view.State["YammerCookies"] as CookieContainer;
            }
            else
            {
                _service.Authenticate(_network, _clientId, _email, _password);
                if (_view.State != null) { _view.State["YammerCookies"] = _service.CookieContainer; }
            }

            _view.Model = GetMessages(_numberOfMessages);
        }

        public override void ReleaseView()
        {
            View.Load -= View_Load;
        }

        private YammerListModel GetMessages(int limit)
        {
            var json = _service.GetJson(limit);
            var objects = JsonConvert.DeserializeObject<JsonMessageModel>(json);
            if (objects == null || objects.messages.Count() == 0)
            {
                _service.Authenticate(_network, _clientId, _email, _password);
                json = _service.GetJson(limit);
                objects = JsonConvert.DeserializeObject<JsonMessageModel>(json);
            }

            return objects == null ? new YammerListModel { Messages = Enumerable.Empty<YammerListItem>() } : new YammerListModel { Messages = YammerParserService.Convert(objects) };
        }

        private bool LivingCookiesExist()
        {
            if (_view.State != null && _view.State["YammerCookies"] is CookieContainer)
            {
                var cookies = _view.State["YammerCookies"] as CookieContainer;
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
                _numberOfMessages = Globals.YammerNumberOfMessages;
            }
            catch (Exception)
            {
                _email = _password = _clientId = _network = String.Empty;
                _numberOfMessages = 0;
            }
        }
    }
}
