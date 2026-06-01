using System.Collections.Generic;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    // Observer Pattern — Concrete Subject
    // Wraps IFacebookService and notifies registered observers when data changes.
    public class FacebookDataSubject : IFacebookService
    {
        private readonly IFacebookService r_RealService;
        private readonly List<IFacebookDataObserver> r_Observers = new List<IFacebookDataObserver>();

        public FacebookDataSubject(IFacebookService i_RealService)
        {
            r_RealService = i_RealService;
        }

        public void RegisterObserver(IFacebookDataObserver i_Observer)
        {
            r_Observers.Add(i_Observer);
        }

        public void RemoveObserver(IFacebookDataObserver i_Observer)
        {
            r_Observers.Remove(i_Observer);
        }

        private void notifyObservers(eFacebookDataChangeType i_ChangeType)
        {
            foreach (IFacebookDataObserver observer in r_Observers)
            {
                observer.OnFacebookDataChanged(i_ChangeType);
            }
        }

        public User LoggedInUser => r_RealService.LoggedInUser;

        public LoginResult Login(string i_AppId)
        {
            return r_RealService.Login(i_AppId);
        }

        public LoginResult ConnectWithToken(string i_Token)
        {
            return r_RealService.ConnectWithToken(i_Token);
        }

        public void Logout()
        {
            r_RealService.Logout();
            notifyObservers(eFacebookDataChangeType.LoggedOut);
        }

        public List<User> GetFriends()
        {
            return r_RealService.GetFriends();
        }

        public List<Album> GetAlbums()
        {
            return r_RealService.GetAlbums();
        }

        public List<Post> GetRecentPosts(int i_MaxCount)
        {
            return r_RealService.GetRecentPosts(i_MaxCount);
        }

        public List<Photo> GetPhotosFromAlbum(Album i_Album)
        {
            return r_RealService.GetPhotosFromAlbum(i_Album);
        }

        public void PostStatus(string i_Content)
        {
            r_RealService.PostStatus(i_Content);
            notifyObservers(eFacebookDataChangeType.PostPublished);
        }

        public void UploadPhotoToAlbum(Album i_Album, string i_FilePath)
        {
            r_RealService.UploadPhotoToAlbum(i_Album, i_FilePath);
            notifyObservers(eFacebookDataChangeType.PhotoUploaded);
        }
    }
}
