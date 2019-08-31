using System.Windows;
using System.Windows.Controls;

namespace JConradOOPProject.Views
{
    /// <summary>
    /// Interaction logic for StartMenu.xaml
    /// </summary>
    public partial class StartMenu : UserControl
    {
        private static StartMenu instance;

        private StartMenu()
        {
            InitializeComponent();
        }
        public static StartMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StartMenu();
                }
                return instance;
            }
        }

        /// <summary>
        /// Button NEW GAME
        /// Redirects the player to the new game menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.SwitchWindowContent(new NewGame());
        }

        /// <summary>
        /// Button LOAD GAME
        /// Displays a list of players who have played the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLoadGame_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.SwitchWindowContent(new LoadGame());
        }

        /// <summary>
        /// Button EXIT
        /// Shuts down the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
