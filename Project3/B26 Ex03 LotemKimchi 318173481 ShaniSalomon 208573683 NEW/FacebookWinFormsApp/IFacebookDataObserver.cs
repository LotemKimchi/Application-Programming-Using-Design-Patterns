namespace BasicFacebookFeatures
{
    public enum eFacebookDataChangeType
    {
        PostPublished,
        PhotoUploaded,
        LoggedOut
    }

    // Observer Pattern — Observer interface
    public interface IFacebookDataObserver
    {
        void OnFacebookDataChanged(eFacebookDataChangeType i_ChangeType);
    }
}
