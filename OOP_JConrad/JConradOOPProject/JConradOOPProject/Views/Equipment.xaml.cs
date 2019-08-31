using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using JConradOOPProject.ViewModels;

namespace JConradOOPProject.Views
{
    using GameObjects.Tools;
    using GameObjects.Tools.Skills;
    using GameObjects.Tools.Weapons;
    using GameObjects.Tools.Shields;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Interaction logic for Equipment.xaml
    /// </summary>
    public partial class Equipment : UserControl
    {
        private GameEngine parent;

        public Equipment(GameEngine parent)
        {
            InitializeComponent();
            this.parent = parent;
            byte id = 0;

            FillInventoryTab(ref id);
            FillSkillsTab(ref id);
            FillShopTab(ref id);
        }

        /// <summary>
        /// Process the OnClick event of button "Use"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUse_Click(object sender, RoutedEventArgs e)
        {
            this.ValidateListBox(Inventory);
            
            Equip((Item)this.Inventory.SelectedItem);
        }

        /// <summary>
        /// Button SLOT
        /// Gets the selected skill from the listbox and assigns it to the respective slot (1, 2 or 3 accordingly).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSlot_Click(object sender, RoutedEventArgs e)
        {
            this.ValidateListBox(Skills);

            Button slot = (Button)sender;

            int slotNumber = 0;

            if (slot.Name.Equals("Slot1", StringComparison.Ordinal) == true)
                slotNumber = 0;
            if (slot.Name.Equals("Slot2", StringComparison.Ordinal) == true)
                slotNumber = 1;
            if (slot.Name.Equals("Slot3", StringComparison.Ordinal) == true)
                slotNumber = 2;

            Equip((Item)this.Skills.SelectedItem, slotNumber);
        }

        /// <summary>
        /// Button BUY
        /// Performs buying operation with the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBuy_Click(object sender, RoutedEventArgs e)
        {
            this.ValidateListBox(Shop);

            try
            {
                Buy((Item)this.Shop.SelectedItem);
            }
            catch (ArgumentOutOfRangeException ex) //TODO: custom exception here
            {
                string msg = string.Format("Your gold ({0}) is not enough to buy {1} ({2})",
                    parent.Player.GoldAmount, ((Item)this.Shop.SelectedItem).Name,
                    ((Item)this.Shop.SelectedItem).Price);
                MessageBox.Show(msg, "Not enough money");
            }
        }

        /// <summary>
        /// Button BACK
        /// Returns the player to the game map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.SwitchWindowContent(new GameMap(this.parent), true);
        }

        /// <summary>
        /// Fill Inventory tab with stock weapons and armors
        /// </summary>
        /// <param name="items"></param>
        private void FillInventoryTab(ref byte id)
        {
            if (parent.Inventory == null)
            {
                parent.Inventory = new ObservableCollection<Item>();
                parent.Inventory.Add(new Axe(id++, "Axe", inputPrice: 65, imageSource: "item"));
                parent.Inventory.Last().Description = "A very sharp and powerful axe.  Use it carefully.";
                parent.Inventory.Add(new Chopper(id++, "Chopper", inputPrice: 100, imageSource: "item"));
                parent.Inventory.Last().Description = "Short deadly cold weapon for severe enemies.";
                parent.Inventory.Add(new Cutter(id++, "Cutter", inputPrice: 200, imageSource: "item"));
                parent.Inventory.Last().Description = "Use it kill enemies.";
                parent.Inventory.Add(new ChainArmour(id++, "Chain Armour", inputPrice: 125, imageSource: "item"));
                parent.Inventory.Last().Description = "Protects you against hits of your enemies.";
                parent.Inventory.Add(new SweatCloth(id++, "Sweat Cloth", inputPrice: 85, imageSource: "item"));
                parent.Inventory.Last().Description = "Protects you against hits and odour.";
            }

            Inventory.ItemsSource = parent.Inventory;


            //TODO: add handling of property SpecialValue, where?
            //if (item is Weapon)
            //    specialValue = String.Concat(((Weapon)item.HitPoints), " Attack");
            //else if (item is Shield)
            //    specialValue = String.Concat(((Shield) item).DefPoints, " Defence");
        }

        /// <summary>
        /// Fill Skills tab with stock skills
        /// </summary>
        /// <param name="items"></param>
        private void FillSkillsTab(ref byte id)
        {
            if (parent.Skills == null)
            {
                parent.Skills = new ObservableCollection<Skill>();
                
                parent.Skills.Add(new Skill(id++, "Agility", 85, 1m, 5m, 1m, "item"));
                parent.Skills.Last().Description = "This skill increases your hit rate";
                
                parent.Skills.Add(new Skill(id++, "Strength", 150, 2m, 2m, 2m, "item"));
                parent.Skills.Last().Description = "This skill increases your defense and attack power.";

                parent.Skills.Add(new Skill(id++, "Dexterity", 150, 1m, 2m, 3m, "item"));
                parent.Skills.Last().Description = "This skill increases your mental and physical powers";
            }

            Skills.ItemsSource = parent.Skills;

            //TODO: add handling of specialValue, where?
            //string specialValue = String.Concat("Damage Coef: ", skill.DamageCoeff, skill.DefenceCoeff, " Defence Coef; ");
        }

        /// <summary>
        /// Fill the shop with stock weapons, armors and skills
        /// </summary>
        /// <param name="items"></param>
        private void FillShopTab(ref byte id)
        {
            if (parent.Shop == null)
            {
                parent.Shop = new ObservableCollection<Item>();

                parent.Shop.Add(new DoubleAxe(id++, "Double Axe", 100, imageSource: "item"));
                parent.Shop.Last().Description = "Double size axe for better satisfaction.";
                parent.Shop.Add(new Knife(id++, "Knife", 100, imageSource: "item"));
                parent.Shop.Last().Description = "Short cold weapon for quick stabbing";
                parent.Shop.Add(new Skill(id++, "Some skill 3", 85, 1m, 1m, 1m, "item"));
                parent.Shop.Last().Description = "Skill enriching your perfomance.";
                parent.Shop.Add(new Skill(id++, "Some skill 4", 150, 2m, 2m, 2m, "item"));
                parent.Shop.Last().Description = "Enhances your attack and defence.";
            }

            Shop.ItemsSource = parent.Shop;

            //TODO: add handling of SpecialValue
            //string specialValue = String.Concat("Damage Coef: ", skill.DamageCoeff, skill.DefenceCoeff, " Defence Coef; ");

            //if (item is Weapon)
            //    specialValue = String.Concat(((Weapon)item.HitPoints), " Attack");
            //else if (item is Shield)
            //    specialValue = String.Concat(((Shield) item).DefPoints, " Defence");
        }

        /// <summary>
        /// Handle equipping the hero with a weapon, shield or skill
        /// </summary>
        public void Equip(Item item, int slot=0)
        {
            if (item is Weapon)
            {
                //take the weapon that the hero currently has and place it in the inventory
                if (parent.Player.CurrentWeapon != null)
                {
                    parent.Inventory.Add(parent.Player.CurrentWeapon);
                }

                parent.Player.CurrentWeapon = (Weapon)item;
                parent.Inventory.Remove(item);
            }
            else if (item is Shield)
            {
                //take the shield that the hero currently has and place it in the inventory
                if (parent.Player.CurrentShield != null)
                {
                    parent.Inventory.Add(parent.Player.CurrentShield);
                }

                parent.Player.CurrentShield = (Shield)item;
                parent.Inventory.Remove(item);
            }
            else if (item is Skill)
            {
                if (parent.Player.CurrentSkills[slot] != null)
                {
                    parent.Skills.Add(parent.Player.CurrentSkills[slot]);
                }
                parent.Player.CurrentSkills[slot] = (Skill)item;
                parent.Skills.Remove((Skill)item);
            }
        }

        /// <summary>
        /// Buy a weapon/shield/skill from the shop
        /// </summary>
        public void Buy(Item item)
        {
            if (item.Price > parent.Player.GoldAmount)
            {
                //TODO: here we can use custom exception
                throw new ArgumentOutOfRangeException(string.Format("Not enough gold to buy {0}", item.Name));
            }
            if (item is Weapon || (item is Shield))
            {
                parent.Inventory.Add(item);
            }
            else if (item is Skill)
            {
                parent.Skills.Add((Skill)item);
            }

            parent.Player.GoldAmount -= (int)item.Price;
            parent.Shop.Remove(item);
        }

        /// <summary>
        /// Checks whether a listbox has a selected item. If not, informs the user with a message.
        /// </summary>
        /// <param name="target"></param>
        private void ValidateListBox(ListBox target)
        {
            if (target.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item.", "Select An Item");
                return;
            }
        }
    }
}
