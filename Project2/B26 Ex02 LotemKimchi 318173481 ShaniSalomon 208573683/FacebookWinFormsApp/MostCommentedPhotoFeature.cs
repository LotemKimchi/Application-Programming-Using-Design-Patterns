using FacebookWrapper.ObjectModel;
using System;

namespace BasicFacebookFeatures
{
    public class MostCommentedPhotoFeature : IFacebookFeature<Photo>
    {
        public Photo Execute(User i_User)
        {
            Photo mostCommentedPhoto = null;
            int maxComment = 0;

            try
            {
                foreach (Album album in i_User.Albums)
                {
                    foreach (Photo photo in album.Photos)
                    {
                        int commentCount = photo.Comments.Count;

                        if (commentCount > maxComment)
                        {
                            mostCommentedPhoto = photo;
                            maxComment = commentCount;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return mostCommentedPhoto;
        }
    }
}