using System;
using System.Linq;

namespace Spinit.Wpc.Synologen.OPQ.Site.Code.Config
{
    public class NodeComponents : OPQConfigurationElementCollection<NodeComponent>
    {
        public NodeComponents() : base(x => x.Name)
        {
            AddElementName = "node";
        }

        public bool Contains(string name)
        {
            return this.Any(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}