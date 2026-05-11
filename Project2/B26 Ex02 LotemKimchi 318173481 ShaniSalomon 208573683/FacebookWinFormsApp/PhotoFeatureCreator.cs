using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    // Factory Method Pattern
    public abstract class PhotoFeatureCreator
    {
        public abstract IFacebookFeature<Photo> CreateFeature();

        public Photo RunAnalysis(User i_User)
        {
            IFacebookFeature<Photo> feature = CreateFeature();
            return feature.Execute(i_User);
        }
    }

    //Concrete Creators 

    public class MostLikedPhotoCreator : PhotoFeatureCreator
    {
        public override IFacebookFeature<Photo> CreateFeature()
        {
            return new MostLikedPhotoFeature();
        }
    }

    public class MostCommentedPhotoCreator : PhotoFeatureCreator
    {
        public override IFacebookFeature<Photo> CreateFeature()
        {
            return new MostCommentedPhotoFeature();
        }
    }

    public class OldestPhotoCreator : PhotoFeatureCreator
    {
        public override IFacebookFeature<Photo> CreateFeature()
        {
            return new OldestPhotoFeature();
        }
    }

    public class NewestPhotoCreator : PhotoFeatureCreator
    {
        public override IFacebookFeature<Photo> CreateFeature()
        {
            return new NewestPhotoFeature();
        }
    }

    public class MostTaggedPhotoCreator : PhotoFeatureCreator
    {
        public override IFacebookFeature<Photo> CreateFeature()
        {
            return new MostTaggedPhotoFeature();
        }
    }
}
