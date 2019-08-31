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
using JConradOOPProject.ViewModels;

namespace JConradOOPProject.Views
{
    /// <summary>
    /// Interaction logic for GameMap.xaml
    /// </summary>
    public partial class GameMap : UserControl
    {
        private GameEngine parentGameEngine;

        public GameMap(GameEngine parent)
        {
            InitializeComponent();
            this.parentGameEngine = parent;

            // Fills the 
            this.PlayerInfo.DataContext = this.parentGameEngine;
        }

        /// <summary>
        /// LOCATION
        /// Executes on every location click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Location_Click(object sender, RoutedEventArgs e)
        {
            Button location = (Button)sender;

            string locationText = location.Name.ToString().Remove(0, 8);

            TriggerIntroAction continueAction = delegate()
            {
                MainMenu.SwitchWindowContent(new Battle(locationText), true);
            };

            MainMenu.SwitchWindowContent(new Intro("battle_intro.jpg", String.Concat("Heading to ", locationText, " for battle..."), continueAction), true);
        }

        /// <summary>
        /// Button QUIT
        /// Closes the current game session, saves the content and returns the user to the main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonQuit_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.SwitchWindowContent(StartMenu.Instance);
        }

        /// <summary>
        /// Button EQUIPMENT
        /// Navigates to the equipment menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEquipment_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.SwitchWindowContent(new Equipment(this.parentGameEngine), true);
        }
    }

    // To be removed
    public class TestPlayer
    {
        public string PlayerName { get; set; }
        public int PlayerLevel { get; set; }
        public int PlayerExperience { get; set; }
        public int PlayerGold { get; set; }

        public TestPlayer(string name, int level, int experience, int gold)
        {
            this.PlayerName = name;
            this.PlayerLevel = level;
            this.PlayerExperience = experience;
            this.PlayerGold = gold;
        }
    }
}
