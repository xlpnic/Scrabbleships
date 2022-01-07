namespace WordGame
{
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        readonly Regex allowedCharactersRegex;

        public GameView()
        {
            InitializeComponent();
            allowedCharactersRegex = new Regex(@"^[A-Z]$");
        }

        private void LetterBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var t = (TextBox)sender;
            t.Background = new SolidColorBrush(Colors.Yellow);
        }

        private void LetterBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var t = (TextBox)sender;
            t.Background = new SolidColorBrush(Colors.White);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var key = e.Key.ToString();
            var matches = allowedCharactersRegex.Matches(key);
            var t = (TextBox)sender;

            if (matches.Count == 1)
            {
                if (t.Text != "")
                {
                    // The new character is valid, so clear whetever is currently in ther so we can replace it.
                    t.Text = "";
                }
            }
            else
            {
                // The character entered is not valid, so do nothing with it.
                e.Handled = true;
            }
        }

        private void TextBox_DragLeave(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }
    }
}
