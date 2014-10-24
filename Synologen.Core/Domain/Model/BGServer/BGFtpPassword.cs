using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
	public class BGFtpPassword : Entity
	{
		public BGFtpPassword()
		{
			Created = DateTime.Now;
		}
		public virtual string Password { get; set; }
		public virtual DateTime Created { get; private set; }
	}
}