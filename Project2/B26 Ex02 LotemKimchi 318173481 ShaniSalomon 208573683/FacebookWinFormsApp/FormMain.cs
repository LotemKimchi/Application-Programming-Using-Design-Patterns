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

        // UI → Proxy (cache) → Facade (FacebookManager) → FacebookWrapper
        private readonly IFacebookService r_FacebookService =
            new CachingFacebookProxy(new FacebookManager());

        // Factory Method: maps ComboBox label → ConcreteCreator
        private readonly Dictionary<string, PhotoFeatureCreator> r_PhotoCreators =
            new Dictionary<string, PhotoFeatureCreator>
            {
                { "Most Liked Photo",     new MostLikedPhotoCreator()     },
                { "Most Commented Photo", new MostCommentedPhotoCreator() },
                { "Oldest Photo",         new OldestPhotoCreator()         },
                { "Newest Photo",         new NewestPhotoCreator()         },
                { "Most Tagged Photo",    new MostTaggedPhotoCreator()     }
            };

        // Friends section
        private Label m_LabelFriendsHeader;
        private TextBox m_TextBoxSearchFriend;
        private FlowLayoutPanel m_FlowFriends;
        private Panel m_PanelFriendDetails;
        private PictureBox m_PictureBoxFriendDetail;
        private Label m_LabelFriendDetailName;
        private Label m_LabelFriendDetailInfo;
        private Label m_LabelFriendsCount;
        private readonly List<User> r_AllFriends = new List<User>();

        // Photos Analyst section (Strategy pattern via ComboBox)
        private ComboBox m_ComboPhotoStrategy;
        private Button m_ButtonAnalyzePhoto;
        private Panel m_PanelPhotoResult;
        private PictureBox m_PictureBoxPhotoResult;
        private Label m_LabelPhotoResultTitle;
        private Label m_LabelPhotoResultDetails;

        // Post Composer section
        private TabPage m_TabPagePost;
        private TextBox m_TextBoxPostContent;
        private Label m_LabelCharCount;
        private Button m_ButtonPost;
        private FlowLayoutPanel m_FlowRecentPosts;
        private const int k_MaxPostLength = 500;

        // Album Analyst section (enhanced)
        private DataGridView m_DataGridAlbums;
        private Label m_LabelSelectedAlbum;
        private Label m_LabelAlbumPhotoCount;
        private FlowLayoutPanel m_FlowAlbumThumbnails;
        private Button m_ButtonUploadPhoto;
        private Button m_ButtonLoadAlbums;
        private Album m_CurrentSelectedAlbum;
        private readonly List<Album> r_LoadedAlbums = new List<Album>();

        // Two-Way Data Binding: BindingList notifies the DataGridView of any
        // property change on AlbumViewModel (INotifyPropertyChanged) automatically.
        private readonly BindingList<AlbumViewModel> m_AlbumBindingList =
            new BindingList<AlbumViewModel>();

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

            setupFriendsSection();
            setupPhotosAnalystTab();
            setupPostTab();
            setupAlbumAnalystTab();
        }

        private void buttonAnalyzePhoto_Click(object sender, EventArgs e)
        {
            if (ensureLoggedIn())
            {
                // === Factory Method Pattern: look up the ConcreteCreator for the selected name ===
                string selectedName = m_ComboPhotoStrategy.SelectedItem as string;
                PhotoFeatureCreator creator;

                if (r_PhotoCreators.TryGetValue(selectedName, out creator))
                {
                    runPhotoAnalysis(creator);
                }
            }
        }

        // Uses the Factory Method pattern: creator.RunAnalysis() calls CreateFeature().Execute()
        // Multi-threading: the analysis iterates many albums/photos via the API —
        // running it on a background thread keeps the UI responsive.
        private void runPhotoAnalysis(PhotoFeatureCreator i_Creator)
        {
            m_PictureBoxPhotoResult.ImageLocation = null;
            m_LabelPhotoResultDetails.Text = "Analyzing...";
            m_LabelPhotoResultTitle.Text = m_ComboPhotoStrategy.SelectedItem.ToString();
            m_ButtonAnalyzePhoto.Enabled = false;

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
            {
                e.Result = i_Creator.RunAnalysis(e.Argument as User);
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                m_ButtonAnalyzePhoto.Enabled = true;

                if (e.Error != null)
                {
                    m_LabelPhotoResultDetails.Text = "Error during analysis.";
                    return;
                }

                Photo photo = e.Result as Photo;

                if (photo == null)
                {
                    m_LabelPhotoResultDetails.Text = "No photo found for this strategy.";
                }
                else
                {
                    displayPhotoResult(photo);
                }
            };

            worker.RunWorkerAsync(r_FacebookService.LoggedInUser);
        }

        private void displayPhotoResult(Photo i_Photo)
        {
            m_PictureBoxPhotoResult.ImageLocation = i_Photo.PictureNormalURL;

            StringBuilder details = new StringBuilder();
            details.AppendLine($"Created: {i_Photo.CreatedTime}");
            details.AppendLine();
            details.AppendLine($"Likes:    {safeCount(() => i_Photo.LikedBy != null ? (int?)i_Photo.LikedBy.Count : null)}");
            details.AppendLine($"Comments: {safeCount(() => i_Photo.Comments != null ? (int?)i_Photo.Comments.Count : null)}");
            details.AppendLine($"Tags:     {safeCount(() => i_Photo.Tags != null ? (int?)i_Photo.Tags.Count : null)}");

            string caption = safeGet(() => i_Photo.Name);
            if (!string.IsNullOrEmpty(caption))
            {
                details.AppendLine();
                details.AppendLine("Caption:");
                details.AppendLine(caption);
            }

            m_LabelPhotoResultDetails.Text = details.ToString();
        }

        private static string safeCount(Func<int?> i_Getter)
        {
            string result = "N/A";

            try
            {
                int? value = i_Getter();
                if (value.HasValue)
                {
                    result = value.Value.ToString();
                }
            }
            catch
            {
                // Keep default "N/A"
            }

            return result;
        }

        private static string safeGet(Func<string> i_Getter)
        {
            string result = null;

            try
            {
                result = i_Getter();
            }
            catch
            {
                // Keep default null
            }

            return result;
        }

        private void textBoxPostContent_TextChanged(object sender, EventArgs e)
        {
            int len = m_TextBoxPostContent.Text.Length;
            m_LabelCharCount.Text = $"{len} / {k_MaxPostLength} characters";

            if (len >= k_MaxPostLength)
            {
                m_LabelCharCount.ForeColor = Color.FromArgb(255, 120, 120);
            }
            else if (len > k_MaxPostLength * 0.8)
            {
                m_LabelCharCount.ForeColor = Color.FromArgb(255, 200, 100);
            }
            else
            {
                m_LabelCharCount.ForeColor = Color.FromArgb(160, 200, 255);
            }
        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            if (ensureLoggedIn())
            {
                string content = m_TextBoxPostContent.Text.Trim();

                if (string.IsNullOrEmpty(content))
                {
                    MessageBox.Show("Post cannot be empty.", "Empty Post");
                }
                else
                {
                    tryPostStatus(content);
                }
            }
        }

        private void tryPostStatus(string i_Content)
        {
            try
            {
                r_FacebookService.PostStatus(i_Content);
                MessageBox.Show("Posted successfully!", "Success");
                m_TextBoxPostContent.Clear();
                loadRecentPosts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to post to Facebook.\n\nReason: {ex.Message}\n\n(Publishing via API was restricted by Facebook - this feature may not work for all accounts.)",
                    "Post Failed");
            }
        }

        private void loadRecentPosts()
        {
            m_FlowRecentPosts.Controls.Clear();
            showEmptyPostsMessage("Loading posts...");

            // Multi-threading: GetRecentPosts() calls the Facebook API — run it in the
            // background so the UI thread stays responsive.
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
            {
                e.Result = r_FacebookService.GetRecentPosts(15);
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                m_FlowRecentPosts.Controls.Clear();

                if (e.Error != null)
                {
                    showEmptyPostsMessage("Error loading posts");
                    return;
                }

                List<Post> posts = e.Result as List<Post>;

                if (posts.Count == 0)
                {
                    showEmptyPostsMessage("No posts to display");
                }
                else
                {
                    foreach (Post post in posts)
                    {
                        m_FlowRecentPosts.Controls.Add(createPostCard(post));
                    }
                }
            };

            worker.RunWorkerAsync();
        }

        private void buttonLoadAlbums_Click(object sender, EventArgs e)
        {
            if (ensureLoggedIn())
            {
                loadAlbumsIntoGrid();
            }
        }

        private void loadAlbumsIntoGrid()
        {
            r_LoadedAlbums.Clear();
            m_AlbumBindingList.Clear();
            m_LabelSelectedAlbum.Text = "Loading...";
            m_ButtonLoadAlbums.Enabled = false;

            // Multi-threading: fetch albums + photo counts on a background thread
            // so the UI stays responsive during the (slow) Facebook API calls.
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
            {
                List<Album> albums = r_FacebookService.GetAlbums();
                var results = new List<Tuple<Album, int>>();

                foreach (Album album in albums)
                {
                    int photoCount = r_FacebookService.GetPhotosFromAlbum(album).Count;
                    results.Add(Tuple.Create(album, photoCount));
                }

                e.Result = results;
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                m_ButtonLoadAlbums.Enabled = true;

                if (e.Error != null)
                {
                    m_LabelSelectedAlbum.Text = "Error loading albums";
                    return;
                }

                var results = e.Result as List<Tuple<Album, int>>;

                foreach (Tuple<Album, int> pair in results)
                {
                    r_LoadedAlbums.Add(pair.Item1);

                    // Two-Way Binding: adding to BindingList updates the grid automatically
                    m_AlbumBindingList.Add(new AlbumViewModel
                    {
                        Name = pair.Item1.Name,
                        PhotoCount = pair.Item2
                    });
                }

                m_LabelSelectedAlbum.Text = r_LoadedAlbums.Count == 0
                    ? "No albums found"
                    : $"Found {r_LoadedAlbums.Count} albums — click a row to inspect";
            };

            worker.RunWorkerAsync();
        }

        private void dataGridAlbums_SelectionChanged(object sender, EventArgs e)
        {
            int index = -1;

            if (m_DataGridAlbums.SelectedRows.Count > 0)
            {
                index = m_DataGridAlbums.SelectedRows[0].Index;
            }

            if (index >= 0 && index < r_LoadedAlbums.Count)
            {
                showSelectedAlbum(r_LoadedAlbums[index]);
            }
        }

        private void showSelectedAlbum(Album i_Album)
        {
            m_LabelSelectedAlbum.Text = i_Album.Name != null ? i_Album.Name : "(unnamed)";

            List<Photo> photos = r_FacebookService.GetPhotosFromAlbum(i_Album);
            m_LabelAlbumPhotoCount.Text = $"{photos.Count} photos";

            m_FlowAlbumThumbnails.Controls.Clear();
            int shown = 0;

            foreach (Photo photo in photos)
            {
                if (shown >= 4)
                {
                    break;
                }

                var pic = new PictureBox
                {
                    Size = new Size(60, 60),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    ImageLocation = photo.PictureNormalURL,
                    BackColor = Color.FromArgb(15, 32, 75),
                    Margin = new Padding(2)
                };
                m_FlowAlbumThumbnails.Controls.Add(pic);
                shown++;
            }

            m_ButtonUploadPhoto.Enabled = true;
            m_CurrentSelectedAlbum = i_Album;
        }

        private void buttonUploadPhoto_Click(object sender, EventArgs e)
        {
            if (m_CurrentSelectedAlbum != null)
            {
                tryUploadPhotoToAlbum(m_CurrentSelectedAlbum);
            }
        }

        private void tryUploadPhotoToAlbum(Album i_Album)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select a photo to upload";
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*";
                dialog.FilterIndex = 1;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        r_FacebookService.UploadPhotoToAlbum(i_Album, dialog.FileName);
                        MessageBox.Show("Photo uploaded successfully!", "Upload Success");

                        // Two-Way Data Binding: instead of reloading the entire grid,
                        // update only the affected AlbumViewModel. The BindingList
                        // propagates the change to the DataGridView automatically via
                        // INotifyPropertyChanged — no Rows.Clear() or re-fetch needed.
                        int albumIndex = r_LoadedAlbums.IndexOf(i_Album);

                        if (albumIndex >= 0 && albumIndex < m_AlbumBindingList.Count)
                        {
                            // Proxy cache was invalidated by UploadPhotoToAlbum —
                            // this call fetches the fresh count from the API.
                            m_AlbumBindingList[albumIndex].PhotoCount =
                                r_FacebookService.GetPhotosFromAlbum(i_Album).Count;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Failed to upload photo.\n\nReason: {ex.Message}\n\n(Uploading via API may not be available for all accounts.)",
                            "Upload Failed");
                    }
                }
            }
        }

        private bool ensureLoggedIn()
        {
            bool isLoggedIn = r_FacebookService.LoggedInUser != null;

            if (!isLoggedIn)
            {
                MessageBox.Show("Please login first");
            }

            return isLoggedIn;
        }

        public bool LoginWithFacebook()
        {
            bool success = false;
            LoginResult loginResult = r_FacebookService.Login(k_AppId);

            if (!string.IsNullOrEmpty(loginResult.AccessToken))
            {
                afterLogin();
                displayUserInfo();
                success = true;
            }
            else
            {
                string errorMessage = string.IsNullOrEmpty(loginResult.ErrorMessage)
                    ? "User cancelled login."
                    : loginResult.ErrorMessage;

                MessageBox.Show(errorMessage, "Login Failed");
            }

            return success;
        }

        public bool LoginWithDesigAccount()
        {
            bool success = false;

            try
            {
                LoginResult loginResult = r_FacebookService.ConnectWithToken(k_DesignPatternsToken);

                if (loginResult != null && !string.IsNullOrEmpty(loginResult.AccessToken))
                {
                    afterLogin();
                    displayUserInfo();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Failed");
            }

            return success;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            LoginResult loginResult = r_FacebookService.Login(k_AppId);

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
            User user = r_FacebookService.LoggedInUser;

            if (user != null)
            {
                labelName.Text = user.Name;
                labelBirthday.Text = $"Born:   {user.Birthday}";

                listBoxAlbums.Items.Clear();
                List<Album> albums = r_FacebookService.GetAlbums();

                if (albums.Count == 0)
                {
                    listBoxAlbums.Items.Add("No albums found");
                }
                else
                {
                    foreach (Album album in albums)
                    {
                        listBoxAlbums.Items.Add(album.Name);
                    }
                }

                loadFriends();
                loadRecentPosts();
            }
        }

        private void loadFriends()
        {
            m_FlowFriends.Controls.Clear();
            r_AllFriends.Clear();
            m_LabelFriendsCount.Text = "Loading...";
            showFlowMessage("Loading friends...", Color.FromArgb(160, 200, 255));

            // Multi-threading: GetFriends() hits the Facebook API — run it in the
            // background so the UI thread stays responsive.
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
            {
                e.Result = r_FacebookService.GetFriends();
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    m_LabelFriendsCount.Text = "Error loading friends";
                    return;
                }

                List<User> friends = e.Result as List<User>;
                r_AllFriends.AddRange(friends);
                m_LabelFriendsCount.Text = $"{r_AllFriends.Count} friends";

                m_FlowFriends.Controls.Clear();

                if (r_AllFriends.Count == 0)
                {
                    showFlowMessage("No friends available (Facebook API restricts this)", Color.FromArgb(160, 200, 255));
                }
                else
                {
                    renderFriendCards(r_AllFriends);
                }
            };

            worker.RunWorkerAsync();
        }

        private void renderFriendCards(IEnumerable<User> i_Friends)
        {
            m_FlowFriends.SuspendLayout();
            m_FlowFriends.Controls.Clear();

            foreach (User friend in i_Friends)
            {
                m_FlowFriends.Controls.Add(createFriendCard(friend));
            }

            if (m_FlowFriends.Controls.Count == 0)
            {
                showFlowMessage("No friends match your search", Color.FromArgb(160, 200, 255));
            }

            m_FlowFriends.ResumeLayout();
        }

        private void friendCard_Click(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            User friend = ctrl != null ? ctrl.Tag as User : null;

            if (friend != null)
            {
                showFriendDetails(friend);
            }
        }

        private void showFriendDetails(User i_Friend)
        {
            m_PictureBoxFriendDetail.ImageLocation = i_Friend.PictureNormalURL;
            m_LabelFriendDetailName.Text = i_Friend.Name;

            var info = new StringBuilder();

            if (!string.IsNullOrEmpty(i_Friend.Birthday))
            {
                info.AppendLine($"Birthday: {i_Friend.Birthday}");
            }

            if (i_Friend.Location != null && !string.IsNullOrEmpty(i_Friend.Location.Name))
            {
                info.AppendLine($"Location: {i_Friend.Location.Name}");
            }

            if (i_Friend.Hometown != null && !string.IsNullOrEmpty(i_Friend.Hometown.Name))
            {
                info.AppendLine($"Hometown: {i_Friend.Hometown.Name}");
            }

            m_LabelFriendDetailInfo.Text = info.Length > 0 ? info.ToString() : "No additional info available";
        }

        private void textBoxSearchFriend_TextChanged(object sender, EventArgs e)
        {
            string query = m_TextBoxSearchFriend.Text.Trim().ToLower();
            IEnumerable<User> filtered;

            if (string.IsNullOrEmpty(query))
            {
                filtered = r_AllFriends;
            }
            else
            {
                filtered = r_AllFriends.Where(f => f.Name != null && f.Name.ToLower().Contains(query));
            }

            renderFriendCards(filtered);
        }

        private void afterLogin()
        {
            User user = r_FacebookService.LoggedInUser;

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

            // Friends
            m_FlowFriends.Controls.Clear();
            r_AllFriends.Clear();
            m_LabelFriendsCount.Text = "";
            m_TextBoxSearchFriend.Text = "";
            m_PictureBoxFriendDetail.ImageLocation = null;
            m_LabelFriendDetailName.Text = "Select a friend to see details";
            m_LabelFriendDetailInfo.Text = "";

            // Albums tab
            labelAlbumName.Text = "";
            labelAlbumsCount.Text = "";

            // Photos Analyst tab (new Strategy UI)
            m_PictureBoxPhotoResult.ImageLocation = null;
            m_LabelPhotoResultTitle.Text = "Results will appear here";
            m_LabelPhotoResultDetails.Text = "";
            m_ComboPhotoStrategy.SelectedIndex = 0;

            // Post Composer tab
            m_TextBoxPostContent.Text = "";
            m_FlowRecentPosts.Controls.Clear();
            m_LabelCharCount.Text = $"0 / {k_MaxPostLength} characters";
            m_LabelCharCount.ForeColor = Color.FromArgb(160, 200, 255);

            // Album Analyst tab
            r_LoadedAlbums.Clear();
            m_AlbumBindingList.Clear(); // grid updates automatically via Two-Way Binding
            m_LabelSelectedAlbum.Text = "Select an album from the list";
            m_LabelAlbumPhotoCount.Text = "";
            m_FlowAlbumThumbnails.Controls.Clear();
            m_ButtonUploadPhoto.Enabled = false;
            m_CurrentSelectedAlbum = null;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            r_FacebookService.Logout();
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            // When the user closes the main window (X button), exit the whole
            // application. Logout path never reaches here because it Hide()s
            // the form instead of closing it.
            Application.Exit();
        }

        private void buttonConnectAsDesig_Click_1(object sender, EventArgs e)
        {
        }

        private void buttonMostPhotosAlbum_Click(object sender, EventArgs e)
        {
            if (ensureLoggedIn())
            {
                User user = r_FacebookService.LoggedInUser;
                IFacebookFeature<Album> feature = new AlbumWithMostPhotosFeature(r_FacebookService);
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
        }

        private void buttonOldestPhoto_Click_1(object sender, EventArgs e)
        {
            if (ensureLoggedIn())
            {
                User user = r_FacebookService.LoggedInUser;
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
        }

        private void buttonMostLikedPhoto_Click_1(object sender, EventArgs e)
        {
            if (ensureLoggedIn())
            {
                User user = r_FacebookService.LoggedInUser;
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
        }

        private void buttonMostCommentedPhoto_Click_1(object sender, EventArgs e)
        {
            if (ensureLoggedIn())
            {
                User user = r_FacebookService.LoggedInUser;
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
        }

        private void buttonCountAlbums_Click(object sender, EventArgs e)
        {
            if (ensureLoggedIn())
            {
                User user = r_FacebookService.LoggedInUser;
                IFacebookFeature<int> feature = new CountAlbumsFeature(r_FacebookService);
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
}
