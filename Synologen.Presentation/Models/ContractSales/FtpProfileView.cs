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

		[Required(ErrorMessage = "Namn �r obligatoriskt"), DisplayName("Namn")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Server-URL �r obligatoriskt"), DisplayName("Server-URL")]
		public string ServerURL { get; set; }

		[Required(ErrorMessage = "Anv�ndarnamn �r obligatoriskt"), DisplayName("Anv�ndarnamn")]
		public string Username { get; set; }

		[Required(ErrorMessage = "L�sernord �r obligatoriskt"), DisplayName("L�senord")]
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