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

        // Friends section
        private Label m_LabelFriendsHeader;
        private TextBox m_TextBoxSearchFriend;
        private FlowLayoutPanel m_FlowFriends;
        private Panel m_PanelFriendDetails;
        private PictureBox m_PictureBoxFriendDetail;
        private Label m_LabelFriendDetailName;
        private Label m_LabelFriendDetailInfo;
        private Label m_LabelFriendsCount;
        private List<User> m_AllFriends = new List<User>();

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

        // Smart Photo Gallery section
        private TabPage m_TabPageGallery;
        private ComboBox m_ComboGalleryAlbum;
        private ComboBox m_ComboGalleryYear;
        private NumericUpDown m_NumericMinLikes;
        private ComboBox m_ComboGallerySort;
        private Button m_ButtonApplyGallery;
        private Button m_ButtonLoadGallery;
        private Label m_LabelGalleryStats;
        private FlowLayoutPanel m_FlowGallery;
        private List<GalleryItem> m_GalleryItems = new List<GalleryItem>();
        private bool m_GalleryLoaded;

        private class GalleryItem
        {
            public Photo Photo;
            public string AlbumName;
            public DateTime? CreatedTime;
            public int Year;
            public int Likes;
            public int Comments;
        }

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
            setupGalleryTab();
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
            if (!ensureLoggedIn())
            {
                return;
            }

            User user = m_FacebookManager.LoggedInUser;

            // === Strategy Pattern: choose strategy at runtime based on user selection ===
            IFacebookFeature<Photo> strategy = createPhotoStrategy(m_ComboPhotoStrategy.SelectedItem as string);

            if (strategy == null)
            {
                return;
            }

            m_PictureBoxPhotoResult.ImageLocation = null;
            m_LabelPhotoResultDetails.Text = "Analyzing...";
            m_LabelPhotoResultTitle.Text = m_ComboPhotoStrategy.SelectedItem.ToString();
            Application.DoEvents();

            Photo photo = strategy.Execute(user);

            if (photo == null)
            {
                m_LabelPhotoResultDetails.Text = "No photo found for this strategy.";
                return;
            }

            m_PictureBoxPhotoResult.ImageLocation = photo.PictureNormalURL;

            var details = new StringBuilder();
            details.AppendLine($"Created: {photo.CreatedTime}");
            details.AppendLine();
            details.AppendLine($"Likes:    {safeCount(() => photo.LikedBy?.Count)}");
            details.AppendLine($"Comments: {safeCount(() => photo.Comments?.Count)}");
            details.AppendLine($"Tags:     {safeCount(() => photo.Tags?.Count)}");

            string caption = safeGet(() => photo.Name);
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
            try
            {
                int? value = i_Getter();
                return value.HasValue ? value.Value.ToString() : "N/A";
            }
            catch
            {
                return "N/A";
            }
        }

        private static string safeGet(Func<string> i_Getter)
        {
            try
            {
                return i_Getter();
            }
            catch
            {
                return null;
            }
        }

        private IFacebookFeature<Photo> createPhotoStrategy(string i_StrategyName)
        {
            switch (i_StrategyName)
            {
                case "Most Liked Photo":
                    return new MostLikedPhotoFeature();
                case "Most Commented Photo":
                    return new MostCommentedPhotoFeature();
                case "Oldest Photo":
                    return new OldestPhotoFeature();
                case "Newest Photo":
                    return new NewestPhotoFeature();
                case "Most Tagged Photo":
                    return new MostTaggedPhotoFeature();
                default:
                    return null;
            }
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
            if (!ensureLoggedIn())
            {
                return;
            }

            string content = m_TextBoxPostContent.Text.Trim();

            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show("Post cannot be empty.", "Empty Post");
                return;
            }

            try
            {
                User user = m_FacebookManager.LoggedInUser;
                user.PostStatus(content);

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

            User user = m_FacebookManager.LoggedInUser;
            if (user == null)
            {
                return;
            }

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

        private void setupGalleryTab()
        {
            Color darkBg = Color.FromArgb(20, 40, 90);
            Color tabBg = Color.FromArgb(15, 32, 75);
            Color headerBg = Color.FromArgb(10, 25, 65);
            Color blueBtn = Color.FromArgb(24, 119, 242);
            Color lightBlue = Color.FromArgb(160, 200, 255);

            // --- Tab page ---
            m_TabPageGallery = new TabPage
            {
                Text = "  Gallery",
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
                Text = "Smart Gallery",
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelHeader.Controls.Add(labelHeader);
            m_TabPageGallery.Controls.Add(panelHeader);

            // --- Filter panel ---
            var panelFilters = new Panel
            {
                Location = new Point(20, 75),
                Size = new Size(870, 95),
                BackColor = darkBg,
                BorderStyle = BorderStyle.None
            };
            m_TabPageGallery.Controls.Add(panelFilters);

            // Album filter
            var labelAlbum = new Label
            {
                Text = "Album:",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = lightBlue,
                Location = new Point(15, 12),
                Size = new Size(160, 18),
                BackColor = Color.Transparent
            };
            panelFilters.Controls.Add(labelAlbum);
            m_ComboGalleryAlbum = new ComboBox
            {
                Location = new Point(15, 32),
                Size = new Size(180, 28),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(25, 50, 110),
                ForeColor = Color.White,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            m_ComboGalleryAlbum.Items.Add("All Albums");
            m_ComboGalleryAlbum.SelectedIndex = 0;
            panelFilters.Controls.Add(m_ComboGalleryAlbum);

            // Year filter
            var labelYear = new Label
            {
                Text = "Year:",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = lightBlue,
                Location = new Point(215, 12),
                Size = new Size(100, 18),
                BackColor = Color.Transparent
            };
            panelFilters.Controls.Add(labelYear);
            m_ComboGalleryYear = new ComboBox
            {
                Location = new Point(215, 32),
                Size = new Size(120, 28),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(25, 50, 110),
                ForeColor = Color.White,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            m_ComboGalleryYear.Items.Add("All Years");
            m_ComboGalleryYear.SelectedIndex = 0;
            panelFilters.Controls.Add(m_ComboGalleryYear);

            // Min likes filter
            var labelMinLikes = new Label
            {
                Text = "Min likes:",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = lightBlue,
                Location = new Point(355, 12),
                Size = new Size(100, 18),
                BackColor = Color.Transparent
            };
            panelFilters.Controls.Add(labelMinLikes);
            m_NumericMinLikes = new NumericUpDown
            {
                Location = new Point(355, 32),
                Size = new Size(90, 28),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(25, 50, 110),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Minimum = 0,
                Maximum = 10000
            };
            panelFilters.Controls.Add(m_NumericMinLikes);

            // Sort by
            var labelSort = new Label
            {
                Text = "Sort by:",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = lightBlue,
                Location = new Point(465, 12),
                Size = new Size(150, 18),
                BackColor = Color.Transparent
            };
            panelFilters.Controls.Add(labelSort);
            m_ComboGallerySort = new ComboBox
            {
                Location = new Point(465, 32),
                Size = new Size(180, 28),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(25, 50, 110),
                ForeColor = Color.White,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            m_ComboGallerySort.Items.AddRange(new object[]
            {
                "Newest first",
                "Oldest first",
                "Most liked",
                "Most commented"
            });
            m_ComboGallerySort.SelectedIndex = 0;
            panelFilters.Controls.Add(m_ComboGallerySort);

            // Apply button
            m_ButtonApplyGallery = new Button
            {
                Text = "Apply",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = blueBtn,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(665, 32),
                Size = new Size(90, 28),
                Cursor = Cursors.Hand
            };
            m_ButtonApplyGallery.FlatAppearance.BorderSize = 0;
            m_ButtonApplyGallery.Click += buttonApplyGallery_Click;
            panelFilters.Controls.Add(m_ButtonApplyGallery);

            // Load/Refresh button
            m_ButtonLoadGallery = new Button
            {
                Text = "Load Photos",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(66, 183, 42),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(765, 32),
                Size = new Size(95, 28),
                Cursor = Cursors.Hand
            };
            m_ButtonLoadGallery.FlatAppearance.BorderSize = 0;
            m_ButtonLoadGallery.Click += buttonLoadGallery_Click;
            panelFilters.Controls.Add(m_ButtonLoadGallery);

            // Stats label
            m_LabelGalleryStats = new Label
            {
                Location = new Point(15, 68),
                Size = new Size(845, 22),
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = lightBlue,
                Text = "Click 'Load Photos' to fetch your photos from Facebook",
                BackColor = Color.Transparent
            };
            panelFilters.Controls.Add(m_LabelGalleryStats);

            // Gallery flow
            m_FlowGallery = new FlowLayoutPanel
            {
                Location = new Point(20, 180),
                Size = new Size(870, 450),
                BackColor = darkBg,
                AutoScroll = true,
                BorderStyle = BorderStyle.None,
                Padding = new Padding(10),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };
            m_TabPageGallery.Controls.Add(m_FlowGallery);

            tabPageAnalytics.TabPages.Add(m_TabPageGallery);
        }

        private void buttonLoadGallery_Click(object sender, EventArgs e)
        {
            if (!ensureLoggedIn())
            {
                return;
            }

            loadAllGalleryItems();
        }

        private void loadAllGalleryItems()
        {
            m_GalleryItems.Clear();
            m_FlowGallery.Controls.Clear();

            m_LabelGalleryStats.Text = "Loading photos...";
            Application.DoEvents();

            User user = m_FacebookManager.LoggedInUser;
            if (user == null)
            {
                m_LabelGalleryStats.Text = "Not logged in";
                return;
            }

            try
            {
                foreach (Album album in user.Albums)
                {
                    foreach (Photo photo in album.Photos)
                    {
                        var item = new GalleryItem
                        {
                            Photo = photo,
                            AlbumName = album.Name,
                            CreatedTime = tryGetCreatedTime(photo),
                            Likes = safeCountInt(() => photo.LikedBy?.Count),
                            Comments = safeCountInt(() => photo.Comments?.Count)
                        };
                        item.Year = item.CreatedTime.HasValue ? item.CreatedTime.Value.Year : 0;
                        m_GalleryItems.Add(item);
                    }
                }

                m_GalleryLoaded = true;
                populateAlbumFilter(user);
                populateYearFilter();
                applyGalleryFilters();
            }
            catch (Exception ex)
            {
                m_LabelGalleryStats.Text = "Cannot load photos: " + ex.Message;
            }
        }

        private static DateTime? tryGetCreatedTime(Photo i_Photo)
        {
            try
            {
                return i_Photo.CreatedTime;
            }
            catch
            {
                return null;
            }
        }

        private static int safeCountInt(Func<int?> i_Getter)
        {
            try
            {
                return i_Getter() ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        private void populateAlbumFilter(User i_User)
        {
            m_ComboGalleryAlbum.Items.Clear();
            m_ComboGalleryAlbum.Items.Add("All Albums");

            try
            {
                foreach (Album album in i_User.Albums)
                {
                    m_ComboGalleryAlbum.Items.Add(album.Name);
                }
            }
            catch
            {
                // ignore - at least "All Albums" will still be available
            }

            m_ComboGalleryAlbum.SelectedIndex = 0;
        }

        private void populateYearFilter()
        {
            m_ComboGalleryYear.Items.Clear();
            m_ComboGalleryYear.Items.Add("All Years");

            var years = m_GalleryItems
                .Where(i => i.Year > 0)
                .Select(i => i.Year)
                .Distinct()
                .OrderByDescending(y => y);

            foreach (int year in years)
            {
                m_ComboGalleryYear.Items.Add(year.ToString());
            }

            m_ComboGalleryYear.SelectedIndex = 0;
        }

        private void buttonApplyGallery_Click(object sender, EventArgs e)
        {
            if (!m_GalleryLoaded)
            {
                MessageBox.Show("Please load photos first.", "Gallery empty");
                return;
            }

            applyGalleryFilters();
        }

        private void applyGalleryFilters()
        {
            // === Filtering (multi-criteria LINQ chain) ===
            IEnumerable<GalleryItem> filtered = m_GalleryItems;

            string albumFilter = m_ComboGalleryAlbum.SelectedItem as string;
            if (!string.IsNullOrEmpty(albumFilter) && albumFilter != "All Albums")
            {
                filtered = filtered.Where(i => i.AlbumName == albumFilter);
            }

            string yearFilter = m_ComboGalleryYear.SelectedItem as string;
            if (!string.IsNullOrEmpty(yearFilter) && yearFilter != "All Years")
            {
                int year = int.Parse(yearFilter);
                filtered = filtered.Where(i => i.Year == year);
            }

            int minLikes = (int)m_NumericMinLikes.Value;
            if (minLikes > 0)
            {
                filtered = filtered.Where(i => i.Likes >= minLikes);
            }

            // === Sorting ===
            string sortBy = m_ComboGallerySort.SelectedItem as string;
            switch (sortBy)
            {
                case "Newest first":
                    filtered = filtered.OrderByDescending(i => i.CreatedTime ?? DateTime.MinValue);
                    break;
                case "Oldest first":
                    filtered = filtered.OrderBy(i => i.CreatedTime ?? DateTime.MaxValue);
                    break;
                case "Most liked":
                    filtered = filtered.OrderByDescending(i => i.Likes);
                    break;
                case "Most commented":
                    filtered = filtered.OrderByDescending(i => i.Comments);
                    break;
            }

            var result = filtered.ToList();
            renderGallery(result);
            updateGalleryStats(result);
        }

        private void renderGallery(List<GalleryItem> i_Items)
        {
            m_FlowGallery.SuspendLayout();
            m_FlowGallery.Controls.Clear();

            foreach (GalleryItem item in i_Items)
            {
                m_FlowGallery.Controls.Add(createGalleryCard(item));
            }

            if (i_Items.Count == 0)
            {
                var label = new Label
                {
                    Text = "No photos match your filters",
                    ForeColor = Color.FromArgb(160, 200, 255),
                    Font = new Font("Segoe UI", 10, FontStyle.Italic),
                    AutoSize = true,
                    Margin = new Padding(12, 20, 0, 0)
                };
                m_FlowGallery.Controls.Add(label);
            }

            m_FlowGallery.ResumeLayout();
        }

        private void updateGalleryStats(List<GalleryItem> i_Items)
        {
            if (i_Items.Count == 0)
            {
                m_LabelGalleryStats.Text = $"Showing 0 of {m_GalleryItems.Count} photos";
                return;
            }

            double avgLikes = i_Items.Average(i => i.Likes);
            double avgComments = i_Items.Average(i => i.Comments);
            int totalLikes = i_Items.Sum(i => i.Likes);

            m_LabelGalleryStats.Text =
                $"Showing {i_Items.Count} of {m_GalleryItems.Count} photos   |   " +
                $"Avg likes: {avgLikes:F1}   |   " +
                $"Avg comments: {avgComments:F1}   |   " +
                $"Total likes: {totalLikes}";
        }

        private Panel createGalleryCard(GalleryItem i_Item)
        {
            var card = new Panel
            {
                Size = new Size(130, 165),
                BackColor = Color.FromArgb(25, 50, 110),
                Margin = new Padding(6),
                Cursor = Cursors.Hand,
                Tag = i_Item
            };

            var pic = new PictureBox
            {
                Size = new Size(118, 118),
                Location = new Point(6, 6),
                SizeMode = PictureBoxSizeMode.Zoom,
                ImageLocation = i_Item.Photo.PictureNormalURL,
                BackColor = Color.FromArgb(15, 32, 75),
                Cursor = Cursors.Hand,
                Tag = i_Item
            };

            var labelStats = new Label
            {
                Text = $"♥ {i_Item.Likes}   💬 {i_Item.Comments}",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(0, 128),
                Size = new Size(130, 18),
                TextAlign = ContentAlignment.TopCenter,
                Cursor = Cursors.Hand,
                Tag = i_Item
            };

            var labelAlbum = new Label
            {
                Text = truncate(i_Item.AlbumName, 18),
                Font = new Font("Segoe UI", 7, FontStyle.Italic),
                ForeColor = Color.FromArgb(160, 200, 255),
                Location = new Point(0, 146),
                Size = new Size(130, 16),
                TextAlign = ContentAlignment.TopCenter,
                Cursor = Cursors.Hand,
                Tag = i_Item
            };

            card.Controls.Add(pic);
            card.Controls.Add(labelStats);
            card.Controls.Add(labelAlbum);

            card.Click += galleryCard_Click;
            pic.Click += galleryCard_Click;
            labelStats.Click += galleryCard_Click;
            labelAlbum.Click += galleryCard_Click;

            return card;
        }

        private static string truncate(string i_Text, int i_MaxLen)
        {
            if (string.IsNullOrEmpty(i_Text))
            {
                return "";
            }

            return i_Text.Length <= i_MaxLen ? i_Text : i_Text.Substring(0, i_MaxLen - 1) + "…";
        }

        private void galleryCard_Click(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            if (ctrl == null)
            {
                return;
            }

            GalleryItem item = ctrl.Tag as GalleryItem;
            if (item == null)
            {
                return;
            }

            showGalleryDetail(item);
        }

        private void showGalleryDetail(GalleryItem i_Item)
        {
            var dialog = new Form
            {
                Text = "Photo Details",
                Size = new Size(780, 640),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.FromArgb(15, 32, 75),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var pic = new PictureBox
            {
                Location = new Point(20, 20),
                Size = new Size(500, 550),
                SizeMode = PictureBoxSizeMode.Zoom,
                ImageLocation = i_Item.Photo.PictureNormalURL,
                BackColor = Color.FromArgb(20, 40, 90)
            };
            dialog.Controls.Add(pic);

            var labelTitle = new Label
            {
                Text = "Photo Details",
                Location = new Point(540, 20),
                Size = new Size(210, 28),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            dialog.Controls.Add(labelTitle);

            var details = new StringBuilder();
            details.AppendLine($"Album:");
            details.AppendLine($"  {i_Item.AlbumName}");
            details.AppendLine();
            details.AppendLine($"Created:");
            details.AppendLine($"  {(i_Item.CreatedTime?.ToString("MMM dd, yyyy HH:mm") ?? "Unknown")}");
            details.AppendLine();
            details.AppendLine($"Likes:    {i_Item.Likes}");
            details.AppendLine($"Comments: {i_Item.Comments}");

            string caption = safeGet(() => i_Item.Photo.Name);
            if (!string.IsNullOrEmpty(caption))
            {
                details.AppendLine();
                details.AppendLine("Caption:");
                details.AppendLine($"  {(caption.Length > 100 ? caption.Substring(0, 100) + "..." : caption)}");
            }

            var labelDetails = new Label
            {
                Text = details.ToString(),
                Location = new Point(540, 60),
                Size = new Size(220, 480),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(200, 220, 255),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.TopLeft
            };
            dialog.Controls.Add(labelDetails);

            var closeBtn = new Button
            {
                Text = "Close",
                Location = new Point(600, 560),
                Size = new Size(100, 32),
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                DialogResult = DialogResult.OK
            };
            closeBtn.FlatAppearance.BorderSize = 0;
            dialog.Controls.Add(closeBtn);
            dialog.AcceptButton = closeBtn;

            dialog.ShowDialog(this);
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
            m_AllFriends.Clear();

            User user = m_FacebookManager.LoggedInUser;
            if (user == null)
            {
                return;
            }

            try
            {
                foreach (User friend in user.Friends)
                {
                    m_AllFriends.Add(friend);
                }

                m_LabelFriendsCount.Text = $"{m_AllFriends.Count} friends";

                if (m_AllFriends.Count == 0)
                {
                    showFlowMessage("No friends available (Facebook API restricts this)", Color.FromArgb(160, 200, 255));
                    return;
                }

                renderFriendCards(m_AllFriends);
            }
            catch
            {
                m_LabelFriendsCount.Text = "";
                showFlowMessage("Cannot load friends (permission missing)", Color.FromArgb(220, 120, 120));
            }
        }

        private void showFlowMessage(string text, Color color)
        {
            var label = new Label
            {
                Text = text,
                ForeColor = color,
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

            if (ctrl == null)
            {
                return;
            }

            User friend = ctrl.Tag as User;
            if (friend == null)
            {
                return;
            }

            showFriendDetails(friend);
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

            if (string.IsNullOrEmpty(query))
            {
                renderFriendCards(m_AllFriends);
                return;
            }

            var filtered = m_AllFriends.Where(f => f.Name != null && f.Name.ToLower().Contains(query));
            renderFriendCards(filtered);
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

            // Friends
            m_FlowFriends.Controls.Clear();
            m_AllFriends.Clear();
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

            // Smart Gallery tab
            m_GalleryItems.Clear();
            m_GalleryLoaded = false;
            m_FlowGallery.Controls.Clear();
            m_ComboGalleryAlbum.Items.Clear();
            m_ComboGalleryAlbum.Items.Add("All Albums");
            m_ComboGalleryAlbum.SelectedIndex = 0;
            m_ComboGalleryYear.Items.Clear();
            m_ComboGalleryYear.Items.Add("All Years");
            m_ComboGalleryYear.SelectedIndex = 0;
            m_NumericMinLikes.Value = 0;
            m_ComboGallerySort.SelectedIndex = 0;
            m_LabelGalleryStats.Text = "Click 'Load Photos' to fetch your photos from Facebook";
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
