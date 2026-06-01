using FacebookWrapper.ObjectModel;
using System.Collections.Generic;

namespace BasicFacebookFeatures
{
    public class CountAlbumsFeature : IFacebookFeature<int>
    {
        private readonly IFacebookService r_Service;

        public CountAlbumsFeature(IFacebookService i_Service)
        {
            r_Service = i_Service;
        }

        public int Execute(User i_User)
        {
            int albumsCount = -1;

            try
            {
                List<Album> albums = r_Service.GetAlbums();
                albumsCount = albums.Count;
            }
            catch
            {
                // Return -1 to signal permission error
            }

            return albumsCount;
        }
    }
}
