using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    public class OldestPhotoFeature : DateExtremePhotoFeature
    {
        public OldestPhotoFeature(IFacebookService i_Service) : base(i_Service) { }

        protected override bool isBetter(Photo i_Candidate, Photo i_Current)
        {
            return i_Candidate.CreatedTime < i_Current.CreatedTime;
        }
    }
}
