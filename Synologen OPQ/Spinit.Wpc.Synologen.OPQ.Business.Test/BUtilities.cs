using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Opq.Business;

namespace Spinit.Wpc.Synologen.OPQ.Business.Test
{
	[TestFixture, Description ("The unit tests for the utilities business layer.")]
	public class BUtilities
	{
		[SetUp, Description ("Initialize.")]
		public void NodeManagerInit ()
		{
		}

		[TearDown, Description ("Close.")]
		public void NodeManagerCleanUp ()
		{
		}

		[Test, Explicit, Description ("Creates a document."), Category ("Document")]
		public void CreateDocument ()
		{
			//Type TestTextsType = typeof (TestTexts);

			//PropertyInfo[] propertyInfos = TestTextsType.GetProperties ();

			//PropertyInfo propInfo = TestTextsType.GetProperty ("TestString_2", typeof (string));

			//ParameterExpression t = Expression.Parameter (typeof (T), "t");
			//MemberExpression prop = Expression.Property (t, property);

			//string ret = (string) propInfo.GetValue (null, null);
					
			//string tmp = ResourceExtension.CreateWith<TestTexts, string> ("TestString_2").Invoke (null);

			//Assert.AreEqual (TestTexts.TestString_1, tmp, "Wrong string fetched.");
		}
	}
}
