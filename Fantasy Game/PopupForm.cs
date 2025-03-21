using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Fantasy_Game
{
    public partial class PopupForm : Form
    {
        private int startYPosition;
        public PopupForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;

            try
            {
                PopupBackground.Image = Image.FromFile(@".\Resources\dark-fantasy-scene.jpg");
                PopupBackground.SizeMode = PictureBoxSizeMode.StretchImage;

                PopupBackground.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resim yüklenirken hata oluştu: " + ex.Message);
            }

            slidingLabel.Text = "The world was once a place of balance and light, flourishing under the Radiant Concord, where ancient orders of warriors, mages, and hunters safeguarded the harmony between mortals and the chaotic forces lurking in the shadows. At its heart stood the sacred Citadel of Lumina, a beacon of divine power, and the forest of Eterna, where nature itself breathed life into the land. But this age of peace was shattered when the Demon Lord Malakar rose from the depths of the Abyssal Maw, an ancient and ever-changing dungeon born of mortal despair. Malakar's emergence spread a plague of darkness across the realm, consuming cities in fire and ruin, twisting creatures into abominations, and corrupting the very land. The Citadel fell, its holy halls overrun, and the world plunged into a cursed twilight, where ash choked the skies and the stars dared not shine. Now, the world teeters on the brink of oblivion, its people scattered in isolated fortresses, clinging to life as demonic forces roam freely, preying on the weak and spreading Malakar's influence.\r\n\r\nIn this desolation, four unlikely champions rise. The Warrior, a once-proud knight, carries the weight of their village’s destruction, driven by a relentless thirst for redemption. The Archer, a lone survivor of a cursed forest, hunts answers and vengeance for their lost people, their every step shadowed by whispers of the spirits they failed to save. The Mage, consumed by forbidden knowledge, wields dark power at the cost of their own sanity, seeking to undo the horrors they unwittingly unleashed. The Paladin, the last of a holy order, stands as a crumbling pillar of light, burdened by the fall of their sacred citadel but unwilling to let hope die. Drawn by fate or desperation, they descend into the Abyssal Maw, a dungeon that reshapes itself to mirror the fears, failures, and desires of those who enter, its corridors filled with nightmarish creatures and traps designed to break both body and spirit.\r\n\r\nAs the champions delve deeper, they uncover fragments of a forgotten prophecy—a faint promise that Malakar’s destruction could restore the light, though only at a terrible cost. The dungeon tests more than their strength; it forces them to confront the ghosts of their pasts and the darkness within themselves. Every battle demands sacrifice, every step draws them closer to the edge of despair, and every victory feels as hollow as the ruins they leave behind. Yet they press on, for they are the last hope of a broken world. Together, they must face the abyss and its master, knowing that should they fail, the last embers of light will be extinguished, and the world will be lost to eternal shadow..";

            this.slidingLabel.Parent = this.PopupBackground;
            this.slidingLabel.BackColor = Color.Transparent;

            startYPosition = this.ClientSize.Height;
            slidingLabel.Top = startYPosition;
            slidingLabel.Left = (this.ClientSize.Width - slidingLabel.Width) / 2;

            timer1 = new Timer();
            timer1.Interval = 50; 
            timer1.Tick += new EventHandler(Timer_Tick);
            timer1.Start();

            this.Resize += new EventHandler(Form_Resize);
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            slidingLabel.Top -= 5;

            if (slidingLabel.Top + slidingLabel.Height < 0)
            {
                timer1.Stop();
                GoBack();
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            slidingLabel.Left = (this.ClientSize.Width - slidingLabel.Width) / 2;
        }



        private void PopupForm_Load(object sender, EventArgs e)
        {

        }
        private void GoBack()
        {
            this.Close();   
            CreateCharacterForm characther = new CreateCharacterForm();
            characther.Show();
        }

        private void CharactherCreateExitButton_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void PopupBackground_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void PopupForm_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void slidingLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
