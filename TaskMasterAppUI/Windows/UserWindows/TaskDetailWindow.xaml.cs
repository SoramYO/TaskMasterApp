using System.Windows;
using TaskModel = TaskMasterAppDAL.Models.Task;

namespace TaskMasterAppUI.Windows.UserWindows
{
    /// <summary>
    /// Interaction logic for TaskDetailWindow.xaml
    /// </summary>
    public partial class TaskDetailWindow : Window
    {
        private TaskModel _task;
        public TaskDetailWindow(TaskModel task)
        {
            InitializeComponent();
            _task = task;
            LoadTaskData();
        }

        private void LoadTaskData()
        {
            TitleTextBox.Text = _task.Title;
            DescriptionTextBox.Text = _task.Description;
            CreatedDatePicker.Text = _task.CreatedDate.ToString();
            DueDatePicker.Text = _task.DueDate.ToString();

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _task.Title = TitleTextBox.Text;
            _task.DueDate = DateTime.Parse(DueDatePicker.Text);

            // Xử lý logic lưu task
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý logic xóa task
            this.Close();
        }

        private void Mute_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý logic mute task
            this.Close();
        }
        private void Check_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý logic mute task
            this.Close();
        }
        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý logic mute task
            this.Close();
        }
        private void IsCompletedButton_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý logic mute task
            this.Close();
        }
    }
}
