using System;
using NHibernate;
using Spinit.Wpc.Synologen.Data.Commands;
using Spinit.Wpc.Synologen.Data.Queries;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription
{
	public abstract class BasePresenter<TView> : Presenter<TView> where TView : class, IView
	{
		protected ISession Session { get; private set; }
		private ITempDataProvider _tempDataProvider;
		private const string ActionMessageKey = "ActionMessage";
		private const string TempDataProviderKey = "__TempDataProvider";

		protected BasePresenter(TView view, ISession session) : base(view)
		{
			Session = session;
			View.Load += Load;
		}

		private void Load(object sender, EventArgs e)
		{
			_tempDataProvider = (ITempDataProvider) HttpContext.Items[TempDataProviderKey];
		}

		protected TResult Query<TResult>(Query<TResult> query)
		{
			query.Session = Session;
			return query.Execute();
		}

		protected void Execute(Command command)
		{
			command.Session = Session;
			command.Execute();
		}

		protected TResult Execute<TResult>(Command<TResult> command)
		{
			command.Session = Session;
			command.Execute();
			return command.Result;
		}

		public object GetActionMessage()
		{
		    if (_tempDataProvider == null) throw new ApplicationException("No temp data provider has been set!");
		    return _tempDataProvider.Get(ActionMessageKey);
		}

		public void SetActionMessage(object value)
		{
			if (_tempDataProvider == null) throw new ApplicationException("No temp data provider has been set!");
			_tempDataProvider.Set(ActionMessageKey, value);
		}

		public override void ReleaseView()
		{
			View.Load -= Load;
			HttpContext.Items[TempDataProviderKey] = _tempDataProvider;
		}
	}
}