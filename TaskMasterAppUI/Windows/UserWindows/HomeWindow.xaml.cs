using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using TaskMasterAppBLL.Service.Implement;
using TaskMasterAppBLL.Service.Interface;
using TaskModel = TaskMasterAppDAL.Models.Task;
using System.Media;
using System.Security.Claims;
using System.Windows.Media;
using TaskMasterAppDAL.Models;
using Microsoft.Win32;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

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
        private DispatcherTimer timer;
        private DateTime alarmTime;
        private SoundPlayer player;
        private string alarmSoundFilePath;
        private List<string> ringtoneList = new List<string>();
        private readonly string ringtoneFilePath = "ringtoneList.json";
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private string selectedSoundFilePath;

        public HomeWindow()
        {
            InitializeComponent();

            Tasks = new ObservableCollection<TaskModel>();
            TaskListBox.ItemsSource = Tasks;
            IsAddTaskPopupOpen = false;

            LoadCategories();
        }

        private void LoadCategories()
        {
            var categories = _taskCategoryService.GetCategory();
            foreach (var category in categories)
            {
                CategoryFilterComboBox.Items.Add(new ComboBoxItem { Content = category.CategoryName, Tag = category });
            }
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskPopup.IsOpen = !AddTaskPopup.IsOpen;
            if (!AddTaskPopup.IsOpen)
            {
                LoadData();
            }

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
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
            SetupTimer();

        }

        public void LoadData()
        {
            ShowDateNow();
            LoadTasks();
            LoadRingtoneList();
            CategoryFilterComboBox.SelectedIndex = 0;
            RingtoneComboBox.ItemsSource = ringtoneList;

        }
        private void LoadRingtoneList()
        {
            if (File.Exists(ringtoneFilePath))
            {
                ringtoneList = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(ringtoneFilePath)) ?? new List<string>();
            }

            RingtoneComboBox.ItemsSource = ringtoneList;
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

        private void SetupTimer()
        {
            // Khởi tạo DispatcherTimer để kiểm tra mỗi giây
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Kiểm tra mỗi giây
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            TimeLabel.Text = currentTime.ToString("HH:mm:ss");
            int overdueCount = 0; // Biến để đếm số lượng task quá hạn

            foreach (var task in Tasks)
            {
                var taskTime = task.DueDate;
                if (taskTime.HasValue)
                {
                    if (currentTime.Hour == taskTime.Value.Hour && currentTime.Minute == taskTime.Value.Minute && currentTime.Second == taskTime.Value.Second)
                    {
                        TriggerAlarm(task.Title);
                    }

                    if (!task.IsCompleted == false && task.DueDate < currentTime)
                    {
                        overdueCount++;
                    }
                }
            }

            TaskCountTextBlock.Text = $"Tổng: {Tasks.Count} - Quá hạn: {overdueCount}";
        }

        private void TriggerAlarm(string value)
        {
            PlaySound(@"C:\Users\ngoxu\Downloads\nhac.wav");
            MessageBox.Show($"Báo thức {value} đến giờ!");
            StopSound();
        }

        private void PlaySound(string filePath)
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
            }
            player = new SoundPlayer(filePath);
            player.PlayLooping();
        }

        private void StopSound()
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
                player = null;
            }
        }

        private void CogButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.ContextMenu != null)
            {
                button.ContextMenu.IsOpen = true;
            }
        }


        private void TaskListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedTask = TaskListBox.SelectedItem as TaskModel;

            if (selectedTask != null)
            {
                TaskDetailWindow taskDetailWindow = new TaskDetailWindow(selectedTask);
                taskDetailWindow.ShowDialog();
                LoadData();
            }
        }

        private void TaskListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(TaskListBox);

            var listBoxItem = TaskListBox.InputHitTest(point) as FrameworkElement;
            while (listBoxItem != null && !(listBoxItem is ListBoxItem))
            {
                listBoxItem = VisualTreeHelper.GetParent(listBoxItem) as FrameworkElement;
            }

            if (listBoxItem != null)
            {
                // Lấy TaskModel từ DataContext của ListBoxItem
                var selectedTask = listBoxItem.DataContext as TaskModel;
                if (selectedTask != null)
                {
                    // Đánh dấu task là hoàn thành
                    _taskService.CheckTask(selectedTask);
                    LoadData();
                }
            }
        }

        private void CategoryFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCategory = (CategoryFilterComboBox.SelectedItem as ComboBoxItem)?.Tag as Category;
            if (selectedCategory != null)
            {
                LoadTasksByCategory(selectedCategory);
            }
            else if (CategoryFilterComboBox.SelectedIndex == 0)
            {
                LoadTasks();
            }
        }

        private void LoadTasksByCategory(Category category)
        {
            Tasks.Clear();
            var tasksByCategory = _taskService.TaskByCategory(category);
            foreach (var task in tasksByCategory)
            {
                Tasks.Add(task);
            }
        }
        private void Button_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (alarmSoundFilePath == null) // Chỉ chặn nếu không có file âm thanh nào
            {
                MessageBox.Show("Vui lòng chọn một tệp âm thanh trước.");
            }
        }


        private void SelectSoundButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Audio Files|*.wav;*.mp3;*.aac;*.m4a|All Files|*.*",
                Title = "Select a Sound File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                alarmSoundFilePath = openFileDialog.FileName;

                // Thêm vào danh sách nếu chưa tồn tại
                if (!ringtoneList.Contains(alarmSoundFilePath))
                {
                    ringtoneList.Add(alarmSoundFilePath);
                    SaveRingtoneList(); // Lưu vào file JSON
                }

                MessageBox.Show("Chọn thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SaveRingtoneList()
        {
            File.WriteAllText(ringtoneFilePath, JsonConvert.SerializeObject(ringtoneList));
        }
        private void RingtoneComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RingtoneComboBox.SelectedItem != null)
            {
                selectedSoundFilePath = RingtoneComboBox.SelectedItem.ToString();
            }
        }
        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedSoundFilePath) && File.Exists(selectedSoundFilePath))
            {
                if (mediaPlayer.Source != null && mediaPlayer.Position != TimeSpan.Zero)
                {
                    mediaPlayer.Stop();
                }
                else
                {
                    mediaPlayer.Open(new Uri(selectedSoundFilePath));
                    mediaPlayer.Play();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tệp âm thanh hợp lệ để nghe thử.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
