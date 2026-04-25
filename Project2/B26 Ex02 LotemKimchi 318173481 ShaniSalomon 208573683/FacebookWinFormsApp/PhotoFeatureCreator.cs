using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    // Factory Method Pattern
    //
    // Creator (abstract): PhotoFeatureCreator
    //   - declares the factory method CreateFeature() that subclasses override
    //   - provides RunAnalysis() as a template that uses the created product
    //
    // ConcreteCreators: one per analysis strategy (see below)
    //
    // Product (interface): IFacebookFeature<Photo>   (already defined)
    // ConcreteProducts:    MostLikedPhotoFeature, MostCommentedPhotoFeature, etc.
    //
    // Benefit: adding a new analysis requires only a new ConcreteCreator +
    //          ConcreteProduct — FormMain never needs to change (Open/Closed Principle).

    public abstract class PhotoFeatureCreator
    {
        // Factory Method — subclasses decide which concrete product to instantiate
        public abstract IFacebookFeature<Photo> CreateFeature();

        // Template Method — uses the factory method; FormMain calls this
        public Photo RunAnalysis(User i_User)
        {
            IFacebookFeature<Photo> feature = CreateFeature();
            return feature.Execute(i_User);
        }
    }

    // ── Concrete Creators ────────────────────────────────────────────────────────

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
