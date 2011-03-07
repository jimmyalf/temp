using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;

namespace Synologen.LensSubscription.BGWebService.App.Context
{
    public class WcfContextExtension : IExtension<OperationContext>
    {
        public WcfContextExtension()
        {
            Items = new Dictionary<string, object>();
        }

        public IDictionary Items { get; private set; }
        public void Attach(OperationContext owner) { }
        public void Detach(OperationContext owner) { }
    }
}