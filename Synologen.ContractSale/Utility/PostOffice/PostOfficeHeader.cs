using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Invoicing.PostOffice
{
    public class PostOfficeHeader
    {
        protected readonly EdiAddress Sender;
        protected readonly EdiAddress Receiver;
        protected string Instruction { get; set; }
        protected string MessageType { get; set; }

        public PostOfficeHeader(string instruction, string messageType, EdiAddress sender, EdiAddress receiver)
        {
            Sender = sender;
            Receiver = receiver;
            Instruction = instruction;
            MessageType = messageType;
        }

        public virtual string Render()
        {
            var attributes = new PostOfficeHeaderAttributeList();
            attributes.Add("SND", Sender.Address);
            attributes.Add("SNDKVAL", Sender.Quantifier);
            attributes.Add("REC", Receiver.Address);
            attributes.Add("RECKVAL", Receiver.Quantifier);
            attributes.Add("MSGTYPE", MessageType);

            //<?POSTEN SND="5562626100" SNDKVAL="14" REC="5560334285" RECKVAL="14" MSGTYPE="SVEFAKTURA"?>
            //<?POSTNET SND="STREAMS000345" REC="SE00087815000" MSGTYPE="SYNOLOG"?>
            return string.Format("<?{0} {1}?>", Instruction, attributes.Render());
        }
    }
}