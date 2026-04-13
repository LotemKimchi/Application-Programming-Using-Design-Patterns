using FacebookWrapper.ObjectModel;
using System;

namespace BasicFacebookFeatures
{
    public class NewestPhotoFeature : IFacebookFeature<Photo>
    {
        public Photo Execute(User i_User)
        {
            Photo newestPhoto = null;

            try
            {
                foreach (Album album in i_User.Albums)
                {
                    foreach (Photo photo in album.Photos)
                    {
                        if (newestPhoto == null ||
                            photo.CreatedTime > newestPhoto.CreatedTime)
                        {
                            newestPhoto = photo;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return newestPhoto;
        }
    }
}
