﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Denna kod har genererats av ett verktyg.
//     Körtidsversion:2.0.50727.3082
//
//     Ändringar i denna fil kan orsaka fel och kommer att förloras om
//     koden återgenereras.
// </auto-generated>
//------------------------------------------------------------------------------


// 
// This source code was auto-generated by xsd, Version=2.0.50727.42.
// 
namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CoreComponentParameters:1:0")]
[System.Xml.Serialization.XmlRootAttribute("Instance", Namespace="urn:oasis:names:tc:ubl:CoreComponentParameters:1:0", IsNullable=false)]
public partial class InstanceType {
    
    private string nameField;
    
    private string codeListIDField;
    
    private string codeListAgencyIDField;
    
    private string codeListAgencyNameField;
    
    private string codeListNameField;
    
    private string codeListVersionIDField;
    
    private string codeListUniformResourceIDField;
    
    private string codeListSchemeUniformResourceIDField;
    
    private string languageIDField;
    
    /// <remarks/>
    public string Name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="normalizedString")]
    public string CodeListID {
        get {
            return this.codeListIDField;
        }
        set {
            this.codeListIDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="normalizedString")]
    public string CodeListAgencyID {
        get {
            return this.codeListAgencyIDField;
        }
        set {
            this.codeListAgencyIDField = value;
        }
    }
    
    /// <remarks/>
    public string CodeListAgencyName {
        get {
            return this.codeListAgencyNameField;
        }
        set {
            this.codeListAgencyNameField = value;
        }
    }
    
    /// <remarks/>
    public string CodeListName {
        get {
            return this.codeListNameField;
        }
        set {
            this.codeListNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="normalizedString")]
    public string CodeListVersionID {
        get {
            return this.codeListVersionIDField;
        }
        set {
            this.codeListVersionIDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="anyURI")]
    public string CodeListUniformResourceID {
        get {
            return this.codeListUniformResourceIDField;
        }
        set {
            this.codeListUniformResourceIDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="anyURI")]
    public string CodeListSchemeUniformResourceID {
        get {
            return this.codeListSchemeUniformResourceIDField;
        }
        set {
            this.codeListSchemeUniformResourceIDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="language")]
    public string LanguageID {
        get {
            return this.languageIDField;
        }
        set {
            this.languageIDField = value;
        }
    }
}
}