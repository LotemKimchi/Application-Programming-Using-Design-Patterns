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

        private const string k_AppId = "1783124789311728";
        private const string k_DesignPatternsToken = "EAAUm6cZC4eUEBQTAa3rRgO39UZCIJLeD9OpF5SYAevqSaFI16sfjT6JznpAUbyX5Soyj4Uv2ZBRkesoHO9omNcJ3KSYPZCExgaKrIprACUMIVnhiHzT5a46zbdC2VkvZC04n1ZARj8WmvOCYyuIdmRZBNjtWZCFJrbjFoms5t3sU8G9dO1xDCYH7kkfU67heIUZCFDIuTtL0CzF2JUHBpRpwPdXYilOJW811z3C5fY9TOyBiUwZAqx4ZAV6YS5ZBBtYKdsb7";

        private FacebookManager m_FacebookManager = new FacebookManager();

        private bool ensureLoggedIn()
        {
            bool isLoggedIn = m_FacebookManager.LoggedInUser != null;

            if (!isLoggedIn)
            {
                MessageBox.Show("Please login first");
            }

            return isLoggedIn;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            LoginResult loginResult = m_FacebookManager.Login(k_AppId);

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

            if (user != null)
            {
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

            if (user != null)
            {
                buttonLogin.Text = $"\u2714  {user.Name}";
                buttonLogin.BackColor = Color.FromArgb(66, 183, 42);

                pictureBoxProfile.ImageLocation = user.PictureNormalURL;

                buttonLogin.Enabled = false;
                buttonLogout.Enabled = true;

                toolStripStatusLabel1.Text = $"\uD83D\uDFE2  Logged in as {user.Name}";
                toolStripStatusLabel1.ForeColor = Color.FromArgb(66, 183, 42);
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            m_FacebookManager.Logout();

            buttonLogin.Text = "\u25B6  Login with Facebook";
            buttonLogin.BackColor = Color.FromArgb(24, 119, 242);

            toolStripStatusLabel1.Text = "\u26AA  Not logged in";
            toolStripStatusLabel1.ForeColor = Color.FromArgb(101, 103, 107);

            buttonLogin.Enabled = true;
            buttonLogout.Enabled = false;

            listBoxAlbums.Items.Clear();

            // Profile
            labelName.Text = "";
            labelBirthday.Text = "";
            pictureBoxProfile.ImageLocation = null;

            // Albums & Photos tab
            labelOldestPhotoDate.Text = "";
            labelAlbumName.Text = "";
            pictureBoxOldestPhoto.ImageLocation = null;

            // Analytics tab
            labelMostLikedStatus.Text = "";
            pictureBoxMostLikePhoto.ImageLocation = null;
            labelMostCommentedStatus.Text = "";
            pictureBoxMostCommentedPhoto.ImageLocation = null;
            labelAlbumsCount.Text = "";
        }

        private void buttonConnectAsDesig_Click_1(object sender, EventArgs e)
        {
            try
            {
                LoginResult loginResult = m_FacebookManager.ConnectWithToken(k_DesignPatternsToken);

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

        private void buttonMostPhotosAlbum_Click(object sender, EventArgs e)
        {
            if (!ensureLoggedIn())
            {
                return;
            }

            User user = m_FacebookManager.LoggedInUser;

            IFacebookFeature<Album> feature = new AlbumWithMostPhotosFeature();

            Album album = feature.Execute(user);

            if (album != null)
            {
                labelAlbumName.Text = album.Name;
            }
            else
            {
                labelAlbumName.Text = "No albums found";
            }
        }

        private void buttonOldestPhoto_Click_1(object sender, EventArgs e)
        {
            if (!ensureLoggedIn())
            {
                return;
            }

            User user = m_FacebookManager.LoggedInUser;

            IFacebookFeature<Photo> feature = new OldestPhotoFeature();

            pictureBoxOldestPhoto.ImageLocation = null;
            labelOldestPhotoDate.Text = "";

            Photo photo = feature.Execute(user);

            if (photo != null)
            {
                pictureBoxOldestPhoto.ImageLocation = photo.PictureNormalURL;
                labelOldestPhotoDate.Text = $"Oldest photo created at {photo.CreatedTime}";
            }
            else
            {
                labelOldestPhotoDate.Text = "No photos found";
            }
        }

        private void buttonMostLikedPhoto_Click_1(object sender, EventArgs e)
        {
            if (!ensureLoggedIn())
            {
                return;
            }

            User user = m_FacebookManager.LoggedInUser;

            IFacebookFeature<Photo> feature = new MostLikedPhotoFeature();

            labelMostLikedStatus.Visible = false;
            pictureBoxMostLikePhoto.ImageLocation = null;

            Photo photo = feature.Execute(user);

            if (photo != null)
            {
                pictureBoxMostLikePhoto.ImageLocation = photo.PictureNormalURL;
            }
            else
            {
                labelMostLikedStatus.Text = "No photo found";
                labelMostLikedStatus.Visible = true;
            }
        }

        private void buttonMostCommentedPhoto_Click_1(object sender, EventArgs e)
        {
            if (!ensureLoggedIn())
            {
                return;
            }

            User user = m_FacebookManager.LoggedInUser;

            IFacebookFeature<Photo> feature = new MostCommentedPhotoFeature();

            labelMostCommentedStatus.Visible = false;
            pictureBoxMostCommentedPhoto.ImageLocation = null;

            Photo photo = feature.Execute(user);

            if (photo != null)
            {
                pictureBoxMostCommentedPhoto.ImageLocation = photo.PictureNormalURL;
                labelMostCommentedStatus.Text = $"{photo.Comments.Count} comments";
                labelMostCommentedStatus.Visible = true;
            }
            else
            {
                labelMostCommentedStatus.Text = "No photo found";
                labelMostCommentedStatus.Visible = true;
            }
        }

        private void buttonCountAlbums_Click(object sender, EventArgs e)
        {
            if (!ensureLoggedIn())
            {
                return;
            }

            User user = m_FacebookManager.LoggedInUser;

            IFacebookFeature<int> feature = new CountAlbumsFeature();

            int albumsCount = feature.Execute(user);

            labelAlbumsCount.Text = $"Albums Count: {albumsCount}";
        }
    }
}
