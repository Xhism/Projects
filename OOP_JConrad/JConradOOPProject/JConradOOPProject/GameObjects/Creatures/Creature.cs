
using JConradOOPProject.GameObjects.Tools.Shields;
using JConradOOPProject.GameObjects.Tools.Weapons;
using JConradOOPProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JConradOOPProject.GameObjects.Creatures
{
    public abstract class Creature : GameObject, IMovable
    {
        public virtual decimal InitialHealth
        { 
            get
            {
                return 100;
            }
        }
        public virtual decimal InitialAttackPower
        {
            get
            {
                return 100;
            }
        }
        public virtual decimal InitialDefencePower
        {
            get
            {
                return 100;
            }
        }
        public virtual int InitialPrecision
        {
            get
            {
                return 100;
            }
        }
        public virtual int InitialSpeed
        {
            get
            {
                return 100;
            }
        }

        public string Name { get; set; }
        public decimal Health { get; set; }
        public decimal AttackPower { get; set; }
        public decimal DefencePower { get; set; }
        public int Precision { get; set; }
        public int Speed { get; set; }
        public Weapon CurrentWeapon { get; set; }
        public Shield CurrentShield { get; set; }
        public bool IsAlive
        {
            get
            {
                return this.Health > 0;
            }
        }
       
        public Creature() : this(String.Empty, new Position(), 0, 0)
        { }
        public Creature(string name, Position position, int width, int heigth) : base(position, width, heigth)
        {
            this.Name = name;
            this.Health = InitialHealth;
            this.AttackPower = InitialAttackPower;
            this.DefencePower = InitialDefencePower;
            this.Precision = InitialPrecision;
            this.Speed = InitialSpeed;
        }

        public void Move(int deltaX, int deltaY)
        {
            this.Position.X += deltaX;
            this.Position.Y += deltaY;
        }

        public virtual decimal GetHitPower()
        {
            return this.AttackPower + this.CurrentWeapon.Power;
        }

        public virtual decimal GetDefencePower()
        {
            return this.DefencePower + this.CurrentShield.DefencePower;
        }
    }
}
