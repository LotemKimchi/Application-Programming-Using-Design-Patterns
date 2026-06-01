using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    // Strategy Pattern — Strategy interface for sorting friends
    public interface IFriendSortStrategy
    {
        IEnumerable<User> Sort(IEnumerable<User> i_Friends);
    }
}
