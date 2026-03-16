using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class MostCommentedPhotoFeature
    {
        public Photo GetMostCommentedPhotoFeature(User i_User)
        {
            Photo mostCommentedPhoto = null;
            int maxComment = 0;

            try
            {
                foreach(Album album in i_User.Albums)
                {
                    foreach (Photo photo in album.Photos)
                    {
                        int commentCount = photo.Comments.Count;

                        if(commentCount > maxComment)
                        {
                            mostCommentedPhoto = photo;
                            maxComment = commentCount;
                        }
                    }
                }
            }
            catch (Exception i_Exception)
            {
            }

            return mostCommentedPhoto;
        }
    }
}
