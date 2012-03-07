using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Spinit.Wpc.Content.Data;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services
{
	public class RoutingService : RoutingServiceBase, IRoutingService
	{
		private readonly Tree _treeRepository;

		public RoutingService()
		{
			var connectionString = Utility.Business.Globals.ConnectionString("WpcServer");
			_treeRepository = new Tree(connectionString);	
		}

		public override string GetPageUrl(int pageId)
		{
			return _treeRepository.GetFileUrlDownString(pageId);
		}
	}

	public abstract class RoutingServiceBase
	{
		protected virtual string BuildQueryString(object parameters)
		{
			var enumeratedList = GetAnonymousParameters(parameters).ToList();
			if(!enumeratedList.Any()) return string.Empty;
			return "?" + enumeratedList.Select(x => x.Item1 + "=" + x.Item2.ToString()).Aggregate((item, next) => item + next);
		}

		protected virtual IEnumerable<Tuple<string,object>> GetAnonymousParameters(object parameters)
		{
			if (parameters == null) yield break;
			foreach(var prop in parameters.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				yield return new Tuple<string, object>(prop.Name, prop.GetValue(parameters, null));
			}
		}

		public string GetPageUrl(int pageId, object requestParameters)
		{
			return GetPageUrl(pageId) + BuildQueryString(requestParameters);
		}

		public abstract string GetPageUrl(int pageId);
	}
}