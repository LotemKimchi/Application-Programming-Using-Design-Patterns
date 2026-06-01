using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    public class MostLikedPhotoFeature : MaxScorePhotoFeature
    {
        public MostLikedPhotoFeature(IFacebookService i_Service) : base(i_Service) { }

        protected override int getScore(Photo i_Photo)
        {
            return i_Photo.LikedBy != null ? i_Photo.LikedBy.Count : 0;
        }
    }
}
