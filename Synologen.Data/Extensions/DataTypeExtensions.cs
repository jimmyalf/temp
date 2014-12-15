using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spinit.Wpc.Synologen.Data.Extensions
{
    public static class DataTypeExtensions
    {
        public static bool HasValueAndPositive(this int? candidate)
        {
            return candidate.HasValue && candidate.Value > 0;
        }
        
    }
}
