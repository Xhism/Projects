namespace JConradOOPProject.GameObjects.Tools.Weapons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    public class Knife : Weapon
    {
         //constants 
        const int HitPoints = 20;

        // Fields
        private ColorEnum color;

        // Constructors
        public Knife() 
            : base()
        { }

        public Knife(byte id, string inputName, int inputPrice)
            : base(id, inputName, inputPrice)
        {
        }

        public Knife(byte id, string inputName, int inputPrice, string imageSource)
            : base(id, inputName, inputPrice, imageSource)
        {
        }
        
        //properties        
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
