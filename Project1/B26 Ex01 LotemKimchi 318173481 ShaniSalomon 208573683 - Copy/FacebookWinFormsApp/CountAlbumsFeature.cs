using FacebookWrapper.ObjectModel;
using System;

namespace BasicFacebookFeatures
{
    public class CountAlbumsFeature : IFacebookFeature<int>
    {
        public int Execute(User i_User)
        {
            int albumsCount = 0;

            try
            {
                foreach (Album album in i_User.Albums)
                {
                    albumsCount++;
                }
            }
            catch (Exception)
            {
                return -1;
            }

            return albumsCount;
        }
    }
}