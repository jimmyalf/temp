using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.UI.Mvc.Site.Services
{
    public class GeocodingService : IGeocodingService
    {
        public Coordinates GetCoordinates(string address)
        {
            //return new Coordinates { Longitude = 11.97456m, Latitude = 57.70887m };
            throw new NotImplementedException();
        }
    }
}