using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;

namespace BasicFacebookFeatures
{
    // Shared interface for FacebookManager (Facade) and CachingFacebookProxy (Proxy).
    // FormMain depends only on this interface — it never touches FacebookWrapper directly.
    public interface IFacebookService
    {
        // ── Session ──────────────────────────────────────────────────────────────
        User LoggedInUser { get; }
        LoginResult Login(string i_AppId);
        LoginResult ConnectWithToken(string i_Token);
        void Logout();

        // ── Facade methods: simplified view of the Facebook Graph API ─────────
        List<User>  GetFriends();
        List<Album> GetAlbums();
        List<Post>  GetRecentPosts(int i_MaxCount);
        List<Photo> GetPhotosFromAlbum(Album i_Album);
        void PostStatus(string i_Content);
        void UploadPhotoToAlbum(Album i_Album, string i_FilePath);
    }
}
