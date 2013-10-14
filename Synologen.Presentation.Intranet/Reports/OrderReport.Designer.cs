namespace Spinit.Wpc.Synologen.Presentation.Intranet.Reports
{
	/// <summary>
	/// Summary description for OrderReport.
	/// </summary>
	partial class OrderReport
    {


        /// <summary>
		/// Clean up any resources being used.
		/// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

		#region ActiveReport Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(OrderReport));
            this.detail = new GrapeCity.ActiveReports.SectionReportModel.Detail();
            this.reportPageOne = new GrapeCity.ActiveReports.SectionReportModel.SubReport();
            this.reportPageTwo = new GrapeCity.ActiveReports.SectionReportModel.SubReport();
            this.pageBreak1 = new GrapeCity.ActiveReports.SectionReportModel.PageBreak();
            this.reportPageThree = new GrapeCity.ActiveReports.SectionReportModel.SubReport();
            this.pageBreak2 = new GrapeCity.ActiveReports.SectionReportModel.PageBreak();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.reportPageOne,
            this.reportPageTwo,
            this.pageBreak1,
            this.reportPageThree,
            this.pageBreak2});
            this.detail.Height = 1.095226F;
            this.detail.Name = "detail";
            // 
            // reportPageOne
            // 
            this.reportPageOne.CloseBorder = false;
            this.reportPageOne.Height = 0.2952756F;
            this.reportPageOne.Left = 0F;
            this.reportPageOne.Name = "reportPageOne";
            this.reportPageOne.Report = null;
            this.reportPageOne.ReportName = "reportPageOne";
            this.reportPageOne.Top = 0F;
            this.reportPageOne.Width = 6.299212F;
            // 
            // reportPageTwo
            // 
            this.reportPageTwo.CloseBorder = false;
            this.reportPageTwo.Height = 0.3937008F;
            this.reportPageTwo.Left = 0F;
            this.reportPageTwo.Name = "reportPageTwo";
            this.reportPageTwo.Report = null;
            this.reportPageTwo.ReportName = "reportPageTwo";
            this.reportPageTwo.Top = 0.2952756F;
            this.reportPageTwo.Width = 6.299212F;
            // 
            // pageBreak1
            // 
            this.pageBreak1.Height = 0.01F;
            this.pageBreak1.Left = 0F;
            this.pageBreak1.Name = "pageBreak1";
            this.pageBreak1.Size = new System.Drawing.SizeF(6.299212F, 0.01F);
            this.pageBreak1.Top = 0.2952756F;
            this.pageBreak1.Width = 6.299212F;
            // 
            // reportPageThree
            // 
            this.reportPageThree.CloseBorder = false;
            this.reportPageThree.Height = 0.3937008F;
            this.reportPageThree.Left = 0F;
            this.reportPageThree.Name = "reportPageThree";
            this.reportPageThree.Report = null;
            this.reportPageThree.ReportName = "reportPageThree";
            this.reportPageThree.Top = 0.6889764F;
            this.reportPageThree.Width = 6.299212F;
            // 
            // pageBreak2
            // 
            this.pageBreak2.Height = 0.01F;
            this.pageBreak2.Left = 0F;
            this.pageBreak2.Name = "pageBreak2";
            this.pageBreak2.Size = new System.Drawing.SizeF(6.299212F, 0.01F);
            this.pageBreak2.Top = 0.6889764F;
            this.pageBreak2.Width = 6.299212F;
            // 
            // OrderReport
            // 
            this.MasterReport = false;
            this.PageSettings.DefaultPaperSize = false;
            this.PageSettings.Margins.Bottom = 0.984252F;
            this.PageSettings.Margins.Left = 0.984252F;
            this.PageSettings.Margins.Right = 0.984252F;
            this.PageSettings.Margins.Top = 0.984252F;
            this.PageSettings.PaperHeight = 11.69291F;
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.PageSettings.PaperWidth = 8.267716F;
            this.PrintWidth = 6.299212F;
            this.Sections.Add(this.detail);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
            "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
		#endregion






        private GrapeCity.ActiveReports.SectionReportModel.Detail detail;
        private GrapeCity.ActiveReports.SectionReportModel.SubReport reportPageOne;
        private GrapeCity.ActiveReports.SectionReportModel.SubReport reportPageTwo;
        private GrapeCity.ActiveReports.SectionReportModel.PageBreak pageBreak1;
        private GrapeCity.ActiveReports.SectionReportModel.SubReport reportPageThree;
        private GrapeCity.ActiveReports.SectionReportModel.PageBreak pageBreak2;
	}
}
