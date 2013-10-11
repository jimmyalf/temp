using System;
using System.Xml.Serialization;

namespace Spinit.Wpc.Forum.Components {
    public class ModerationQueueStatus {

        [XmlElement("count")]
        public int Count;

        [XmlElement("ageInMinutes")]
        public int AgeInMinutes;
    }
}
