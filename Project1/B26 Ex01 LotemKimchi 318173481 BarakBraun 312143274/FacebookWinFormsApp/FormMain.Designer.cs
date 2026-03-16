namespace BasicFacebookFeatures
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabPageAnalytics = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.labelName = new System.Windows.Forms.Label();
            this.labelBirthday = new System.Windows.Forms.Label();
            this.pictureBoxProfile = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonConnectAsDesig = new System.Windows.Forms.Button();
            this.textBoxAppID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOldestPhoto = new System.Windows.Forms.Button();
            this.pictureBoxOldestPhoto = new System.Windows.Forms.PictureBox();
            this.labelOldestPhotoDate = new System.Windows.Forms.Label();
            this.labelAlbumName = new System.Windows.Forms.Label();
            this.buttonMostPhotosAlbum = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxAlbums = new System.Windows.Forms.ListBox();
            this.labelAlbumList = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonMostLikedPhoto = new System.Windows.Forms.Button();
            this.pictureBoxMostLikePhoto = new System.Windows.Forms.PictureBox();
            this.buttonMostCommentedPhoto = new System.Windows.Forms.Button();
            this.pictureBoxMostCommentedPhoto = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonCountAlbums = new System.Windows.Forms.Button();
            this.labelAlbumsCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();

            this.tabPageAnalytics.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOldestPhoto)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMostLikePhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMostCommentedPhoto)).BeginInit();
            this.SuspendLayout();

            // ──────────────────────────────────────────────
            // tabPageAnalytics (TabControl)
            // ──────────────────────────────────────────────
            this.tabPageAnalytics.Controls.Add(this.tabPage1);
            this.tabPageAnalytics.Controls.Add(this.tabPage2);
            this.tabPageAnalytics.Controls.Add(this.tabPage3);
            this.tabPageAnalytics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageAnalytics.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageAnalytics.Location = new System.Drawing.Point(0, 0);
            this.tabPageAnalytics.Name = "tabPageAnalytics";
            this.tabPageAnalytics.SelectedIndex = 0;
            this.tabPageAnalytics.Size = new System.Drawing.Size(920, 620);
            this.tabPageAnalytics.TabIndex = 54;

            // ──────────────────────────────────────────────
            // tabPage1 — Profile
            // ──────────────────────────────────────────────
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.tableLayoutPanel4);
            this.tabPage1.Controls.Add(this.buttonConnectAsDesig);
            this.tabPage1.Controls.Add(this.textBoxAppID);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.buttonLogout);
            this.tabPage1.Controls.Add(this.buttonLogin);
            this.tabPage1.Location = new System.Drawing.Point(4, 35);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(912, 581);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "  Profile  ";

            // buttonLogin
            this.buttonLogin.BackColor = System.Drawing.Color.FromArgb(24, 119, 242);
            this.buttonLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogin.FlatAppearance.BorderSize = 0;
            this.buttonLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(16, 102, 214);
            this.buttonLogin.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogin.ForeColor = System.Drawing.Color.White;
            this.buttonLogin.Location = new System.Drawing.Point(25, 25);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(265, 52);
            this.buttonLogin.TabIndex = 36;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);

            // buttonConnectAsDesig
            this.buttonConnectAsDesig.BackColor = System.Drawing.Color.White;
            this.buttonConnectAsDesig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConnectAsDesig.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(24, 119, 242);
            this.buttonConnectAsDesig.FlatAppearance.BorderSize = 2;
            this.buttonConnectAsDesig.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(232, 240, 254);
            this.buttonConnectAsDesig.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnectAsDesig.ForeColor = System.Drawing.Color.FromArgb(24, 119, 242);
            this.buttonConnectAsDesig.Location = new System.Drawing.Point(25, 90);
            this.buttonConnectAsDesig.Margin = new System.Windows.Forms.Padding(4);
            this.buttonConnectAsDesig.Name = "buttonConnectAsDesig";
            this.buttonConnectAsDesig.Size = new System.Drawing.Size(265, 52);
            this.buttonConnectAsDesig.TabIndex = 56;
            this.buttonConnectAsDesig.Text = "Connect As Design Patterns";
            this.buttonConnectAsDesig.UseVisualStyleBackColor = false;
            this.buttonConnectAsDesig.Click += new System.EventHandler(this.buttonConnectAsDesig_Click_1);

            // buttonLogout
            this.buttonLogout.BackColor = System.Drawing.Color.FromArgb(101, 103, 107);
            this.buttonLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogout.FlatAppearance.BorderSize = 0;
            this.buttonLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(75, 77, 82);
            this.buttonLogout.Enabled = false;
            this.buttonLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogout.ForeColor = System.Drawing.Color.White;
            this.buttonLogout.Location = new System.Drawing.Point(25, 155);
            this.buttonLogout.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(265, 52);
            this.buttonLogout.TabIndex = 52;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = false;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);

            // label1 (AppID instructions)
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(101, 103, 107);
            this.label1.Location = new System.Drawing.Point(325, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 80);
            this.label1.TabIndex = 53;
            this.label1.Text = "This is the AppID of \"Design Patterns App 2.4\".\r\nThe grader will use it to test y" +
    "our app.\r\nType here your own AppID to test it:\r\n";

            // textBoxAppID
            this.textBoxAppID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAppID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAppID.Location = new System.Drawing.Point(325, 120);
            this.textBoxAppID.Name = "textBoxAppID";
            this.textBoxAppID.Size = new System.Drawing.Size(460, 34);
            this.textBoxAppID.TabIndex = 54;
            this.textBoxAppID.Text = "1450160541956417";

            // tableLayoutPanel4 (user profile card)
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.labelName, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.pictureBoxProfile, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.pictureBox1, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.labelBirthday, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(25, 245);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.Padding = new System.Windows.Forms.Padding(8);
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(860, 200);
            this.tableLayoutPanel4.TabIndex = 74;

            // labelName
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(28, 30, 33);
            this.labelName.Location = new System.Drawing.Point(3, 5);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(120, 32);
            this.labelName.TabIndex = 57;
            this.labelName.Text = "labelName";

            // labelBirthday
            this.labelBirthday.AutoSize = true;
            this.labelBirthday.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBirthday.ForeColor = System.Drawing.Color.FromArgb(101, 103, 107);
            this.labelBirthday.Location = new System.Drawing.Point(3, 8);
            this.labelBirthday.Name = "labelBirthday";
            this.labelBirthday.Size = new System.Drawing.Size(120, 24);
            this.labelBirthday.TabIndex = 59;
            this.labelBirthday.Text = "labelBirthday";

            // pictureBoxProfile
            this.pictureBoxProfile.Location = new System.Drawing.Point(8, 5);
            this.pictureBoxProfile.Name = "pictureBoxProfile";
            this.pictureBoxProfile.Size = new System.Drawing.Size(130, 130);
            this.pictureBoxProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxProfile.TabIndex = 55;
            this.pictureBoxProfile.TabStop = false;

            // pictureBox1
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(330, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 58;
            this.pictureBox1.TabStop = false;

            // ──────────────────────────────────────────────
            // tabPage2 — Albums & Photos
            // ──────────────────────────────────────────────
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 35);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(912, 581);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "  Albums & Photos  ";

            // tableLayoutPanel1 (album list section)
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.listBoxAlbums, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelAlbumList, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(870, 235);
            this.tableLayoutPanel1.TabIndex = 79;

            // labelAlbumList
            this.labelAlbumList.AutoEllipsis = true;
            this.labelAlbumList.AutoSize = true;
            this.labelAlbumList.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAlbumList.ForeColor = System.Drawing.Color.FromArgb(28, 30, 33);
            this.labelAlbumList.Location = new System.Drawing.Point(3, 3);
            this.labelAlbumList.Name = "labelAlbumList";
            this.labelAlbumList.Size = new System.Drawing.Size(120, 30);
            this.labelAlbumList.TabIndex = 73;
            this.labelAlbumList.Text = "Albums List";

            // listBoxAlbums
            this.listBoxAlbums.BackColor = System.Drawing.Color.White;
            this.listBoxAlbums.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxAlbums.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxAlbums.FormattingEnabled = true;
            this.listBoxAlbums.ItemHeight = 24;
            this.listBoxAlbums.Location = new System.Drawing.Point(3, 41);
            this.listBoxAlbums.Name = "listBoxAlbums";
            this.listBoxAlbums.Size = new System.Drawing.Size(864, 190);
            this.listBoxAlbums.TabIndex = 72;

            // tableLayoutPanel2 (oldest photo + most photos album)
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.buttonOldestPhoto, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pictureBoxOldestPhoto, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelAlbumName, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelOldestPhotoDate, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonMostPhotosAlbum, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(20, 265);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(8);
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(870, 290);
            this.tableLayoutPanel2.TabIndex = 80;

            // buttonOldestPhoto
            this.buttonOldestPhoto.BackColor = System.Drawing.Color.FromArgb(24, 119, 242);
            this.buttonOldestPhoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOldestPhoto.FlatAppearance.BorderSize = 0;
            this.buttonOldestPhoto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(16, 102, 214);
            this.buttonOldestPhoto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOldestPhoto.ForeColor = System.Drawing.Color.White;
            this.buttonOldestPhoto.Location = new System.Drawing.Point(11, 11);
            this.buttonOldestPhoto.Name = "buttonOldestPhoto";
            this.buttonOldestPhoto.Size = new System.Drawing.Size(250, 52);
            this.buttonOldestPhoto.TabIndex = 74;
            this.buttonOldestPhoto.Text = "Oldest Photo";
            this.buttonOldestPhoto.UseVisualStyleBackColor = false;
            this.buttonOldestPhoto.Click += new System.EventHandler(this.buttonOldestPhoto_Click_1);

            // buttonMostPhotosAlbum
            this.buttonMostPhotosAlbum.BackColor = System.Drawing.Color.FromArgb(24, 119, 242);
            this.buttonMostPhotosAlbum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMostPhotosAlbum.FlatAppearance.BorderSize = 0;
            this.buttonMostPhotosAlbum.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(16, 102, 214);
            this.buttonMostPhotosAlbum.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMostPhotosAlbum.ForeColor = System.Drawing.Color.White;
            this.buttonMostPhotosAlbum.Location = new System.Drawing.Point(11, 11);
            this.buttonMostPhotosAlbum.Name = "buttonMostPhotosAlbum";
            this.buttonMostPhotosAlbum.Size = new System.Drawing.Size(250, 52);
            this.buttonMostPhotosAlbum.TabIndex = 76;
            this.buttonMostPhotosAlbum.Text = "Album With Most Photos";
            this.buttonMostPhotosAlbum.UseVisualStyleBackColor = false;
            this.buttonMostPhotosAlbum.Click += new System.EventHandler(this.buttonMostPhotosAlbum_Click);

            // labelOldestPhotoDate
            this.labelOldestPhotoDate.AutoSize = true;
            this.labelOldestPhotoDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOldestPhotoDate.ForeColor = System.Drawing.Color.FromArgb(101, 103, 107);
            this.labelOldestPhotoDate.Location = new System.Drawing.Point(11, 5);
            this.labelOldestPhotoDate.Name = "labelOldestPhotoDate";
            this.labelOldestPhotoDate.Size = new System.Drawing.Size(165, 24);
            this.labelOldestPhotoDate.TabIndex = 78;
            this.labelOldestPhotoDate.Text = "Oldest Photo Date";

            // labelAlbumName
            this.labelAlbumName.AutoSize = true;
            this.labelAlbumName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAlbumName.ForeColor = System.Drawing.Color.FromArgb(28, 30, 33);
            this.labelAlbumName.Location = new System.Drawing.Point(11, 5);
            this.labelAlbumName.Name = "labelAlbumName";
            this.labelAlbumName.Size = new System.Drawing.Size(120, 28);
            this.labelAlbumName.TabIndex = 77;
            this.labelAlbumName.Text = "Album Name";

            // pictureBoxOldestPhoto
            this.pictureBoxOldestPhoto.Location = new System.Drawing.Point(11, 5);
            this.pictureBoxOldestPhoto.Name = "pictureBoxOldestPhoto";
            this.pictureBoxOldestPhoto.Size = new System.Drawing.Size(210, 155);
            this.pictureBoxOldestPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOldestPhoto.TabIndex = 75;
            this.pictureBoxOldestPhoto.TabStop = false;

            // ──────────────────────────────────────────────
            // tabPage3 — Analytics
            // ──────────────────────────────────────────────
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.tableLayoutPanel3);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(4, 35);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(912, 581);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "  Analytics  ";

            // label4 (hidden — was a duplicate label, kept for compatibility)
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 0);
            this.label4.TabIndex = 75;
            this.label4.Text = "";
            this.label4.Visible = false;

            // tableLayoutPanel3
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonMostLikedPhoto, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.pictureBoxMostLikePhoto, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.buttonMostCommentedPhoto, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.pictureBoxMostCommentedPhoto, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.buttonCountAlbums, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.labelAlbumsCount, 1, 4);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(20, 15);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(870, 526);
            this.tableLayoutPanel3.TabIndex = 0;
            this.tableLayoutPanel3.SetColumnSpan(this.label2, 2);
            this.tableLayoutPanel3.SetColumnSpan(this.label3, 2);

            // label2 — Photo Analytics header
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(28, 30, 33);
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "Photo Analytics";

            // buttonMostLikedPhoto
            this.buttonMostLikedPhoto.BackColor = System.Drawing.Color.FromArgb(24, 119, 242);
            this.buttonMostLikedPhoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMostLikedPhoto.FlatAppearance.BorderSize = 0;
            this.buttonMostLikedPhoto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(16, 102, 214);
            this.buttonMostLikedPhoto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMostLikedPhoto.ForeColor = System.Drawing.Color.White;
            this.buttonMostLikedPhoto.Location = new System.Drawing.Point(3, 15);
            this.buttonMostLikedPhoto.Name = "buttonMostLikedPhoto";
            this.buttonMostLikedPhoto.Size = new System.Drawing.Size(240, 55);
            this.buttonMostLikedPhoto.TabIndex = 68;
            this.buttonMostLikedPhoto.Text = "Most Liked Photo";
            this.buttonMostLikedPhoto.UseVisualStyleBackColor = false;
            this.buttonMostLikedPhoto.Click += new System.EventHandler(this.buttonMostLikedPhoto_Click_1);

            // pictureBoxMostLikePhoto
            this.pictureBoxMostLikePhoto.Location = new System.Drawing.Point(3, 8);
            this.pictureBoxMostLikePhoto.Name = "pictureBoxMostLikePhoto";
            this.pictureBoxMostLikePhoto.Size = new System.Drawing.Size(240, 158);
            this.pictureBoxMostLikePhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxMostLikePhoto.TabIndex = 69;
            this.pictureBoxMostLikePhoto.TabStop = false;

            // buttonMostCommentedPhoto
            this.buttonMostCommentedPhoto.BackColor = System.Drawing.Color.FromArgb(24, 119, 242);
            this.buttonMostCommentedPhoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMostCommentedPhoto.FlatAppearance.BorderSize = 0;
            this.buttonMostCommentedPhoto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(16, 102, 214);
            this.buttonMostCommentedPhoto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMostCommentedPhoto.ForeColor = System.Drawing.Color.White;
            this.buttonMostCommentedPhoto.Location = new System.Drawing.Point(3, 15);
            this.buttonMostCommentedPhoto.Name = "buttonMostCommentedPhoto";
            this.buttonMostCommentedPhoto.Size = new System.Drawing.Size(240, 55);
            this.buttonMostCommentedPhoto.TabIndex = 70;
            this.buttonMostCommentedPhoto.Text = "Most Commented Photo";
            this.buttonMostCommentedPhoto.UseVisualStyleBackColor = false;
            this.buttonMostCommentedPhoto.Click += new System.EventHandler(this.buttonMostCommentedPhoto_Click_1);

            // pictureBoxMostCommentedPhoto
            this.pictureBoxMostCommentedPhoto.Location = new System.Drawing.Point(3, 8);
            this.pictureBoxMostCommentedPhoto.Name = "pictureBoxMostCommentedPhoto";
            this.pictureBoxMostCommentedPhoto.Size = new System.Drawing.Size(240, 158);
            this.pictureBoxMostCommentedPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxMostCommentedPhoto.TabIndex = 71;
            this.pictureBoxMostCommentedPhoto.TabStop = false;

            // label3 — Album Analytics header
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(28, 30, 33);
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 30);
            this.label3.TabIndex = 1;
            this.label3.Text = "Album Analytics";

            // buttonCountAlbums
            this.buttonCountAlbums.BackColor = System.Drawing.Color.FromArgb(24, 119, 242);
            this.buttonCountAlbums.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCountAlbums.FlatAppearance.BorderSize = 0;
            this.buttonCountAlbums.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(16, 102, 214);
            this.buttonCountAlbums.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCountAlbums.ForeColor = System.Drawing.Color.White;
            this.buttonCountAlbums.Location = new System.Drawing.Point(3, 15);
            this.buttonCountAlbums.Name = "buttonCountAlbums";
            this.buttonCountAlbums.Size = new System.Drawing.Size(200, 52);
            this.buttonCountAlbums.TabIndex = 73;
            this.buttonCountAlbums.Text = "Count Albums";
            this.buttonCountAlbums.UseVisualStyleBackColor = false;
            this.buttonCountAlbums.Click += new System.EventHandler(this.buttonCountAlbums_Click);

            // labelAlbumsCount
            this.labelAlbumsCount.AutoSize = true;
            this.labelAlbumsCount.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAlbumsCount.ForeColor = System.Drawing.Color.FromArgb(28, 30, 33);
            this.labelAlbumsCount.Location = new System.Drawing.Point(3, 18);
            this.labelAlbumsCount.Name = "labelAlbumsCount";
            this.labelAlbumsCount.Size = new System.Drawing.Size(145, 30);
            this.labelAlbumsCount.TabIndex = 74;
            this.labelAlbumsCount.Text = "Albums Count:";

            // ──────────────────────────────────────────────
            // FormMain
            // ──────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.ClientSize = new System.Drawing.Size(920, 620);
            this.Controls.Add(this.tabPageAnalytics);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(920, 660);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facebook Analytics";

            this.tabPageAnalytics.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOldestPhoto)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMostLikePhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMostCommentedPhoto)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabPageAnalytics;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelBirthday;
        private System.Windows.Forms.PictureBox pictureBoxProfile;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonConnectAsDesig;
        private System.Windows.Forms.TextBox textBoxAppID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonOldestPhoto;
        private System.Windows.Forms.PictureBox pictureBoxOldestPhoto;
        private System.Windows.Forms.Label labelOldestPhotoDate;
        private System.Windows.Forms.Label labelAlbumName;
        private System.Windows.Forms.Button buttonMostPhotosAlbum;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listBoxAlbums;
        private System.Windows.Forms.Label labelAlbumList;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonMostLikedPhoto;
        private System.Windows.Forms.PictureBox pictureBoxMostLikePhoto;
        private System.Windows.Forms.Button buttonMostCommentedPhoto;
        private System.Windows.Forms.PictureBox pictureBoxMostCommentedPhoto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonCountAlbums;
        private System.Windows.Forms.Label labelAlbumsCount;
        private System.Windows.Forms.Label label4;
    }
}
