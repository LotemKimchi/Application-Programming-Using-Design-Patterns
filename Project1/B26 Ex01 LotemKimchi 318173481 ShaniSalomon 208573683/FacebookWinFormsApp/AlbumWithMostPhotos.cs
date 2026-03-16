using FacebookWrapper.ObjectModel;
using System;

namespace BasicFacebookFeatures
{
    public class AlbumWithMostPhotosFeature : IFacebookFeature<Album>
    {
        public Album Execute(User i_User)
        {
            Album albumWithMostPhotos = null;
            int maxPhotos = 0;

            try
            {
                foreach (Album album in i_User.Albums)
                {
                    if (album.Photos.Count > maxPhotos)
                    {
                        maxPhotos = album.Photos.Count;
                        albumWithMostPhotos = album;
                    }
                }
            }
            catch (Exception)
            {
            }

            return albumWithMostPhotos;
        }
    }
}