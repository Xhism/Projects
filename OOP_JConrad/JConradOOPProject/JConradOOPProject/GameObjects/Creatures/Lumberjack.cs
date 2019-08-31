using JConradOOPProject.GameObjects.Tools.Shields;
using JConradOOPProject.GameObjects.Tools.Skills;
using JConradOOPProject.GameObjects.Tools.Weapons;
using JConradOOPProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JConradOOPProject.ViewModels;

namespace JConradOOPProject.GameObjects.Creatures
{
    public partial class Lumberjack : Creature, IAttack
    {
        public int CurrentLevel { get; set; }
        public int CurrentExperience { get; set; }
        public int GoldAmount { get; set; }

        public List<Skill> CurrentSkills { get; private set; } // max 3 skills; set is private because 
        // we must add skills only from one place to make sure the slots are exactly 3
        public int SkillIndex { get; set; } //0, 1, 2: with which skill the attack is performed

        public Lumberjack(string name, int level, int experience, int goldAmount)
            : this(name, new Position(), 0, 0, level, experience, goldAmount)
        { }
        public Lumberjack(string name, Position position, int width, int heigth, int level, int experience, int goldAmount)
            : base(name, position, width, heigth)
        {
            this.CurrentLevel = level;
            this.CurrentExperience = experience;
            this.GoldAmount = goldAmount;
            InitializeSkillSlots();
            this.SkillIndex = 0;
        }

        public override decimal GetHitPower()
        {
            return base.GetHitPower() * this.CurrentSkills[this.SkillIndex].DamageCoeff;
        }

        public override decimal GetDefencePower()
        {
            return base.GetDefencePower() * this.CurrentSkills[this.SkillIndex].DefenceCoeff;
        }

        public void Hit(Creature attacked)
        {
            attacked.Health -= this.GetHitPower() - attacked.GetDefencePower();
            Enemy enemy = attacked as Enemy;
            this.GoldAmount += enemy.WorthInGold;
            this.CurrentExperience += GameParameters.GetExperienceReward(this.CurrentLevel);
            this.CurrentLevel = GameParameters.GetCurrentLevel(this.CurrentExperience);
        }

        private void InitializeSkillSlots()
        {
            this.CurrentSkills = new List<Skill>();
            this.CurrentSkills.Add(null); //we want exactly 3 slots, must be initialised before entering the inventory
            this.CurrentSkills.Add(null);
            this.CurrentSkills.Add(null);
        }
    }
}
