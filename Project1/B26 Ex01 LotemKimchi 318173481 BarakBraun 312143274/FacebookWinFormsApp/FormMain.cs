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
            
            connectWithSavedToken();
        }

        //FacebookWrapper.LoginResult m_LoginResult;
        FacebookManager m_FacebookManager = new FacebookManager();
        private void buttonLogin_Click(object sender, EventArgs e)
        {

            LoginResult loginResult = m_FacebookManager.Login("1783124789311728");

            if (!string.IsNullOrEmpty(loginResult.AccessToken))
            {
                MessageBox.Show($"Logged in as {loginResult.LoggedInUser.Name}");
                afterLogin();
                displayUserInfo();
            }
            else
            {
                string errorMessage = string.IsNullOrEmpty(loginResult.ErrorMessage)
                    ? "User cancelled login."
                    : loginResult.ErrorMessage;

                MessageBox.Show(errorMessage);
            }
            Clipboard.SetText("design.patterns");
        }

        private void displayUserInfo()
        {
            User user = m_FacebookManager.LoggedInUser;

            if (user == null)
            {
                return;
            }

            labelName.Text = user.Name;
            labelBirthday.Text = user.Birthday;

            listBoxAlbums.Items.Clear();
            try
            {
                foreach (Album album in user.Albums)
                {
                    listBoxAlbums.Items.Add(album.Name);
                }

                if (listBoxAlbums.Items.Count == 0)
                {
                    listBoxAlbums.Items.Add("No albums found");
                }
            }
            catch
            {
                listBoxAlbums.Items.Add("Cannot load albums (permission missing)");
            }
        }

        private void connectWithSavedToken()
        {
            LoginResult loginResult = m_FacebookManager.ConnectWithSavedToken();

            if (loginResult != null && !string.IsNullOrEmpty(loginResult.AccessToken))
            {
                afterLogin();
                displayUserInfo();
            }
        }

        private void afterLogin()
        {
            User user = m_FacebookManager.LoggedInUser;

            if (user == null)
            {
                return;
            }

            buttonLogin.Text = $"Logged in as {user.Name}";
            buttonLogin.BackColor = Color.LightGreen;

            pictureBoxProfile.ImageLocation = user.PictureNormalURL;

            buttonLogin.Enabled = false;
            buttonLogout.Enabled = true;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            m_FacebookManager.Logout();

            buttonLogin.Text = "Login";
            buttonLogin.BackColor = SystemColors.Control;

            buttonLogin.Enabled = true;
            buttonLogout.Enabled = false;

            //listBoxFriend.Items.Clear();
            listBoxAlbums.Items.Clear();

            labelName.Text = "";
            labelBirthday.Text = "";

            pictureBoxProfile.ImageLocation = null;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void labelBoxFriends_Click(object sender, EventArgs e)
        {

        }

        private void buttonConnectAsDesig_Click_1(object sender, EventArgs e)
        {
            try
            {
                LoginResult loginResult = m_FacebookManager.ConnectWithToken(
                    "EAAUm6cZC4eUEBQTAa3rRgO39UZCIJLeD9OpF5SYAevqSaFI16sfjT6JznpAUbyX5Soyj4Uv2ZBRkesoHO9omNcJ3KSYPZCExgaKrIprACUMIVnhiHzT5a46zbdC2VkvZC04n1ZARj8WmvOCYyuIdmRZBNjtWZCFJrbjFoms5t3sU8G9dO1xDCYH7kkfU67heIUZCFDIuTtL0CzF2JUHBpRpwPdXYilOJW811z3C5fY9TOyBiUwZAqx4ZAV6YS5ZBBtYKdsb7"
                );

                if (loginResult != null && !string.IsNullOrEmpty(loginResult.AccessToken))
                {
                    afterLogin();
                    displayUserInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Failed");
            }
        }

        private void buttonMostLikedFriend_Click(object sender, EventArgs e)
        {
            User user = m_FacebookManager.LoggedInUser;

            if (user == null)
            {
                MessageBox.Show("Please login first");
                return;
            }

            MostLikedFriendFeature feature = new MostLikedFriendFeature();

            string result = feature.GetMostLikedFriendFeature(m_FacebookManager.LoggedInUser);

            MessageBox.Show("Most liked friend: " + result);
        }

        private void buttonMostLikedPhoto_Click(object sender, EventArgs e)
        {
            User user = m_FacebookManager.LoggedInUser;

            if (user == null)
            {
                MessageBox.Show("Please login first");
                return;
            }

            MostLikedPhotoFeature feature = new MostLikedPhotoFeature();

            Photo photo = feature.GetMostLikedPhotoFeature(m_FacebookManager.LoggedInUser);

            if (photo != null)
            {
                pictureBoxMostLikePhoto.ImageLocation = photo.PictureNormalURL;
            }
            else
            {
                MessageBox.Show("No photo found");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void buttonOldestPhoto_Click(object sender, EventArgs e)
        {
            User user = m_FacebookManager.LoggedInUser;

            if (user == null)
            {
                MessageBox.Show("Please login first");
                return;
            }

            OldestPhotoFeature feature = new OldestPhotoFeature();

            Photo photo = feature.GetOldestPhoto(m_FacebookManager.LoggedInUser);

            if (photo != null)
            {
                pictureBoxOldestPhoto.ImageLocation = photo.PictureNormalURL;

                MessageBox.Show($"Oldest photo created at {photo.CreatedTime}");
            }
            else
            {
                MessageBox.Show("No photos found");
            }
        }

        private void buttonMostCommentedPhoto_Click(object sender, EventArgs e)
        {
            User user = m_FacebookManager.LoggedInUser;

            if (user == null)
            {
                MessageBox.Show("Please login first");
                return;
            }

            MostCommentedPhotoFeature feature = new MostCommentedPhotoFeature();
            Photo photo = feature.GetMostCommentedPhotoFeature(m_FacebookManager.LoggedInUser);
            if (photo != null)
            {
                pictureBoxMostCommentedPhoto.ImageLocation = photo.PictureNormalURL;
                MessageBox.Show($"Most commented photo has {photo.Comments.Count} comments");
            }
            else
            {
                MessageBox.Show("No photos found");
            }

        }

        private void buttonMostPhotosAlbum_Click_1(object sender, EventArgs e)
        {
            User user = m_FacebookManager.LoggedInUser;

            if (user == null)
            {
                MessageBox.Show("Please login first");
                return;
            }

            AlbumWithMostPhotos feature = new AlbumWithMostPhotos();

            Album album = feature.GetAlbumWithMostPhotos(m_FacebookManager.LoggedInUser);

            if (album != null)
            {
                MessageBox.Show($"Album with most photos: {album.Name}");
            }
            else
            {
                MessageBox.Show("No albums found");
            }
        }

        private void buttonFriendWithLongestName_Click(object sender, EventArgs e)
        {
            User user = m_FacebookManager.LoggedInUser;

            if (user == null)
            {
                MessageBox.Show("Please login first");
                return;
            }

            FriendWithLongestNameFeature feature = new FriendWithLongestNameFeature();

            User friend = feature.GetFriendWithLongestName(user);

            if (friend != null)
            {
                MessageBox.Show($"Friend with longest name: {friend.Name}");
            }
            else
            {
                MessageBox.Show("No friends found");
            }
        }
    }
}
