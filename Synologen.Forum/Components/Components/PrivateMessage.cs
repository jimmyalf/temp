using System;
using System.Collections;
using Spinit.Wpc.Forum;
using Spinit.Wpc.Forum.Enumerations;

namespace Spinit.Wpc.Forum.Components {

    public class PrivateMessage : Thread {

        ArrayList recipients = new ArrayList();
        public ArrayList Recipients {
            get { return recipients; }
            set { recipients = value; }
        }

    }
}
