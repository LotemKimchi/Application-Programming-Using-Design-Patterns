using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;

namespace BasicFacebookFeatures
{
    // Interface for FacebookManager (Facade) and CachingFacebookProxy (Proxy)
    public interface IFacebookService
    {
        //Session
        User LoggedInUser { get; }
        LoginResult Login(string i_AppId);
        LoginResult ConnectWithToken(string i_Token);
        void Logout();

        //Facade methods
        List<User>  GetFriends();
        List<Album> GetAlbums();
        List<Post>  GetRecentPosts(int i_MaxCount);
        List<Photo> GetPhotosFromAlbum(Album i_Album);
        void PostStatus(string i_Content);
        void UploadPhotoToAlbum(Album i_Album, string i_FilePath);
    }
}
