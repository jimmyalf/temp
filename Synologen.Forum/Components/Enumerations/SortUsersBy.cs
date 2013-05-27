using System;

namespace Spinit.Wpc.Forum.Enumerations {

    // *********************************************************************
    //  SortUsersBy
    //
    /// <summary>
    /// Enum for control how user's are sorted - Note the sort is performed
    /// in the database.
    /// </summary>
    /// 
    // ********************************************************************/
    public enum SortUsersBy {

        JoinedDate = 0,
        Username = 1,
        Website = 2,
        LastActiveDate = 3,
        Posts = 4

    }
}
