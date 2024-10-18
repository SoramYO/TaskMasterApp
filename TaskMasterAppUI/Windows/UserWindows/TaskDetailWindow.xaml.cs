using System.Windows;

using TaskMasterAppBLL.Service.Implement;
using TaskMasterAppBLL.Service.Interface;
using TaskModel = TaskMasterAppDAL.Models.Task;

namespace TaskMasterAppUI.Windows.UserWindows
{
    /// <summary>
    /// Interaction logic for TaskDetailWindow.xaml
    /// </summary>
    public partial class TaskDetailWindow : Window
    {
        private TaskModel _task;
        private ITaskService _taskService = new TaskService();
        private ITaskCategoryService _taskCategoryService = new TaskCategoryService();
        public TaskDetailWindow(TaskModel task)
        {
            InitializeComponent();
            _task = task;
            LoadCategories();
            LoadTaskData();
        }

        private void LoadTaskData()
        {
            TitleTextBox.Text = _task.Title;
            DescriptionTextBox.Text = _task.Description;
            CreatedDatePicker.Text = _task.CreatedDate.ToString();
            DueDatePicker.Text = _task.DueDate.ToString();
            IconNotification.Icon = _task.Notification == true ? FontAwesome.WPF.FontAwesomeIcon.Bell : FontAwesome.WPF.FontAwesomeIcon.BellSlash;
            IconIsCompleted.Icon = _task.IsCompleted == true ? FontAwesome.WPF.FontAwesomeIcon.CheckCircle : FontAwesome.WPF.FontAwesomeIcon.Circle;
            CategoryComboBox.SelectedValue = _task.CategoryId;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _task.Title = TitleTextBox.Text;
            _task.Description = DescriptionTextBox.Text;
            _task.DueDate = DueDatePicker.SelectedDate.Value;
            _task.CreatedDate = CreatedDatePicker.SelectedDate.Value;
            _task.Notification = IconNotification.Icon == FontAwesome.WPF.FontAwesomeIcon.Bell ? true : false;
            _task.IsCompleted = IconIsCompleted.Icon == FontAwesome.WPF.FontAwesomeIcon.CheckCircle ? true : false;
            _task.CategoryId = (int)CategoryComboBox.SelectedValue;
            _taskService.UpdateTask(_task);
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //ask yes no before delete
            MessageBoxResult answer = MessageBox.Show("Ban co chac chan xoa?", "Xoa task!!", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {
                _taskService.DeleteTask(_task);
                MessageBox.Show("Xoa thanh cong");
                Close();
            }
        }

        private void Mute_Click(object sender, RoutedEventArgs e)
        {
            _taskService.MuteTask(_task);
            LoadTaskData();
        }
        private void Check_Click(object sender, RoutedEventArgs e)
        {
            _taskService.CheckTask(_task);
            LoadTaskData();
        }
        private void LoadCategories()
        {
            CategoryComboBox.ItemsSource = null;
            CategoryComboBox.ItemsSource = _taskCategoryService.GetCategory();
            CategoryComboBox.SelectedValuePath = "CategoryId";
            CategoryComboBox.DisplayMemberPath = "CategoryName";
            CategoryComboBox.SelectedIndex = 0;
        }
    }
}
