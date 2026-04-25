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

        private readonly FacebookManager r_FacebookManager = new FacebookManager();

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

        private void setupFriendsSection()
        {
            Color darkBg = Color.FromArgb(20, 40, 90);
            Color lightBlue = Color.FromArgb(160, 200, 255);

            // Header
            m_LabelFriendsHeader = new Label
            {
                Text = "Friends",
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(350, 85),
                Size = new Size(200, 30),
                BackColor = Color.Transparent
            };
            tabPage1.Controls.Add(m_LabelFriendsHeader);

            // Friends count label
            m_LabelFriendsCount = new Label
            {
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = lightBlue,
                Location = new Point(350, 118),
                Size = new Size(300, 20),
                BackColor = Color.Transparent
            };
            tabPage1.Controls.Add(m_LabelFriendsCount);

            // Search box
            m_TextBoxSearchFriend = new TextBox
            {
                Location = new Point(350, 143),
                Size = new Size(540, 30),
                Font = new Font("Segoe UI", 10),
                BackColor = darkBg,
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            m_TextBoxSearchFriend.TextChanged += textBoxSearchFriend_TextChanged;
            tabPage1.Controls.Add(m_TextBoxSearchFriend);

            // Friends list (flow layout)
            m_FlowFriends = new FlowLayoutPanel
            {
                Location = new Point(350, 180),
                Size = new Size(540, 270),
                BackColor = darkBg,
                AutoScroll = true,
                BorderStyle = BorderStyle.None,
                Padding = new Padding(8),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };
            tabPage1.Controls.Add(m_FlowFriends);

            // Friend details panel
            m_PanelFriendDetails = new Panel
            {
                Location = new Point(350, 460),
                Size = new Size(540, 130),
                BackColor = darkBg,
                BorderStyle = BorderStyle.None
            };
            tabPage1.Controls.Add(m_PanelFriendDetails);

            m_PictureBoxFriendDetail = new PictureBox
            {
                Location = new Point(15, 15),
                Size = new Size(100, 100),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(15, 32, 75)
            };
            var detailClip = new System.Drawing.Drawing2D.GraphicsPath();
            detailClip.AddEllipse(0, 0, 100, 100);
            m_PictureBoxFriendDetail.Region = new Region(detailClip);
            m_PanelFriendDetails.Controls.Add(m_PictureBoxFriendDetail);

            m_LabelFriendDetailName = new Label
            {
                Location = new Point(130, 25),
                Size = new Size(395, 30),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.White,
                Text = "Select a friend to see details"
            };
            m_PanelFriendDetails.Controls.Add(m_LabelFriendDetailName);

            m_LabelFriendDetailInfo = new Label
            {
                Location = new Point(130, 60),
                Size = new Size(395, 50),
                Font = new Font("Segoe UI", 10),
                ForeColor = lightBlue
            };
            m_PanelFriendDetails.Controls.Add(m_LabelFriendDetailInfo);
        }

        private void setupPhotosAnalystTab()
        {
            // Hide old hardcoded buttons and panels - we replace them with a proper Strategy chooser
            tableLayoutPanel3.Visible = false;

            Color darkBg = Color.FromArgb(20, 40, 90);
            Color blueBtn = Color.FromArgb(24, 119, 242);
            Color lightBlue = Color.FromArgb(160, 200, 255);

            // Intro label
            var labelIntro = new Label
            {
                Text = "Choose an analysis strategy:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(40, 90),
                Size = new Size(400, 28),
                BackColor = Color.Transparent
            };
            tabPage3.Controls.Add(labelIntro);

            // ComboBox with strategies
            m_ComboPhotoStrategy = new ComboBox
            {
                Location = new Point(40, 125),
                Size = new Size(360, 34),
                Font = new Font("Segoe UI", 10),
                BackColor = darkBg,
                ForeColor = Color.White,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            m_ComboPhotoStrategy.Items.AddRange(new object[]
            {
                "Most Liked Photo",
                "Most Commented Photo",
                "Oldest Photo",
                "Newest Photo",
                "Most Tagged Photo"
            });
            m_ComboPhotoStrategy.SelectedIndex = 0;
            tabPage3.Controls.Add(m_ComboPhotoStrategy);

            // Analyze button
            m_ButtonAnalyzePhoto = new Button
            {
                Text = "Analyze",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = blueBtn,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(420, 124),
                Size = new Size(160, 36),
                Cursor = Cursors.Hand
            };
            m_ButtonAnalyzePhoto.FlatAppearance.BorderSize = 0;
            m_ButtonAnalyzePhoto.FlatAppearance.MouseOverBackColor = Color.FromArgb(16, 102, 214);
            m_ButtonAnalyzePhoto.Click += buttonAnalyzePhoto_Click;
            tabPage3.Controls.Add(m_ButtonAnalyzePhoto);

            // Result panel
            m_PanelPhotoResult = new Panel
            {
                Location = new Point(40, 185),
                Size = new Size(830, 440),
                BackColor = darkBg,
                BorderStyle = BorderStyle.None
            };
            tabPage3.Controls.Add(m_PanelPhotoResult);

            // Result title
            m_LabelPhotoResultTitle = new Label
            {
                Location = new Point(20, 15),
                Size = new Size(790, 30),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.White,
                Text = "Results will appear here",
                TextAlign = ContentAlignment.MiddleLeft
            };
            m_PanelPhotoResult.Controls.Add(m_LabelPhotoResultTitle);

            // Result picture
            m_PictureBoxPhotoResult = new PictureBox
            {
                Location = new Point(20, 55),
                Size = new Size(400, 350),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(15, 32, 75),
                BorderStyle = BorderStyle.None
            };
            m_PanelPhotoResult.Controls.Add(m_PictureBoxPhotoResult);

            // Result details
            m_LabelPhotoResultDetails = new Label
            {
                Location = new Point(445, 55),
                Size = new Size(365, 350),
                Font = new Font("Segoe UI", 11),
                ForeColor = lightBlue,
                TextAlign = ContentAlignment.TopLeft
            };
            m_PanelPhotoResult.Controls.Add(m_LabelPhotoResultDetails);
        }

        private void buttonAnalyzePhoto_Click(object sender, EventArgs e)
        {
            if (ensureLoggedIn())
            {
                // === Strategy Pattern: choose strategy at runtime based on user selection ===
                IFacebookFeature<Photo> strategy = createPhotoStrategy(m_ComboPhotoStrategy.SelectedItem as string);

                if (strategy != null)
                {
                    runPhotoStrategy(strategy);
                }
            }
        }

        private void runPhotoStrategy(IFacebookFeature<Photo> i_Strategy)
        {
            User user = r_FacebookManager.LoggedInUser;

            m_PictureBoxPhotoResult.ImageLocation = null;
            m_LabelPhotoResultDetails.Text = "Analyzing...";
            m_LabelPhotoResultTitle.Text = m_ComboPhotoStrategy.SelectedItem.ToString();
            Application.DoEvents();

            Photo photo = i_Strategy.Execute(user);

            if (photo == null)
            {
                m_LabelPhotoResultDetails.Text = "No photo found for this strategy.";
            }
            else
            {
                displayPhotoResult(photo);
            }
        }

        private void displayPhotoResult(Photo i_Photo)
        {
            m_PictureBoxPhotoResult.ImageLocation = i_Photo.PictureNormalURL;

            StringBuilder details = new StringBuilder();
            details.AppendLine($"Created: {i_Photo.CreatedTime}");
            details.AppendLine();
            details.AppendLine($"Likes:    {safeCount(() => i_Photo.LikedBy?.Count)}");
            details.AppendLine($"Comments: {safeCount(() => i_Photo.Comments?.Count)}");
            details.AppendLine($"Tags:     {safeCount(() => i_Photo.Tags?.Count)}");

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

        private IFacebookFeature<Photo> createPhotoStrategy(string i_StrategyName)
        {
            IFacebookFeature<Photo> strategy = null;

            switch (i_StrategyName)
            {
                case "Most Liked Photo":
                    strategy = new MostLikedPhotoFeature();
                    break;
                case "Most Commented Photo":
                    strategy = new MostCommentedPhotoFeature();
                    break;
                case "Oldest Photo":
                    strategy = new OldestPhotoFeature();
                    break;
                case "Newest Photo":
                    strategy = new NewestPhotoFeature();
                    break;
                case "Most Tagged Photo":
                    strategy = new MostTaggedPhotoFeature();
                    break;
            }

            return strategy;
        }

        private void setupPostTab()
        {
            Color darkBg = Color.FromArgb(20, 40, 90);
            Color tabBg = Color.FromArgb(15, 32, 75);
            Color headerBg = Color.FromArgb(10, 25, 65);
            Color blueBtn = Color.FromArgb(24, 119, 242);
            Color lightBlue = Color.FromArgb(160, 200, 255);

            // --- Tab page ---
            m_TabPagePost = new TabPage
            {
                Text = "  Post",
                BackColor = tabBg
            };

            // --- Header bar ---
            var panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 56,
                BackColor = headerBg,
                Padding = new Padding(18, 0, 0, 0)
            };
            var labelHeader = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Post",
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelHeader.Controls.Add(labelHeader);
            m_TabPagePost.Controls.Add(panelHeader);

            // --- Left: Composer ---
            var labelCompose = new Label
            {
                Text = "What's on your mind?",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 85),
                Size = new Size(400, 30),
                BackColor = Color.Transparent
            };
            m_TabPagePost.Controls.Add(labelCompose);

            m_TextBoxPostContent = new TextBox
            {
                Location = new Point(30, 125),
                Size = new Size(420, 260),
                Multiline = true,
                Font = new Font("Segoe UI", 11),
                BackColor = darkBg,
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                MaxLength = k_MaxPostLength,
                ScrollBars = ScrollBars.Vertical
            };
            m_TextBoxPostContent.TextChanged += textBoxPostContent_TextChanged;
            m_TabPagePost.Controls.Add(m_TextBoxPostContent);

            m_LabelCharCount = new Label
            {
                Location = new Point(30, 395),
                Size = new Size(420, 22),
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = lightBlue,
                Text = $"0 / {k_MaxPostLength} characters",
                TextAlign = ContentAlignment.MiddleRight,
                BackColor = Color.Transparent
            };
            m_TabPagePost.Controls.Add(m_LabelCharCount);

            m_ButtonPost = new Button
            {
                Text = "Post to Facebook",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = blueBtn,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(30, 430),
                Size = new Size(420, 48),
                Cursor = Cursors.Hand
            };
            m_ButtonPost.FlatAppearance.BorderSize = 0;
            m_ButtonPost.FlatAppearance.MouseOverBackColor = Color.FromArgb(16, 102, 214);
            m_ButtonPost.Click += buttonPost_Click;
            m_TabPagePost.Controls.Add(m_ButtonPost);

            // --- Right: Recent posts ---
            var labelRecent = new Label
            {
                Text = "Your Recent Posts",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(485, 85),
                Size = new Size(400, 30),
                BackColor = Color.Transparent
            };
            m_TabPagePost.Controls.Add(labelRecent);

            m_FlowRecentPosts = new FlowLayoutPanel
            {
                Location = new Point(485, 125),
                Size = new Size(405, 500),
                BackColor = darkBg,
                AutoScroll = true,
                BorderStyle = BorderStyle.None,
                Padding = new Padding(8),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };
            m_TabPagePost.Controls.Add(m_FlowRecentPosts);

            tabPageAnalytics.TabPages.Add(m_TabPagePost);
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
                User user = r_FacebookManager.LoggedInUser;
                user.PostStatus(i_Content);

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

            User user = r_FacebookManager.LoggedInUser;

            if (user != null)
            {
                try
                {
                    int shown = 0;
                    foreach (Post post in user.Posts)
                    {
                        m_FlowRecentPosts.Controls.Add(createPostCard(post));
                        shown++;

                        if (shown >= 15)
                        {
                            break;
                        }
                    }

                    if (shown == 0)
                    {
                        showEmptyPostsMessage("No posts to display");
                    }
                }
                catch
                {
                    showEmptyPostsMessage("Cannot load posts (permission missing)");
                }
            }
        }

        private void showEmptyPostsMessage(string i_Text)
        {
            var label = new Label
            {
                Text = i_Text,
                ForeColor = Color.FromArgb(160, 200, 255),
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                AutoSize = true,
                Margin = new Padding(12, 20, 0, 0)
            };
            m_FlowRecentPosts.Controls.Add(label);
        }

        private void setupAlbumAnalystTab()
        {
            Color darkBg = Color.FromArgb(20, 40, 90);
            Color lightBlue = Color.FromArgb(160, 200, 255);
            Color blueBtn = Color.FromArgb(24, 119, 242);
            Color greenBtn = Color.FromArgb(66, 183, 42);

            // Hide old album list (replaced by DataGridView)
            tableLayoutPanel1.Visible = false;

            // Load Albums button
            m_ButtonLoadAlbums = new Button
            {
                Text = "Load Albums",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = blueBtn,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(20, 68),
                Size = new Size(160, 36),
                Cursor = Cursors.Hand
            };
            m_ButtonLoadAlbums.FlatAppearance.BorderSize = 0;
            m_ButtonLoadAlbums.Click += buttonLoadAlbums_Click;
            tabPage2.Controls.Add(m_ButtonLoadAlbums);

            // DataGridView for albums with photo count
            m_DataGridAlbums = new DataGridView
            {
                Location = new Point(20, 112),
                Size = new Size(450, 175),
                BackgroundColor = darkBg,
                GridColor = Color.FromArgb(40, 70, 130),
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.White,
                ColumnHeadersHeight = 28,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            m_DataGridAlbums.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(10, 25, 65);
            m_DataGridAlbums.ColumnHeadersDefaultCellStyle.ForeColor = lightBlue;
            m_DataGridAlbums.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            m_DataGridAlbums.DefaultCellStyle.BackColor = darkBg;
            m_DataGridAlbums.DefaultCellStyle.ForeColor = Color.White;
            m_DataGridAlbums.DefaultCellStyle.SelectionBackColor = Color.FromArgb(24, 119, 242);
            m_DataGridAlbums.DefaultCellStyle.SelectionForeColor = Color.White;
            m_DataGridAlbums.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 50, 110);
            m_DataGridAlbums.Columns.Add("AlbumName", "Album Name");
            m_DataGridAlbums.Columns.Add("PhotoCount", "Photos");
            m_DataGridAlbums.Columns["PhotoCount"].Width = 70;
            m_DataGridAlbums.Columns["PhotoCount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            m_DataGridAlbums.SelectionChanged += dataGridAlbums_SelectionChanged;
            tabPage2.Controls.Add(m_DataGridAlbums);

            // Right panel - selected album details
            var panelAlbumDetail = new Panel
            {
                Location = new Point(490, 68),
                Size = new Size(400, 222),
                BackColor = darkBg
            };
            tabPage2.Controls.Add(panelAlbumDetail);

            var labelDetailTitle = new Label
            {
                Text = "Selected Album",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = lightBlue,
                Location = new Point(12, 12),
                Size = new Size(375, 25),
                BackColor = Color.Transparent
            };
            panelAlbumDetail.Controls.Add(labelDetailTitle);

            m_LabelSelectedAlbum = new Label
            {
                Text = "Select an album from the list",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                Location = new Point(12, 40),
                Size = new Size(375, 25),
                BackColor = Color.Transparent
            };
            panelAlbumDetail.Controls.Add(m_LabelSelectedAlbum);

            m_LabelAlbumPhotoCount = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 9),
                ForeColor = lightBlue,
                Location = new Point(12, 68),
                Size = new Size(375, 22),
                BackColor = Color.Transparent
            };
            panelAlbumDetail.Controls.Add(m_LabelAlbumPhotoCount);

            m_FlowAlbumThumbnails = new FlowLayoutPanel
            {
                Location = new Point(12, 95),
                Size = new Size(375, 70),
                BackColor = Color.FromArgb(15, 32, 75),
                AutoScroll = false,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };
            panelAlbumDetail.Controls.Add(m_FlowAlbumThumbnails);

            m_ButtonUploadPhoto = new Button
            {
                Text = "Upload Photo to Album",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = greenBtn,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(12, 174),
                Size = new Size(220, 36),
                Cursor = Cursors.Hand,
                Enabled = false
            };
            m_ButtonUploadPhoto.FlatAppearance.BorderSize = 0;
            m_ButtonUploadPhoto.Click += buttonUploadPhoto_Click;
            panelAlbumDetail.Controls.Add(m_ButtonUploadPhoto);
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
            m_DataGridAlbums.Rows.Clear();
            m_LabelSelectedAlbum.Text = "Loading...";
            Application.DoEvents();

            User user = r_FacebookManager.LoggedInUser;

            try
            {
                foreach (Album album in user.Albums)
                {
                    r_LoadedAlbums.Add(album);
                    int photoCount = 0;
                    try { photoCount = album.Photos.Count; } catch { }
                    m_DataGridAlbums.Rows.Add(album.Name, photoCount);
                }

                if (r_LoadedAlbums.Count == 0)
                {
                    m_LabelSelectedAlbum.Text = "No albums found";
                }
                else
                {
                    m_LabelSelectedAlbum.Text = $"Found {r_LoadedAlbums.Count} albums — click a row to inspect";
                }
            }
            catch (Exception ex)
            {
                m_LabelSelectedAlbum.Text = "Error loading albums: " + ex.Message;
            }
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
            m_LabelSelectedAlbum.Text = i_Album.Name ?? "(unnamed)";

            int photoCount = 0;
            try { photoCount = i_Album.Photos.Count; } catch { }
            m_LabelAlbumPhotoCount.Text = $"{photoCount} photos";

            m_FlowAlbumThumbnails.Controls.Clear();
            int shown = 0;

            try
            {
                foreach (Photo photo in i_Album.Photos)
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
            }
            catch
            {
                // Cannot load thumbnails
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
                        i_Album.UploadPhoto(dialog.FileName);
                        MessageBox.Show("Photo uploaded successfully!", "Upload Success");
                        loadAlbumsIntoGrid();
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

        private Panel createPostCard(Post i_Post)
        {
            var card = new Panel
            {
                Size = new Size(365, 110),
                BackColor = Color.FromArgb(25, 50, 110),
                Margin = new Padding(3, 3, 3, 8),
                BorderStyle = BorderStyle.None
            };

            string dateText = safeGet(() => i_Post.CreatedTime.HasValue ? i_Post.CreatedTime.Value.ToString("MMM dd, yyyy") : "");
            var labelDate = new Label
            {
                Text = dateText,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.FromArgb(160, 200, 255),
                Location = new Point(10, 8),
                Size = new Size(345, 18),
                BackColor = Color.Transparent
            };
            card.Controls.Add(labelDate);

            string message = safeGet(() => i_Post.Message) ?? safeGet(() => i_Post.Description) ?? "(no text)";
            var labelMessage = new Label
            {
                Text = message.Length > 150 ? message.Substring(0, 147) + "..." : message,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.White,
                Location = new Point(10, 28),
                Size = new Size(345, 55),
                BackColor = Color.Transparent
            };
            card.Controls.Add(labelMessage);

            string likesText = $"Likes: {safeCount(() => i_Post.LikedBy?.Count)}   Comments: {safeCount(() => i_Post.Comments?.Count)}";
            var labelStats = new Label
            {
                Text = likesText,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.FromArgb(160, 200, 255),
                Location = new Point(10, 85),
                Size = new Size(345, 18),
                BackColor = Color.Transparent
            };
            card.Controls.Add(labelStats);

            return card;
        }

        private bool ensureLoggedIn()
        {
            bool isLoggedIn = r_FacebookManager.LoggedInUser != null;

            if (!isLoggedIn)
            {
                MessageBox.Show("Please login first");
            }

            return isLoggedIn;
        }

        public bool LoginWithFacebook()
        {
            bool success = false;
            LoginResult loginResult = r_FacebookManager.Login(k_AppId);

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
                LoginResult loginResult = r_FacebookManager.ConnectWithToken(k_DesignPatternsToken);

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
            LoginResult loginResult = r_FacebookManager.Login(k_AppId);

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
            User user = r_FacebookManager.LoggedInUser;

            if (user != null)
            {
                labelName.Text = user.Name;
                labelBirthday.Text = $"Born:   {user.Birthday}";

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

                loadFriends();
                loadRecentPosts();
            }
        }

        private void loadFriends()
        {
            m_FlowFriends.Controls.Clear();
            r_AllFriends.Clear();

            User user = r_FacebookManager.LoggedInUser;

            if (user != null)
            {
                try
                {
                    foreach (User friend in user.Friends)
                    {
                        r_AllFriends.Add(friend);
                    }

                    m_LabelFriendsCount.Text = $"{r_AllFriends.Count} friends";

                    if (r_AllFriends.Count == 0)
                    {
                        showFlowMessage("No friends available (Facebook API restricts this)", Color.FromArgb(160, 200, 255));
                    }
                    else
                    {
                        renderFriendCards(r_AllFriends);
                    }
                }
                catch
                {
                    m_LabelFriendsCount.Text = "";
                    showFlowMessage("Cannot load friends (permission missing)", Color.FromArgb(220, 120, 120));
                }
            }
        }

        private void showFlowMessage(string i_Text, Color i_Color)
        {
            Label label = new Label
            {
                Text = i_Text,
                ForeColor = i_Color,
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                AutoSize = true,
                Margin = new Padding(12, 20, 0, 0)
            };
            m_FlowFriends.Controls.Add(label);
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

        private Panel createFriendCard(User i_Friend)
        {
            var card = new Panel
            {
                Size = new Size(95, 125),
                BackColor = Color.FromArgb(25, 50, 110),
                Margin = new Padding(6),
                Cursor = Cursors.Hand,
                Tag = i_Friend
            };

            var pic = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(7, 7),
                SizeMode = PictureBoxSizeMode.Zoom,
                ImageLocation = i_Friend.PictureNormalURL,
                BackColor = Color.FromArgb(15, 32, 75),
                Cursor = Cursors.Hand,
                Tag = i_Friend
            };
            var picClip = new System.Drawing.Drawing2D.GraphicsPath();
            picClip.AddEllipse(0, 0, 80, 80);
            pic.Region = new Region(picClip);

            var nameLabel = new Label
            {
                Text = i_Friend.Name,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(0, 90),
                Size = new Size(95, 32),
                TextAlign = ContentAlignment.TopCenter,
                Cursor = Cursors.Hand,
                Tag = i_Friend
            };

            card.Controls.Add(pic);
            card.Controls.Add(nameLabel);

            card.Click += friendCard_Click;
            pic.Click += friendCard_Click;
            nameLabel.Click += friendCard_Click;

            return card;
        }

        private void friendCard_Click(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            User friend = ctrl?.Tag as User;

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
            User user = r_FacebookManager.LoggedInUser;

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
            m_DataGridAlbums.Rows.Clear();
            m_LabelSelectedAlbum.Text = "Select an album from the list";
            m_LabelAlbumPhotoCount.Text = "";
            m_FlowAlbumThumbnails.Controls.Clear();
            m_ButtonUploadPhoto.Enabled = false;
            m_CurrentSelectedAlbum = null;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            r_FacebookManager.Logout();
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
                User user = r_FacebookManager.LoggedInUser;
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
        }

        private void buttonOldestPhoto_Click_1(object sender, EventArgs e)
        {
            if (ensureLoggedIn())
            {
                User user = r_FacebookManager.LoggedInUser;
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
                User user = r_FacebookManager.LoggedInUser;
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
                User user = r_FacebookManager.LoggedInUser;
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
                User user = r_FacebookManager.LoggedInUser;
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
}
