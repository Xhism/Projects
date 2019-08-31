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
using System.IO;
using JConradOOPProject.ViewModels;

namespace JConradOOPProject.Views
{
    /// <summary>
    /// Interaction logic for LoadGame.xaml
    /// </summary>
    public partial class LoadGame : UserControl
    {
        private GameEngine gameEngine;

        public LoadGame()
        {
            InitializeComponent();

            this.FillLoadGameList();
        }

        /// <summary>
        /// Button PLAY
        /// Starts the game of the selected player from the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            if (this.SavedGamesList.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a player to play with first.", "Select A Player");
                return;
            }

            try
            {
                string currentDir = Environment.CurrentDirectory;
                string loadFilePath = currentDir + "\\saves\\" + SavedGamesList.SelectedItem.ToString();

                StreamReader loadFromFile = new StreamReader(loadFilePath);

                //Load Name, Level, Experience and Gold
                this.gameEngine.PlayerName = loadFromFile.ReadLine();
                this.gameEngine.PlayerLevel = int.Parse(loadFromFile.ReadLine());
                this.gameEngine.PlayerExperience = int.Parse(loadFromFile.ReadLine());
                this.gameEngine.PlayerGold = int.Parse(loadFromFile.ReadLine());

                //Load Inventory, Skills and Shop
                this.gameEngine.DeserializeInventory(loadFromFile.ReadLine());
                this.gameEngine.DeserializeSkills(loadFromFile.ReadLine());
                this.gameEngine.DeserializeShop(loadFromFile.ReadLine());

                loadFromFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error.");
            }

            // Initializes the game
            this.gameEngine.Initialize();


        }

        /// <summary>
        /// Button BACK
        /// Returns the player back to the main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.SwitchWindowContent(StartMenu.Instance);
        }

        /// <summary>
        /// Fills the load game list with all the available players.
        /// </summary>
        private void FillLoadGameList()
        {
            string path = @".\saves\";

            // Checks if the specified path exists
            if (Directory.Exists(path))
            {
                DirectoryInfo savedGames = new DirectoryInfo(path);
                FileInfo[] saveFiles = savedGames.GetFiles("*.txt");

                this.SavedGamesList.ItemsSource = saveFiles;
            }
            else // If not, creates one for future use
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }
    }
}