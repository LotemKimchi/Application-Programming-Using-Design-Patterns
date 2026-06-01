using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    // Factory Method Pattern
    public abstract class PhotoFeatureCreator
    {
        protected readonly IFacebookService r_Service;

        protected PhotoFeatureCreator(IFacebookService i_Service)
        {
            r_Service = i_Service;
        }

        public abstract IFacebookFeature<Photo> CreateFeature();

        public Photo RunAnalysis(User i_User)
        {
            IFacebookFeature<Photo> feature = CreateFeature();

            return feature.Execute(i_User);
        }
    }

    // Concrete Creators

    public class MostLikedPhotoCreator : PhotoFeatureCreator
    {
        public MostLikedPhotoCreator(IFacebookService i_Service) : base(i_Service) { }

        public override IFacebookFeature<Photo> CreateFeature()
        {
            return new MostLikedPhotoFeature(r_Service);
        }
    }

    public class MostCommentedPhotoCreator : PhotoFeatureCreator
    {
        public MostCommentedPhotoCreator(IFacebookService i_Service) : base(i_Service) { }

        public override IFacebookFeature<Photo> CreateFeature()
        {
            return new MostCommentedPhotoFeature(r_Service);
        }
    }

    public class OldestPhotoCreator : PhotoFeatureCreator
    {
        public OldestPhotoCreator(IFacebookService i_Service) : base(i_Service) { }

        public override IFacebookFeature<Photo> CreateFeature()
        {
            return new OldestPhotoFeature(r_Service);
        }
    }

    public class NewestPhotoCreator : PhotoFeatureCreator
    {
        public NewestPhotoCreator(IFacebookService i_Service) : base(i_Service) { }

        public override IFacebookFeature<Photo> CreateFeature()
        {
            return new NewestPhotoFeature(r_Service);
        }
    }

    public class MostTaggedPhotoCreator : PhotoFeatureCreator
    {
        public MostTaggedPhotoCreator(IFacebookService i_Service) : base(i_Service) { }

        public override IFacebookFeature<Photo> CreateFeature()
        {
            return new MostTaggedPhotoFeature(r_Service);
        }
    }
}
