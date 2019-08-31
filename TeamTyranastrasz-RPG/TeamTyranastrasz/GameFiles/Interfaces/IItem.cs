﻿namespace RpgGame.Interfaces
{
    public interface IItem
    {
        string Name { get; set; }

        int Strength { get; }

        int Dexterity { get; }

        int Vitality { get; }

        int Intelligence { get; }

        int AttackPoints { get; }

        int DefencePoints { get; }

        int HitPoints { get; }

        int LevelRequirement { get; set; }

        int Price { get; set; }

        string Description { get; }

        bool IsConsumable { get; }

        Pictures Picture { get; set; }

        ItemType ItemType { get; set; }
    }
}