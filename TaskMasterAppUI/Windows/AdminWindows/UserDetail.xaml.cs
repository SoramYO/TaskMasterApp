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
using TaskMasterAppBLL.Service.Implement;
using TaskMasterAppBLL.Service.Interface;
using TaskMasterAppDAL.Models;

namespace TaskMasterAppUI.Windows.AdminWindows
{
    /// <summary>
    /// Interaction logic for UserDetail.xaml
    /// </summary>
    public partial class UserDetail : Window
    {
        private IRoleService _roleService = new RoleService();
        private IUserService _userService = new UserService();
        public string email { get; set; }

        public UserDetail()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RoleComboBox.ItemsSource = null;
            RoleComboBox.ItemsSource = _roleService.GetAllRole();

            RoleComboBox.DisplayMemberPath = "RoleName";
            RoleComboBox.SelectedValuePath = "RoleId";
            RoleComboBox.SelectedIndex = 0;
            LableDetail.Content = "Add User";
            if (email != null)
            {
                var user = _userService.GetByUserName(email);
                LableDetail.Content = "Update User";
                UserId.Text = user.UserId.ToString();
                UserNameTextBox.Text = user.UserName;
                EmailTextBox.Text = user.Email;
                PasswordTextBox.Text = user.PasswordHash;
                RoleComboBox.SelectedItem = user.RoleId;
            }



        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (email != null)
            {
                var user = new User();
                user.UserId = int.Parse(UserId.Text);
                user.UserName = UserNameTextBox.Text;
                user.Email = EmailTextBox.Text;
                user.PasswordHash = PasswordTextBox.Text;
                user.RoleId = (int)RoleComboBox.SelectedValue;
                _userService.UpdateUser(user);
                MessageBox.Show("Update user successfully");
                Close();
            }
            else
            {
                User user = new User
                {
                    UserName = UserNameTextBox.Text,
                    Email = EmailTextBox.Text,
                    PasswordHash = PasswordTextBox.Text,
                    RoleId = (int)RoleComboBox.SelectedValue
                };
                _userService.AddUser(user);
                Close();
            }
        }
    }
}
