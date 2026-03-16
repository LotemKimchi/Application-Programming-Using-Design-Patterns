using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BasicFacebookFeatures
{
    public class OldestPhotoFeature
    {
        public Photo GetOldestPhoto(User i_User)
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
            catch (Exception i_Exception)
            {
            }

            return oldestPhoto;
        }
    }
}
