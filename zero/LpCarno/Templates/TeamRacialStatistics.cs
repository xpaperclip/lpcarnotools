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
    
    
    #line 1 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "10.0.0.0")]
    public partial class TeamRacialStatistics : TeamRacialStatisticsBase
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
            this.Write("{|class=\"sortable wikitable\" style=\"text-align:center;\"\r\n!colspan=2 align=left st" +
                    "yle=\"border-left:  1px solid #777; border-top: 1px solid #777; border-right: 1px" +
                    " solid #777;\" |\r\n");
            
            #line 7 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 if (this.IncludeRaces) { 
            
            #line default
            #line hidden
            this.Write(@"!colspan=4 align=left style=""border-right: 1px solid #777; border-top: 1px solid #777;""                               |{{T}}
!colspan=4 align=left style=""border-right: 1px solid #777; border-top: 1px solid #777;""                               |{{Z}}
!colspan=4 align=left style=""border-right: 1px solid #777; border-top: 1px solid #777;""                               |{{P}}
");
            
            #line 11 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 } 
            
            #line default
            #line hidden
            
            #line 12 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 if (this.IncludeVs) { 
            
            #line default
            #line hidden
            this.Write(@"!colspan=4 align=left style=""border-right: 1px solid #777; border-top: 1px solid #777;""                               |vs. {{T}}
!colspan=4 align=left style=""border-right: 1px solid #777; border-top: 1px solid #777;""                               |vs. {{Z}}
!colspan=4 align=left style=""border-right: 1px solid #777; border-top: 1px solid #777;""                               |vs. {{P}}
");
            
            #line 16 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 } 
            
            #line default
            #line hidden
            this.Write("|-\r\n\r\n!style=\"border-left: 1px solid #777;\" width=2px   |\r\n!style=\"border-right: " +
                    "1px solid #777;\"            |Team\r\n");
            
            #line 21 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 if (this.IncludeRaces) { 
            
            #line default
            #line hidden
            this.Write(@"<!-- Terran -->                      ! width=12px |Σ
                                     ! width=12px |W
                                     ! width=12px |L
!style=""border-right: 1px solid #777;"" width=24px |%
<!-- Zerg -->                        ! width=12px |Σ
                                     ! width=12px |W
                                     ! width=12px |L
!style=""border-right: 1px solid #777;"" width=24px |%
<!-- Protoss -->                     ! width=12px |Σ
                                     ! width=12px |W
                                     ! width=12px |L
!style=""border-right: 1px solid #777;"" width=24px |%
");
            
            #line 34 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 } 
            
            #line default
            #line hidden
            
            #line 35 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 if (this.IncludeVs) { 
            
            #line default
            #line hidden
            this.Write(@"<!-- Terran -->                      ! width=12px |Σ
                                     ! width=12px |W
                                     ! width=12px |L
!style=""border-right: 1px solid #777;"" width=24px |%
<!-- Zerg -->                        ! width=12px |Σ
                                     ! width=12px |W
                                     ! width=12px |L
!style=""border-right: 1px solid #777;"" width=24px |%
<!-- Protoss -->                     ! width=12px |Σ
                                     ! width=12px |W
                                     ! width=12px |L
!style=""border-right: 1px solid #777;"" width=24px |%
");
            
            #line 48 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 } 
            
            #line default
            #line hidden
            
            #line 49 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 foreach (Bag bag in this.Rows) { 
            
            #line default
            #line hidden
            this.Write("|-\r\n|style=\"border-left: 1px solid #777;\"             | ");
            
            #line 51 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["index"]));
            
            #line default
            #line hidden
            this.Write("\r\n|align=left style=\"border-right: 1px solid #777;\" | {{team/");
            
            #line 52 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["team"]));
            
            #line default
            #line hidden
            this.Write("}}\r\n");
            
            #line 53 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 if (this.IncludeRaces) { 
            
            #line default
            #line hidden
            this.Write("<!-- Terran -->                                   | ");
            
            #line 54 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["Ttotal"]));
            
            #line default
            #line hidden
            this.Write("\r\n                                                  | ");
            
            #line 55 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["Twin"]));
            
            #line default
            #line hidden
            this.Write("\r\n                                                  | ");
            
            #line 56 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["Tloss"]));
            
            #line default
            #line hidden
            this.Write("\r\n|style=\"border-right: 1px solid #777;\"            | ");
            
            #line 57 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["Tpc"]));
            
            #line default
            #line hidden
            this.Write("\r\n<!-- Zerg -->                                     | ");
            
            #line 58 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["Ztotal"]));
            
            #line default
            #line hidden
            this.Write("\r\n                                                  | ");
            
            #line 59 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["Zwin"]));
            
            #line default
            #line hidden
            this.Write("\r\n                                                  | ");
            
            #line 60 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["Zloss"]));
            
            #line default
            #line hidden
            this.Write("\r\n|style=\"border-right: 1px solid #777;\"            | ");
            
            #line 61 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["Zpc"]));
            
            #line default
            #line hidden
            this.Write("\r\n<!-- Protoss -->                                  | ");
            
            #line 62 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["Ptotal"]));
            
            #line default
            #line hidden
            this.Write("\r\n                                                  | ");
            
            #line 63 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["Pwin"]));
            
            #line default
            #line hidden
            this.Write("\r\n                                                  | ");
            
            #line 64 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["Ploss"]));
            
            #line default
            #line hidden
            this.Write("\r\n|style=\"border-right: 1px solid #777;\"            | ");
            
            #line 65 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["Ppc"]));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 66 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 } 
            
            #line default
            #line hidden
            
            #line 67 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 if (this.IncludeVs) { 
            
            #line default
            #line hidden
            this.Write("<!-- Terran -->                                   | ");
            
            #line 68 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["vTtotal"]));
            
            #line default
            #line hidden
            this.Write("\r\n                                                  | ");
            
            #line 69 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["vTwin"]));
            
            #line default
            #line hidden
            this.Write("\r\n                                                  | ");
            
            #line 70 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["vTloss"]));
            
            #line default
            #line hidden
            this.Write("\r\n|style=\"border-right: 1px solid #777;\"            | ");
            
            #line 71 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["vTpc"]));
            
            #line default
            #line hidden
            this.Write("\r\n<!-- Zerg -->                                     | ");
            
            #line 72 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["vZtotal"]));
            
            #line default
            #line hidden
            this.Write("\r\n                                                  | ");
            
            #line 73 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["vZwin"]));
            
            #line default
            #line hidden
            this.Write("\r\n                                                  | ");
            
            #line 74 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["vZloss"]));
            
            #line default
            #line hidden
            this.Write("\r\n|style=\"border-right: 1px solid #777;\"            | ");
            
            #line 75 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["vZpc"]));
            
            #line default
            #line hidden
            this.Write("\r\n<!-- Protoss -->                                  | ");
            
            #line 76 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["vPtotal"]));
            
            #line default
            #line hidden
            this.Write("\r\n                                                  | ");
            
            #line 77 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["vPwin"]));
            
            #line default
            #line hidden
            this.Write("\r\n                                                  | ");
            
            #line 78 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["vPloss"]));
            
            #line default
            #line hidden
            this.Write("\r\n|style=\"border-right: 1px solid #777;\"            | ");
            
            #line 79 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(bag["vPpc"]));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 80 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 } 
            
            #line default
            #line hidden
            
            #line 81 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 } 
            
            #line default
            #line hidden
            this.Write("|-\r\n!colspan=2 style=\"border-bottom: 1px solid #777; border-right: 1px solid #777" +
                    "; border-left: 1px solid #777;\" | Overall\r\n");
            
            #line 84 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 if (this.IncludeRaces) { 
            
            #line default
            #line hidden
            this.Write("!style=\"border-bottom: 1px solid #777; \"                <!-- Terran --> | ");
            
            #line 85 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["Ttotal"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"\t\t\t\t\t\t\t\t| ");
            
            #line 86 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["Twin"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"\t\t\t\t\t\t\t\t| ");
            
            #line 87 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["Tloss"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; border-right: 1px solid #777;\"   | ");
            
            #line 88 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["Tpc"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"                  <!-- Zerg --> | ");
            
            #line 89 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["Ztotal"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"\t\t\t\t\t\t\t\t| ");
            
            #line 90 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["Zwin"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"\t\t\t\t\t\t\t\t| ");
            
            #line 91 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["Zloss"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; border-right: 1px solid #777;\"   | ");
            
            #line 92 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["Zpc"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"               <!-- Protoss --> | ");
            
            #line 93 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["Ptotal"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"\t\t\t\t\t\t\t\t| ");
            
            #line 94 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["Pwin"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"\t\t\t\t\t\t\t\t| ");
            
            #line 95 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["Ploss"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; border-right: 1px solid #777;\"   | ");
            
            #line 96 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["Ppc"]));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 97 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 } 
            
            #line default
            #line hidden
            
            #line 98 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 if (this.IncludeVs) { 
            
            #line default
            #line hidden
            this.Write("!style=\"border-bottom: 1px solid #777; \"                <!-- Terran --> | ");
            
            #line 99 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["vTtotal"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"\t\t\t\t\t\t\t\t| ");
            
            #line 100 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["vTwin"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"\t\t\t\t\t\t\t\t| ");
            
            #line 101 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["vTloss"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; border-right: 1px solid #777;\"   | ");
            
            #line 102 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["vTpc"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"                  <!-- Zerg --> | ");
            
            #line 103 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["vZtotal"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"\t\t\t\t\t\t\t\t| ");
            
            #line 104 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["vZwin"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"\t\t\t\t\t\t\t\t| ");
            
            #line 105 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["vZloss"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; border-right: 1px solid #777;\"   | ");
            
            #line 106 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["vZpc"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"               <!-- Protoss --> | ");
            
            #line 107 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["vPtotal"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"\t\t\t\t\t\t\t\t| ");
            
            #line 108 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["vPwin"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; \"\t\t\t\t\t\t\t\t| ");
            
            #line 109 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["vPloss"]));
            
            #line default
            #line hidden
            this.Write("\r\n!style=\"border-bottom: 1px solid #777; border-right: 1px solid #777;\"   | ");
            
            #line 110 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Params["vPpc"]));
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 111 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"<!-- |-
!colspan=2 align=left style=""border-left: 1px solid #777; border-bottom: 1px solid #777; border-right: 1px solid #777;""  |
!colspan=4 align=left style=""border-bottom: 1px solid #777; border-right: 1px solid #777;""                               |
!colspan=4 align=left style=""border-bottom: 1px solid #777; border-right: 1px solid #777;""                               |
!colspan=4 align=left style=""border-bottom: 1px solid #777; border-right: 1px solid #777;""                               | -->
|}
");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 118 "F:\git\xpaperclip\lpcarnotools\zero\LpCarno\Templates\TeamRacialStatistics.tt"

public bool IncludeRaces { get; set; }
public bool IncludeVs { get; set; }
public Bag Params { get; set; }
public IEnumerable<Bag> Rows { get; set; }

        
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
    public class TeamRacialStatisticsBase
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
