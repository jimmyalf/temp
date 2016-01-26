using System;
using System.Collections;

namespace Spinit.Wpc.Forum.Components {

	public class ServiceSchedule : IComparer {

		#region Fields
		#endregion

		#region Properties
		public int		ServiceId {
			get{return _serviceId;}
			set{_serviceId = value ; }
		}
		public string	ScheduleName {
			get{return _scheduleName;}
			set{_scheduleName = value ; }
		}
		public string	MachineName {
			get{return _machineName;}
			set{_machineName = value;}
		}
		public DateTime	StartDate {
			get{return _startDate;}
			set{_startDate = value;}
		}
		public DateTime	EndDate {
			get{return _endDate;}
			set{_endDate = value;}
		}
		public string	ServiceParameters {
			get{return _serviceParameters;}
			set{_serviceParameters = value;}
		}
		public int		ScheduleTypeCode {
			get{return _scheduleTypeCode;}
			set{_scheduleTypeCode = value;}
		}
		public int		RunTimeHour {
			get{return _runTimeHour;}
			set{_runTimeHour = value;}
		}
		public int		RunTimeMinute {
			get{return _runTimeMinute;}
			set{_runTimeMinute = value;}
		}
		public int		DelayHour {
			get{return _delayHour;}
			set{_delayHour = value;}
		}
		public int		DelayMinute {
			get{return _delayMinute;}
			set{_delayMinute = value;}
		}
		public int		RunDaily {
			get{return _runDaily;}
			set{_runDaily = value;}
		}
		public int		RunWeekly {
			get{return _runWeekly;}
			set{_runWeekly = value;}
		}
		public bool		RunMonthly {
			get{return _runMonthly;}
			set{_runMonthly = value;}
		}
		public bool		RunYearly {
			get{return _runYearly;}
			set{_runYearly = value;}
		}
		public bool		RunOnce {
			get{return _runOnce;}
			set{_runOnce = value;}
		}
		public DateTime	LastRunTime {
			get{return _lastRunTime;}
			set{_lastRunTime = value;}
		}
		public DateTime	NextRunTime {
			get{return _nextRunTime;}
			set{_nextRunTime = value;}
		}
		#endregion

		#region Events
		#endregion

		#region Public Methods

		public ServiceSchedule() {
		}

		#endregion

		#region Protected Methods
		#endregion

		#region Protected Data
		#endregion

		#region Private Methods
		#endregion

		#region Private Data
		protected int		_serviceId;
		protected string	_scheduleName;
		protected string	_machineName;
		protected DateTime	_startDate;
		protected DateTime	_endDate;
		protected string	_serviceParameters;
		protected int		_scheduleTypeCode;
		protected int		_runTimeHour;
		protected int		_runTimeMinute;
		protected int		_delayHour;
		protected int		_delayMinute;
		protected int		_runDaily;
		protected int		_runWeekly;
		protected bool		_runMonthly;
		protected bool		_runYearly;
		protected bool		_runOnce;
		protected DateTime	_lastRunTime;
		protected DateTime	_nextRunTime;
		#endregion

		#region IComparer Members

		public int Compare(object x, object y) {
			// TODO:  Add ServiceSchedule.Compare implementation
			return 0;
		}

		#endregion

	}


}