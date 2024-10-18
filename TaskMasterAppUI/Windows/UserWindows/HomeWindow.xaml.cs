using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskMasterAppBLL.Service.Implement;
using TaskMasterAppBLL.Service.Interface;
using TaskModel = TaskMasterAppDAL.Models.Task;
namespace TaskMasterAppUI.Windows.UserWindows
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private ITaskCategoryService _taskCategoryService = new TaskCategoryService();
        private ITaskService _taskService = new TaskService();
        public ObservableCollection<TaskModel> Tasks { get; set; }
        public bool IsAddTaskPopupOpen { get; set; }
        public HomeWindow()
        {
            InitializeComponent();

            Tasks = new ObservableCollection<TaskModel>();
            TaskListBox.ItemsSource = Tasks;
            IsAddTaskPopupOpen = false;
        }
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the visibility of the Popup
            AddTaskPopup.IsOpen = !AddTaskPopup.IsOpen;
            LoadData();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();

        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CalendarChoose_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CalendarChoose.SelectedDate.HasValue)
            {
                DateTime selectedDate = CalendarChoose.SelectedDate.Value;
                SelectedDay.Text = selectedDate.Day.ToString();
                SelectedMonth.Text = selectedDate.ToString("MMMM yyyy");
                SelectedDayOfWeek.Text = selectedDate.ToString("dddd");

                LoadTasksByDay(selectedDate);
                LoadTaskByDate();
            }
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            ShowDateNow();
            LoadTasks();
        }

        private void ShowDateNow()
        {
            DateTime dateTimeNow = DateTime.Now;
            SelectedDay.Text = dateTimeNow.Day.ToString();
            SelectedMonth.Text = dateTimeNow.ToString("MMMM yyyy");
            SelectedDayOfWeek.Text = dateTimeNow.ToString("dddd");
        }

        private void LoadTasks()
        {
            Tasks.Clear();
            var taskList = _taskService.GetTasks();
            foreach (var task in taskList)
            {
                Tasks.Add(task);
            }
        }
        private void LoadTasksByDay(DateTime date)
        {
            Tasks.Clear();
            var tasksByDate = _taskService.GetTaskByDay(date);
            foreach (var task in tasksByDate)
            {
                Tasks.Add(task);
            }
        }




        private void LoadTaskByDate()
        {
            TaskListBox.ItemsSource = null;
            TaskListBox.ItemsSource = Tasks;
        }
        private void TaskItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TaskModel selectedTask = (sender as FrameworkElement)?.DataContext as TaskModel;
            if (selectedTask != null)
            {
                TaskDetailWindow taskDetailWindow = new TaskDetailWindow(selectedTask);
                taskDetailWindow.ShowDialog();
            }
        }
        private void TaskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lấy item được chọn
            var selectedTask = TaskListBox.SelectedItem as TaskModel;

            if (selectedTask != null)
            {
                // Mở cửa sổ chi tiết task
                TaskDetailWindow taskDetailWindow = new TaskDetailWindow(selectedTask);

                // Set dữ liệu cho TaskDetailWindow (giả sử TaskDetailWindow có property để nhận dữ liệu)

                // Hiển thị TaskDetailWindow
                taskDetailWindow.ShowDialog();
            }
        }


    }
}
