using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JConradOOPProject.Views
{
    using JConradOOPProject.ViewModels;

    /// <summary>
    /// Interaction logic for Battle.xaml
    /// </summary>
    public partial class Battle : UserControl
    {
        const int INITIAL_HEALTH = 100;
        const int HEALTH_BAR_MAXWIDTH = 264; // Pixels

        private string location; // The field is not accessed directly nor can be modified by the user

        // To be synced with GameEngine
        private int playerHealth;
        private int enemyHealth;

        public Battle(string inputLocation)
        {
            InitializeComponent();

            this.location = inputLocation;
            this.SetLocation();

            // Test purposes
            this.PlayerHealth = 45;
            this.EnemyHealth = 82;
        }

        /// <summary>
        /// Player's health property used for initial set. Performs an automatic change of the health bar aswell.
        /// </summary>
        public int PlayerHealth
        {
            get { return this.playerHealth; }
            set
            {
                this.playerHealth = value;
                this.SetHealthBarPts(this.PlayerHealthBar, this.playerHealth);
            }
        }

        /// <summary>
        /// Enemy's health property used for initial set. Performs an automatic change of the health bar aswell.
        /// </summary>
        public int EnemyHealth
        {
            get { return this.enemyHealth; }
            set
            {
                this.enemyHealth = value;
                this.SetHealthBarPts(this.EnemyHealthBar, this.enemyHealth);
            }
        }

        /// <summary>
        /// Sets the background to the location accordingly
        /// </summary>
        private void SetLocation()
        {
            string path = String.Concat("../Images/BattleGrounds/", this.location.ToLower(), ".jpg");
            this.BattleBackground.Source = new BitmapImage(new Uri(path, UriKind.Relative));
        }

        /// <summary>
        /// Updates the specified health bar by a given value (health points)
        /// </summary>
        /// <param name="target"></param>
        /// <param name="points"></param>
        private void SetHealthBarPts(object target, int points)
        {
            Border bar = (Border)target;
            int barWidth = this.HealthToPixels(points);

            bar.DataContext = new { Health = points, HealthPerc = barWidth };
        }

        /// <summary>
        /// Converts health value to the respective pixel value of the health bar
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private int HealthToPixels(int points)
        {
            return HEALTH_BAR_MAXWIDTH * points / INITIAL_HEALTH;
        }

        /// <summary>
        /// Decreases target's health (with an animation)
        /// </summary>
        /// <param name="getter"></param>
        /// <param name="setter"></param>
        /// <param name="decreaseValue"></param>
        private async void DecreaseHealth(Func<int> getter, Action<int> setter, Border target, int decreaseValue)
        {
            if (decreaseValue > getter())
            {
                // Problems with catching exception from async method.
                MessageBox.Show("Decr value is bigger than actual value.", "Fatal Error");
                return;
            }

            int currentHealthPxl = this.HealthToPixels(getter());

            setter(getter() - decreaseValue);
            
            int decreasedHealthPxl = this.HealthToPixels(getter());

            //System.Threading.Thread.Sleep(3000);

            // Transfers the data to the static class (cant be passed as method arguments)
            BattleAnimationTaskThreading.currentWidth = currentHealthPxl;
            BattleAnimationTaskThreading.decreaseWidth = decreasedHealthPxl;

            var progress = new Progress<int>(ct => target.DataContext = new { Health = getter(), HealthPerc = ct });
            await Task.Factory.StartNew(() => BattleAnimationTaskThreading.AdjustHealth(progress), TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Test purposes
        /// </summary>
        private void ButtonAnim_Click(object sender, RoutedEventArgs e)
        {
            this.DecreaseHealth(() => this.playerHealth, n => this.playerHealth= n, this.PlayerHealthBar, 22);

            // One animation at time.
            //this.DecreaseHealth(() => this.enemyHealth, n => this.enemyHealth = n, this.EnemyHealthBar, 70);
        }
    }
}