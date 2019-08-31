using JConradOOPProject.GameObjects.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JConradOOPProject.Interfaces
{
    public interface IAttack
    {
        void Hit(Creature creature);
    }
}
