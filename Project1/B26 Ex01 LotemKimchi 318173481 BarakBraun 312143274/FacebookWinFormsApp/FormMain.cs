using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 25;
        }

        FacebookWrapper.LoginResult m_LoginResult;

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            m_LoginResult = FacebookWrapper.FacebookService.Login(
                "1783124789311728",
                "email",
                "public_profile",
                "user_friends");

            Clipboard.SetText("design.patterns");

            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                MessageBox.Show($"Logged in as {m_LoginResult.LoggedInUser.Name}");
                displayUserInfo();
            }
            else
            {
                MessageBox.Show(m_LoginResult.ErrorMessage);
            }
            //if (m_LoginResult == null)
            //{
            //    login();
            //}
        }

        //אולי צריך להוסיף  לסעיף G
        private void displayUserInfo()
        {
            User user = m_LoginResult.LoggedInUser;

            labelName.Text = user.Name;
            labelBirthday.Text = user.Birthday;

            labelFriendsList.Text = "Friends List";
            listBoxFriend.Items.Clear();

            foreach (User friend in user.Friends)
            {
                listBoxFriend.Items.Add(friend.Name);
            }

            labelAlbumList.Text = "Album List";
            listBoxAlbums.Items.Clear();

            foreach (Album album in user.Albums)
            {
                listBoxAlbums.Items.Add(album.Name);
            }
        }
        private void login()
        {
            m_LoginResult = FacebookService.Login(
                /// (This is Desig Patter's App ID. replace it with your own)
                textBoxAppID.Text,
                /// requested permissions:
                "email",
                "public_profile"
                /// add any relevant permissions
                );

            if (string.IsNullOrEmpty(m_LoginResult.ErrorMessage))
            {
                afterLogin();
            }

            User user = m_LoginResult.LoggedInUser;

        }


        private void buttonConnectAsDesig_Click(object sender, EventArgs e)
        {
            try
            {
                m_LoginResult = FacebookService.Connect("EAAUm6cZC4eUEBQTAa3rRgO39UZCIJLeD9OpF5SYAevqSaFI16sfjT6JznpAUbyX5Soyj4Uv2ZBRkesoHO9omNcJ3KSYPZCExgaKrIprACUMIVnhiHzT5a46zbdC2VkvZC04n1ZARj8WmvOCYyuIdmRZBNjtWZCFJrbjFoms5t3sU8G9dO1xDCYH7kkfU67heIUZCFDIuTtL0CzF2JUHBpRpwPdXYilOJW811z3C5fY9TOyBiUwZAqx4ZAV6YS5ZBBtYKdsb7");
                displayUserInfo();
                afterLogin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(m_LoginResult.ErrorMessage, "Login Failed");
            }
        }

        private void afterLogin()
        {

            buttonLogin.Text = $"Logged in as {m_LoginResult.LoggedInUser.Name}";
            buttonLogin.BackColor = Color.LightGreen;
            pictureBoxProfile.ImageLocation = m_LoginResult.LoggedInUser.PictureNormalURL;
            buttonLogin.Enabled = false;
            buttonLogout.Enabled = true;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            FacebookService.LogoutWithUI();
            buttonLogin.Text = "Login";
            buttonLogin.BackColor = buttonLogout.BackColor;
            m_LoginResult = null;
            buttonLogin.Enabled = true;
            buttonLogout.Enabled = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void labelBoxFriends_Click(object sender, EventArgs e)
        {

        }
    }
}
