using System;
using System.Web.UI.WebControls;

namespace Spinit.Wpc.Forum.Controls {

    
    public class TimezoneDropDownList : DropDownList {

        public TimezoneDropDownList() {
            Items.Add (new ListItem ("(GMT -12:00) International Dateline West", "-12"));
            Items.Add (new ListItem ("(GMT -11:00) Midway Island, Samoa", "-11"));
			Items.Add (new ListItem ("(GMT -10:00) Hawaii Island", "-11"));
            Items.Add (new ListItem ("(GMT -09:00) Alaska", "-9"));
            Items.Add (new ListItem ("(GMT -08:00) Pacific Time (US & Canada); Tijuana", "-8"));
            Items.Add (new ListItem ("(GMT -07:00) Mountain Time (US & Canada)", "-7"));
            Items.Add (new ListItem ("(GMT -06:00) Central Time (US & Canada)", "-6"));
            Items.Add (new ListItem ("(GMT -05:00) Eastern Time (US & Canada)", "-5"));
            Items.Add (new ListItem ("(GMT -04:00) Atlantic Time (Canada)", "-4"));
//            Items.Add (new ListItem ("(GMT -03:30) Newfoundland", "-3.5"));
            Items.Add (new ListItem ("(GMT -03:00) Buenos Aires, Georgetown", "-3"));
            Items.Add (new ListItem ("(GMT -02:00) Mid-Atlantic", "-2"));
            Items.Add (new ListItem ("(GMT -01:00) Cape Verde Is.", "-1"));
            Items.Add (new ListItem ("(GMT) Casablanca, Monrovia", "0"));
            Items.Add (new ListItem ("(GMT) Greenwich Mean Time : Dublin, Edinburgh, Lisbon, London", "0"));
            Items.Add (new ListItem ("(GMT +01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna", "1"));
            Items.Add (new ListItem ("(GMT +02:00) Athens, Bucharest, Istanbul, Minsk", "2"));
            Items.Add (new ListItem ("(GMT +03:00) Moscow, St. Petersburg, Volgograd", "3"));
//            Items.Add (new ListItem ("(GMT +03:30) Tehran", "3.5"));
            Items.Add (new ListItem ("(GMT +04:00) Abu Dhabi, Muscat", "4"));
//            Items.Add (new ListItem ("(GMT +04:30) Kabul", "4.5"));
            Items.Add (new ListItem ("(GMT +05:00) Islamabad, Karachi, Tashkent", "5"));
//            Items.Add (new ListItem ("(GMT +05:30) Chennai, Kolkata, Mumbai, New Delhi", "5.5"));
//            Items.Add (new ListItem ("(GMT +05:45) Kathmandu", "5.75"));
            Items.Add (new ListItem ("(GMT +06:00) Almaty, Novosibirsk", "6"));
            Items.Add (new ListItem ("(GMT +06:30) Rangoon", "6.5"));
            Items.Add (new ListItem ("(GMT +07:00) Bangkok, Hanoi, Jakarta", "7"));
            Items.Add (new ListItem ("(GMT +08:00) Beijing, Chongqing, Hong Kong, Urumqi", "8"));
            Items.Add (new ListItem ("(GMT +09:00) Osaka, Sapporo, Tokyo", "9"));
//            Items.Add (new ListItem ("(GMT +09:30) Adelaide", "9.5"));
            Items.Add (new ListItem ("(GMT +10:00) Canberra, Melbourne, Sydney", "10"));
            Items.Add (new ListItem ("(GMT +11:00) Magadan, Solomon Is., New Caledonia", "11"));
            Items.Add (new ListItem ("(GMT +12:00) Auckland, Fiji, Kamchatka, Marshal Is.", "12"));
            Items.Add (new ListItem ("(GMT +13:00) Nuku'alfoa", "13"));
        }
    
    }
}
