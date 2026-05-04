using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    public class MostLikedPhotoFeature : MaxScorePhotoFeature
    {
        protected override int getScore(Photo i_Photo)
        {
            return i_Photo.LikedBy.Count;
        }
    }
}
