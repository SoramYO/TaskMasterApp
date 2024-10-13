using System.Windows;
using System.Windows.Input;

namespace TaskMasterAppUI.Windows.UserWindows
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();

        }

        private void NoteTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NoteTextBlock.Focus();
        }

        private void NoteTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NoteTextBox.Text) && NoteTextBox.Text.Length > 0)
            {
                NoteTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                NoteTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void TimeTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TimeTextBlock.Focus();
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(TimeTextBox.Text) && TimeTextBox.Text.Length > 0)
            {
                TimeTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                TimeTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
