using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class FormLogin : Form
    {
        private FormMain m_MainForm;
        private Timer m_StarTimer;
        private readonly Random r_Random = new Random();
        private float[] m_StarX;
        private float[] m_StarY;
        private float[] m_StarSize;
        private float[] m_StarAlpha;
        private const int k_StarCount = 60;

        public FormLogin()
        {
            initializeStars();
            setupForm();
            setupUI();
            startStarAnimation();
        }

        private void initializeStars()
        {
            m_StarX = new float[k_StarCount];
            m_StarY = new float[k_StarCount];
            m_StarSize = new float[k_StarCount];
            m_StarAlpha = new float[k_StarCount];

            for (int i = 0; i < k_StarCount; i++)
            {
                m_StarX[i] = r_Random.Next(0, 650);
                m_StarY[i] = r_Random.Next(0, 500);
                m_StarSize[i] = (float)(r_Random.NextDouble() * 3 + 1);
                m_StarAlpha[i] = (float)(r_Random.NextDouble() * 200 + 55);
            }
        }

        private void setupForm()
        {
            this.Text = "Login - Facebook Analyzer";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(520, 420);
            this.BackColor = Color.FromArgb(15, 32, 75);
            this.Name = "FormLogin";
            this.DoubleBuffered = true;
            this.Paint += FormLogin_Paint;
        }

        private void setupUI()
        {
            var panelCenter = new Panel();
            panelCenter.Size = new Size(400, 340);
            panelCenter.Location = new Point(60, 40);
            panelCenter.BackColor = Color.FromArgb(18, 38, 90);
            this.Controls.Add(panelCenter);

            // Title
            var labelTitle = new Label();
            labelTitle.Text = "Facebook\nAnalyzer";
            labelTitle.Font = new Font("Segoe UI", 34, FontStyle.Bold);
            labelTitle.ForeColor = Color.White;
            labelTitle.Location = new Point(0, 0);
            labelTitle.Size = new Size(400, 130);
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            panelCenter.Controls.Add(labelTitle);

            // Subtitle
            var labelSubtitle = new Label();
            labelSubtitle.Text = "Discover insights from your Facebook profile";
            labelSubtitle.Font = new Font("Segoe UI", 11, FontStyle.Italic);
            labelSubtitle.ForeColor = Color.FromArgb(160, 200, 255);
            labelSubtitle.Location = new Point(0, 135);
            labelSubtitle.Size = new Size(400, 30);
            labelSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            panelCenter.Controls.Add(labelSubtitle);

            // Login Button
            var buttonLogin = new Button();
            buttonLogin.Text = "Login to Facebook";
            buttonLogin.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            buttonLogin.BackColor = Color.FromArgb(24, 119, 242);
            buttonLogin.ForeColor = Color.White;
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.FlatAppearance.BorderSize = 0;
            buttonLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(16, 102, 214);
            buttonLogin.Size = new Size(320, 52);
            buttonLogin.Location = new Point(40, 195);
            buttonLogin.Cursor = Cursors.Hand;
            buttonLogin.Click += buttonLogin_Click;
            addRoundedCorners(buttonLogin, 8);
            panelCenter.Controls.Add(buttonLogin);

            // Design Patterns Button
            var buttonDesig = new Button();
            buttonDesig.Text = "Login with Desig";
            buttonDesig.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            buttonDesig.BackColor = Color.FromArgb(66, 183, 42);
            buttonDesig.ForeColor = Color.White;
            buttonDesig.FlatStyle = FlatStyle.Flat;
            buttonDesig.FlatAppearance.BorderSize = 0;
            buttonDesig.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 160, 30);
            buttonDesig.Size = new Size(320, 52);
            buttonDesig.Location = new Point(40, 265);
            buttonDesig.Cursor = Cursors.Hand;
            buttonDesig.Click += buttonDesig_Click;
            addRoundedCorners(buttonDesig, 8);
            panelCenter.Controls.Add(buttonDesig);
        }

        private void addRoundedCorners(Button i_Button, int i_Radius)
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, i_Button.Width, i_Button.Height);

            path.AddArc(rect.X, rect.Y, i_Radius * 2, i_Radius * 2, 180, 90);
            path.AddArc(rect.Right - i_Radius * 2, rect.Y, i_Radius * 2, i_Radius * 2, 270, 90);
            path.AddArc(rect.Right - i_Radius * 2, rect.Bottom - i_Radius * 2, i_Radius * 2, i_Radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - i_Radius * 2, i_Radius * 2, i_Radius * 2, 90, 90);
            path.CloseFigure();

            i_Button.Region = new Region(path);
        }

        private void startStarAnimation()
        {
            m_StarTimer = new Timer();
            m_StarTimer.Interval = 100;
            m_StarTimer.Tick += (s, e) =>
            {
                for (int i = 0; i < k_StarCount; i++)
                {
                    m_StarAlpha[i] += (float)(r_Random.NextDouble() * 40 - 20);
                    m_StarAlpha[i] = Math.Max(30, Math.Min(255, m_StarAlpha[i]));
                }

                this.Invalidate();
            };

            m_StarTimer.Start();
        }

        private void FormLogin_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Gradient background
            using (var brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(10, 25, 65),
                Color.FromArgb(25, 50, 120),
                LinearGradientMode.ForwardDiagonal))
            {
                g.FillRectangle(brush, this.ClientRectangle);
            }

            // Stars
            for (int i = 0; i < k_StarCount; i++)
            {
                int alpha = (int)m_StarAlpha[i];

                using (var starBrush = new SolidBrush(Color.FromArgb(alpha, 255, 255, 255)))
                {
                    g.FillEllipse(starBrush, m_StarX[i], m_StarY[i], m_StarSize[i], m_StarSize[i]);
                }
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            openMainForm(i_IsDemoAccount: false);
        }

        private void buttonDesig_Click(object sender, EventArgs e)
        {
            openMainForm(i_IsDemoAccount: true);
        }

        private void openMainForm(bool i_IsDemoAccount)
        {
            if (m_MainForm == null || m_MainForm.IsDisposed)
            {
                m_MainForm = new FormMain();
            }

            bool loginSuccess = i_IsDemoAccount
                ? m_MainForm.LoginWithDesigAccount()
                : m_MainForm.LoginWithFacebook();

            if (loginSuccess)
            {
                m_MainForm.Show();
                this.Hide();
            }
        }

        public void ShowLoginScreen()
        {
            if (m_MainForm != null && !m_MainForm.IsDisposed)
            {
                m_MainForm.Hide();
            }

            this.Show();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            m_StarTimer?.Stop();
            m_StarTimer?.Dispose();

            if (m_MainForm != null && !m_MainForm.IsDisposed)
            {
                m_MainForm.Close();
            }

            base.OnFormClosed(e);
        }
    }
}
