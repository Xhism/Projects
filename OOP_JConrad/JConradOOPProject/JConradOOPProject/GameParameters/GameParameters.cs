using System;

namespace JConradOOPProject.ViewModels
{
    public struct GameParameters
    {
        //Minimum experience required for level 1, 2, 3
        private static int[] levelMinExperience = new int[] { 0, 101, 301 };
        private static int[] rewardedExperience = new int[] { 50, 100, 200 };
        public static int PlayerHitChance = 50;
        public static int EnemyHitChance = 50;
        public static int InitialLevel = 1;
        public static int InitialGold = 100; 
        public static int InitialExperience = 0;

        public static int GetCurrentLevel(int experience)
        {
            int level = 0;
            while (level < levelMinExperience.Length && experience >= levelMinExperience[level])
            {
                level++;
            }
            return level;
        }
        public static int GetExperienceReward(int level)
        {
            try
            {
                return rewardedExperience[level - 1];
            }
            catch (IndexOutOfRangeException)
            {
                return 0;
            }            
        }
    }
}
