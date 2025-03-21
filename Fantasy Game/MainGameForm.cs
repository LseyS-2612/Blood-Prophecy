using System;
using System.Drawing;
using System.Windows.Forms;
using static Fantasy_Game.Characters;
using System.Collections.Generic;
using Fantasy_Game;
using System.Threading.Tasks;
using Fantasy_Game.Properties;


namespace Fantasy_Game
{
    public partial class MainGameForm : Form
    {
        private Enemy currentEnemy; 
        private string characterName;
        private object character;
        private Node currentNode;
        bool townmodebool = true;
        bool dungeonmodebool = false;
        int currentFloor;
        public object playerCharacter;
        bool hasUsedBuff = false;
        int diceRoll;



     
        private List<string> messageHistory = new List<string>();


        public MainGameForm(object character,string characterName)
        {
            InitializeComponent();
            this.character = character;
            this.characterName = characterName;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None; 
            this.TopMost = true;
            MainGameLabel.Text = "Welcome to the Town!"; 
            UpdateCharacterAbilities();
            MainGameClassLabel.Text = character.GetType().Name; 
            MainGameNameLabel.Text = characterName; 
            MainGamelArmourLabel.Text=GetPlayerArmour(character).ToString();
            MainGamelGoldLabel.Text = GetGoldAmount(character).ToString(); 
            int goldAmount;


            InitializeMap();


            currentNode = town;
            UpdateLocationLabel();
            TownMode();
            currentEnemy = EnemyRepository.GetEnemyByFloor(currentFloor);
            try
            {
                MainGameBackground.Image = Image.FromFile(@".\Resources\dark-fantasy-scene.jpg");
                MainGameBackground.SizeMode = PictureBoxSizeMode.StretchImage;

          
                MainGameBackground.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resim yüklenirken hata oluştu: " + ex.Message);
            }

            ExitBattleMode();

        }

        private Node town, church, market, dungeonFloor1, dungeonFloor2, dungeonFloor3, dungeonFloor4, dungeonFloor5, dungeonFloor6;


        private void UpdateCharacterAbilities()
        {
            if (character is Warrior warrior)
            {
                SetAbilityButton(Ab1button, warrior.Ability1, @".\Resources\Ability_warrior_incite.png");
                SetAbilityButton(Ab2button, warrior.Ability2, @".\Resources\Ability_warrior_innerrage.png");
                SetAbilityButton(Ab3button, warrior.Ability3, @".\Resources\Ability_warrior_warbringer.png");
                SetAbilityButton(Ab4button, warrior.Ability4, @".\Resources\Ability_warrior_rallyingcry.png");
            }
            else if (character is Archer archer)
            {
                SetAbilityButton(Ab1button, archer.Ability1, @".\Resources\Ability_hunter_quickshot.png");
                SetAbilityButton(Ab2button, archer.Ability2, @".\Resources\Ability_rogue_improvedrecuperate.png");
                SetAbilityButton(Ab3button, archer.Ability3, @".\Resources\Ability_hunter_snipershot.png");
                SetAbilityButton(Ab4button, archer.Ability4, @".\Resources\Ability_hunter_beastwithin.png");
            }
            else if (character is Mage mage)
            {
                SetAbilityButton(Ab1button, mage.Ability1, @".\Resources\Ability_mage_arcanebarrage_nightborne.png");
                SetAbilityButton(Ab2button, mage.Ability2, @".\Resources\Ability_mage_studentofthemind.png");
                SetAbilityButton(Ab3button, mage.Ability3, @".\Resources\Ability_mage_greaterpyroblast.png");
                SetAbilityButton(Ab4button, mage.Ability4, @".\Resources\Ability_mage_potentspirit.png");
            }
            else if (character is Paladin paladin)
            {
                SetAbilityButton(Ab1button, paladin.Ability1, @".\Resources\Ability_paladin_bladeofjustice.png");
                SetAbilityButton(Ab2button, paladin.Ability2, @".\Resources\Ability_paladin_holyavenger.png");
                SetAbilityButton(Ab3button, paladin.Ability3, @".\Resources\Ability_paladin_lightoftheprotector.png");
                SetAbilityButton(Ab4button, paladin.Ability4, @".\Resources\Ability_paladin_selflesshealer.png");
            }
        }



        private void SetAbilityButton(Button button, Ability ability, string imagePath)
        {
            button.Text = ability.Name; 
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.FlatAppearance.MouseOverBackColor = Color.Transparent; 
            button.BackColor = Color.Transparent; 
            
            try
            {
                button.BackgroundImage = Image.FromFile(imagePath);
                button.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                ShowMessage($"Error loading image for {ability.Name}: {ex.Message}");
            }
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(button, ability.Description);
        }


        private void InitializeMap()
        {
            town = new Node("Town", 0, () => ShowMessage("You are in the Town. Explore the Church or the Market."));
            market = new Node("Market", 0);
            church = new Node("Church", 0);

            dungeonFloor1 = new Node("Dungeon Floor 1", 1, () => ShowMessage("You entered Dungeon Floor 1. Prepare yourself for battle!"));
            dungeonFloor2 = new Node("Dungeon Floor 2", 2, () => ShowMessage("You are now on Dungeon Floor 2. Enemies are getting stronger."));
            dungeonFloor3 = new Node("Dungeon Floor 3", 3, () => ShowMessage("Dungeon Floor 3. You feel a dark presence."));
            dungeonFloor4 = new Node("Dungeon Floor 4", 4, () => ShowMessage("Dungeon Floor 4. The air is heavy with danger."));
            dungeonFloor5 = new Node("Dungeon Floor 5", 5, () => ShowMessage("Dungeon Floor 5. The final challenges await."));
            dungeonFloor6 = new Node("Dungeon Floor 6", 6, () => ShowMessage("Dungeon Floor 6! A powerful enemy appears."));


            dungeonFloor1.Next = dungeonFloor2;
            dungeonFloor2.Previous = dungeonFloor1;
            dungeonFloor2.Next = dungeonFloor3;
            dungeonFloor3.Previous = dungeonFloor2;
            dungeonFloor3.Next = dungeonFloor4;
            dungeonFloor4.Previous = dungeonFloor3;
            dungeonFloor4.Next = dungeonFloor5;
            dungeonFloor5.Previous = dungeonFloor4;
            dungeonFloor5.Next = dungeonFloor6;
            dungeonFloor6.Previous = dungeonFloor5;

            market.Next = church;
            church.Previous = market;
        }


        private void ShowMessage(string message)
        {
            if (MessagePanel == null || MainGameLabel == null)
            {
                MessageBox.Show("MessagePanel veya MainGameLabel bulunamadı!", "Hata");
                return;
            }

            messageHistory.Add(message);

            MainGameLabel.Text = string.Join(Environment.NewLine, messageHistory);

            MainGameLabel.Height = MainGameLabel.PreferredHeight;

            if (MainGameLabel.Height > MessagePanel.Height)
            {
                MainGameLabel.Top = MessagePanel.Height - MainGameLabel.Height - 20;
            }
            else
            {
                MainGameLabel.Top = 20;
            }


        }

        private void MainGameForm_Load(object sender, EventArgs e)
        {

            UpdateHealthAndManaBars();
            UpdateLocationLabel();
            TownMode();
            int currentFloor = currentNode?.FloorNumber ?? 1; 
            Console.WriteLine($"Current Floor: {currentFloor}");
            playerCharacter = character;


        }
        private void UpdateLocationLabel()
        {
            if (CurrentLocationLabel != null && currentNode != null)
            {
                CurrentLocationLabel.Text = $"Current Location: {currentNode.Name}";
            }
            else
            {
                CurrentLocationLabel.Text = "Unknown Location"; 
            }
        }


        private void MoveTo(Node nextNode)
        {
            if (nextNode == null)
            {
                if (currentNode == town)
                {
                    return;
                }
                ShowMessage("You can't go this way!");
                return;
            }

            currentNode = nextNode;

            currentNode.EnterAction?.Invoke();

            UpdateLocationLabel();
        }


        private void UpdateHealthAndManaBars()
        {
            if (character is Warrior warrior)
            {
                HealthPB.Maximum = 100;
                HealthPB.Value = warrior.Health;
                Mana_StaminaPB.Maximum = 50;
                Mana_StaminaPB.Value = Math.Max(0, warrior.Stamina);
                LabelHealthControl.Text = HealthPB.Value.ToString();
                ManaLabelControl.Text = Mana_StaminaPB.Value.ToString();
                Mana_StaminaLBPB.Text = "Stamina";

            }
            else if (character is Archer archer)
            {
                HealthPB.Maximum = 100;
                HealthPB.Value = archer.Health;
                Mana_StaminaPB.Maximum = 50;
                Mana_StaminaPB.Value = Math.Max(0, archer.Stamina);
                LabelHealthControl.Text = HealthPB.Value.ToString();
                ManaLabelControl.Text = Mana_StaminaPB.Value.ToString();
                Mana_StaminaLBPB.Text = "Stamina";
            }
            else if (character is Mage mage)
            {
                HealthPB.Maximum = 70;
                HealthPB.Value = mage.Health;
                Mana_StaminaPB.Maximum = 100;
                Mana_StaminaPB.Value = Math.Max(0, mage.Mana);
                LabelHealthControl.Text = HealthPB.Value.ToString();
                ManaLabelControl.Text = Mana_StaminaPB.Value.ToString();
            }
            else if (character is Paladin paladin)
            {
                HealthPB.Maximum = 120;
                HealthPB.Value = paladin.Health;
                Mana_StaminaPB.Maximum = 80;
                Mana_StaminaPB.Value = Math.Max(0, paladin.Mana);
                LabelHealthControl.Text = HealthPB.Value.ToString();
                ManaLabelControl.Text = Mana_StaminaPB.Value.ToString();
            }
        }

        private void DungeonMode()
        {
            townmodebool = false;
            dungeonmodebool = true;

            if (currentNode.Name.Contains("Dungeon Floor"))
            {
                TownButton.Visible = true;
                DungeonButton.Visible = false;
                PreviousFloorButton.Text = "Previous Floor";
                NextFloorButton.Text = "Next Floor";
                PreviousFloorButton.Enabled = currentNode != dungeonFloor1; 
                NextFloorButton.Enabled = currentNode != dungeonFloor6; 
            }
        }

        private void TownMode()
        {
            townmodebool = true;
            dungeonmodebool = false;

            if (currentNode.Name.Contains("Town"))
            {
                TownButton.Visible = false;
                DungeonButton.Visible = true;
                PreviousFloorButton.Text = "Market";
                NextFloorButton.Text = "Church";
                PreviousFloorButton.Enabled = currentNode != market; 
                NextFloorButton.Enabled = currentNode != church; 
            }
        }

        private void MainGameBackground_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CloseAllButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DungeonButton_Click(object sender, EventArgs e)
        {

            if (currentNode == town || currentNode == market || currentNode == church)
            {
                ShowMessage("Steel yourself, for you now descend into the heart of darkness. May God grant you strength, for what lies ahead is a realm of shadows and terror.\n\n");
                MoveTo(dungeonFloor1); 
                DungeonMode();

                StartBattle(playerCharacter, currentEnemy);
                
            }
            else
            {
                ShowMessage("You are already in the Dungeon!");
            }
        }


        private void NextFloorButton_Click(object sender, EventArgs e)
        {
            if (dungeonmodebool == true)
            {
                if (currentNode == dungeonFloor6)
                {
                    ShowMessage("The silence rising from the depths of the dungeon echoes your victory. The darkness has been vanquished, and terror has retreated. Here, you have written your legend!\n");
                    NextFloorButton.Enabled = false; 
                }
                else
                {
                    PreviousFloorButton.Enabled = true;
                    MoveTo(currentNode.Next);
                    currentFloor++;
                    currentEnemy = EnemyRepository.GetEnemyByFloor(currentFloor);
                    StartBattle(playerCharacter, currentEnemy); 

                }
            }
            else if (townmodebool == true)
            {
                if (currentNode == town || currentNode == market)
                {
                    MoveTo(church);
                    ShowMessage("You are now at the Church.\n");

                    NextFloorButton.Enabled = false;
                    PreviousFloorButton.Enabled = true;
                }
                else
                {
                    ShowMessage("You are already at the Church.");
                }
            }
            else
            {
                ShowMessage("You can't go further!");
            }
        }


        private void PreviousFloorButton_Click(object sender, EventArgs e)
        {
            if (dungeonmodebool == true)
            {
                if (currentNode == dungeonFloor1)
                { 
                    PreviousFloorButton.Enabled = false;
                }
                else
                {
                    NextFloorButton.Enabled = true;
                    MoveTo(currentNode.Previous);
                }
            }
            else if (townmodebool == true)
            {
                if (currentNode == town || currentNode == church)
                {
                    MoveTo(market);
                    ShowMessage("You are now at the Market.\n");

                    NextFloorButton.Enabled = true;
                    PreviousFloorButton.Enabled = false;
                }
                else
                {
                    ShowMessage("You are already at the Market.");
                }
            }
            else
            {
                ShowMessage("You can't go further!");
            }
        }


        private void CurrentLocationLabel_Click(object sender, EventArgs e)
        {

        }

        private void MaiinLabel_Click(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private async void Ab1button_Click(object sender, EventArgs e)
        {
            if (playerCharacter is Characters.Warrior warrior)
            {
                UseAbility(warrior.Ability1, warrior);
            }
            else if (playerCharacter is Characters.Archer archer)
            {
                UseAbility(archer.Ability1, archer);
            }
            else if (playerCharacter is Characters.Mage mage)
            {
                UseAbility(mage.Ability1, mage);
            }
            else if (playerCharacter is Characters.Paladin paladin)
            {
                UseAbility(paladin.Ability1, paladin);
            }
            else
            {
                ShowMessage("Invalid character or no ability assigned.");
            }
            await Task.Delay(1000);
        }

        private void TownButton_Click(object sender, EventArgs e)
        {
            if (currentNode == dungeonFloor1 || currentNode == dungeonFloor6)
            {
                MoveTo(town);
                TownMode(); 
                ExitBattleMode(); 
            }
            else
            {
                ShowMessage("You can only return to the Town from the top or bottom floor of the Dungeon!");
            }
        }


        private void UseAbility(Characters.Ability ability, object character)
        {
            
            diceRoll = RollDice();
            double multiplier = GetLuckMultiplier(diceRoll);
            
            int damage = (int)(ability.Damage * multiplier);

            if (ability.IsHealing)
            {
                int healAmount = (int)(30 * multiplier);
                HealPlayer(character, healAmount);
                ShowMessage($"{ability.Name} was used. Healed for {healAmount} health.");
            }
            // Eğer yetenek buff ise
            else if (ability.IsBuffing)
            {
                ShowMessage($"{ability.Name} was used. Damage increased temporarily!");
            }

            else
            {
                if (character is Characters.Warrior warrior)
                {
                    if (warrior.Stamina >= ability.ResourceCost)
                    {
                        warrior.Stamina -= ability.ResourceCost;
                        ApplyDamageToEnemy(damage);
                        ShowMessage($"{ability.Name} was used. Dealt {damage} damage!");
                    }
                    else
                    {
                        ShowMessage("Not enough stamina to use this ability!");
                    }
                }
                else if (character is Characters.Archer archer)
                {
                    if (archer.Stamina >= ability.ResourceCost)
                    {
                        archer.Stamina -= ability.ResourceCost;
                        ApplyDamageToEnemy(damage);
                        ShowMessage($"{ability.Name} was used. Dealt {damage} damage!");
                    }
                    else
                    {
                        ShowMessage("Not enough stamina to use this ability!");
                    }
                }
                else if (character is Characters.Mage mage)
                {
                    if (mage.Mana >= ability.ResourceCost)
                    {
                        mage.Mana -= ability.ResourceCost;
                        ApplyDamageToEnemy(damage);
                        ShowMessage($"{ability.Name} was used. Dealt {damage} damage!");
                    }
                    else
                    {
                        ShowMessage("Not enough mana to use this ability!");
                    }
                }
                else if (character is Characters.Paladin paladin)
                {
                    if (paladin.Mana >= ability.ResourceCost)
                    {
                        paladin.Mana -= ability.ResourceCost;
                        ApplyDamageToEnemy(damage);
                        ShowMessage($"{ability.Name} was used. Dealt {damage} damage!");
                    }
                    else
                    {
                        ShowMessage("Not enough mana to use this ability!");
                    }
                }
            }        
            UpdateHealthAndManaBars(); 
        }





        public class Node
        {
            public string Name { get; set; }
            public Node Next { get; set; }
            public Node Previous { get; set; }
            public Action EnterAction { get; set; }
            public int FloorNumber { get; set; } 

            public Node(string name, int floorNumber = 0, Action enterAction = null)
            {
                Name = name;
                FloorNumber = floorNumber;
                EnterAction = enterAction;
            }

        }

        private void BuffCharacter(object character, int buffAmount, int duration)
        {
            if (character is Characters.Warrior warrior && !hasUsedBuff)
            {
                warrior.Ability1.Damage = Math.Min(warrior.Ability1.Damage + buffAmount, 150); 
                warrior.Stamina -= 25; 
                hasUsedBuff = true; 
                ShowMessage($"{warrior.Name} damage increased by {buffAmount}. Current Damage: {warrior.Ability1.Damage}");
                Task.Delay(duration * 1000).ContinueWith(_ =>
                {
                    warrior.Ability1.Damage = Math.Max(warrior.Ability1.Damage - buffAmount, 0);
                    hasUsedBuff = false;
                    ShowMessage($"{warrior.Name} buff ended. Current Damage: {warrior.Ability1.Damage}");
                });
            }
            else if (character is Characters.Archer archer && !hasUsedBuff)
            {
                archer.Ability1.Damage = Math.Min(archer.Ability1.Damage + buffAmount, 130);
                archer.Stamina -= 25; 
                hasUsedBuff = true;
                ShowMessage($"{archer.Name} damage increased by {buffAmount}. Current Damage: {archer.Ability1.Damage}");
                Task.Delay(duration * 1000).ContinueWith(_ =>
                {
                    archer.Ability1.Damage = Math.Max(archer.Ability1.Damage - buffAmount, 0);
                    hasUsedBuff = false; 
                    ShowMessage($"{archer.Name} buff ended. Current Damage: {archer.Ability1.Damage}");
                });
            }
            else if (character is Characters.Mage mage && !hasUsedBuff)
            {
                mage.Ability1.Damage = Math.Min(mage.Ability1.Damage + buffAmount, 140);
                mage.Mana -= 25; 
                hasUsedBuff = true;
                ShowMessage($"{mage.Name} damage increased by {buffAmount}. Current Damage: {mage.Ability1.Damage}");
                Task.Delay(duration * 1000).ContinueWith(_ =>
                {
                    mage.Ability1.Damage = Math.Max(mage.Ability1.Damage - buffAmount, 0);
                    hasUsedBuff = false; 
                    ShowMessage($"{mage.Name} buff ended. Current Damage: {mage.Ability1.Damage}");
                });
            }
            else if (character is Characters.Paladin paladin && !hasUsedBuff)
            {
                paladin.Ability1.Damage = Math.Min(paladin.Ability1.Damage + buffAmount, 160);
                paladin.Mana -= 25; 
                hasUsedBuff = true;
                ShowMessage($"{paladin.Name} damage increased by {buffAmount}. Current Damage: {paladin.Ability1.Damage}");
                Task.Delay(duration * 1000).ContinueWith(_ =>
                {
                    paladin.Ability1.Damage = Math.Max(paladin.Ability1.Damage - buffAmount, 0);
                    hasUsedBuff = false; 
                    ShowMessage($"{paladin.Name} buff ended. Current Damage: {paladin.Ability1.Damage}");
                });
            }
            else
            {
                ShowMessage("Character has already buffed!");
            }
        }
        private void HealCharacterFully(object character)
        {
            if (character is Characters.Warrior warrior)
                warrior.Health = 100;
            else if (character is Characters.Archer archer)
                archer.Health = 100;
            else if (character is Characters.Mage mage)
                mage.Health = 70;
            else if (character is Characters.Paladin paladin)
                paladin.Health = 120;
        }

        private void ResourceFully(object characther)
        {
            if (character is Characters.Warrior warrior)
                warrior.Stamina = 50;
            else if (character is Characters.Archer archer)
                archer.Stamina = 50;
            else if (character is Characters.Mage mage)
                mage.Mana = 100;
            else if (character is Characters.Paladin paladin)
                paladin.Mana = 80;
        }

        private int RollDice()
        {
            Random random = new Random();
            return random.Next(1, 21); 
        }

        private double GetLuckMultiplier(int diceRoll)
        {
            if (diceRoll >= 15) return 1.5; 
            if (diceRoll >= 5) return 1.0;  
            return 0.8;                     
        }

        private int GetGoldAmount(object character)
        {
            if (character is Warrior warrior) return warrior.Gold;
            if (character is Archer archer) return archer.Gold;
            if (character is Mage mage) return mage.Gold;
            if (character is Paladin paladin) return paladin.Gold;
            return 0;
        }
        private int GetPlayerArmour(object character)
        {
            if (character is Warrior warrior) return warrior.Armour;
            if (character is Archer archer) return archer.Armour;
            if (character is Mage mage) return mage.Armour;
            if (character is Paladin paladin) return paladin.Armour;
            return 0;
        }

        private int GetPlayerHealth(object character)
        {
            if (character is Warrior warrior) return warrior.Health;
            if (character is Archer archer) return archer.Health;
            if (character is Mage mage) return mage.Health;
            if (character is Paladin paladin) return paladin.Health;
            return 0;
        }

        private void ApplyDamageToEnemy(int damage)
        {
            if (currentEnemy != null)
            {
                currentEnemy.Health -= damage;
            }
        }

        private int AddGoldToPlayer()
        {
            int goldAmount = 0; 
            if (CurrentLocationLabel.Text.Equals("Dungeon Floor 1"))
            {
                goldAmount = 50;
            }
            else if (CurrentLocationLabel.Text.Equals("Dungeon Floor 2"))
            {
                goldAmount = 50;
            }
            else if (CurrentLocationLabel.Text.Equals("Dungeon Floor 3"))
            {
                goldAmount = 60;
            }
            else if (CurrentLocationLabel.Text.Equals("Dungeon Floor 4"))
            {
                goldAmount = 60;
            }
            else if (CurrentLocationLabel.Text.Equals("Dungeon Floor 5"))
            {
                goldAmount = 80;
            }
            else if (CurrentLocationLabel.Text.Equals("Dungeon Floor 6"))
            {
                goldAmount = 100;
            }


            if (character is Warrior warrior) warrior.Gold += goldAmount;
            if (character is Archer archer) archer.Gold += goldAmount;
            if (character is Mage mage) mage.Gold += goldAmount;
            if (character is Paladin paladin) paladin.Gold += goldAmount;

            MainGamelGoldLabel.Text = GetGoldAmount(character).ToString();
            return goldAmount;
        }


        private void EnterBattleMode()
        {
            Ab1button.Visible = true;
            Ab2button.Visible = true;
            Ab3button.Visible = true;
            Ab4button.Visible = true;
            label2.Visible = true;
            EnemyHealthLabel.Visible = true;

            PreviousFloorButton.Visible = false;
            NextFloorButton.Visible = false;
        }

        private void ExitBattleMode()
        {
            Ab1button.Visible = false;
            Ab2button.Visible = false;
            Ab3button.Visible = false;
            Ab4button.Visible = false;
            label2.Visible = false;
            EnemyHealthLabel.Visible = false;

            PreviousFloorButton.Visible = true;
            NextFloorButton.Visible = true;
            hasUsedBuff = false;
        }

        private async void Ab2button_Click(object sender, EventArgs e)
        {

            if (playerCharacter is Characters.Warrior warrior)
            {
                UseAbility(warrior.Ability2, warrior);
            }
            else if (playerCharacter is Characters.Archer archer)
            {
                UseAbility(archer.Ability2, archer);
            }
            else if (playerCharacter is Characters.Mage mage)
            {
                UseAbility(mage.Ability2, mage);
            }
            else if (playerCharacter is Characters.Paladin paladin)
            {
                UseAbility(paladin.Ability2, paladin);
            }
            else
            {
                ShowMessage("Invalid character or no ability assigned.");
            }
            await Task.Delay(1000);

        }

        private async void Ab3button_Click(object sender, EventArgs e)
        {
            if (playerCharacter is Characters.Warrior warrior)
            {
                UseAbility(warrior.Ability3, warrior);
            }
            else if (playerCharacter is Characters.Archer archer)
            {
                UseAbility(archer.Ability3, archer);
            }
            else if (playerCharacter is Characters.Mage mage)
            {
                UseAbility(mage.Ability3, mage);
            }
            else if (playerCharacter is Characters.Paladin paladin)
            {
                UseAbility(paladin.Ability3, paladin);
            }
            else
            {
                ShowMessage("Invalid character or no ability assigned.");
            }
            await Task.Delay(1000);
        }

        private async void Ab4button_Click(object sender, EventArgs e)
        {
            BuffCharacter(playerCharacter, 10, 60);
            if (playerCharacter is Characters.Warrior warrior)
            {
                UseAbility(warrior.Ability4, warrior);
            }
            else if (playerCharacter is Characters.Archer archer)
            {
                UseAbility(archer.Ability4, archer);
            }
            else if (playerCharacter is Characters.Mage mage)
            {
                UseAbility(mage.Ability4, mage);
            }
            else if (playerCharacter is Characters.Paladin paladin)
            {
                UseAbility(paladin.Ability4, paladin);
            }
            else
            {
                ShowMessage("Invalid character or no ability assigned.");
            }
            await Task.Delay(1000);
        }
        private void ButtonEnabled()
        { 
            Ab1button.Enabled = true;
            Ab2button.Enabled = true;
            Ab3button.Enabled = true;
            Ab4button.Enabled = true;
        }
        private void ButtonDisabled()
        {
            Ab1button.Enabled = false;
            Ab2button.Enabled = false;
            Ab3button.Enabled = false;
            Ab4button.Enabled = false;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void GetEnemyHealth(Enemy enemy)
        {
            EnemyHealthLabel.Text = enemy.Health.ToString();
            if(enemy.Health <= 0)
            {
                enemy.Health = 0;
                EnemyHealthLabel.Text = enemy.Health.ToString();
                
            }   
        }   

        private async void BattleLoop(object playerCharacter, Enemy enemy)
        {
            
            bool playerTurn = true;
            GetEnemyHealth(enemy);
            ButtonDisabled();

            while (true)
            {
                ShowMessage("It's your turn! Choose an ability:\n");
                if (playerTurn)
                {
                    ShowMessage("It's your turn! Choose an ability:\n");
                    ButtonEnabled();

                    await Task.Delay(1000);
                    Ability ability = await ChooseAbilityAsync(character);
                    DiceResultLabel.Text = diceRoll.ToString();
                    ButtonDisabled();
                    await Task.Delay(1000);
                    GetEnemyHealth(enemy);

                    if (ability != null)
                    {

                        if (enemy.Health <= 0)
                        {
                            ShowMessage($"***You defeated {enemy.Name}***\n\n");
                            ExitBattleMode(); 
                            ShowMessage($"You earned {AddGoldToPlayer()} gold!\n");
                            break;
                        }
                    }
                    playerTurn = false;
                    await Task.Delay(1000);
                    
                }
                else if (!playerTurn)
                {
                    string abilityName;
                    int abilityDamage;
                    ButtonDisabled();
                    enemy.enemy_UseAbility(new Random().Next(1, 3), out abilityName, out abilityDamage);


                    int diceRoll = RollDice();
                    DiceResultLabel.Text = diceRoll.ToString();
                    double multiplier = GetLuckMultiplier(diceRoll);

                    int enemydamage = (int)(abilityDamage * multiplier) * (1-GetPlayerArmour(playerCharacter)/100);
                    enemydamage = Math.Max(0, enemydamage);

                    ApplyDamageToPlayer(playerCharacter, enemydamage);
                   
                    ShowMessage($"{enemy.Name} used {abilityName}, dealing {enemydamage} damage!\n");

                    if (GetPlayerHealth(playerCharacter) <= 0)
                    {
                        ShowMessage("---YOU DIED---");
                        ExitBattleMode();
                        MoveTo(town);
                        TownMode();
                        HealCharacterFully(playerCharacter);
                        ResourceFully(playerCharacter);
                        UpdateHealthAndManaBars();  

                        break;
                    }
                    await Task.Delay(1000); 
                    playerTurn = true;
                }

              
            }
        }

        private void StartBattle(object playerCharacter, Enemy enemy)
        {
            if (enemy == null)
            {
                ShowMessage("No enemy encountered!");
                return;
            }
            ShowMessage($"You encountered a {enemy.Name}!");
            currentEnemy = EnemyRepository.GetEnemyByFloor(currentFloor);

            EnterBattleMode();
            BattleLoop(playerCharacter, enemy);
        }



        private void EndBattle()
        {
            ShowMessage("Battle ended!");

            Ab1button.Visible = false;
            Ab2button.Visible = false;
            Ab3button.Visible = false;
            Ab4button.Visible = false;

            PreviousFloorButton.Visible = true;
            NextFloorButton.Visible = true;

            Ab1button.Click -= null;
            Ab2button.Click -= null;
            Ab3button.Click -= null;
            Ab4button.Click -= null;
        }



        private void ApplyDamageToPlayer(object character, int damage)
        {
            if (character is Warrior warrior)
            {
                warrior.Health -= damage;
                warrior.Health = Math.Max(0, warrior.Health); 
                UpdateHealthAndManaBars();
            }
            else if (character is Archer archer)
            {
                archer.Health -= damage;
                archer.Health = Math.Max(0, archer.Health); 
                UpdateHealthAndManaBars();
            }
            else if (character is Mage mage)
            {
                mage.Health -= damage;
                mage.Health = Math.Max(0, mage.Health); 
                UpdateHealthAndManaBars();
            }
            else if (character is Paladin paladin)
            {
                paladin.Health -= damage;
                paladin.Health = Math.Max(0, paladin.Health); 
                UpdateHealthAndManaBars();
            }
            else
            {
                throw new InvalidOperationException("Unknown character type.");
            }

            ShowMessage($"{((dynamic)character).Name} took {damage} damage!\n");
        }


        private void HealPlayer(object character, int healAmount )
        {
            if (character is Warrior warrior)
            {
                warrior.Health += healAmount;
                if (warrior.Health > 100) 
                {
                    warrior.Health = 100;
                }
                ShowMessage($"{warrior.Name} healed for {healAmount} health points. Current Health: {warrior.Health}");
            }
            else if (character is Archer archer)
            {
                archer.Health += healAmount;
                if (archer.Health > 100)
                {
                    archer.Health = 100;
                }
                ShowMessage($"{archer.Name} healed for {healAmount} health points. Current Health: {archer.Health}");
            }
            else if (character is Mage mage)
            {
                mage.Health += healAmount;
                if (mage.Health > 70)
                {
                    mage.Health = 70;
                }
                ShowMessage($"{mage.Name} healed for {healAmount} health points. Current Health: {mage.Health}");
            }
            else if (character is Paladin paladin)
            {
                paladin.Health += healAmount;
                if (paladin.Health > 120) 
                {
                    paladin.Health = 120;
                }
                ShowMessage($"{paladin.Name} healed for {healAmount} health points. Current Health: {paladin.Health}");
            }
            else
            {
                ShowMessage("Unknown character type for healing.");
            }

            UpdateHealthAndManaBars();
        }
        private void ClearButtonEvents()
        {
            Ab1button.Click -= null;
            Ab2button.Click -= null;
            Ab3button.Click -= null;
            Ab4button.Click -= null;
        }
        private async Task<Ability> ChooseAbilityAsync(object character)
        {
            var tcs = new TaskCompletionSource<Ability>();
            ClearButtonEvents();
            if (character is Warrior warrior)
            {
                Ab1button.Text = warrior.Ability1.Name;
                Ab2button.Text = warrior.Ability2.Name;
                Ab3button.Text = warrior.Ability3.Name;
                Ab4button.Text = warrior.Ability4.Name;

                Ab1button.Click += (s, e) => { tcs.TrySetResult(warrior.Ability1); ClearButtonEvents(); };
                Ab2button.Click += (s, e) => { tcs.TrySetResult(warrior.Ability2); ClearButtonEvents(); };
                Ab3button.Click += (s, e) => { tcs.TrySetResult(warrior.Ability3); ClearButtonEvents(); };
                Ab4button.Click += (s, e) => { tcs.TrySetResult(warrior.Ability4); ClearButtonEvents(); };
            }
            else if (character is Archer archer)
            {
                Ab1button.Text = archer.Ability1.Name;
                Ab2button.Text = archer.Ability2.Name;
                Ab3button.Text = archer.Ability3.Name;
                Ab4button.Text = archer.Ability4.Name;

                Ab1button.Click += (s, e) => { tcs.TrySetResult(archer.Ability1); ClearButtonEvents(); };
                Ab2button.Click += (s, e) => { tcs.TrySetResult(archer.Ability2); ClearButtonEvents(); };
                Ab3button.Click += (s, e) => { tcs.TrySetResult(archer.Ability3); ClearButtonEvents(); };
                Ab4button.Click += (s, e) => { tcs.TrySetResult(archer.Ability4); ClearButtonEvents(); };
            }
            else if (character is Mage mage)
            {
                Ab1button.Text = mage.Ability1.Name;
                Ab2button.Text = mage.Ability2.Name;
                Ab3button.Text = mage.Ability3.Name;
                Ab4button.Text = mage.Ability4.Name;

                Ab1button.Click += (s, e) => { tcs.TrySetResult(mage.Ability1); ClearButtonEvents(); };
                Ab2button.Click += (s, e) => { tcs.TrySetResult(mage.Ability2); ClearButtonEvents(); };
                Ab3button.Click += (s, e) => { tcs.TrySetResult(mage.Ability3); ClearButtonEvents(); };
                Ab4button.Click += (s, e) => { tcs.TrySetResult(mage.Ability4); ClearButtonEvents(); };
            }
            else if (character is Paladin paladin)
            {
                Ab1button.Text = paladin.Ability1.Name;
                Ab2button.Text = paladin.Ability2.Name;
                Ab3button.Text = paladin.Ability3.Name;
                Ab4button.Text = paladin.Ability4.Name;

                Ab1button.Click += (s, e) => { tcs.TrySetResult(paladin.Ability1); ClearButtonEvents(); };
                Ab2button.Click += (s, e) => { tcs.TrySetResult(paladin.Ability2); ClearButtonEvents(); };
                Ab3button.Click += (s, e) => { tcs.TrySetResult(paladin.Ability3); ClearButtonEvents(); };
                Ab4button.Click += (s, e) => { tcs.TrySetResult(paladin.Ability4); ClearButtonEvents(); };
            }
            else
            {
                ShowMessage("Unknown character type.");
                tcs.TrySetResult(null);
            }

            var timeoutTask = Task.Delay(30000);
            var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

            if (completedTask == timeoutTask)
            {
                ShowMessage("Timed out! No ability chosen.");
                return null;
            }

            return await tcs.Task;
        }

    }
}