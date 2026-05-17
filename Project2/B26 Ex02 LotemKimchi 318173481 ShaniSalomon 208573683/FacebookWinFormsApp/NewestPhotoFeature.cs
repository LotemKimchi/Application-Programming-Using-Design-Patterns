using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    public class NewestPhotoFeature : DateExtremePhotoFeature
    {
        public NewestPhotoFeature(IFacebookService i_Service) : base(i_Service) { }

        protected override bool isBetter(Photo i_Candidate, Photo i_Current)
        {
            return i_Candidate.CreatedTime > i_Current.CreatedTime;
        }
    }
}
