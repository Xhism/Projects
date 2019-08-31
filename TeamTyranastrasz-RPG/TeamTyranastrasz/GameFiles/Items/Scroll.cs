﻿namespace RpgGame.Items
{
    public class Scroll : Item
    {
        private const ItemType ScrollItemType = ItemType.Scroll;

        public Scroll(string name, int price, Pictures picture, int defencePoints = 0, int attackPoints = 0, int hitPoints = 0, int str = 0, int dex = 0, int vit = 0, int intl = 0, int levelRequirement = 0, string description = null)
            : base(name, price, picture, Scroll.ScrollItemType, defencePoints, attackPoints, hitPoints, str, dex, vit, intl, levelRequirement, description)
        {
            this.IsConsumable = true;
        }
    }
}
