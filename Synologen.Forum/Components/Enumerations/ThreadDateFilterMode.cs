using System;

namespace Spinit.Wpc.Forum.Enumerations {

    public enum ThreadDateFilterMode {
        LastVisit,
        OneDay,
        TwoDays,
        ThreeDays,
        OneWeek,
        TwoWeeks,
        OneMonth,
        TwoMonths,
        ThreeMonths,
        SixMonths,
        OneYear,
        All
    }

    public enum ThreadUsersFilter {
        All,
        HideTopicsParticipatedIn,
        HideTopicsNotParticipatedIn,
        HideTopicsByAnonymousUsers,
        HideTopicsByNonAnonymousUsers
    }
}
