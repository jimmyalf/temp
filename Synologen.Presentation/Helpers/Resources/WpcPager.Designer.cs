﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Spinit.Wpc.Synologen.Presentation.Helpers.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class WpcPager {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal WpcPager() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Spinit.Wpc.Synologen.Presentation.Helpers.Resources.WpcPager", typeof(WpcPager).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Page {0} of {1} ({2} items).
        /// </summary>
        public static string CenterText {
            get {
                return ResourceManager.GetString("CenterText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to « First.
        /// </summary>
        public static string FirstText {
            get {
                return ResourceManager.GetString("FirstText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Last ».
        /// </summary>
        public static string LastText {
            get {
                return ResourceManager.GetString("LastText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Next ».
        /// </summary>
        public static string Next {
            get {
                return ResourceManager.GetString("Next", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to « Previous.
        /// </summary>
        public static string PreviousText {
            get {
                return ResourceManager.GetString("PreviousText", resourceCulture);
            }
        }
    }
}
