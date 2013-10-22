//------------------------------------------------------------------------------
// <copyright company="Telligent Systems, Inc.">
//	Copyright (c) Telligent Systems, Inc.  All rights reserved. Please visit
//	http://www.communityserver.org for more details.
// </copyright>                                                                
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.IO;
using System.Web.Caching;
using System.Text.RegularExpressions;
using System.Text;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Components {

    /// <summary>
    /// This class is used to take the body of a post message and pre-format it to HTML. Two versions
    /// of the post body are stored in the database: raw original post and the transformed pre-formatted
    /// post. Pre-transforming the post offers better performance.
    /// </summary>
    public class Transforms {

		#region FormatPost
        // *********************************************************************
        //  FormatPost
        //
        /// <summary>
        /// Main public routine called to perform string transforms.
        /// </summary>
        /// 
        // ********************************************************************/
        public static string FormatPost (string rawPostBody)  {
            return FormatPost (rawPostBody, PostType.HTML, true);
        }

        public static string FormatPost (string rawPostBody, PostType postType) {
            return FormatPost (rawPostBody, postType, true);
        }

        public static string FormatPost (string rawPostBody, PostType postType, bool allowCustomTransforms) {
            string formattedPost = rawPostBody;	

			// Perform HTML Encoding of any tag elements, if not PostType.HTML
			//
			if (postType != PostType.HTML) {
				formattedPost = HttpUtility.HtmlEncode(formattedPost);
				formattedPost = formattedPost.Replace("\n", Globals.HtmlNewLine);
			} 

			// Remove any script code
			//
			formattedPost = Transforms.StripScriptTags(formattedPost);

			// Fix to reverse certain items that were HtmlEncoded above
			//
			formattedPost = UnHtmlEncode(formattedPost);

			// Do BBCode transform, if any
			//
			formattedPost = BBcodeToHtml(formattedPost); 

			// Peform specialized transforms first.
			//
			if (allowCustomTransforms) {
				formattedPost = EmoticonTransforms(formattedPost);
				formattedPost = PerformSpecializedTransforms(formattedPost);
			}

            return formattedPost;
        }
		#endregion

		#region CensorPost
		public static string CensorPost( string formattedPost ) {
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled;
			ArrayList censors = Spinit.Wpc.Forum.Censors.GetCensors();

			foreach( Censor censor in censors ) {
				formattedPost = Regex.Replace(formattedPost, censor.Word, censor.Replacement, options);
			}
			return formattedPost;
		}
		#endregion

		#region UnHtmlEncode
		// *******************************************************************
		// UnHtmlEncode
		//
		/// <summary>
		/// If a post has been HtmlEncoded using the Web.HttpUtility, there are
		/// a few characters we don't want encoded.  This will allow for the 
		/// bbcode transforms to properly work.  So we must "UnHtmlEncode" these
		/// items, back to what they originally were.
		/// </summary>
		/// <param name="formattedPost">string of post</param>
		/// <returns>
		/// An Un-HtmlEncoded String of select items.
		/// </returns>
		// *******************************************************************
		static string UnHtmlEncode(string formattedPost) {
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled;

			// &quot;
			// TODO: edit the regex match to only replace within a set of brackets [].
			//
			formattedPost = Regex.Replace(formattedPost, "&quot;", "\"", options);
			
			return formattedPost;
		}
		#endregion

		#region BBDecode
        // *********************************************************************
        //  BBDecode
        //
        /// <summary>
        /// Transforms a BBCode encoded string in appropriate HTML
        /// </summary>
        /// 
        // ********************************************************************/
        static string BBcodeToHtml(string encodedString) {
            // TDD TODO this shouldn't be hard coded, should be in a style sheet
			//

			// Used for normal quoting with a "<pic> <username> wrote:" prefix.
			//
			string quoteStartHtml = "";	
			string quoteEndHtml = "</td></tr></table></td></tr></table></BLOCKQUOTE>";
			//quoteStartHtml = "<BLOCKQUOTE><table width=\"85%\"><tr><td class=\"txt4\"><img src=\"" + Globals.GetSkinPath() +"/images/icon-quote.gif" + "\">&nbsp;<strong>$1 wrote:</strong></td></tr><tr><td class=\"quoteTable\"><table width=\"100%\"><tr><td width=\"100%\" valign=\"top\" class=\"txt4\">$3</td></tr></table></td></tr></table></BLOCKQUOTE>";

			// Used for when a username is not supplied.
			//
			string emptyquoteStartHtml = "<BLOCKQUOTE><table width=\"85%\"><tr><td class=\"quoteTable\"><table width=\"100%\"><tr><td width=\"100%\" valign=\"top\" class=\"txt4\">";
			string emptyquoteEndHtml = "</td></tr></table></td></tr></table></BLOCKQUOTE>";
			//string emptyquoteStartHtml = "<BLOCKQUOTE><table width=\"85%\"><tr><td class=\"quoteTable\"><table width=\"100%\"><tr><td width=\"100%\" valign=\"top\" class=\"txt4\">$2</td></tr></table></td></tr></table></BLOCKQUOTE>";

			// When using the Importer, we do not have a skin path.  We hardcode it here to
			// the default path.
			//
			if (Globals.GetSkinPath() == "")
				quoteStartHtml = "<BLOCKQUOTE><table width=\"85%\"><tr><td class=\"txt4\"><img src=\"themes/default/images/icon-quote.gif" + "\">&nbsp;<strong>$1 wrote:</strong></td></tr><tr><td class=\"quoteTable\"><table width=\"100%\"><tr><td width=\"100%\" valign=\"top\" class=\"txt4\">";
			else
				quoteStartHtml = "<BLOCKQUOTE><table width=\"85%\"><tr><td class=\"txt4\"><img src=\"" + Globals.GetSkinPath() +"/images/icon-quote.gif" + "\">&nbsp;<strong>$1 wrote:</strong></td></tr><tr><td class=\"quoteTable\"><table width=\"100%\"><tr><td width=\"100%\" valign=\"top\" class=\"txt4\">";

			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled;

            // Bold, Italic, Underline
            //
            encodedString = Regex.Replace(encodedString, @"\[b(?:\s*)\]((.|\n)*?)\[/b(?:\s*)\]", "<b>$1</b>", options);
			encodedString = Regex.Replace(encodedString, @"\[i(?:\s*)\]((.|\n)*?)\[/i(?:\s*)\]", "<i>$1</i>", options);
            encodedString = Regex.Replace(encodedString, @"\[u(?:\s*)\]((.|\n)*?)\[/u(?:\s*)\]", "<u>$1</u>", options);

			// Left, Right, Center
			encodedString = Regex.Replace(encodedString, @"\[left(?:\s*)\]((.|\n)*?)\[/left(?:\s*)]", "<div style=\"text-align:left\">$1</div>", options);
			encodedString = Regex.Replace(encodedString, @"\[center(?:\s*)\]((.|\n)*?)\[/center(?:\s*)]", "<div style=\"text-align:center\">$1</div>", options);
			encodedString = Regex.Replace(encodedString, @"\[right(?:\s*)\]((.|\n)*?)\[/right(?:\s*)]", "<div style=\"text-align:right\">$1</div>", options);

            // Quote
            //
            //encodedString = Regex.Replace(encodedString, "\\[quote(?:\\s*)user=\"((.|\n)*?)\"\\]((.|\n)*?)\\[/quote(\\s*)\\]", quote, options);
			//encodedString = Regex.Replace(encodedString, "\\[quote(\\s*)\\]((.|\n)*?)\\[/quote(\\s*)\\]", emptyquote, options);
			encodedString = Regex.Replace(encodedString, "\\[quote(?:\\s*)user=\"((.|\n)*?)\"\\]", quoteStartHtml, options);
			encodedString = Regex.Replace(encodedString, "\\[/quote(\\s*)\\]", quoteEndHtml, options);
			encodedString = Regex.Replace(encodedString, "\\[quote(\\s*)\\]", emptyquoteStartHtml, options);
			encodedString = Regex.Replace(encodedString, "\\[/quote(\\s*)\\]", emptyquoteEndHtml, options);
 

            // Anchors
            //
            encodedString = Regex.Replace(encodedString, "<a href=\"((.|\n)*?)\">((.|\n)*?)</a>", "<a target=\"_blank\" title=\"$1\" href=\"$1\">$3</a>", options);
			encodedString = Regex.Replace(encodedString, @"\[url(?:\s*)\]www\.(.*?)\[/url(?:\s*)\]", "<a href=\"http://www.$1\" target=\"_blank\" title=\"$1\">$1</a>", options );
			encodedString = Regex.Replace(encodedString, @"\[url(?:\s*)\]((.|\n)*?)\[/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$1</a>", options );
			encodedString = Regex.Replace(encodedString, @"\[url=""((.|\n)*?)(?:\s*)""\]((.|\n)*?)\[/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>", options );
			encodedString = Regex.Replace(encodedString, @"\[url=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>", options );
			encodedString = Regex.Replace(encodedString, @"\[link(?:\s*)\]((.|\n)*?)\[/link(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$1</a>", options );
			encodedString = Regex.Replace(encodedString, @"\[link=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/link(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>", options );

            // Image
            //
            encodedString = Regex.Replace(encodedString, @"\[img(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "<img src=\"$1\" border=\"0\" />", options);
			encodedString = Regex.Replace(encodedString, @"\[img=((.|\n)*?)x((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "<img width=\"$1\" height=\"$3\" src=\"$5\" border=\"0\" />", options );

            // Color
            //
            encodedString = Regex.Replace(encodedString, @"\[color=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/color(?:\s*)\]", "<span style=\"color=$1;\">$3</span>", options);

            // Horizontal Rule
            //
            encodedString = Regex.Replace(encodedString, @"\[hr(?:\s*)\]", "<hr />", options);
        
            // Email
            //
            encodedString = Regex.Replace(encodedString, @"\[email(?:\s*)\]((.|\n)*?)\[/email(?:\s*)\]", "<a href=\"mailto:$1\">$1</a>", options);

            // Font size
            //
            encodedString = Regex.Replace(encodedString, @"\[size=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/size(?:\s*)\]", "<span style=\"font-size:$1\">$3</span>", options);
			encodedString = Regex.Replace(encodedString, @"\[font=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/font(?:\s*)\]", "<span style=\"font-family:$1;\">$3</span>", options );
			encodedString = Regex.Replace(encodedString, @"\[align=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/align(?:\s*)\]", "<div style=\"text-align:$1;\">$3</span>", options );
			encodedString = Regex.Replace(encodedString, @"\[float=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/float(?:\s*)\]", "<div style=\"float:$1;\">$3</div>", options );

			string sListFormat = "<ol class=\"anf_list\" style=\"list-style:{0};\">$1</ol>";
			// Lists
			encodedString = Regex.Replace(encodedString, @"\[\*(?:\s*)]\s*([^\[]*)", "<li>$1</li>", options );
			encodedString = Regex.Replace(encodedString, @"\[list(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", "<ul class=\"anf_list\">$1</ul>", options );
			encodedString = Regex.Replace(encodedString, @"\[list=1(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format( sListFormat, "decimal" ), options );
			encodedString = Regex.Replace(encodedString, @"\[list=i(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format( sListFormat, "lower-roman" ), RegexOptions.Compiled );
			encodedString = Regex.Replace(encodedString, @"\[list=I(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format( sListFormat, "upper-roman" ), RegexOptions.Compiled );
			encodedString = Regex.Replace(encodedString, @"\[list=a(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format( sListFormat, "lower-alpha" ), RegexOptions.Compiled );
			encodedString = Regex.Replace(encodedString, @"\[list=A(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format( sListFormat, "upper-alpha" ), RegexOptions.Compiled );

            return encodedString;
        }
		#endregion

		#region Strip Tags
        public static string StripForPreview (string content) {
            content = Regex.Replace(content, "<br>", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			content = Regex.Replace(content, "<br/>", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			content = Regex.Replace(content, "<br />", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            content = Regex.Replace(content, "<p>", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            content = content.Replace("'", "&#39;");

            return StripHtmlXmlTags(content);
        }

        public static string StripHtmlXmlTags (string content) {
            return Regex.Replace(content, "<[^>]+>", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        // *********************************************************************
        //  StripScriptTags
        //
        /// <summary>
        /// Helper function used to ensure we don't inject script into the db.
        /// </summary>
        /// <param name="dirtyText">Text to be cleaned for script tags</param>
        /// <returns>Clean text with no script tags.</returns>
        /// 
        // ********************************************************************/
        public static string StripScriptTags(string content) {
            string cleanText;

            // Perform RegEx
            content = Regex.Replace(content, "<script((.|\n)*?)</script>", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            cleanText = Regex.Replace(content, "\"javascript:", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return cleanText;
        }
		#endregion

		#region Emoticon & User Transforms
        static string EmoticonTransforms (string formattedPost) {

            try {
                // Load the emoticon transform table
                //
				ArrayList emoticonTxTable = Smilies.GetSmilies();
				
				const string imgFormat = "<img src=\"{0}/{1}\" alt=\"{2}\" />";

				// EAD 6/27/2004: Changed to loop through twice.
				// Once for brackets first, so to capture the Party emoticon
				// (special emoticons that REQUIRE brackets), so not to replace
				// with other non-bracket icons. Less efficient yes, but captures 
				// all emoticons properly.
				//
				string smileyPattern	= "";
				string replacePattern	= "";
				RegexOptions options	= RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline ;
				foreach ( Smiley smiley in emoticonTxTable ) {

					string forumHomePath = Globals.GetSiteUrls().Home;

					if (forumHomePath == "/")
						forumHomePath = "";

					smileyPattern	= string.Format(@"\[{0}\]", Regex.Escape(smiley.SmileyCode) );
					replacePattern	= string.Format(imgFormat, forumHomePath, smiley.SmileyUrl, smiley.SmileyText);

					formattedPost = Regex.Replace( formattedPost, smileyPattern, replacePattern, options );				
					if ( smiley.IsSafeWithoutBrackets() ) {
						formattedPost = Regex.Replace( formattedPost, Regex.Escape(smiley.SmileyCode), 
								string.Format(imgFormat, Globals.ApplicationPath, smiley.SmileyUrl, smiley.SmileyText), 
								options ); 
					}
				}

				return formattedPost;
            } catch( Exception e ) {
				ForumException ex = new ForumException( ForumExceptionType.UnknownError, "Could not transform smilies in the post.", e );
				ex.Log();

				return formattedPost;
            }

        }

        // *********************************************************************
        //  PerformUserTransforms
        //
        /// <summary>
        /// Performs the user defined transforms
        /// </summary>
        /// 
        // ********************************************************************/
        static string PerformUserTransforms(string stringToTransform, ArrayList userDefinedTransforms) {
            int iLoop = 0;			

            while (iLoop < userDefinedTransforms.Count) {		
		        
                // Special work for anchors
                stringToTransform = Regex.Replace(stringToTransform, userDefinedTransforms[iLoop].ToString(), userDefinedTransforms[iLoop+1].ToString(), RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);

                iLoop += 2;
            }

            return stringToTransform;
        }


        // *********************************************************************
        //  LoadTransformFile
        //
        /// <summary>
        /// Returns an array list containing transforms that the user defined in a transform file.
        /// </summary>
        /// 
        // ********************************************************************/
        private static ArrayList LoadTransformFile (string filename) {
            string cacheKey = "transformTable-" + filename;
            ArrayList tranforms;
            string filenameOfTransformFile;

            // read the transformation hashtable from the cache
            //
            tranforms = (ArrayList) HttpRuntime.Cache[cacheKey];

            if (tranforms == null) {

                tranforms = new ArrayList();

                // Grab the transform file
                //
                filenameOfTransformFile = ForumContext.Current.Context.Request.MapPath("~/" + filename);

                if (filenameOfTransformFile.Length > 0) {

                    StreamReader sr = File.OpenText( filenameOfTransformFile );

                    // Read through each set of lines in the text file
                    //
                    string line = sr.ReadLine(); 
                    string replaceLine = "";

                    while (line != null) {

                        line = Regex.Escape(line);
                        replaceLine = sr.ReadLine();

                        // make sure replaceLine != null
                        //
                        if (replaceLine == null) 
                            break;
					
                        line = line.Replace("<CONTENTS>", "((.|\n)*?)");
                        line = line.Replace("<WORDBOUNDARY>", "\\b");
                        line = line.Replace("<", "&lt;");
                        line = line.Replace(">", "&gt;");
                        line = line.Replace("\"", "&quot;");

                        replaceLine = replaceLine.Replace("<CONTENTS>", "$1");					
					
                        tranforms.Add(line);
                        tranforms.Add(replaceLine);

                        line = sr.ReadLine();

                    }

                    // close the streamreader
                    //
                    sr.Close();		

                    // slap the ArrayList into the cache and set its dependency to the transform file.
                    //
                    HttpRuntime.Cache.Insert(cacheKey, tranforms, new CacheDependency(filenameOfTransformFile));
                }
            }
  
            return (ArrayList) HttpRuntime.Cache[cacheKey];
        }
		#endregion

		#region FormatEditNotes()
        private static string FormatEditNotes2(string stringToTransform) {

            Match match;
            StringBuilder editTable = null;

            match = Regex.Match(stringToTransform, ".Edit by=&quot;(?<Editor>(.|\\n)*?)&quot;.(?<Notes>(.|\\n)*?)./Edit.", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (match.Captures.Count > 0) {

                editTable = new StringBuilder();

                editTable.Append( "<table>" );
                editTable.Append( "<tr>" );
                editTable.Append( "<td>" );
                editTable.Append( match.Groups["Editor"].ToString() );
                editTable.Append( "</td>" );
                editTable.Append( "</tr>" );
                editTable.Append( "<tr>" );
                editTable.Append( "<td>" );
                editTable.Append( match.Groups["Notes"].ToString() );
                editTable.Append( "</td>" );
                editTable.Append( "</tr>" );
                editTable.Append( "</table>" );
            }
            if (editTable != null)
                return stringToTransform.Replace(match.ToString(), editTable.ToString());
            else
                return stringToTransform;
        }
		#endregion

		#region SourceCodeMarkup
        private static string SourceCodeMarkup(string stringToTransform) {
			StringBuilder formattedSource = new StringBuilder();
            string[] table = new string[3];

            table[0] = "<table border=\"0\" cellspacing=\"0\" width=\"100%\">";
            table[1] = "<tr><td width=\"15\"></td><td bgcolor=\"lightgrey\" width=\"15\"></td><td bgcolor=\"lightgrey\"><br>";
            table[2] = "<br>&nbsp;</td></tr></table>";
            MatchCollection matchs;

            // Look for code
            //
            matchs = Regex.Matches(stringToTransform, "\\[code language=\"(?<lang>(.|\\n)*?)\"\\](?<code>(.|\\n)*?)\\[/code\\]", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

            foreach (Match match in matchs) {

                // Remove HTML formatting code
                //
                string codeToFormat = match.Groups["code"].ToString().Replace("</P>\r\n", "<br>").Replace("<P>","");

                // Get the formatted source code
                //
                formattedSource.Append(table[0]);
                formattedSource.Append(table[1]);
                formattedSource.Append(FormatSource(codeToFormat, GetLanguageType(match.Groups["lang"].ToString())).Replace(Globals.HtmlNewLine, ""));
                formattedSource.Append(table[2]);

                // Update the main string
                //
                stringToTransform = stringToTransform.Replace(match.ToString(), formattedSource.ToString());
				formattedSource.Remove( 0, formattedSource.Length );
            }

            return stringToTransform;
        }
		#endregion

		#region PerformSpecializedTransforms
        private static string PerformSpecializedTransforms(string stringToTransform) {
            StringBuilder stringToReturn = new StringBuilder();
//            MatchCollection matchs;

            stringToTransform = SourceCodeMarkup(stringToTransform);

            return stringToTransform;

			// TDD 3/16/2004
			// commenting out the rest of this code since it's unreachable. If not needed after release then we can remove it.
//            // First we need to crack the string into segments to be transformed
//            // there is only 1 special marker we care about: [code language="xxx"][/code]
//            matchs = Regex.Matches(stringToTransform, "\\[code language=&quot;(?<lang>(.|\\n)*?)&quot;\\](?<code>(.|\\n)*?)\\[/code\\]", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
//
//            // Get the full string that we intend to return
//            //
//            stringToReturn.Append(stringToTransform);
//
//            foreach (Match match in matchs) {
//                char[] charCode = new char[] {'\r', '\n'};
//                string sourceCodeToMarkup;
//                string markedUpSourceCode;
//
//                // Code/Markup content
//                //
//                sourceCodeToMarkup = match.Groups["code"].ToString().TrimStart(charCode).TrimEnd(charCode);
//                //				markedUpSourceCode = FormatSource(sourceCodeToMarkup, ));
//
//                // Replace marked up code
//                //
//                string openTable = "<table width=\"100%\"><tr><td width=\"15\"></td><td bgcolor=\"lightgrey\">";
//                string closeTable = "</td></tr></table>";
//                stringToReturn = stringToReturn.Replace(sourceCodeToMarkup, openTable + markedUpSourceCode + closeTable);
//
//                // Remove the code tags
//                //
//                string openCodeTag = "[code language=&quot;" + match.Groups["lang"] + "&quot;]";
//                string closeCodeTag = "[/code]";
//
//                stringToReturn = stringToReturn.Replace(openCodeTag, ""); 
//                stringToReturn = stringToReturn.Replace(closeCodeTag, ""); 
//
//            }
//
//            return stringToReturn.ToString();
//
        }
		#endregion

		#region Private vars, enums, and helper functions
        static Language GetLanguageType(string language) {
            
            switch (language.ToUpper()) {
                case "VB" :
                    return Language.VB;
                case "JS" :
                    return Language.JScript;
                case "JScript" :
                    return Language.JScript;
                default :
                    return Language.CS;
            }
        }

        public enum Language {
            CS,
            VB,
            JScript
        }

        const int _fontsize = 2;
        const string TAG_FNTRED  = "<font color= \"red\">";
        const string TAG_FNTBLUE = "<font color= \"blue\">" ;
        const string TAG_FNTGRN  = "<font color= \"green\">" ;
        const string TAG_FNTMRN  = "<font color=\"maroon\">" ;
        const string TAG_EFONT   = "</font>" ;


        public static string FormatSource(string htmlEncodedSource, Language language) {

            StringWriter textBuffer = new StringWriter();

            textBuffer.Write("<font face=\"Lucida Console\" size=\"" + _fontsize + "\">");

            if(language == Language.CS) {
                textBuffer.Write(FixCSLine(htmlEncodedSource)) ;
            } else if(language == Language.JScript) {
                textBuffer.Write(FixJSLine(htmlEncodedSource)) ;
            } else if(language == Language.VB) {
                textBuffer.Write(FixVBLine(htmlEncodedSource)) ;
            }

            textBuffer.Write("</font>");

            return textBuffer.ToString();
        }

        static string FixCSLine(string source) {

            if (source == null)
                return null;

            source = Regex.Replace(source, "(?i)(\\t)", "    ");
            source = Regex.Replace(source, "(?i) ", "&nbsp;");

            String[] keywords = new String[] {
                                                 "private", "protected", "public", "namespace", "class", "break",
                                                 "for", "if", "else", "while", "switch", "case", "using",
                                                 "return", "null", "void", "int", "bool", "string", "float",
                                                 "this", "new", "true", "false", "const", "static", "base",
                                                 "foreach", "in", "try", "catch", "finally", "get", "set", "char", "default"};

            String CombinedKeywords = "(?<keyword>" + String.Join("|", keywords) + ")";

            source = Regex.Replace(source, "\\b" + CombinedKeywords + "\\b(?<!//.*)", TAG_FNTBLUE + "${keyword}" + TAG_EFONT);
            source = Regex.Replace(source, "(?<comment>//.*$)", TAG_FNTGRN + "${comment}" + TAG_EFONT);

            return source;
        }

        static string FixJSLine(string source) {
            if (source == null)
                return null;

            source = Regex.Replace(source, "(?i)(\\t)", "    ");

            String[] keywords = new String[] {
                                                 "private", "protected", "public", "namespace", "class", "var",
                                                 "for", "if", "else", "while", "switch", "case", "using", "get",
                                                 "return", "null", "void", "int", "string", "float", "this", "set",
                                                 "new", "true", "false", "const", "static", "package", "function",
                                                 "internal", "extends", "super", "import", "default", "break", "try",
                                                 "catch", "finally" };

            String CombinedKeywords = "(?<keyword>" + String.Join("|", keywords) + ")";

            source = Regex.Replace(source, "\\b" + CombinedKeywords + "\\b(?<!//.*)", TAG_FNTBLUE + "${keyword}" + TAG_EFONT);
            source = Regex.Replace(source, "(?<comment>//.*$)", TAG_FNTGRN + "${comment}" + TAG_EFONT);

            return source;
        }

        static string FixVBLine(string source) {
            if (source == null)
                return null;

            source = Regex.Replace(source, "(?i)(\\t)", "    ");

            String[] keywords = new String[] {
                                                 "Private", "Protected", "Public", "End Namespace", "Namespace",
                                                 "End Class", "Exit", "Class", "Goto", "Try", "Catch", "End Try",
                                                 "For", "End If", "If", "Else", "ElseIf", "Next", "While", "And",
                                                 "Do", "Loop", "Dim", "As", "End Select", "Select", "Case", "Or",
                                                 "Imports", "Then", "Integer", "Long", "String", "Overloads", "True",
                                                 "Overrides", "End Property", "End Sub", "End Function", "Sub", "Me",
                                                 "Function", "End Get", "End Set", "Get", "Friend", "Inherits",
                                                 "Implements","Return", "Not", "New", "Shared", "Nothing", "Finally",
                                                 "False", "Me", "My", "MyBase" };


            String CombinedKeywords = "(?<keyword>" + String.Join("|", keywords) + ")";

            source = Regex.Replace(source, "(?i)\\b" + CombinedKeywords + "\\b(?<!'.*)", TAG_FNTBLUE + "${keyword}" + TAG_EFONT);
            source = Regex.Replace(source, "(?<comment>'(?![^']*&quot;).*$)", TAG_FNTGRN + "${comment}" + TAG_EFONT);

            return source;
        }

		// *********************************************************************
		//  ToDelimitedString
		//
		/// <summary>
		/// Private helper function to convert a collection to delimited string array
		/// </summary>
		/// 
		// ********************************************************************/
		public static string ToDelimitedString(ICollection collection, string delimiter) {

			StringBuilder delimitedString = new StringBuilder();

			// Hashtable is perfomed on Keys
			//
			if (collection is Hashtable) {

				foreach (object o in ((Hashtable) collection).Keys) {
					delimitedString.Append( o.ToString() + delimiter);
				}
			}

			// ArrayList is performed on contained item
			//
			if (collection is ArrayList) {
				foreach (object o in (ArrayList) collection) {
					delimitedString.Append( o.ToString() + delimiter);
				}
			}

			// String Array is performed on value
			//
			if (collection is String[]) {
				foreach (string s in (String[]) collection) {
					delimitedString.Append( s + delimiter);
				}
			}

			return delimitedString.ToString().TrimEnd(Convert.ToChar(delimiter));
		}
		#endregion

    }
}
#region Old Code
/*


            // replace all carraige returns with <br> tags
            strBody = strBody.Replace("\n", "\n" + Globals.HtmlNewLine + "\n");

            // Ensure we have safe anchors
            Match m = Regex.Match(strBody, "href=\"((.|\\n)*?)\"");
            foreach(Capture capture in m.Captures) {

                if ( (!capture.ToString().StartsWith("href=\"http://")) )
                  strBody = strBody.Replace(capture.ToString(), "");

//      TODO          if ( (!capture.ToString().StartsWith("href=\"http://")) && (!capture.ToString().StartsWith("href=\"/")) )
//                    strBody = strBody.Replace(capture.ToString(), "");
            }

            // Create mailto links with any words containing @
//            strBody = Regex.Replace(strBody, "\\b((?<!<a\\s+href\\s*=\\s*\\S+)\\w+@([\\w\\-\\.,@?^=%&:/~\\+#]*[\\w\\-\\@?^=%&/~\\+#])?)", "$1", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            // replace all whitespace with &nbsp;
            //strBody = strBody.Replace(" ", "&nbsp;");

            return strBody;
            */
#endregion
