using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Spinit.Wpc.Synologen.Data.Extensions
{
	public class SynologenSqlParameterCollection : List<IDataParameter>
	{
		private int _currentSetValue;
		public SynologenSqlParameterCollection()
		{
			_currentSetValue = 0;
		}
		
		public SynologenSqlParameterCollection SetValue(object value)
		{
			this[_currentSetValue++].Value = value;
			return this;
		}
		
	}
}