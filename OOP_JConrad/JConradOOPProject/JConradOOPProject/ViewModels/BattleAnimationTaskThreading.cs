using System;
using System.Linq;
using System.Threading.Tasks;

namespace JConradOOPProject.ViewModels
{
    /// <summary>
    /// BattleAnimationTaskThreading is used for separating the tasks of the GUI and the Worker via multithreading.
    /// </summary>
    public static class BattleAnimationTaskThreading
    {
        public static int currentWidth = 0;
        public static int decreaseWidth = 0;

        /// <summary>
        /// Passes the animation operation of health decreasing to a new thread.
        /// </summary>
        /// <param name="progress"></param>
        public static void AdjustHealth(IProgress<int> progress)
        {
            //if (decreaseWidth < 0)
            //{
            //    throw new ArgumentOutOfRangeException("The decreasing value must not exceed the total health.");
            //}

            for (int pixels = currentWidth; pixels >= decreaseWidth; pixels--)
            {
                Task.Delay(15).Wait();
                progress.Report(pixels);
            }
        }
    }
}
