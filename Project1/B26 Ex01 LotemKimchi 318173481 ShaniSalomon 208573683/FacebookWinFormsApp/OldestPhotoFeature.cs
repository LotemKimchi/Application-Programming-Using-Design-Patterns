using FacebookWrapper.ObjectModel;
using System;

namespace BasicFacebookFeatures
{
    public class OldestPhotoFeature : IFacebookFeature<Photo>
    {
        public Photo Execute(User i_User)
        {
            Photo oldestPhoto = null;

            try
            {
                foreach (Album album in i_User.Albums)
                {
                    foreach (Photo photo in album.Photos)
                    {
                        if (oldestPhoto == null ||
                            photo.CreatedTime < oldestPhoto.CreatedTime)
                        {
                            oldestPhoto = photo;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return oldestPhoto;
        }
    }
}