using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    public class MostTaggedPhotoFeature : MaxScorePhotoFeature
    {
        protected override int getScore(Photo i_Photo)
        {
            return i_Photo.Tags != null ? i_Photo.Tags.Count : 0;
        }
    }
}
