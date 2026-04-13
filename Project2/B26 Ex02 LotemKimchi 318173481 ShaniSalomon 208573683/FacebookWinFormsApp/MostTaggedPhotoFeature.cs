using FacebookWrapper.ObjectModel;
using System;

namespace BasicFacebookFeatures
{
    public class MostTaggedPhotoFeature : IFacebookFeature<Photo>
    {
        public Photo Execute(User i_User)
        {
            Photo mostTagged = null;
            int maxTags = 0;

            try
            {
                foreach (Album album in i_User.Albums)
                {
                    foreach (Photo photo in album.Photos)
                    {
                        int tagCount = photo.Tags != null ? photo.Tags.Count : 0;

                        if (tagCount > maxTags)
                        {
                            mostTagged = photo;
                            maxTags = tagCount;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return mostTagged;
        }
    }
}
