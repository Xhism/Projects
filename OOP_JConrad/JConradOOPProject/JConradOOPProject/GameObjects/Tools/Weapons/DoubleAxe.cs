namespace JConradOOPProject.GameObjects.Tools.Weapons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    public class DoubleAxe : Weapon
    {
        //constants 
        const int HitPoints = 120;

        // Fields
        private int doubleAxeHeadSharpness; //do not use it for now
        private ColorEnum color;

        // Constructors
        public DoubleAxe() 
            : base()
        { }

        public DoubleAxe(byte id, string inputName, int inputPrice)
            : base(id, inputName, inputPrice)
        {
        }

        public DoubleAxe(byte id, string inputName, int inputPrice, string imageSource)
            : base(id, inputName, inputPrice, imageSource)
        {
        }

        public DoubleAxe(byte id, string inputName, decimal inputPrice, int inputSharpness)
            : base(id, inputName, inputPrice)
        {
            this.DoubleAxeHeadSharpness = inputSharpness;
        }

        //properties
        public int DoubleAxeHeadSharpness //do not use it for now
        {
            get 
            {
                return this.doubleAxeHeadSharpness;            
            }
            set 
            {
                this.doubleAxeHeadSharpness = value;
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

        public override int Power
        {
            get
            {
                return HitPoints;
            }

        }
    }
}
