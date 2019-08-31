namespace JConradOOPProject.GameObjects.Tools.Weapons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    public abstract class Weapon : Item
    {
        //fields

        public Weapon()
        { }
        public Weapon(byte id, string inputName, decimal inputPrice)
            : base(id, inputName, inputPrice)
        {
        }

        public Weapon(byte id, string inputName, decimal inputPrice, string imageSource)
            : base(id, inputName, inputPrice, imageSource)
        {
        }

        public abstract int Power
        {
            get;
        }
    }
}
