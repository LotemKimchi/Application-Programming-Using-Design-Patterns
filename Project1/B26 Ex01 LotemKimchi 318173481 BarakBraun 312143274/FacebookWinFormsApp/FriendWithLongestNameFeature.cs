using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class FriendWithLongestNameFeature
    {
        public User GetFriendWithLongestName(User i_User)
        {
            User longestNameFriend = null;

            try
            {
                foreach (User friend in i_User.Friends)
                {
                    if (longestNameFriend == null ||
                        friend.Name.Length > longestNameFriend.Name.Length)
                    {
                        longestNameFriend = friend;
                    }
                }
            }
            catch(Exception i_Exception)
            {
            }

            return longestNameFriend;
        }
    }
}
