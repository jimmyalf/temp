using System;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Components {

    public class Rating {
        User user;
        int rating = 0;

        public int Value {
            get { return rating; }
            set { rating = value; }
        }

        public User User {
            get { return user; }
            set { user = value; }
        }
    }
}
