//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option or rebuild the Visual Studio project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "10.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Templates {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Templates() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Templates", global::System.Reflection.Assembly.Load("App_GlobalResources"));
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;html&gt;
        ///  &lt;head&gt;
        ///    &lt;style type=&quot;text/css&quot;&gt;
        ///		td { padding-right: 1em; }
        ///		body { padding:0; margin:0; font: 0.7em Verdana, Arial, Helvetica, sans-serif; } 
        ///		table {font-size: 1.0em;}
        ///	&lt;/style&gt;
        ///  &lt;/head&gt;
        ///	&lt;body&gt;
        ///	&lt;table&gt;
        ///		&lt;tr&gt;&lt;td colspan=&quot;3&quot;&gt;Beställnings-id: {OrderId}&lt;/td&gt;&lt;/tr&gt;
        ///		&lt;tr&gt;&lt;td colspan=&quot;3&quot;&gt;Butik: {ShopName}&lt;/td&gt;&lt;/tr&gt;
        ///		&lt;tr&gt;&lt;td colspan=&quot;3&quot;&gt;Butiksort: {ShopCity}&lt;/td&gt;&lt;/tr&gt;
        ///		&lt;tr&gt;&lt;td colspan=&quot;3&quot;&gt;Båge: {FrameName}&lt;/td&gt;&lt;/tr&gt;
        ///		&lt;tr&gt;&lt;td colspan=&quot;3&quot;&gt;Båge Artnr: {ArticleNumber}&lt;/td&gt;&lt;/tr&gt;
        ///		 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SynologenFrameOrderEmailTemplate {
            get {
                return ResourceManager.GetString("SynologenFrameOrderEmailTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;html&gt;
        ///  &lt;head&gt;
        ///    &lt;style type=&quot;text/css&quot;&gt;
        ///		td { padding-right: 1em; }
        ///		body { padding:0; margin:0; font: 0.7em Verdana, Arial, Helvetica, sans-serif; } 
        ///		table {font-size: 1.0em;}
        ///	&lt;/style&gt;
        ///  &lt;/head&gt;
        ///	&lt;body&gt;
        ///	&lt;table&gt;
        ///		&lt;tr&gt;&lt;td colspan=&quot;6&quot;&gt;Beställnings-id: {OrderId}&lt;/td&gt;&lt;/tr&gt;
        ///		&lt;tr&gt;&lt;td colspan=&quot;6&quot;&gt;Från butik: {ShopName}&lt;/td&gt;&lt;/tr&gt;
        ///		&lt;tr&gt;&lt;td colspan=&quot;6&quot;&gt;Artikel: {Article}&lt;/td&gt;&lt;/tr&gt;
        ///		&lt;tr class=&quot;spacer-row&quot;&gt;&lt;td colspan=&quot;6&quot;/&gt;&lt;/tr&gt;
        ///		&lt;tr&gt;
        ///			&lt;td&gt;Styrka Höger: {RightPower}&lt;/td&gt;
        ///			&lt;td&gt;Addition [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SynologenOrderEmailTemplate {
            get {
                return ResourceManager.GetString("SynologenOrderEmailTemplate", resourceCulture);
            }
        }
    }
}
