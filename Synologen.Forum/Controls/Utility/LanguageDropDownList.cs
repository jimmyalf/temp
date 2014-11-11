using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;
using System.Collections.Specialized;

namespace Spinit.Wpc.Forum.Controls {

    
    public class LanguageDropDownList : DropDownList {

        public LanguageDropDownList() {

            NameValueCollection supportedLanguages = ResourceManager.GetSupportedLanguages();

            foreach (string key in supportedLanguages.Keys) {
                Items.Add( new ListItem(key, supportedLanguages[key]));
            }
        }
    }
}
