using FacebookWrapper.ObjectModel;
using System;

namespace BasicFacebookFeatures
{
    // Template Method Pattern — applied at the Feature level
    // Eliminates the duplicated double-loop across all photo feature classes.
    // Subclasses override a single abstract method to define their selection logic;
    // the loop structure lives here once and is never repeated.

    public abstract class MaxScorePhotoFeature : IFacebookFeature<Photo>
    {
        protected abstract int getScore(Photo i_Photo);

        public Photo Execute(User i_User)
        {
            Photo bestPhoto = null;
            int maxScore = 0;

            try
            {
                foreach (Album album in i_User.Albums)
                {
                    foreach (Photo photo in album.Photos)
                    {
                        int score = getScore(photo);

                        if (score > maxScore)
                        {
                            bestPhoto = photo;
                            maxScore = score;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Facebook API throws when user has no permission — return null to signal no result
            }

            return bestPhoto;
        }
    }

    public abstract class DateExtremePhotoFeature : IFacebookFeature<Photo>
    {
        protected abstract bool isBetter(Photo i_Candidate, Photo i_Current);

        public Photo Execute(User i_User)
        {
            Photo result = null;

            try
            {
                foreach (Album album in i_User.Albums)
                {
                    foreach (Photo photo in album.Photos)
                    {
                        if (result == null || isBetter(photo, result))
                        {
                            result = photo;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Facebook API throws when user has no permission — return null to signal no result
            }

            return result;
        }
    }
}
