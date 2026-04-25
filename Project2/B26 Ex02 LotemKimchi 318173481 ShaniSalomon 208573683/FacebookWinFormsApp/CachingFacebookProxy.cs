using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;

namespace BasicFacebookFeatures
{
    // Proxy Pattern
    // Role: intercepts every call to IFacebookService and returns a cached result when
    //       the same data was already fetched.  This avoids repeated round-trips to the
    //       Facebook Graph API (which are slow and rate-limited).
    //
    // Architecture:  FormMain  →  CachingFacebookProxy  →  FacebookManager (Facade)
    public class CachingFacebookProxy : IFacebookService
    {
        private readonly IFacebookService r_RealService;

        // ── Cache fields ─────────────────────────────────────────────────────────
        private List<User>  m_CachedFriends;
        private List<Album> m_CachedAlbums;
        private List<Post>  m_CachedPosts;
        private readonly Dictionary<string, List<Photo>> r_CachedAlbumPhotos =
            new Dictionary<string, List<Photo>>();

        public CachingFacebookProxy(IFacebookService i_RealService)
        {
            r_RealService = i_RealService;
        }

        // ── Session: always delegate; invalidate cache on login/logout ───────────

        public User LoggedInUser
        {
            get { return r_RealService.LoggedInUser; }
        }

        public LoginResult Login(string i_AppId)
        {
            InvalidateCache();
            return r_RealService.Login(i_AppId);
        }

        public LoginResult ConnectWithToken(string i_Token)
        {
            InvalidateCache();
            return r_RealService.ConnectWithToken(i_Token);
        }

        public void Logout()
        {
            r_RealService.Logout();
            InvalidateCache();
        }

        // ── Cached data access ───────────────────────────────────────────────────

        public List<User> GetFriends()
        {
            if (m_CachedFriends == null)
            {
                m_CachedFriends = r_RealService.GetFriends();
            }

            return m_CachedFriends;
        }

        public List<Album> GetAlbums()
        {
            if (m_CachedAlbums == null)
            {
                m_CachedAlbums = r_RealService.GetAlbums();
            }

            return m_CachedAlbums;
        }

        public List<Post> GetRecentPosts(int i_MaxCount)
        {
            if (m_CachedPosts == null)
            {
                m_CachedPosts = r_RealService.GetRecentPosts(i_MaxCount);
            }

            return m_CachedPosts;
        }

        public List<Photo> GetPhotosFromAlbum(Album i_Album)
        {
            string albumId = i_Album?.Id ?? string.Empty;
            List<Photo> photos;

            if (!r_CachedAlbumPhotos.TryGetValue(albumId, out photos))
            {
                photos = r_RealService.GetPhotosFromAlbum(i_Album);
                r_CachedAlbumPhotos[albumId] = photos;
            }

            return photos;
        }

        // ── Write operations: delegate + selectively invalidate related cache ───

        public void PostStatus(string i_Content)
        {
            r_RealService.PostStatus(i_Content);
            m_CachedPosts = null; // posts list is now stale
        }

        public void UploadPhotoToAlbum(Album i_Album, string i_FilePath)
        {
            r_RealService.UploadPhotoToAlbum(i_Album, i_FilePath);

            // Invalidate photo cache for this album (photo count changed)
            string albumId = i_Album?.Id ?? string.Empty;
            r_CachedAlbumPhotos.Remove(albumId);
            m_CachedAlbums = null; // album list may reflect new photo count
        }

        // ── Cache management ─────────────────────────────────────────────────────

        public void InvalidateCache()
        {
            m_CachedFriends = null;
            m_CachedAlbums  = null;
            m_CachedPosts   = null;
            r_CachedAlbumPhotos.Clear();
        }
    }
}
