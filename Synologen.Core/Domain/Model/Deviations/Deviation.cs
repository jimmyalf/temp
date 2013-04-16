using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Deviations
{
	public class Deviation : Entity
	{
		public Deviation()
		{
			Created = DateTime.Now;
		}

		public virtual DeviationCategory Category { get; set; }
		public virtual DateTime Created { get; set; }
	}
}