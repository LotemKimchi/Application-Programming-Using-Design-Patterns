using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System.Collections.Generic;
using System.IO;

namespace BasicFacebookFeatures
{
    // Facade Pattern
    // Role: provides a single, simplified interface to the complex FacebookWrapper API.
    // FormMain (and the Proxy) never call FacebookWrapper directly — they go through here.
    public class FacebookManager : IFacebookService
    {
        private LoginResult m_LoginResult;

        // ── Session ──────────────────────────────────────────────────────────────

        public User LoggedInUser
        {
            get { return m_LoginResult != null ? m_LoginResult.LoggedInUser : null; }
        }

        public LoginResult Login(string i_AppId)
        {
            m_LoginResult = FacebookService.Login(
                i_AppId,
                "email",
                "public_profile",
                "user_friends",
                "user_photos",
                "user_posts");

            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                File.WriteAllText("token.txt", m_LoginResult.AccessToken);
            }

            return m_LoginResult;
        }

        public LoginResult ConnectWithToken(string i_Token)
        {
            m_LoginResult = FacebookService.Connect(i_Token);

            if (m_LoginResult != null && !string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                File.WriteAllText("token.txt", m_LoginResult.AccessToken);
            }

            return m_LoginResult;
        }

        public void Logout()
        {
            FacebookService.LogoutWithUI();

            if (File.Exists("token.txt"))
            {
                File.Delete("token.txt");
            }

            m_LoginResult = null;
        }

        // ── Facade methods: hide the Facebook Graph API complexity ────────────

        public List<User> GetFriends()
        {
            List<User> friends = new List<User>();
            User user = LoggedInUser;

            if (user != null)
            {
                try
                {
                    foreach (User friend in user.Friends)
                    {
                        friends.Add(friend);
                    }
                }
                catch
                {
                    // Return empty list on permission failure
                }
            }

            return friends;
        }

        public List<Album> GetAlbums()
        {
            List<Album> albums = new List<Album>();
            User user = LoggedInUser;

            if (user != null)
            {
                try
                {
                    foreach (Album album in user.Albums)
                    {
                        albums.Add(album);
                    }
                }
                catch
                {
                    // Return empty list on permission failure
                }
            }

            return albums;
        }

        public List<Post> GetRecentPosts(int i_MaxCount)
        {
            List<Post> posts = new List<Post>();
            User user = LoggedInUser;

            if (user != null)
            {
                try
                {
                    int count = 0;

                    foreach (Post post in user.Posts)
                    {
                        posts.Add(post);
                        count++;

                        if (count >= i_MaxCount)
                        {
                            break;
                        }
                    }
                }
                catch
                {
                    // Return empty list on permission failure
                }
            }

            return posts;
        }

        public List<Photo> GetPhotosFromAlbum(Album i_Album)
        {
            List<Photo> photos = new List<Photo>();

            if (i_Album != null)
            {
                try
                {
                    foreach (Photo photo in i_Album.Photos)
                    {
                        photos.Add(photo);
                    }
                }
                catch
                {
                    // Return empty list on permission failure
                }
            }

            return photos;
        }

        public void PostStatus(string i_Content)
        {
            User user = LoggedInUser;

            if (user != null)
            {
                user.PostStatus(i_Content);
            }
        }

        public void UploadPhotoToAlbum(Album i_Album, string i_FilePath)
        {
            if (i_Album != null)
            {
                i_Album.UploadPhoto(i_FilePath);
            }
        }
    }
}
