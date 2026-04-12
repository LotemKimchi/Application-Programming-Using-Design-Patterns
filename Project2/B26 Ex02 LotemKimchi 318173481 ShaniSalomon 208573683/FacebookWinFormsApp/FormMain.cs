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
        private const string k_AppId = "1783124789311728";
        private const string k_DesignPatternsToken = "EAAUm6cZC4eUEBQ89SIPgqvUNRPYwshVbzNFtykREi0CbEUsssHsY0ceBnLKHx9uOtmH5ClGksE6EzWZBRylGglQToWaaqV2QWsOcus79byyncz93TDesQvzX2pv2kllZA8mEg5iDMiYktoptWXySLSrS4Y2ATeDyEEFsJLZBmyshcy464jImETOhjyGYYKxJDZBWhxzRWLsRZApkMmJiEG742LGjEq486o9RgdhFrkuTLT0xup5efuMsJNL8ENsJqZC";

        private FacebookManager m_FacebookManager = new FacebookManager();

        public FormMain()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            FacebookWrapper.FacebookService.s_CollectionLimit = 25;

            var clipPath = new System.Drawing.Drawing2D.GraphicsPath();

            clipPath.AddEllipse(0, 0, pictureBoxProfile.Width, pictureBoxProfile.Height);
            pictureBoxProfile.Region = new Region(clipPath);

            buttonLogin.Visible = false;
            buttonConnectAsDesig.Visible = false;
        }

        private bool ensureLoggedIn()
        {
            bool isLoggedIn = m_FacebookManager.LoggedInUser != null;

            if (!isLoggedIn)
            {
                MessageBox.Show("Please login first");
            }

            return isLoggedIn;
        }

        public bool LoginWithFacebook()
        {
            LoginResult loginResult = m_FacebookManager.Login(k_AppId);

            if (!string.IsNullOrEmpty(loginResult.AccessToken))
            {
                afterLogin();
                displayUserInfo();
                return true;
            }

            string errorMessage = string.IsNullOrEmpty(loginResult.ErrorMessage)
                ? "User cancelled login."
                : loginResult.ErrorMessage;

            MessageBox.Show(errorMessage, "Login Failed");
            return false;
        }

        public bool LoginWithDesigAccount()
        {
            try
            {
                LoginResult loginResult = m_FacebookManager.ConnectWithToken(k_DesignPatternsToken);

                if (loginResult != null && !string.IsNullOrEmpty(loginResult.AccessToken))
                {
                    afterLogin();
                    displayUserInfo();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Failed");
            }

            return false;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            LoginResult loginResult = m_FacebookManager.Login(k_AppId);

            if (!string.IsNullOrEmpty(loginResult.AccessToken))
            {
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
                labelBirthday.Text = $"🎂  Born:   {user.Birthday}";

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
                pictureBoxProfile.ImageLocation = user.PictureNormalURL;
                buttonLogout.Enabled = true;

                toolStripStatusLabel1.Text = $"Logged in as {user.Name}";
                toolStripStatusLabel1.ForeColor = Color.FromArgb(66, 183, 42);
            }
        }

        private void clearUserData()
        {
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

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            m_FacebookManager.Logout();
            clearUserData();

            FormLogin loginForm = Application.OpenForms["FormLogin"] as FormLogin;

            if (loginForm != null)
            {
                loginForm.ShowLoginScreen();
            }
            else
            {
                this.Close();
            }
        }

        private void buttonConnectAsDesig_Click_1(object sender, EventArgs e)
        {
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

            if (albumsCount == -1)
            {
                labelAlbumsCount.Text = "No permission to access albums";
            }
            else
            {
                labelAlbumsCount.Text = $"Albums Count: {albumsCount}";
            }
        }
    }
}
