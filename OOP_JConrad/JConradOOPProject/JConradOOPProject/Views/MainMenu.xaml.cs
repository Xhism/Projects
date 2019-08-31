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
using System.Windows.Shapes;

namespace JConradOOPProject.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        /// <summary>
        /// Initializes main components and renders main menu buttons.
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
            this.ContentSwitcher.Children.Add(StartMenu.Instance);
        }

        /// <summary>
        /// Switches the content of main menu by given UserControl
        /// </summary>
        /// <param name="newContent"></param>
        public static void SwitchWindowContent(UserControl newContent, bool removeCredits = false)
        {
            MainMenu accessMasterWindow = Application.Current.MainWindow as MainMenu;

            // Removes credits
            if (removeCredits)
            {
                accessMasterWindow.Credits.Visibility = Visibility.Hidden;
            }
            else if (accessMasterWindow.Credits.Visibility == Visibility.Hidden)
            {
                accessMasterWindow.Credits.Visibility = Visibility.Visible;
            }

            // Clears the current content of the window
            accessMasterWindow.ContentSwitcher.Children.Clear();

            // Sets the new content
            accessMasterWindow.ContentSwitcher.Children.Add(newContent);
        }
    }
}
