using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper;
using System.IO;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class MostLikedFriendFeature
    {
        public string GetMostLikedFriendFeature(User i_User)
        {
            Dictionary<string,int> likesCounter = new Dictionary<string,int>();

            try
            {
                foreach (Album album in i_User.Albums)
                {
                    foreach (Photo photo in album.Photos)
                    {
                        foreach (User liker in photo.LikedBy)
                        {
                            if (!likesCounter.ContainsKey(liker.Name))
                            {
                                likesCounter[liker.Name] = 0;
                            }

                            likesCounter[liker.Name]++;
                        }
                    }
                }
            }
            catch (Exception i_Exception)
            {
            }

            string mostLikedFriend = null;
            int maxLikes = 0;

            if (likesCounter.Count == 0)
            {
                mostLikedFriend = "Cannot access likes due to Facebook permissions";
            }       
            else if (likesCounter.Count == 0)
            {
                mostLikedFriend = "No likes found";
            }
            else
            {
                foreach (KeyValuePair<string, int> pair in likesCounter)
                {
                    if (pair.Value > maxLikes)
                    {
                        maxLikes = pair.Value;
                        mostLikedFriend = pair.Key;
                    }
                }
            }

            return mostLikedFriend;
            //return likesCounter.OrderByDescending(pair => pair.Value).First().Key;
        }       
    }
}
