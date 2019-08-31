using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JConradOOPProject.GameObjects.Creatures
{
    public class EnemyLumberjack : Enemy
    {
        public EnemyLumberjack(string name)
            : base(name)
        { }

        public override int WorthInGold
        {
            get { return 100; }
        }
    }
}
