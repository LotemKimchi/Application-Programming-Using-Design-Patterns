using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    public class MostCommentedPhotoFeature : MaxScorePhotoFeature
    {
        public MostCommentedPhotoFeature(IFacebookService i_Service) : base(i_Service) { }

        protected override int getScore(Photo i_Photo)
        {
            return i_Photo.Comments.Count;
        }
    }
}
