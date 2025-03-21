using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace Fantasy_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            try
            {
                InitializeComponent();

                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
                this.TopMost = true;


                LoadBackgroundImage();
                LoadCustomFont();

                GameNameLabel.Parent = BackGround;
                GameNameLabel.BackColor = Color.Transparent;

                Center();
                this.Resize += (sender, e) => Center();

                CharactherCreateButton.FlatStyle = FlatStyle.Flat;
                CharactherCreateButton.FlatAppearance.BorderSize = 0;
                CloseButton.FlatStyle = FlatStyle.Flat;
                CloseButton.FlatAppearance.BorderSize = 0;

                BackGround.Size = new Size(this.Width, this.Height);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Form oluşturulurken hata oluştu: " + ex.Message);
            }
        }

        private void LoadBackgroundImage()
        {
            try
            {
                string imagePath = @".\Resources\dark-fantasy-scene.jpg";
                if (!System.IO.File.Exists(imagePath))
                {
                    throw new FileNotFoundException("Resim dosyası bulunamadı.", imagePath);
                }

                BackGround.Image = Image.FromFile(imagePath);
                BackGround.SizeMode = PictureBoxSizeMode.StretchImage;
                BackGround.Size = new Size(this.Width, this.Height);

                BackGround.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resim yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void LoadCustomFont()
        {
            try
            {
                string fontPath = @".\Resources\NightmarePills-BV2w.ttf";
                if (!System.IO.File.Exists(fontPath))
                {
                    throw new FileNotFoundException("Font dosyası bulunamadı.", fontPath);
                }

                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile(fontPath);

                GameNameLabel.Font = new Font(pfc.Families[0], 60);
                CharactherCreateButton.Font = new Font(pfc.Families[0], 26);
                CloseButton.Font = new Font(pfc.Families[0], 26);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Font yüklenirken hata oluştu: " + ex.Message);
            }
        }

 
        private void Center()
        {
            GameNameLabel.Location = new Point(
                (this.ClientSize.Width - GameNameLabel.Width) / 2,  
                40  
            );
            CharactherCreateButton.Location = new Point(
                (this.ClientSize.Width - CharactherCreateButton.Width) / 2, 170
            );
            CloseButton.Location = new Point(
                (this.ClientSize.Width - CloseButton.Width) / 2, 270
            );
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CharactherCreateButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            CreateCharacterForm characther = new CreateCharacterForm();
            characther.Show();

        }

        private void GameNameLabel_Click(object sender, EventArgs e)
        {

        }
    }
}