namespace JConradOOPProject.GameObjects.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    public abstract class Item : GameObject
    {
        //fields
        private byte idItem;
        private string name;
        private decimal price;
        private string description;
        private string imageSource;

        //constructors
        public Item()
        { }

        public Item(byte id, string inputName, decimal inputPrice)
        {
            this.IdItem = id;
            this.Name = inputName;
            this.Price = inputPrice;
        }

        public Item(byte id, string inputName, decimal inputPrice, string imageSource)
        {
            this.IdItem = id;
            this.Name = inputName;
            this.Price = inputPrice;
            this.ImageSource = "../Images/Items/" + imageSource + ".png";
        }

        //properties
        public byte IdItem
        {
            get { return this.idItem; }
            set { this.idItem = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public decimal Price
        {
            get
            {
                return this.price;
            }
            set 
            {
                if (value <= 0)
                {
                    throw new Exception("The price must be postive number.");
                }
                this.price = value; 
            }
        }

        public string ImageSource
        {
            get
            {
                return this.imageSource;
            }
            set
            {
                this.imageSource = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new Exception("The inputed description must have at least one character.");
                }
                this.description = value;
            }
        }

        //methods
        public override string ToString()
        {
            return base.ToString();
        }

    }
}
