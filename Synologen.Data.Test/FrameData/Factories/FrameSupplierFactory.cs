using System;
using System.Collections.Generic;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Data.Test.FrameData.Factories
{
    public class FrameSupplierFactory
    {
        public static IEnumerable<FrameSupplier> GetFrameSuppliers()
        {
            return new[]
			{
				GetSupplier("Hoya", "kundservice@hoya.se"),
				GetSupplier("Carl Zeiss", "kundservice@carlzeiss.se")			
			};
        }

        public static FrameSupplier ScrabmleFrameSupplier(FrameSupplier frameSupplier)
        {
            frameSupplier.Name = frameSupplier.Name.Reverse();
            frameSupplier.Name = frameSupplier.Name.Reverse();

            return frameSupplier;
        }

        private static FrameSupplier GetSupplier(string name, string email)
        {
            return new FrameSupplier {Name = name, Email = email};
        }
    }
}