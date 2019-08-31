namespace JConradOOPProject.GameObjects.Tools.Weapons
{
    using System;

    [Serializable]
    class Chopper : Weapon
    {
        //constants 
        const int HitPoints = 150;

        // Fields
        private ColorEnum color;

        // Constructors
        public Chopper() 
            : base()
        { }

        public Chopper(byte id, string inputName, int inputPrice)
            : base(id, inputName, inputPrice)
        {
        }

        public Chopper(byte id, string inputName, int inputPrice, string imageSource)
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
