using System;
using System.Net.Mail;
using System.ComponentModel;
using System.Xml;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Components {

    /// <summary>
    /// This class contains properties for an EmailTemplate.
    /// </summary>
    public class EmailTemplate : System.Net.Mail.MailMessage
    {
        EmailType emailType;
        Guid emailID;

        public EmailTemplate() {
        }

        public EmailTemplate(XmlNode node) {

            // Read the attributes
            //
            Priority    = (MailPriority) Enum.Parse(Priority.GetType(), node.Attributes.GetNamedItem("priority").InnerText);
            emailType   = (EmailType) EmailType.Parse(EmailType.GetType(), node.Attributes.GetNamedItem("emailType").InnerText);
            Subject     = node.SelectSingleNode("subject").InnerText;
            Body        = node.SelectSingleNode("body").InnerText;
            From        = new MailAddress(node.SelectSingleNode("from").InnerText);

        }

        public EmailType EmailType {
            get {
                return emailType;
            }
        }

        public Guid EmailID {
            get { return emailID; }
            set { emailID = value; }
        }
    }

}
