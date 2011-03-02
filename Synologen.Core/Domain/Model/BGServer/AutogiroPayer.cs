namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
	public class AutogiroPayer : Entity
	{
		public virtual string Name { get; set; }
		public virtual AutogiroServiceType ServiceType { get; set;}
	}
}