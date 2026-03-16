using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    public class AlbumWithMostPhotos
    {
        public Album GetAlbumWithMostPhotos(User i_User)
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
