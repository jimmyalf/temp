namespace Spinit.Wpc.Synologen.Presentation.Intranet.Reports
{
	/// <summary>
	/// Summary description for OrderReport.
	/// </summary>
	partial class OrderReport
	{
		private DataDynamics.ActiveReports.Detail detail;

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
			this.detail = new DataDynamics.ActiveReports.Detail();
			this.reportPageOne = new DataDynamics.ActiveReports.SubReport();
			this.reportPageTwo = new DataDynamics.ActiveReports.SubReport();
			this.pageBreak1 = new DataDynamics.ActiveReports.PageBreak();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// detail
			// 
			this.detail.ColumnSpacing = 0F;
			this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.reportPageOne,
            this.reportPageTwo,
            this.pageBreak1});
			this.detail.Height = 0.4062502F;
			this.detail.Name = "detail";
			// 
			// reportPageOne
			// 
			this.reportPageOne.CloseBorder = false;
			this.reportPageOne.Height = 0.198F;
			this.reportPageOne.Left = 0F;
			this.reportPageOne.Name = "reportPageOne";
			this.reportPageOne.Report = null;
			this.reportPageOne.ReportName = "reportPageOne";
			this.reportPageOne.Top = 0F;
			this.reportPageOne.Width = 6.562F;
			// 
			// reportPageTwo
			// 
			this.reportPageTwo.CloseBorder = false;
			this.reportPageTwo.Height = 0.198F;
			this.reportPageTwo.Left = 0F;
			this.reportPageTwo.Name = "reportPageTwo";
			this.reportPageTwo.Report = null;
			this.reportPageTwo.ReportName = "reportPageTwo";
			this.reportPageTwo.Top = 0.198F;
			this.reportPageTwo.Width = 6.562F;
			// 
			// pageBreak1
			// 
			this.pageBreak1.Height = 0.2F;
			this.pageBreak1.Left = 0F;
			this.pageBreak1.Name = "pageBreak1";
			this.pageBreak1.Size = new System.Drawing.SizeF(6.5F, 0.2F);
			this.pageBreak1.Top = 0.198F;
			this.pageBreak1.Width = 6.5F;
			// 
			// OrderReport
			// 
			this.MasterReport = false;
			this.PageSettings.PaperHeight = 11F;
			this.PageSettings.PaperWidth = 8.5F;
			this.PrintWidth = 6.562F;
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

		private DataDynamics.ActiveReports.SubReport reportPageOne;
		private DataDynamics.ActiveReports.SubReport reportPageTwo;
		private DataDynamics.ActiveReports.PageBreak pageBreak1;
	}
}
