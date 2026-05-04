using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    public class OldestPhotoFeature : DateExtremePhotoFeature
    {
        protected override bool isBetter(Photo i_Candidate, Photo i_Current)
        {
            return i_Candidate.CreatedTime < i_Current.CreatedTime;
        }
    }
}
