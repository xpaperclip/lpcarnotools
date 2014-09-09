﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 10.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace LpCarno.Templates
{
    using System.Collections.Generic;
    using LxTools;
    using System;
    
    
    #line 1 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "10.0.0.0")]
    public partial class PlayerStatistics : PlayerStatisticsBase
    {
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
        public virtual string TransformText()
        {
            this.GenerationEnvironment = null;
            
            #line 5 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 switch (this.HeaderType) { 
            
            #line default
            #line hidden
            
            #line 6 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 case "all": 
            
            #line default
            #line hidden
            this.Write("{| class=\"sortable wikitable\" style=\"text-align:center; width:");
            
            #line 6 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 if (IncludeAllKills) { 
            
            #line default
            #line hidden
            this.Write("1050px");
            
            #line 6 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("1000px");
            
            #line 6 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\"\r\n");
            
            #line 7 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 break; 
            
            #line default
            #line hidden
            
            #line 8 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 case "top10": 
            
            #line default
            #line hidden
            this.Write("{| class=\"sortable wikitable\" style=\"text-align:center; width:");
            
            #line 8 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 if (IncludeAllKills) { 
            
            #line default
            #line hidden
            this.Write("1050px");
            
            #line 8 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("1000px");
            
            #line 8 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\"\r\n! colspan=");
            
            #line 9 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(12 + (IncludeAllKills ? 1 : 0) + (IncludeTeamColumn ? 1 : 0)));
            
            #line default
            #line hidden
            this.Write(" | Top 10\r\n|-\r\n");
            
            #line 11 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 break; 
            
            #line default
            #line hidden
            
            #line 12 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 case "top10-complete": 
            
            #line default
            #line hidden
            this.Write("{| class=\"sortable wikitable collapsible collapsed\" style=\"margin-top:-14px; text" +
                    "-align:center; width:");
            
            #line 12 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 if (IncludeAllKills) { 
            
            #line default
            #line hidden
            this.Write("1050px");
            
            #line 12 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("1000px");
            
            #line 12 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\"\r\n! colspan=");
            
            #line 13 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(12 + (IncludeAllKills ? 1 : 0) + (IncludeTeamColumn ? 1 : 0)));
            
            #line default
            #line hidden
            this.Write(" | Complete Table\r\n|-\r\n");
            
            #line 15 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 break; 
            
            #line default
            #line hidden
            
            #line 16 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 } 
            
            #line default
            #line hidden
            this.Write("! colspan=");
            
            #line 17 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(4 + (IncludeAllKills ? 1 : 0) + (IncludeTeamColumn ? 1 : 0)));
            
            #line default
            #line hidden
            this.Write(" | Player\r\n! colspan=2 width=100px | Games\r\n! colspan=2 width=100px | vs. {{T}}\r\n" +
                    "! colspan=2 width=100px | vs. {{Z}}\r\n! colspan=2 width=100px | vs. {{P}}\r\n|-\r\n! " +
                    "width=2px |\r\n! width=2px |\r\n! width=2px |\r\n! width=125px |ID\r\n");
            
            #line 27 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 if (IncludeTeamColumn) { 
            
            #line default
            #line hidden
            this.Write("! width=175px |Team\r\n");
            
            #line 28 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 } 
            
            #line default
            #line hidden
            
            #line 29 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 if (IncludeAllKills) { 
            
            #line default
            #line hidden
            this.Write("! width=60px |All-Kills\r\n");
            
            #line 30 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 } 
            
            #line default
            #line hidden
            this.Write("! Record\r\n! Win%\r\n! Record\r\n! Win%\r\n! Record\r\n! Win%\r\n! Record\r\n! Win%\r\n|-\r\n");
            
            #line 40 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 foreach (Indexing<Bag> idxobj in this.Rows) {
	   int index = idxobj.Index;
	   Bag obj = idxobj.Object; 
            
            #line default
            #line hidden
            this.Write("| ");
            
            #line 43 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(index));
            
            #line default
            #line hidden
            this.Write("\r\n| {{FlagRace|");
            
            #line 44 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(obj["flag"]));
            
            #line default
            #line hidden
            this.Write("|");
            
            #line 44 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(obj["race"]));
            
            #line default
            #line hidden
            this.Write("}}\r\n|align=left | [[");
            
            #line 45 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(obj["player"]));
            
            #line default
            #line hidden
            this.Write("]]\r\n");
            
            #line 46 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 if (IncludeTeamColumn) { 
            
            #line default
            #line hidden
            this.Write("|align=left | {{team/");
            
            #line 46 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(obj["team"]));
            
            #line default
            #line hidden
            this.Write("}}\r\n");
            
            #line 47 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 } 
            
            #line default
            #line hidden
            
            #line 48 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 if (IncludeAllKills) { 
            
            #line default
            #line hidden
            this.Write("| ");
            
            #line 48 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(obj["allkills"]));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 49 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 } 
            
            #line default
            #line hidden
            this.Write("| ");
            
            #line 50 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(obj["wl"]));
            
            #line default
            #line hidden
            this.Write("\r\n| ");
            
            #line 51 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(obj["vT"]));
            
            #line default
            #line hidden
            this.Write("\r\n| ");
            
            #line 52 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(obj["vZ"]));
            
            #line default
            #line hidden
            this.Write("\r\n| ");
            
            #line 53 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(obj["vP"]));
            
            #line default
            #line hidden
            this.Write("\r\n|-\r\n");
            
            #line 55 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"
 } 
            
            #line default
            #line hidden
            this.Write("|}\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 57 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\PlayerStatistics.tt"

public bool IncludeAllKills { get; set; }
public bool IncludeTeamColumn { get; set; }
public string HeaderType { get; set; }
public IEnumerable<Indexing<Bag>> Rows { get; set; }

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "10.0.0.0")]
    public class PlayerStatisticsBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
    }
    #endregion
}
