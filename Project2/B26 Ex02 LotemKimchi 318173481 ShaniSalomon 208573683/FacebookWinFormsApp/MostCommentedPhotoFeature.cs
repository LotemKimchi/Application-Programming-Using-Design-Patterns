using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    public class MostCommentedPhotoFeature : MaxScorePhotoFeature
    {
        protected override int getScore(Photo i_Photo)
        {
            return i_Photo.Comments.Count;
        }
    }
}
