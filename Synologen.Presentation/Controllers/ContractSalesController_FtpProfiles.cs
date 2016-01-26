using System;
using System.Configuration;
using System.Web.Mvc;
using NHibernate;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Data.Queries.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Synologen.Presentation.Models.ContractSales;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Controllers
{
	public partial class ContractSalesController
	{

	    [HttpGet]
		public ActionResult AddFtpProfile()
		{
			return View(_viewService.SetFtpProfileViewDefaults(new FtpProfileView(), "Skapa ny FTP-profil"));
		}

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddFtpProfile(FtpProfileView ftpProfileView)
        {
            if (!ModelState.IsValid)
            {
                return View(_viewService.SetFtpProfileViewDefaults(ftpProfileView, "Skapa ny FTP-profil"));
            }
            var ftpProfile = _viewService.ParseFtpProfile(ftpProfileView);
            Execute(new CreateContractFtpProfileCommand(ftpProfile, _session.Connection.ConnectionString));
            MessageQueue.SetMessage("En ny ftp-profil har sparats");
            return Redirect(ComponentPages.FtpProfiles.Replace("~", ""));
        }

	    [HttpGet]
	    public ActionResult EditFtpProfile(int id)
	    {
	        var ftpProfile = new ContractFtpProfileByIdQuery(id, _session.Connection.ConnectionString).Execute();
	        return View(_viewService.GetFtpProfileView(ftpProfile, "Redigera profil"));
	    }

	    [HttpPost, ValidateAntiForgeryToken]
	    public ActionResult EditFtpProfile(FtpProfileView ftpProfileView)
	    {
            var ftpProfile = new ContractFtpProfileByIdQuery(ftpProfileView.Id, _session.Connection.ConnectionString).Execute();
            if (!ModelState.IsValid)
	        {
                return View(_viewService.GetFtpProfileView(ftpProfile, "Redigera profil"));
            }

            ftpProfile.Name = ftpProfileView.Name;
            ftpProfile.PassiveFtp = ftpProfileView.PassiveFtp;
            ftpProfile.Password = ftpProfileView.Password;
            ftpProfile.ProtocolType = (FtpProtocolType) ftpProfileView.SelectedFtpProtocolType;
            ftpProfile.ServerUrl = ftpProfileView.ServerURL;
            ftpProfile.Username = ftpProfileView.Username;

            Execute(new UpdateContractFtpProfileCommand(ftpProfile, _session.Connection.ConnectionString));
            MessageQueue.SetMessage("Ftp-profilen har redigerats");
            return Redirect(ComponentPages.FtpProfiles.Replace("~", ""));
        }
    }
}