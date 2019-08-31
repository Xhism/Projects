namespace JConradOOPProject.GameObjects.Tools.Weapons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    public class Axe : Weapon
    {
        //constants 
        const int HitPoints = 100;

        // Fields
        private int axeHeadSharpness; //do not use it for now
        private ColorEnum color;

        // Constructors
        public Axe() 
            : base()
        { }

        public Axe(byte id, string inputName, int inputPrice)
            : base(id, inputName, inputPrice)
        {
        }

        public Axe(byte id, string inputName, int inputPrice, string imageSource)
            : base(id, inputName, inputPrice, imageSource)
        {
        }

        public Axe(byte id, string inputName, decimal inputPrice, int inputSharpness)
            : base(id, inputName, inputPrice)
        {
            this.AxeHeadSharpness = inputSharpness;
        }

        //properties

        public int AxeHeadSharpness //do not use it for now
        {
            get 
            {
                return this.axeHeadSharpness;            
            }
            set 
            {
                this.axeHeadSharpness = value;
            }
        }

        public ColorEnum Color
        {
            get 
            {
                return this.color;
            }
            set
            {
                try
                {
                    this.color = value;
                }
                catch (Exception)
                {                    
                    throw new Exception("Invalid color");
                }                
            }

        }
        public override string ToString()
        {
            return base.ToString();
        }

        public override int Power
        {
            get
            {
                return HitPoints;
            }

        }
    }
}
