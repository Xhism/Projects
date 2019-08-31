using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JConradOOPProject.GameObjects.Tools.Skills
{
    class oldAttack : Skill
    {
        //fields
        private byte levelOfSkill;
        private bool alwaysHit;
        private decimal damageCoeff;
        private decimal hitRateCoeff;

        //constructors
        public oldAttack(byte id, string inputName, decimal inputPrice)
            : base(id, inputName, inputPrice)
        {
            this.HitRateCoeff = 1.0m;
        }

        public oldAttack(byte id, string inputName, decimal inputPrice, decimal hitRate)
            : base(id, inputName, inputPrice)
        {
            this.HitRateCoeff = hitRate;
        }

        public oldAttack(byte id, string inputName, decimal inputPrice, byte inputLevel)
            : base(id, inputName, inputPrice)
        {
            this.levelOfSkill = inputLevel;
            this.HitRateCoeff = 1.0m;
        }

        //properties
        public bool AlwaysHit
        {
            get
            {
                return this.alwaysHit;
            }
            set 
            {
                this.alwaysHit = true;
            }
        }

        public decimal DamageCoeff
        {
            get
            {
                return this.damageCoeff;
            }
            set
            {
                this.damageCoeff = value;
            }
        }

        public decimal HitRateCoeff
        {
            get
            {
                return this.hitRateCoeff;
            }
            set
            {
                this.hitRateCoeff = value;
            }
        }

        //methods

    }
}
