using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class MostLikedPhotoFeature
    {
        public Photo GetMostLikedPhotoFeature(User i_User)
        {
            Photo mostLiked = null;
            int maxLikes = 0;

            try
            {
                foreach (Album album in i_User.Albums)
                {
                    foreach (Photo photo in album.Photos)
                    {
                        int liked = photo.LikedBy.Count;
                        if (liked > maxLikes)
                        {
                            mostLiked = photo;
                            maxLikes = liked;
                        }
                    }
                }
            }
            catch (Exception i_Exception)
            {
            }

            return mostLiked;
        }
    }
}
