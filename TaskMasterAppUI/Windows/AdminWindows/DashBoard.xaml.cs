using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TaskMasterAppBLL.Service.Implement;
using TaskMasterAppBLL.Service.Interface;
using TaskMasterAppDAL.Models;

namespace TaskMasterAppUI.Windows.AdminWindows
{
    public partial class DashBoard : Window
    {
        private IUserService _userService = new UserService();
        private ITaskService _taskService = new TaskService();

        public DashBoard()
        {
            InitializeComponent();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            // Lấy tổng số người dùng
            var totalUsers = _userService.GetAllUsers().Count();
            TotalUsersTextBlock.Text = totalUsers.ToString();

            // Lấy danh sách người dùng và số lượng task
            var userRanking = _userService.GetAllUsers()
            .Select(user => new
            {
                UserName = user.UserName,
                Email = user.Email,
                RoleId = user.RoleId,
                Password = user.PasswordHash,
                TaskCount = _taskService.GetTasksByUserId(user.UserId).Count(),
                IncompleteTaskCount = _taskService.GetTasksByUserId(user.UserId).Count(t => !(bool)t.IsCompleted),
                CompletedTaskCount = _taskService.GetTasksByUserId(user.UserId).Count(t => (bool)t.IsCompleted)

            })
            .OrderByDescending(u => u.TaskCount)
                            .ToList();

            UserRankingListView.ItemsSource = userRanking;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            UserDetail userDetail = new UserDetail();
            userDetail.ShowDialog();
            LoadDashboardData();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserRankingListView.SelectedItem == null)
            {
                MessageBox.Show("Please select a user to update");
                return;
            }

            var user = UserRankingListView.SelectedItem;
            UserDetail userDetail = new UserDetail();
            userDetail.email = user.GetType().GetProperty("Email").GetValue(user, null).ToString();
            userDetail.ShowDialog();
            LoadDashboardData();


        }
    }
}
