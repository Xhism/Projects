using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JConradOOPProject.GameObjects.Creatures
{
    public class ForestGhost: Enemy
    {
        public ForestGhost(string name)
            : base(name)
        { }
        public override int WorthInGold
        {
            get { return 50; }
        }
    }
}
