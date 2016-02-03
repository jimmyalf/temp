using System;

namespace Spinit.Wpc.Forum.Components
{
	/// <summary>
	/// Summary description for Smily.
	/// </summary>
	public class Smily
	{

		#region Fields
		#endregion

		#region Properties
		#endregion

		#region Events
		#endregion

		#region Public Methods
		/// <summary>
		/// 
		/// </summary>
		public Smily() {

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="smilyId"></param>
		/// <param name="code"></param>
		/// <param name="imageUrl"></param>
		/// <param name="description"></param>
		public Smily( string code, string imageUrl, string description ) {
			_code			= code;
			_imageUrl		= imageUrl;
			_description	= description;
		}
		#endregion

		#region Protected Methods
		#endregion

		#region Protected Data
		#endregion

		#region Private Methods
		#endregion

		#region Private Data
		private string	_code;
		private string	_imageUrl;
		private string	_description;
		#endregion
		
	}
}
