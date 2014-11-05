using System;
using System.Xml.Serialization;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;

namespace Spinit.Wpc.Synologen.Svefaktura.SBDH
{
    [Serializable]
    [XmlRoot("StandardBusinessDocument", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader", IsNullable = false)]
    public class Document
    {
        public Document()
        {
            StandardBusinessDocumentHeader = new Header();
        }

        public Document(EdiAddress sender, EdiAddress receiver)
            : this()
        {
            StandardBusinessDocumentHeader.Sender.Identifier.SetIdentity(sender);
            StandardBusinessDocumentHeader.Receiver.Identifier.SetIdentity(receiver);
        }

        public Header StandardBusinessDocumentHeader { get; set; }
    }

    [Serializable]
    [XmlRoot("StandardBusinessDocumentHeader", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader", IsNullable = false)]
    public class Header
    {
        public Header()
        {
            HeaderVersion = new HeaderVersion("1.0");
            Sender = new Sender();
            Receiver = new Receiver();
            DocumentIdentification = new DocumentIdentification();
        }

        public HeaderVersion HeaderVersion { get; set; }
        public Sender Sender { get; set; }
        public Receiver Receiver { get; set; }
        public DocumentIdentification DocumentIdentification { get; set; }
    }

    [Serializable]
    [XmlRoot("HeaderVersion", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader", IsNullable = false)]
    public class HeaderVersion
    {
        public HeaderVersion() { }
        public HeaderVersion(string value)
        {
            Value = value;
        }

        [XmlText]
        public string Value { get; set; }
    }

    [Serializable]
    [XmlRoot("Sender", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader", IsNullable = false)]
    public class Sender : Party { }

    [Serializable]
    [XmlRoot("Receiver", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader", IsNullable = false)]
    public class Receiver : Party { }

    public class Party
    {
        public Party()
        {
            Identifier = new Identifier();
        }

        public Identifier Identifier { get; set; }
    }

    [Serializable]
    [XmlRoot("Identifier", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader", IsNullable = false)]
    public class Identifier
    {
        public void SetIdentity(string id, string authority = null)
        {
            Value = id;
            Authority = authority;
        }

        public void SetIdentity(EdiAddress address, string authority = null)
        {
            SetIdentity(address.ToString(), authority);
        }


        [XmlAttribute("Authority")]
        public string Authority { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    [Serializable]
    [XmlRoot("DocumentIdentification", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader", IsNullable = false)]
    public class DocumentIdentification
    {
        public DocumentIdentification()
        {
            Standard = new Standard("urn:sfti:documents:BasicInvoice:1:0");
            TypeVersion = new TypeVersion("1.0");
            InstanceIdentifier = new InstanceIdentifier(1);
            Type = new Type("BasicInvoice");
            MultipleType = new MultipleType(false);
            CreationDateAndTime = new CreationDateAndTime();
        }
        public Standard Standard { get; set; }
        public TypeVersion TypeVersion { get; set; }
        public InstanceIdentifier InstanceIdentifier { get; set; }
        public Type Type { get; set; }
        public MultipleType MultipleType { get; set; }
        public CreationDateAndTime CreationDateAndTime { get; set; }
    }

    [XmlRoot("Standard", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader", IsNullable = false)]
    public class Standard
    {
        public Standard() { }
        public Standard(string value)
        {
            Value = value;
        }

        [XmlText]
        public string Value { get; set; }
    }

    [XmlRoot("TypeVersion", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader", IsNullable = false)]
    public class TypeVersion
    {
        public TypeVersion() { }
        public TypeVersion(string value)
        {
            Value = value;
        }

        [XmlText]
        public string Value { get; set; }
    }

    [XmlRoot("InstanceIdentifier", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader", IsNullable = false)]
    public class InstanceIdentifier
    {
        public InstanceIdentifier() {}
        public InstanceIdentifier(int value)
        {
            Value = value;
        }

        [XmlText]
        public int Value { get; set; }
    }

    [XmlRoot("Type", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader", IsNullable = false)]
    public class Type
    {
        public Type() { }
        public Type(string value)
        {
            Value = value;
        }

        [XmlText]
        public string Value { get; set; }
    }

    [XmlRoot("MultipleType", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader", IsNullable = false)]
    public class MultipleType
    {
        public MultipleType() { }
        public MultipleType(bool value)
        {
            Value = value;
        }

        [XmlText]
        public bool Value { get; set; }
    }

    [XmlRoot("CreationDateAndTime", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader", IsNullable = false)]
    public class CreationDateAndTime
    {
        public CreationDateAndTime()
        {
            Value = DateTime.Now;
        }
        public CreationDateAndTime(DateTime value)
        {
            Value = value;
        }

        [XmlText]
        public DateTime Value { get; set; }
    }
}