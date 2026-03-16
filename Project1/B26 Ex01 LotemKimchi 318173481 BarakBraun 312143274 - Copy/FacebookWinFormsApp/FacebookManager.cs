using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class FacebookManager
    {
        private LoginResult m_LoginResult;

        public LoginResult Login(string appId)
        {
            m_LoginResult = FacebookService.Login(
                appId,
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

        public LoginResult ConnectWithSavedToken()
        {
            if (File.Exists("token.txt"))
            {
                string token = File.ReadAllText("token.txt");
                m_LoginResult = FacebookService.Connect(token);
            }

            return m_LoginResult;
        }

        public User LoggedInUser
        {
            get { return m_LoginResult?.LoggedInUser; }
        }

        public LoginResult ConnectWithToken(string i_Token)
        {
            m_LoginResult = FacebookService.Connect(i_Token);
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
    }
}
