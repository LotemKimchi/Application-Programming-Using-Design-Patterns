using FacebookWrapper.ObjectModel;
using System;

namespace BasicFacebookFeatures
{
    public class MostLikedPhotoFeature : IFacebookFeature<Photo>
    {
        public Photo Execute(User i_User)
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
            catch (Exception)
            {
            }

            return mostLiked;
        }
    }
}