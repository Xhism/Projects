namespace JConradOOPProject.GameObjects.Tools.Shields
{
    using System;

    [Serializable]
    public abstract class Shield : Item
    {
        public Shield()
        { }

        public Shield(byte id, string inputName, decimal inputPrice)
            : base(id, inputName, inputPrice)
        {
        }

        public Shield(byte id, string inputName, decimal inputPrice, string imageSource)
            : base(id, inputName, inputPrice, imageSource)
        {
        }

        public abstract decimal DefencePower
        {
            get;
        }
    }
}
