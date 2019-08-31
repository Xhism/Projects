﻿namespace RpgGame.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using RpgGame.Interfaces;

    public partial class PlayerInventory : Form
    {
        private ShopInventory itemShop;
        private Label playerStats;
        private Label statsBox;

        private IList<Position> inventoryItemPositions = new List<Position>();
        private IList<Position> equippedItemPositions = new List<Position>();

        public IList<IItem> inventory = GameEngine.PlayerCharacter.Inventory;
        private IList<IItem> equipped = GameEngine.PlayerCharacter.Equiped;

        private IList<PictureBox> inventoryPics = new List<PictureBox>();
        private IList<PictureBox> equippedPics = new List<PictureBox>();

        public PlayerInventory()
        {
            InitializeComponent();
            
            this.equippedItemPositions.Add(new Position(x: 82, y: 10)); // head
            this.equippedItemPositions.Add(new Position(x: 12, y: 80)); // weapon
            this.equippedItemPositions.Add(new Position(x: 82, y: 80)); // chainmail
            this.equippedItemPositions.Add(new Position(x: 152, y: 80)); // gloves
            this.equippedItemPositions.Add(new Position(x: 82, y: 150)); // boots
            
            this.inventoryItemPositions.Add(new Position(x: 12, y: 220));
            this.inventoryItemPositions.Add(new Position(x: 82, y: 220));
            this.inventoryItemPositions.Add(new Position(x: 152, y: 220));
            this.inventoryItemPositions.Add(new Position(x: 222, y: 220));
            this.inventoryItemPositions.Add(new Position(x: 292, y: 220));
            this.inventoryItemPositions.Add(new Position(x: 12, y: 290));
            this.inventoryItemPositions.Add(new Position(x: 82, y: 290));
            this.inventoryItemPositions.Add(new Position(x: 152, y: 290));
            this.inventoryItemPositions.Add(new Position(x: 222, y: 290));
            this.inventoryItemPositions.Add(new Position(x: 292, y: 290));

            playerStats = new Label();
            playerStats.Left = 250;
            playerStats.Top = 20;
            playerStats.Width = 100;
            playerStats.Height = 150;
            playerStats.BackColor = Color.Transparent;
            playerStats.ForeColor = Color.White;
            playerStats.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            Controls.Add(playerStats);

            statsBox = new Label();
            statsBox.Width = 104;
            statsBox.Height = 130;
            statsBox.BackColor = Color.Black;
            statsBox.ForeColor = Color.White;
            statsBox.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            statsBox.TextAlign = ContentAlignment.TopCenter;
            statsBox.BackColor = Color.FromArgb(150, 0, 0, 0);
            Controls.Add(statsBox);
            statsBox.Hide();
        }

        private void PlayerInventory_Load(object sender, EventArgs e)
        {
            RefreshPlayerStats();
            DrawItems();
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            string[] picMeta = p.Name.Split('-');
            string type = picMeta[0];
            int id = int.Parse(picMeta[1]);

            if (type == "inventory")
            {
                string itemType = GetItemType(inventory[id]);

                int slot = -1;

                switch (itemType)
                {
                    case "Helmet":
                        slot = 0;
                        break;
                    case "Weapon":
                        slot = 1;
                        break;
                    case "Chainmail":
                        slot = 2;
                        break;
                    case "Gloves":
                        slot = 3;
                        break;
                    case "Boots":
                        slot = 4;
                        break;
                }

                try
                {
                    // The last parameter seems redundant. It can never be false and the method should be changed.
                    GameEngine.PlayerCharacter.Equip(inventory[id], slot, true);
                    GameEngine.PlayerCharacter.UnEquip(equipped[slot]);

                    IItem tempItem;

                    tempItem = equipped[slot];
                    equipped[slot] = inventory[id];
                    inventory[id] = tempItem;

                    PictureBox tempPic;

                    tempPic = equippedPics[slot];
                    equippedPics[slot] = inventoryPics[id];
                    inventoryPics[id] = tempPic;

                    int tempX = equippedPics[slot].Left;
                    int tempY = equippedPics[slot].Top;

                    equippedPics[slot].Left = inventoryPics[id].Left;
                    equippedPics[slot].Top = inventoryPics[id].Top;
                    inventoryPics[id].Left = tempX;
                    inventoryPics[id].Top = tempY;

                    equippedPics[slot].Name = "equipped-" + slot;
                    inventoryPics[id].Name = "inventory-" + id;

                    GameEngine.PlayerCharacter.Inventory = inventory;
                    GameEngine.PlayerCharacter.Equiped = equipped;
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("You don't have required level for this item!");
                }

                if (GameEngine.BattleScreen != null)
                {
                    GameEngine.BattleScreen.RefreshStats();
                }

                RefreshPlayerStats();
            }
        }

        private void PictureBox_MouseLeave(object sender, EventArgs e)
        {
            statsBox.Hide();
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            string[] picMeta = p.Name.Split('-');
            string type = picMeta[0];
            int id = int.Parse(picMeta[1]);

            if (type == "inventory")
            {
                RefreshStatsBox(inventory[id], inventoryItemPositions[id], type, id);
                statsBox.Top = inventoryItemPositions[id].Y - statsBox.Height;
            }
            else if (type == "equipped")
            {
                RefreshStatsBox(equipped[id], equippedItemPositions[id], type, id);
                statsBox.Top = equippedItemPositions[id].Y + 60;
            }
        }

        // The last two parameters are never used?
        private void RefreshStatsBox(IItem item, Position pos, string type, int id)
        {
            statsBox.Left = pos.X - 20;
            statsBox.Show();

            statsBox.Text = item.Name
                        + "\nStr: " + item.Strength
                        + "\nInt: " + item.Intelligence
                        + "\nDex: " + item.Dexterity
                        + "\nVit: " + item.Vitality
                        + "\nAttack: " + item.AttackPoints
                        + "\nDefence: " + item.DefencePoints
                        + "\nHP: " + item.HitPoints
                        + "\nReq. Level: " + item.LevelRequirement;
        }

        private void RefreshPlayerStats()
        {
            playerStats.Text = "Level: " + GameEngine.PlayerCharacter.Level
                            + "\nStrength: " + GameEngine.PlayerCharacter.Strength
                            + "\nIntelligence: " + GameEngine.PlayerCharacter.Intelligence
                            + "\nDexterity: " + GameEngine.PlayerCharacter.Dexterity
                            + "\nVitality: " + GameEngine.PlayerCharacter.Vitality
                            + "\nAttack: " + GameEngine.PlayerCharacter.Attack()
                            + "\nDefence: " + GameEngine.PlayerCharacter.Defend()
                            + "\nHit Points: " + GameEngine.PlayerCharacter.MaxHitPoints;
        }

        private void DrawImages(PictureBox pictureBox, Image image, Position position, string id)
        {
            pictureBox.Image = image;
            pictureBox.Width = image.Width;
            pictureBox.Height = image.Height;
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Left = position.X;
            pictureBox.Top = position.Y;
            pictureBox.Click += PictureBox_Click;
            pictureBox.MouseEnter += PictureBox_MouseEnter;
            pictureBox.MouseLeave += PictureBox_MouseLeave;
            pictureBox.Name = id;
            this.Controls.Add(pictureBox);
        }

        private void DrawItems()
        {
            int counter = 0;
            if (inventory != null)
            {
                foreach (IItem item in inventory)
                {
                    // This check should be redundant, but it seems WF has problems and exceptions are thrown in some cases without it.
                    if (counter < 10)
                    {
                        PictureBox pic = new PictureBox();
                        inventoryPics.Add(pic);
                        DrawImages(pic, GetImage(item), inventoryItemPositions[counter], "inventory-" + counter);
                    }
                    
                    counter++;
                }
            }
            
            counter = 0;
            if (this.equipped != null)
            {
                foreach (IItem item in this.equipped)
                {
                    // This check should be redundant, but it seems WF has problems and exceptions are thrown in some cases without it.
                    if (counter < 5)
                    {
                        PictureBox pic = new PictureBox();
                        equippedPics.Add(pic);
                        DrawImages(pic, GetImage(item), equippedItemPositions[counter], "equipped-" + counter);
                    }

                    counter++;
                }
            }
        }

        private Image GetImage(IItem item)
        {
            switch (item.Picture)
            {
                case Pictures.HealingPotion:
                    return Properties.Resources.healing_potion;
                case Pictures.Scroll:
                    return Properties.Resources.teleport_scroll;
                case Pictures.WarriorHelmet:
                    return Properties.Resources.head_warrior;
                case Pictures.MageHelmet:
                    return Properties.Resources.head_mage;
                case Pictures.RogueHelmet:
                    return Properties.Resources.head_rogue;
                case Pictures.WarriorChainmail:
                    return Properties.Resources.chaimail_warrior;
                case Pictures.MageChainmail:
                    return Properties.Resources.chaimail_mage;
                case Pictures.RogueChainmail:
                    return Properties.Resources.chaimail_rogue;
                case Pictures.WarriorsGloves:
                    return Properties.Resources.gloves_warrior;
                case Pictures.MageGloves:
                    return Properties.Resources.gloves_mage;
                case Pictures.RogueGLoves:
                    return Properties.Resources.gloves_rogue;
                case Pictures.WarriorBoots:
                    return Properties.Resources.boots_warrior;
                case Pictures.MageBoots:
                    return Properties.Resources.boots_mage;
                case Pictures.RogueBoots:
                    return Properties.Resources.boots_rogue;
                case Pictures.BasickStaff:
                    return Properties.Resources.weapon_staff_basic;
                case Pictures.MidStaff:
                    return Properties.Resources.weapon_staff_mid;
                case Pictures.RareStaff:
                    return Properties.Resources.weapon_staff_rare;
                case Pictures.BasickSword:
                    return Properties.Resources.weapon_sword_basic;
                case Pictures.MidSword:
                    return Properties.Resources.weapon_sword_mid;
                case Pictures.RareSword:
                    return Properties.Resources.weapon_sword_rare;
                case Pictures.BasickAxe:
                    return Properties.Resources.weapon_axe_basic;
                case Pictures.MidAxe:
                    return Properties.Resources.weapon_axe_mid;
                case Pictures.RareAxe:
                    return Properties.Resources.weapon_axe_rare;
                case Pictures.BasickDager:
                    return Properties.Resources.weapon_dager_basic;
                case Pictures.MidDager:
                    return Properties.Resources.weapon_dager_mid;
                case Pictures.RareDager:
                    return Properties.Resources.weapon_dager_rare;
                default:
                    return Properties.Resources.healing_potion;
                //throw new NoPictureException();
            }
        }

        private static string GetItemType(IItem item)
        {
            string type = item.GetType().ToString();
            string[] itemMeta = type.Split('.');
            return itemMeta[itemMeta.Length - 1];
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        // This code reduces the flickering when loading the forms.
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}
