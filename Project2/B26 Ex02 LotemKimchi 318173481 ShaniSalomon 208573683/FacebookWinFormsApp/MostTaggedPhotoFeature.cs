using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    public class MostTaggedPhotoFeature : MaxScorePhotoFeature
    {
        public MostTaggedPhotoFeature(IFacebookService i_Service) : base(i_Service) { }

        protected override int getScore(Photo i_Photo)
        {
            return i_Photo.Tags != null ? i_Photo.Tags.Count : 0;
        }
    }
}
