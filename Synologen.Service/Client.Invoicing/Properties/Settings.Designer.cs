﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synologen.Service.Client.Invoicing.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\moccasin\\SPCS_Administration\\Gemensamma filer")]
        public string SPCSCommonFilesPath {
            get {
                return ((string)(this["SPCSCommonFilesPath"]));
            }
            set {
                this["SPCSCommonFilesPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\moccasin\\SPCS_Administration\\Företag\\Nya Synhälsan Svenska AB")]
        public string SPCSCompanyPath {
            get {
                return ((string)(this["SPCSCompanyPath"]));
            }
            set {
                this["SPCSCompanyPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("carl.berg@spinit.se")]
        public string ReportEmailAddress {
            get {
                return ((string)(this["ReportEmailAddress"]));
            }
            set {
                this["ReportEmailAddress"] = value;
            }
        }
    }
}
