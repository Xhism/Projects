namespace JConradOOPProject.GameObjects.Tools.Skills
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    public class Skill : Item
    {
        //fields
        private decimal damageCoeff;
        private decimal hitRateCoeff;
        private byte levelOfSkill;
        private decimal defenceCoeff;
        private bool alwaysHit;

        //constructors
        public Skill(byte id, string inputName, decimal inputPrice)
            : base(id, inputName, inputPrice)
        {
            this.DamageCoeff = 1.0m;
            this.DamageCoeff = 1.0m;
            this.HitRateCoeff = 1.0m;
        }

        public Skill(byte id, string inputName, decimal inputPrice, decimal damage, decimal hitrate)
            : base(id, inputName, inputPrice)
        {
            this.DamageCoeff = damage;
            this.DefenceCoeff = 1.0m;
            this.HitRateCoeff = hitrate;
        }

        public Skill(byte id, string inputName, decimal inputPrice, decimal defence)
            : this(id, inputName, inputPrice)
        {
        }

        public Skill(byte id, string inputName, decimal inputPrice, decimal damage, decimal hitrate, decimal defence)
            : base(id, inputName, inputPrice)
        {
            this.DamageCoeff = damage;
            this.DefenceCoeff = defence;
            this.HitRateCoeff = hitrate;
        }

        public Skill (byte id, string inputName, decimal inputPrice, decimal damage, decimal hitrate, decimal defence, string imageSource)
            : base (id, inputName, inputPrice, imageSource)
        {
            this.DamageCoeff = damage;
            this.DefenceCoeff = defence;
            this.HitRateCoeff = hitrate;
        }
        
        //properties
        public decimal DefenceCoeff
        {
            get { return this.defenceCoeff; }
            set { this.defenceCoeff = value; }

        }

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


    }
}
