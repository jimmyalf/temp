using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
    public class FtpProfileView
	{
		public int Id { get; set; }
		public string FormLegend { get; set; }

		[Required(ErrorMessage = "Namn är obligatoriskt"), DisplayName("Namn")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Server-URL är obligatoriskt"), DisplayName("Server-URL")]
		public string ServerURL { get; set; }

		[Required(ErrorMessage = "Användarnamn är obligatoriskt"), DisplayName("Användarnamn")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Lösernord är obligatoriskt"), DisplayName("Lösenord")]
		public string Password { get; set; }

        public int SelectedFtpProtocolType { get; set; }

        [DisplayName("Protokolltyp")]
        public IEnumerable<FtpProtocolTypeView> FtpProtocolType { get; set; }

        [DisplayName("Passiv FTP")]
        public bool PassiveFtp { get; set; }

        public SelectList GetFtpProtocolsSelectList()
        {
            return new SelectList(FtpProtocolType, "Id", "Name");
        }
    }
}