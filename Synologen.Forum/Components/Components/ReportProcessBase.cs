using System;
using System.Data;

namespace Spinit.Wpc.Forum.Components
{

	public interface IReportProcess {

		bool Initialize();
		bool Execute( ref System.Data.DataSet results );
	}

	/// <summary>
	/// Summary description for ReportProcessBase.
	/// </summary>
	public class ReportProcessBase : IReportProcess
	{
		public ReportProcessBase()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IReportProcess Members

		public bool Initialize() {
			// TODO:  Add ReportProcessBase.Initialize implementation
			return false;
		}

		public bool Execute(ref DataSet results) {
			// TODO:  Add ReportProcessBase.Execute implementation
			return false;
		}

		#endregion
	}
}
