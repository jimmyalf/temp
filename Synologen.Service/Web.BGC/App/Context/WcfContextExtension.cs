using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;

namespace Synologen.Service.Web.BGC.App.Context
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