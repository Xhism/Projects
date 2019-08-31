namespace JConradOOPProject.ViewModels
{
    using JConradOOPProject.Commands;
    using JConradOOPProject.GameObjects.Creatures;
    using JConradOOPProject.GameObjects.Tools;
    using JConradOOPProject.GameObjects.Tools.Shields;
    using JConradOOPProject.GameObjects.Tools.Skills;
    using JConradOOPProject.GameObjects.Tools.Weapons;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Collections.Generic;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Input;

    public class GameEngine : BaseViewModel
    {
        private static readonly Random randomGen = new Random();
        private string playerName;
        private Lumberjack player;
        private Enemy enemy;
        private bool playerCanAttack;
        private bool playerIsOnMainMap;
        private ICommand attack;
        private ICommand run;

        public GameEngine()
        {
        }

        //Properties to be bound
        public ICommand Attack
        {
            get
            {
                if (this.attack == null)
                {
                    attack = new RelayCommand(
                        this.HandlePlayerAttack,
                        this.CanPlayerAttack
                        );
                }
                return this.attack;
            }
        }
        public ICommand Run
        {
            get
            {
                if (this.run == null)
                {
                    run = new RelayCommand(
                        this.HandleRun,
                        this.CanRun
                        );
                }
                return this.run;
            }
        }
        public bool PlayerIsOnMainMap
        {
            get
            {
                return this.playerIsOnMainMap;
            }
            set
            {
                this.playerIsOnMainMap = value;
                OnPropertyChanged("PlayerIsOnMainMap");
            }
        }
        public bool PlayerCanAttack
        {
            get
            {
                return this.playerCanAttack;
            }
            set
            {
                this.playerCanAttack = value;
                OnPropertyChanged("PlayerCanAttack");
            }
        }
        public int AttackWithSkill //0, 1, 2
        {
            get
            {
                return this.player.SkillIndex;
            }
            set
            {
                this.player.SkillIndex = value;
            }
        }
        public Weapon CurrentWeapon
        {
            get
            {
                return this.player.CurrentWeapon;
            }
        }
        public Shield CurrentShield
        {
            get
            {
                return this.player.CurrentShield;
            }
        }
        public int PlayerGold
        {
            get
            {
                return this.player.GoldAmount;
            }
            set
            {
                this.player.GoldAmount = value;
            }
        }
        public decimal PlayerHealth
        {
            get
            {
                return this.player.Health;
            }
        }
        public decimal EnemyHealth
        {
            get
            {
                return this.enemy.Health;
            }
        }
        public int PlayerLevel
        {
            get
            {
                return this.player.CurrentLevel;
            }
            set
            {
                this.player.CurrentLevel = value;
            }
        }
        public int PlayerExperience
        {
            get
            {
                return this.player.CurrentExperience;
            }
            set
            {
                this.player.CurrentExperience = value;
            }
        }
        public string PlayerName
        {
            get
            {
                return this.player.Name;
            }
            set
            {
                this.playerName = value;
            }
        }

        public Lumberjack Player
        {
            get
            {
                return this.player;
            }
        }

        public ObservableCollection<Item> Shop { get; set; }
        public ObservableCollection<Item> Inventory { get; set; }
        public ObservableCollection<Skill> Skills { get; set; }

        /// <summary>
        /// Performs game initialization
        /// </summary>
        public void Initialize()
        {
            this.InitializeEnemy();
            this.InitializePlayer();
        }

        public void StartGame()
        {
            while (this.player.IsAlive && this.enemy.IsAlive)
            {
                if (!this.PlayerCanAttack)
                {
                    EnemyAttack();
                }
            }
        }

        private void InitializeEnemy()
        {
            int i = randomGen.Next(0, 3);
            switch (i)
            {
                case (0):
                    this.enemy = new Bear("");
                    break;
                case (1):
                    this.enemy = new ForestGhost("");
                    break;
                default:
                    this.enemy = new EnemyLumberjack("");
                    break;
            }
        }

        private void InitializePlayer()
        {
            this.player = new Lumberjack(this.playerName, GameParameters.InitialLevel, GameParameters.InitialExperience, GameParameters.InitialGold);
            this.PlayerCanAttack = (this.player.Speed > this.enemy.Speed);
        }

        void HandlePlayerAttack(object player)
        {
            int currentAttackHitCoeff = randomGen.Next(1, 101);
            decimal skillHitCoeff = this.player.CurrentSkills[this.player.SkillIndex].HitRateCoeff;
            if (currentAttackHitCoeff * skillHitCoeff > GameParameters.PlayerHitChance)
            {
                this.player.Hit(this.enemy);
                if (this.enemy.IsAlive)
                {
                    this.PlayerCanAttack = false;
                }
            }
            OnPropertyChanged("EnemyHealth");
            OnPropertyChanged("PlayerGold");
            OnPropertyChanged("PlayerExperience");
        }

        bool CanPlayerAttack(object player)
        {
            return this.PlayerCanAttack;
        }

        void EnemyAttack()
        {
            int currentAttackHitCoeff = randomGen.Next(1, 101);
            if (currentAttackHitCoeff > GameParameters.EnemyHitChance)
            {
                this.enemy.Hit(this.player);
            }

            if (this.player.IsAlive)
            {
                this.PlayerCanAttack = true;
            }
            OnPropertyChanged("PlayerHealth");
        }

        void HandleRun(object player)
        { }

        bool CanRun(object player)
        {
            return true;
        }

        public void SerializeSkills(string path)
        {
            try
            {
                using (Stream stream = File.Open(path, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, Skills);
                }
            }
            catch (IOException ex)
            {
            }
        }
        public void SerializeInventory(string path)
        {
            try
            {
                using (Stream stream = File.Open(path, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, Inventory);
                }
            }
            catch (IOException ex)
            {
            }
        }
        public void SerializeShop(string path)
        {
            try
            {
                using (Stream stream = File.Open(path, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, Shop);
                }
            }
            catch (IOException ex)
            {
            }
        }

        public ObservableCollection<Skill> DeserializeSkills(string path)
        {
            try
            {
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    ObservableCollection<Skill> skills = (ObservableCollection<Skill>)bin.Deserialize(stream);

                    return skills;
                }
            }
            catch (IOException)
            {
                return null;
            }
        }
        public ObservableCollection<Item> DeserializeInventory(string path)
        {
            try
            {
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    ObservableCollection<Item> inventory = (ObservableCollection<Item>)bin.Deserialize(stream);

                    return inventory;
                }
            }
            catch (IOException)
            {
                return null;
            }
        }
        public ObservableCollection<Item> DeserializeShop(string path)
        {
            try
            {
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    ObservableCollection<Item> shop = (ObservableCollection<Item>)bin.Deserialize(stream);

                    return shop;
                }
            }
            catch (IOException)
            {
                return null;
            }
        }


        public void Save()
        {
            try
            {
                //Save paths for later use
                string saveFilePath = @".\saves\" + PlayerName + ".txt";
                string inventoryPath = @".\saves\" + PlayerName + "Inventory.bin";
                string skillsPath = @".\saves\" + PlayerName + "Skills.bin";
                string shopPath = @".\saves\" + PlayerName + "Shop.bin";

                //Delete files, incase overwriting previous save
                File.Delete(saveFilePath);
                File.Delete(inventoryPath);
                File.Delete(skillsPath);
                File.Delete(shopPath);

                //Save data to files
                SerializeInventory(inventoryPath);
                SerializeSkills(skillsPath);
                SerializeShop(shopPath);

                StreamWriter writeSave = new StreamWriter(saveFilePath);

                //Write current name, current level and experience, current gold
                writeSave.WriteLine(PlayerName);
                writeSave.WriteLine(PlayerLevel);
                writeSave.WriteLine(PlayerExperience);
                writeSave.WriteLine(PlayerGold);

                //Write paths for the load method
                writeSave.WriteLine("@\"" + inventoryPath + "\"");
                writeSave.WriteLine("@\"" + skillsPath + "\"");
                writeSave.WriteLine("@\"" + shopPath + "\"");

                writeSave.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
