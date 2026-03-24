using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    public interface IFacebookFeature<T>
    {
        T Execute(User i_User);
    }
}