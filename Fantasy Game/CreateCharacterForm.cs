using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Fantasy_Game.Characters;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;



namespace Fantasy_Game
{   
    public partial class CreateCharacterForm : Form
    {

        public CreateCharacterForm()
        {
            InitializeComponent();
            
            InformationVisibleFalse();
            CenterGB();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            InformationsGroupBox.Size = new System.Drawing.Size(660, 660);
            InformationsGroupBox.FlatStyle = FlatStyle.Flat;
            InformationsGroupBox.Location = new Point(1000, 200);

            NameTextBox.Text = string.Empty;


            GameStoryButton.FlatStyle = FlatStyle.Flat;
            GameStoryButton.FlatAppearance.BorderSize = 0;
            GameStoryButton.FlatAppearance.MouseOverBackColor = GameStoryButton.BackColor;


            try
            {
                string imagePath = @".\Resources\dark-fantasy-scene.jpg";
                if (!System.IO.File.Exists(imagePath))
                {
                    throw new FileNotFoundException("Resim dosyası bulunamadı.", imagePath);
                }

                CharactherCreateFormBackground.Image = Image.FromFile(imagePath);
                CharactherCreateFormBackground.SizeMode = PictureBoxSizeMode.StretchImage;

              
                CharactherCreateFormBackground.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resim yüklenirken hata oluştu: " + ex.Message);
            }
            SetPictureBoxPositions();
        }

        private void SetPictureBoxPositions()
        {
           


            IGBRigthUpperPB.Left = InformationsGroupBox.Width - IGBRigthUpperPB.Width;
            IGBRigthUpperPB.Top = 0;

            IGBLeftBottomPB.Left = 0;
            IGBLeftBottomPB.Top = InformationsGroupBox.Height - IGBLeftBottomPB.Height;

            IGBRigthBottomPB.Left = InformationsGroupBox.Width - IGBRigthBottomPB.Width;
            IGBRigthBottomPB.Top = InformationsGroupBox.Height - IGBRigthBottomPB.Height;
        }

        private void InformationVisibleFalse()
        {
            ClassLabelNAME.Visible = false;
            HealthLabelNAME.Visible = false;
            Mana_StaminaLabelNAME.Visible = false;
            ArmourLabelNAME.Visible = false;
            StoryLabelNAME.Visible = false;
        }

        private void InformationVisibleTrue()
        {
            ClassLabelNAME.Visible = true;
            HealthLabelNAME.Visible = true;
            Mana_StaminaLabelNAME.Visible = true;
            ArmourLabelNAME.Visible = true;
            StoryLabelNAME.Visible = true;
        }

        private void CharactherCreateFormBackground_Click(object sender, EventArgs e)
        {
        }

        private void InformationsGB_Enter(object sender, EventArgs e)
        {
        }

        private void CenterGB()
        {
            InformationsGB.Location = new Point(
                (this.ClientSize.Width - InformationsGB.Width) / 2, 200
            );
        }

        private void CharactherCreateExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void NameLabel_Click(object sender, EventArgs e)
        {
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string selectedCharacter = comboBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedCharacter))
            {
                MessageBox.Show("Please select a valid character.");
                return;
            }

            string characterName = NameTextBox.Text;
            if (string.IsNullOrEmpty(characterName))
            {
                MessageBox.Show("Please enter a character name.");
                return;
            }

         
            object character = null;

            try
            {
                
                character = await Task.Run<object>(() =>
                {
                    switch (selectedCharacter)
                    {
                        case "Warrior":
                            return new Warrior(characterName);
                        case "Archer":
                            return new Archer(characterName);
                        case "Mage":
                            return new Mage(characterName);
                        case "Paladin":
                            return new Paladin(characterName);
                        default:
                            throw new InvalidOperationException("Please select a valid character.");
                    }
                });
                
                
                InformationVisibleTrue();
                DisplayCharacterInfo(character);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

       
        private void DisplayCharacterInfo(object character)
        {
            try
            {
                string selectedCharacter = comboBox1.SelectedItem?.ToString();
                if (character != null)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke((Action)(() => UpdateCharacterDetails(selectedCharacter, character)));
                    }
                    else
                    {
                        UpdateCharacterDetails(selectedCharacter, character);
                    }
                }
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke((Action)ClearCharacterDetails);
                    }
                    else
                    {
                        ClearCharacterDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateCharacterDetails(string selectedCharacter, object character)
        {
            if (character is Warrior warrior)
            {
                ClassLabelNAME.Text = selectedCharacter;
                HealthLabelNAME.Text = warrior.Health.ToString();
                ArmourLabelNAME.Text = warrior.Armour.ToString();
                Mana_StaminaLabelNAME.Text = warrior.Stamina.ToString();
                Mana_StaminaLabel.Text = "Stamina :";
                StoryLabelNAME.Text = warrior.Story;
                CharacterNameLabel.Text = warrior.Name;
            }
            else if (character is Archer archer)
            {
                ClassLabelNAME.Text = selectedCharacter;
                HealthLabelNAME.Text = archer.Health.ToString();
                ArmourLabelNAME.Text = archer.Armour.ToString();
                Mana_StaminaLabelNAME.Text = archer.Stamina.ToString();
                Mana_StaminaLabel.Text = "Stamina :";
                StoryLabelNAME.Text = archer.Story;
                CharacterNameLabel.Text = archer.Name;
            }
            else if (character is Mage mage)
            {
                ClassLabelNAME.Text = selectedCharacter;
                HealthLabelNAME.Text = mage.Health.ToString();
                ArmourLabelNAME.Text = mage.Armour.ToString();
                Mana_StaminaLabelNAME.Text = mage.Mana.ToString();
                Mana_StaminaLabel.Text = "Mana :";
                StoryLabelNAME.Text = mage.Story;
                CharacterNameLabel.Text = mage.Name;
            }
            else if (character is Paladin paladin)
            {
                ClassLabelNAME.Text = selectedCharacter;
                HealthLabelNAME.Text = paladin.Health.ToString();
                ArmourLabelNAME.Text = paladin.Armour.ToString();
                Mana_StaminaLabelNAME.Text = paladin.Mana.ToString();
                Mana_StaminaLabel.Text = "Mana :";
                StoryLabelNAME.Text = paladin.Story;
                CharacterNameLabel.Text = paladin.Name;
            }
        }
        private void ClearCharacterDetails()
        {
            ClassLabelNAME.Text = "";
            HealthLabelNAME.Text = "";
            ArmourLabelNAME.Text = "";
            Mana_StaminaLabelNAME.Text = "";
            StoryLabelNAME.Text = "";
            CharacterNameLabel.Text = "";
        }


        private void CreateCharacterForm_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
         
                e.SuppressKeyPress = true;

                
                CharactherCreateStartButton_Click(CharactherCreateStartButton, EventArgs.Empty);
            }
        }

        private void MaleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void ClassLabel_Click(object sender, EventArgs e)
        {
        }

        private void InformationsGroupBox_Enter(object sender, EventArgs e)
        {
        }

        private void Mana_StaminaLabel_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void StoryLabel_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void ClassLabelNAME_Click(object sender, EventArgs e)
        {
        }

        private void CharactherCreateStartButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Please enter a name.");
                return;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a character class.");
                return;
            }

            if (!MaleRadioButton.Checked && !FemaleRadioButton.Checked)
            {
                MessageBox.Show("Please select a gender.");
                return;
            }

        
            string selectedCharacter = comboBox1.SelectedItem.ToString();
            string characterName = NameTextBox.Text;
            object character = null;

            switch (selectedCharacter)
            {
                case "Warrior":
                    character = new Warrior(characterName);
                    break;
                case "Archer":
                    character = new Archer(characterName);
                    break;
                case "Mage":
                    character = new Mage(characterName);
                    break;
                case "Paladin":
                    character = new Paladin(characterName);
                    break;
                default:
                    MessageBox.Show("Please select a valid character.");
                    return;
            }

          
            MainGameForm mainGameForm = new MainGameForm(character,characterName);
            mainGameForm.Show();
            this.Hide();
        }
      
       

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void GameStoryButton_Click(object sender, EventArgs e)
        {
            PopupForm popup = new PopupForm();
            popup.Show();
            this.Hide();
        }

        private void GenderGB_Enter(object sender, EventArgs e)
        {
            
        }

        private void MaleRadioButton_Click(object sender, EventArgs e)
        {
            GenderPictureBox.Image = Image.FromFile(@".\Resources\warrior_silhouette_man.png");
        }

        private void FemaleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            GenderPictureBox.Image = Image.FromFile(@".\Resources\warrior_silhouette_woman.png");
        }

        private void StoryLabelNAME_Click(object sender, EventArgs e)
        {

        }
    }
}