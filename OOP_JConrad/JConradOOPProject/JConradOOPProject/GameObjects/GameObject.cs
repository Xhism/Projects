using JConradOOPProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JConradOOPProject.GameObjects
{
    [Serializable]
    public abstract class GameObject : BaseViewModel
    {
        [NonSerialized]
        protected Position position;

        public GameObject()
            : this(new Position(0, 0), 0, 0)
        { }
        public GameObject(Position position, int width, int height)
        {
            this.position = position;
            this.Heigth = height;
            this.Width = width;
        }
        
        public Position Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }
        public int Heigth { get; set; }
        public int Width { get; set; }
        
        public static bool Overlap(GameObject obj1, GameObject obj2)
        {
            bool overlap = false;
            for (int x = obj1.Position.X; x < obj1.Position.X + obj1.Width; x++)
            {
                for (int y = obj1.Position.Y; y < obj1.Position.Y + obj1.Heigth; y++)
                {
                    if (x >= obj2.Position.X && x < obj2.Position.X + obj2.Width && y >= obj2.Position.Y && y < obj2.Position.Y + obj2.Heigth)
                    {
                        overlap = true;
                        break;
                    }
                }
            }
            return overlap;
        }
    }
}
