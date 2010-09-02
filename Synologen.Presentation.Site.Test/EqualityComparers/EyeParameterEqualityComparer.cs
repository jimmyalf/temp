using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.EqualityComparers
{
	public class EyeParameterEqualityComparer : IEqualityComparer<EyeParameter>
	{
        public bool Equals(EyeParameter x, EyeParameter y)
        {
            return (x.Left == y.Left) && (x.Right == y.Right);
        }

        public int GetHashCode(EyeParameter obj)
        {
            throw new NotImplementedException();
        }
	}
}