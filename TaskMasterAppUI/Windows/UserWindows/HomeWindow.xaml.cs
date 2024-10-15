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

        public HomeWindow()
        {
            InitializeComponent();
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
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowDateNow();
            GetCategory();
        }

        private void ShowDateNow()
        {
            DateTime dateTimeNow = DateTime.Now;
            SelectedDay.Text = dateTimeNow.Day.ToString();
            SelectedMonth.Text = dateTimeNow.ToString("MMMM yyyy");
            SelectedDayOfWeek.Text = dateTimeNow.ToString("dddd");
        }
        private void GetCategory()
        {
            CategoryComboBox.ItemsSource = null;
            CategoryComboBox.ItemsSource = _taskCategoryService.GetCategory();
            CategoryComboBox.SelectedValuePath = "CategoryId";
            CategoryComboBox.DisplayMemberPath = "CategoryName";
            CategoryComboBox.SelectedIndex = 0;
        }


        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TitleTextBox.Text) || string.IsNullOrEmpty(DescriptionTextBox.Text)
                || PriorityComboBox.SelectedIndex == -1 || CategoryComboBox.SelectedIndex == -1
                || StartTimePicker.DefaultValue == null || EndTimePicker.DefaultValue == null)
            {
                MessageBox.Show("Please fill all fields");
                return;
            }
            TaskModel task = new TaskModel()
            {
                Title = TitleTextBox.Text,
                Description = DescriptionTextBox.Text,
                IsCompleted = false,
                DueDate = EndTimePicker.DefaultValue,
                CreatedDate = StartTimePicker.DefaultValue,
                Priority = PriorityComboBox.SelectedIndex,
                CategoryId = (int)CategoryComboBox.SelectedValue,
                UserId = 1,
            };

            _taskService.AddTask(task);
            MessageBox.Show("Task Added Successfully");

        }
    }
}
