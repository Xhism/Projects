namespace JConradOOPProject.GameObjects.Tools.Weapons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    class Cutter : Weapon
    {
        //constants 
        const int HitPoints = 200;

        //fields
        private int cutterSpeed;

        //constructors
        public Cutter(byte id, string inputName, decimal inputPrice)
            : base(id, inputName, inputPrice)
        { }

        public Cutter(byte id, string inputName, decimal inputPrice, string imageSource)
            : base(id, inputName, inputPrice, imageSource)
        { }

        public Cutter(byte id, string inputName, decimal inputPrice, int inputSpeed)
            : base(id, inputName, inputPrice)
        {
            this.CutterSpeed = inputSpeed;
        }

        public int CutterSpeed
        {
            get 
            {
                return this.cutterSpeed;
            }
            set 
            {
                this.cutterSpeed = value;
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
