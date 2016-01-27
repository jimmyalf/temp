using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
    public class FtpProtocolTypeView
    {
        public FtpProtocolTypeView(int id, string protocolName)
        {
            Id = id;
            Name = protocolName;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}