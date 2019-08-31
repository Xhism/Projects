using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JConradOOPProject.GameObjects.Tools.Skills
{
    class oldDefence : Skill
    {
        //fields
        private byte levelOfSkill;
        private decimal defenceCoeff;

        //constructors
        public oldDefence(byte id, string inputName, decimal inputPrice)
            : base(id, inputName, inputPrice)
        { }

        public oldDefence(byte id, string inputName, decimal inputPrice, byte inputLevel)
            : base(id, inputName, inputPrice)
        {
            this.levelOfSkill = inputLevel;
        }

        //properties
        public decimal DefenceCoeff
        {
            get { return this.defenceCoeff; }
            set { this.defenceCoeff = value; }

        }

        //methods

    }
}
