using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
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
    /// Interaction logic for NewGame.xaml
    /// </summary>
    public partial class NewGame : UserControl
    {
        const string DEFAULT_TXTFIELD_TEXT = "Enter your name";
        private string playerName;
        private GameEngine gameEngine;

        /// <summary>
        /// playerName property
        /// </summary>
        public string PlayerName
        {
            get { return this.playerName; }
            private set
            {
                Regex alphanumeric = new Regex("^[a-zA-Z0-9]*$");

                if (value == DEFAULT_TXTFIELD_TEXT || value == String.Empty)
                {
                    throw new ArgumentException("Please enter a name!");
                }
                else if (!alphanumeric.IsMatch(value))
                {
                    throw new ArgumentException("The name can contain only the following symbols: a-z, A-Z, 0-9");
                }
                else if (!(value.Length >= 3 && value.Length <= 25))
                {
                    throw new ArgumentException("The lengths of the name must be between 3 and 25 symbols.");
                }

                this.playerName = value;
            }
        }

        public NewGame()
        {
            InitializeComponent();

            this.NewGameName.Text = DEFAULT_TXTFIELD_TEXT;
            this.playerName = String.Empty;

            this.gameEngine = new GameEngine();
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
        /// Button START
        /// Sets new player name and proceeds with introducing the player to the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.PlayerName = this.NewGameName.Text;
                this.gameEngine.PlayerName = this.PlayerName;

                // Initializes the game
                this.gameEngine.Initialize();

                // New Game intro >>

                TriggerIntroAction continueAction = delegate()
                {
                    MainMenu.SwitchWindowContent(new GameMap(this.gameEngine), true);
                };

                MainMenu.SwitchWindowContent(new Intro("main_intro.jpg", "Chepelar Man is an outstanding logger who proved himself numerous times at Grizzly and Canidae forests with his agility and strength. Now he has to face the new threat that is about to endanger the livelihoods and lives of many. Will he make it out to the end? ...", continueAction), true);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Invalid name");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fatal error");
            }
        }

        /// <summary>
        /// Clears the default content of the textbox on focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxNewGameName_Focus(object sender, RoutedEventArgs e)
        {
            TextBox current = (TextBox)sender;
            current.Clear();
        }
    }
}
