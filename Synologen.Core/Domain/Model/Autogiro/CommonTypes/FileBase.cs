using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
	public abstract class FileBase<TEntity> : IAutogiroFile<TEntity>
	{
		public DateTime WriteDate { get; set; }
		public PaymentReciever Reciever { get; set; }
		public IEnumerable<TEntity> Posts { get; set; }
	}
}