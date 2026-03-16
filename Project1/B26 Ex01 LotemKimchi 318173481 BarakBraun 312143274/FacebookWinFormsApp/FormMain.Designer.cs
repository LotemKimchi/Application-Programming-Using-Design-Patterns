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
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonMostPhotosAlbum = new System.Windows.Forms.Button();
            this.pictureBoxOldestPhoto = new System.Windows.Forms.PictureBox();
            this.buttonOldestPhoto = new System.Windows.Forms.Button();
            this.pictureBoxMostCommentedPhoto = new System.Windows.Forms.PictureBox();
            this.buttonMostCommentedPhoto = new System.Windows.Forms.Button();
            this.pictureBoxMostLikePhoto = new System.Windows.Forms.PictureBox();
            this.buttonMostLikedPhoto = new System.Windows.Forms.Button();
            this.buttonMostLikedFriend = new System.Windows.Forms.Button();
            this.labelAlbumList = new System.Windows.Forms.Label();
            this.listBoxAlbums = new System.Windows.Forms.ListBox();
            this.labelBirthday = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonConnectAsDesig = new System.Windows.Forms.Button();
            this.pictureBoxProfile = new System.Windows.Forms.PictureBox();
            this.textBoxAppID = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonFriendWithLongestName = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOldestPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMostCommentedPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMostLikePhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(18, 17);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(268, 44);
            this.buttonLogin.TabIndex = 36;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonLogout
            // 
            this.buttonLogout.Enabled = false;
            this.buttonLogout.Location = new System.Drawing.Point(18, 121);
            this.buttonLogout.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(268, 43);
            this.buttonLogout.TabIndex = 52;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 72);
            this.label1.TabIndex = 53;
            this.label1.Text = "This is the AppID of \"Design Patterns App 2.4\".\r\nThe grader will use it to test y" +
    "our app.\r\nType here your own AppID to test it:\r\n";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1086, 561);
            this.tabControl1.TabIndex = 54;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonFriendWithLongestName);
            this.tabPage1.Controls.Add(this.buttonMostPhotosAlbum);
            this.tabPage1.Controls.Add(this.pictureBoxOldestPhoto);
            this.tabPage1.Controls.Add(this.buttonOldestPhoto);
            this.tabPage1.Controls.Add(this.pictureBoxMostCommentedPhoto);
            this.tabPage1.Controls.Add(this.buttonMostCommentedPhoto);
            this.tabPage1.Controls.Add(this.pictureBoxMostLikePhoto);
            this.tabPage1.Controls.Add(this.buttonMostLikedPhoto);
            this.tabPage1.Controls.Add(this.buttonMostLikedFriend);
            this.tabPage1.Controls.Add(this.labelAlbumList);
            this.tabPage1.Controls.Add(this.listBoxAlbums);
            this.tabPage1.Controls.Add(this.labelBirthday);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.labelName);
            this.tabPage1.Controls.Add(this.buttonConnectAsDesig);
            this.tabPage1.Controls.Add(this.pictureBoxProfile);
            this.tabPage1.Controls.Add(this.textBoxAppID);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.buttonLogout);
            this.tabPage1.Controls.Add(this.buttonLogin);
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1078, 526);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonMostPhotosAlbum
            // 
            this.buttonMostPhotosAlbum.Location = new System.Drawing.Point(318, 431);
            this.buttonMostPhotosAlbum.Name = "buttonMostPhotosAlbum";
            this.buttonMostPhotosAlbum.Size = new System.Drawing.Size(188, 35);
            this.buttonMostPhotosAlbum.TabIndex = 72;
            this.buttonMostPhotosAlbum.Text = "Most Photos Album";
            this.buttonMostPhotosAlbum.UseVisualStyleBackColor = true;
            this.buttonMostPhotosAlbum.Click += new System.EventHandler(this.buttonMostPhotosAlbum_Click_1);
            // 
            // pictureBoxOldestPhoto
            // 
            this.pictureBoxOldestPhoto.Location = new System.Drawing.Point(867, 256);
            this.pictureBoxOldestPhoto.Name = "pictureBoxOldestPhoto";
            this.pictureBoxOldestPhoto.Size = new System.Drawing.Size(166, 103);
            this.pictureBoxOldestPhoto.TabIndex = 71;
            this.pictureBoxOldestPhoto.TabStop = false;
            // 
            // buttonOldestPhoto
            // 
            this.buttonOldestPhoto.Location = new System.Drawing.Point(867, 196);
            this.buttonOldestPhoto.Name = "buttonOldestPhoto";
            this.buttonOldestPhoto.Size = new System.Drawing.Size(163, 59);
            this.buttonOldestPhoto.TabIndex = 70;
            this.buttonOldestPhoto.Text = "Oldest Photo";
            this.buttonOldestPhoto.UseVisualStyleBackColor = true;
            this.buttonOldestPhoto.Click += new System.EventHandler(this.buttonOldestPhoto_Click);
            // 
            // pictureBoxMostCommentedPhoto
            // 
            this.pictureBoxMostCommentedPhoto.Location = new System.Drawing.Point(621, 256);
            this.pictureBoxMostCommentedPhoto.Name = "pictureBoxMostCommentedPhoto";
            this.pictureBoxMostCommentedPhoto.Size = new System.Drawing.Size(163, 103);
            this.pictureBoxMostCommentedPhoto.TabIndex = 69;
            this.pictureBoxMostCommentedPhoto.TabStop = false;
            this.pictureBoxMostCommentedPhoto.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // buttonMostCommentedPhoto
            // 
            this.buttonMostCommentedPhoto.Location = new System.Drawing.Point(621, 196);
            this.buttonMostCommentedPhoto.Name = "buttonMostCommentedPhoto";
            this.buttonMostCommentedPhoto.Size = new System.Drawing.Size(163, 59);
            this.buttonMostCommentedPhoto.TabIndex = 68;
            this.buttonMostCommentedPhoto.Text = "Most Comment Photo\r\n\r\n\r\n";
            this.buttonMostCommentedPhoto.UseVisualStyleBackColor = true;
            this.buttonMostCommentedPhoto.Click += new System.EventHandler(this.buttonMostCommentedPhoto_Click);
            // 
            // pictureBoxMostLikePhoto
            // 
            this.pictureBoxMostLikePhoto.Location = new System.Drawing.Point(373, 256);
            this.pictureBoxMostLikePhoto.Name = "pictureBoxMostLikePhoto";
            this.pictureBoxMostLikePhoto.Size = new System.Drawing.Size(163, 103);
            this.pictureBoxMostLikePhoto.TabIndex = 67;
            this.pictureBoxMostLikePhoto.TabStop = false;
            // 
            // buttonMostLikedPhoto
            // 
            this.buttonMostLikedPhoto.Location = new System.Drawing.Point(373, 196);
            this.buttonMostLikedPhoto.Name = "buttonMostLikedPhoto";
            this.buttonMostLikedPhoto.Size = new System.Drawing.Size(163, 59);
            this.buttonMostLikedPhoto.TabIndex = 66;
            this.buttonMostLikedPhoto.Text = "Most Liked Photo";
            this.buttonMostLikedPhoto.UseVisualStyleBackColor = true;
            this.buttonMostLikedPhoto.Click += new System.EventHandler(this.buttonMostLikedPhoto_Click);
            // 
            // buttonMostLikedFriend
            // 
            this.buttonMostLikedFriend.Location = new System.Drawing.Point(63, 431);
            this.buttonMostLikedFriend.Name = "buttonMostLikedFriend";
            this.buttonMostLikedFriend.Size = new System.Drawing.Size(188, 35);
            this.buttonMostLikedFriend.TabIndex = 65;
            this.buttonMostLikedFriend.Text = "Most Liked Friend";
            this.buttonMostLikedFriend.UseVisualStyleBackColor = true;
            this.buttonMostLikedFriend.Click += new System.EventHandler(this.buttonMostLikedFriend_Click);
            // 
            // labelAlbumList
            // 
            this.labelAlbumList.AutoEllipsis = true;
            this.labelAlbumList.AutoSize = true;
            this.labelAlbumList.Location = new System.Drawing.Point(110, 196);
            this.labelAlbumList.Name = "labelAlbumList";
            this.labelAlbumList.Size = new System.Drawing.Size(106, 24);
            this.labelAlbumList.TabIndex = 64;
            this.labelAlbumList.Text = "Albums List";
            // 
            // listBoxAlbums
            // 
            this.listBoxAlbums.FormattingEnabled = true;
            this.listBoxAlbums.ItemHeight = 22;
            this.listBoxAlbums.Location = new System.Drawing.Point(18, 223);
            this.listBoxAlbums.Name = "listBoxAlbums";
            this.listBoxAlbums.Size = new System.Drawing.Size(309, 136);
            this.listBoxAlbums.TabIndex = 62;
            // 
            // labelBirthday
            // 
            this.labelBirthday.AutoSize = true;
            this.labelBirthday.Location = new System.Drawing.Point(947, 17);
            this.labelBirthday.Name = "labelBirthday";
            this.labelBirthday.Size = new System.Drawing.Size(117, 24);
            this.labelBirthday.TabIndex = 59;
            this.labelBirthday.Text = "labelBirthday";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(951, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(123, 78);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 58;
            this.pictureBox1.TabStop = false;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(806, 17);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(101, 24);
            this.labelName.TabIndex = 57;
            this.labelName.Text = "labelName";
            this.labelName.Click += new System.EventHandler(this.label2_Click);
            // 
            // buttonConnectAsDesig
            // 
            this.buttonConnectAsDesig.Location = new System.Drawing.Point(18, 69);
            this.buttonConnectAsDesig.Margin = new System.Windows.Forms.Padding(4);
            this.buttonConnectAsDesig.Name = "buttonConnectAsDesig";
            this.buttonConnectAsDesig.Size = new System.Drawing.Size(268, 44);
            this.buttonConnectAsDesig.TabIndex = 56;
            this.buttonConnectAsDesig.Text = "Connect As Desig";
            this.buttonConnectAsDesig.UseVisualStyleBackColor = true;
            this.buttonConnectAsDesig.Click += new System.EventHandler(this.buttonConnectAsDesig_Click_1);
            // 
            // pictureBoxProfile
            // 
            this.pictureBoxProfile.Location = new System.Drawing.Point(809, 17);
            this.pictureBoxProfile.Name = "pictureBoxProfile";
            this.pictureBoxProfile.Size = new System.Drawing.Size(120, 102);
            this.pictureBoxProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxProfile.TabIndex = 55;
            this.pictureBoxProfile.TabStop = false;
            // 
            // textBoxAppID
            // 
            this.textBoxAppID.Location = new System.Drawing.Point(319, 126);
            this.textBoxAppID.Name = "textBoxAppID";
            this.textBoxAppID.Size = new System.Drawing.Size(446, 28);
            this.textBoxAppID.TabIndex = 54;
            this.textBoxAppID.Text = "1450160541956417";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1078, 526);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonFriendWithLongestName
            // 
            this.buttonFriendWithLongestName.Location = new System.Drawing.Point(577, 431);
            this.buttonFriendWithLongestName.Name = "buttonFriendWithLongestName";
            this.buttonFriendWithLongestName.Size = new System.Drawing.Size(265, 35);
            this.buttonFriendWithLongestName.TabIndex = 73;
            this.buttonFriendWithLongestName.Text = "Friend With Longest Name";
            this.buttonFriendWithLongestName.UseVisualStyleBackColor = true;
            this.buttonFriendWithLongestName.Click += new System.EventHandler(this.buttonFriendWithLongestName_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 561);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOldestPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMostCommentedPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMostLikePhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfile)).EndInit();
            this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.Button buttonLogin;
		private System.Windows.Forms.Button buttonLogout;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBoxAppID;
        private System.Windows.Forms.PictureBox pictureBoxProfile;
        private System.Windows.Forms.Button buttonConnectAsDesig;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelBirthday;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox listBoxAlbums;
        private System.Windows.Forms.Label labelAlbumList;
        private System.Windows.Forms.Button buttonMostLikedFriend;
        private System.Windows.Forms.Button buttonMostLikedPhoto;
        private System.Windows.Forms.PictureBox pictureBoxMostCommentedPhoto;
        private System.Windows.Forms.PictureBox pictureBoxOldestPhoto;
        private System.Windows.Forms.Button buttonOldestPhoto;
        private System.Windows.Forms.Button buttonMostCommentedPhoto;
        private System.Windows.Forms.PictureBox pictureBoxMostLikePhoto;
        private System.Windows.Forms.Button buttonMostPhotosAlbum;
        private System.Windows.Forms.Button buttonFriendWithLongestName;
    }
}

