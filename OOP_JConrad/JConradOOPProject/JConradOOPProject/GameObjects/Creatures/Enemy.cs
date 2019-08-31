using JConradOOPProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JConradOOPProject.GameObjects.Creatures
{
    public abstract class Enemy: Creature, IAttack
    {
        public Enemy(string name)
            : base(name, new Position(), 0, 0)
        { }
        public Enemy(string name, Position position, int width, int height)
            : base(name, position, width, height)
        { }

        public abstract int WorthInGold
        {
            get;
        }

        public void Hit(Creature attacked)
        {
            attacked.Health -= this.GetHitPower() - attacked.GetDefencePower();
        }
    }
}
