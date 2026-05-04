using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;

namespace BasicFacebookFeatures
{
    public class AlbumWithMostPhotosFeature : IFacebookFeature<Album>
    {
        private readonly IFacebookService r_Service;

        public AlbumWithMostPhotosFeature(IFacebookService i_Service)
        {
            r_Service = i_Service;
        }

        public Album Execute(User i_User)
        {
            Album albumWithMostPhotos = null;
            int maxPhotos = 0;

            try
            {
                List<Album> albums = r_Service.GetAlbums();

                foreach (Album album in albums)
                {
                    int photoCount = r_Service.GetPhotosFromAlbum(album).Count;

                    if (photoCount > maxPhotos)
                    {
                        maxPhotos = photoCount;
                        albumWithMostPhotos = album;
                    }
                }
            }
            catch (Exception)
            {
                // Facebook API throws when user has no permission — return null to signal no result
            }

            return albumWithMostPhotos;
        }
    }
}
