using FacebookWrapper.ObjectModel;
using System.Collections.Generic;

namespace BasicFacebookFeatures
{
    public abstract class MaxScorePhotoFeature : IFacebookFeature<Photo>
    {
        private readonly IFacebookService r_Service;

        protected MaxScorePhotoFeature(IFacebookService i_Service)
        {
            r_Service = i_Service;
        }

        protected abstract int getScore(Photo i_Photo);

        public Photo Execute(User i_User)
        {
            Photo bestPhoto = null;
            int maxScore = 0;
            List<Album> albums = r_Service.GetAlbums();

            foreach (Album album in albums)
            {
                List<Photo> photos = r_Service.GetPhotosFromAlbum(album);

                foreach (Photo photo in photos)
                {
                    int score = getScore(photo);

                    if (score > maxScore)
                    {
                        bestPhoto = photo;
                        maxScore = score;
                    }
                }
            }

            return bestPhoto;
        }
    }

    public abstract class DateExtremePhotoFeature : IFacebookFeature<Photo>
    {
        private readonly IFacebookService r_Service;

        protected DateExtremePhotoFeature(IFacebookService i_Service)
        {
            r_Service = i_Service;
        }

        protected abstract bool isBetter(Photo i_Candidate, Photo i_Current);

        public Photo Execute(User i_User)
        {
            Photo result = null;
            List<Album> albums = r_Service.GetAlbums();

            foreach (Album album in albums)
            {
                List<Photo> photos = r_Service.GetPhotosFromAlbum(album);

                foreach (Photo photo in photos)
                {
                    if (result == null || isBetter(photo, result))
                    {
                        result = photo;
                    }
                }
            }

            return result;
        }
    }
}
