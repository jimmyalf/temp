using System;
using System.Web;

namespace Spinit.Wpc.Forum.Components {

    // *********************************************************************
    //  ModerationAuditSummary
    //
    /// <summary>
    /// This class contains the properties for a User.
    /// </summary>
    /// 
    // ********************************************************************/
    public class ModerationAuditSummary  {

        string action;
        int actionSummary;

        public string Action {
            get { return action; }
            set { action = value; }
        }

        public int Total {
            get { return actionSummary; }
            set { actionSummary = value; }
        }
    }
}
