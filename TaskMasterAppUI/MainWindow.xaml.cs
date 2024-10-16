using System.Windows;
using System.Windows.Input;
using TaskMasterAppBLL.Service.Implement;
using TaskMasterAppBLL.Service.Interface;
using TaskMasterAppUI.Windows.AdminWindows;
using TaskMasterAppUI.Windows.UserWindows;

namespace TaskMasterAppUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAuthenticateService _authenticateService;

        public MainWindow()
        {
            InitializeComponent();
            _authenticateService = new AuthenticateService();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UserNameTextBox.Text;
            var password = PassWordBox.Password;

            var user = _authenticateService.AuthenticateUser(username, password);



            if (user != null)
            {
                Application.Current.Properties["UserId"] = _authenticateService.GetEmployeeId(user.UserId);
                if (user.Role.RoleName == "Admin")
                {
                    var dashBoard = new DashBoard();
                    dashBoard.Show();
                    Close();
                }
                else if (user.Role.RoleName == "User")
                {
                    var homeWindow = new HomeWindow();
                    homeWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("You must have account to login");
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }

        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void MouseDownRegister(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Close();
        }

        private void MouseDownReset(object sender, MouseButtonEventArgs e)
        {

        }
    }
}