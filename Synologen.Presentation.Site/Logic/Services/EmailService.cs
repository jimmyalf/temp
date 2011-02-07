using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Services
{
	public class EmailService : IEmailService
	{
		public void SendEmail(string from, string to, string subject, string body) {
			SpinitServices.SendMail(to, null, from, null, subject, body, null, SpinitServices.Priority.Medium, null, DateTime.Now);
		}
	}
}