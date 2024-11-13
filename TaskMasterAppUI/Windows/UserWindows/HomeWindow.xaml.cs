using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TaskMasterAppBLL.Service.Implement;
using TaskMasterAppBLL.Service.Interface;
using TaskMasterAppDAL.Models;
using TaskMasterAppUI.Windows.UserControls;
using Forms = System.Windows.Forms;
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
        private DispatcherTimer timer;
        private DateTime alarmTime;
        private SoundPlayer player;
        private string alarmSoundFilePath;
        private List<string> ringtoneList = new List<string>();
        private readonly string ringtoneFilePath = "ringtoneList.json";
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private string selectedSoundFilePath;
        private Forms.NotifyIcon notifyIcon;
        private bool allowClose = false;
        // Khởi tạo NotifyIcon

        private AddTaskUserControl addTaskUserControl;
        public HomeWindow()
        {
            InitializeComponent();

            Tasks = new ObservableCollection<TaskModel>();
            TaskListBox.ItemsSource = Tasks;
            IsAddTaskPopupOpen = false;



            LoadCategories();

            try
            {
                notifyIcon = new Forms.NotifyIcon();
                notifyIcon.Icon = new System.Drawing.Icon("Images/logo.ico");
                notifyIcon.Text = "Task Master";
                notifyIcon.Visible = true;

                // Tạo context menu cho NotifyIcon
                var contextMenu = new Forms.ContextMenuStrip();
                var openItem = new Forms.ToolStripMenuItem("Mở ứng dụng");
                var exitItem = new Forms.ToolStripMenuItem("Thoát");

                openItem.Click += (s, e) =>
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                    this.Activate(); // Đảm bảo cửa sổ được focus
                };

                exitItem.Click += (s, e) =>
                {
                    allowClose = true;
                    System.Windows.Application.Current.Shutdown();
                };

                contextMenu.Items.Add(openItem);
                contextMenu.Items.Add(exitItem);

                notifyIcon.ContextMenuStrip = contextMenu;
                notifyIcon.DoubleClick += (s, e) =>
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                    this.Activate(); // Đảm bảo cửa sổ được focus
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo NotifyIcon: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }


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
            AddTaskPopup.IsOpen = true;
            if (AddTaskPopup.IsOpen == false)
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
            Hide();
        }
        private void OpenApp_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
            this.Activate();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.Closing += Window_Closing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!allowClose)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                notifyIcon.Dispose();
            }
        }
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                this.Hide();
            }
            base.OnStateChanged(e);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (!allowClose)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                notifyIcon.Dispose();
            }
            base.OnClosing(e);
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
            try
            {
                if (File.Exists(ringtoneFilePath))
                {
                    string jsonContent = File.ReadAllText(ringtoneFilePath);
                    ringtoneList = JsonConvert.DeserializeObject<List<string>>(jsonContent) ?? new List<string>();

                    // Kiểm tra và loại bỏ các file không tồn tại
                    ringtoneList = ringtoneList.Where(path => File.Exists(path)).ToList();

                    // Lưu lại danh sách sau khi đã lọc
                    SaveRingtoneList();
                }
                else
                {
                    ringtoneList = new List<string>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhạc chuông: {ex.Message}",
                               "Lỗi",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
                ringtoneList = new List<string>();
            }
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

        private void TriggerAlarm(string taskTitle)
        {
            try
            {
                // Sử dụng file nhạc đã chọn
                string soundToPlay = selectedSoundFilePath;

                // Nếu chưa chọn nhạc, kiểm tra trong RingtoneComboBox
                if (string.IsNullOrEmpty(soundToPlay) && RingtoneComboBox.SelectedItem != null)
                {
                    soundToPlay = RingtoneComboBox.SelectedItem.ToString();
                }

                // Kiểm tra xem file có tồn tại không
                if (!string.IsNullOrEmpty(soundToPlay) && File.Exists(soundToPlay))
                {
                    PlaySound(soundToPlay);
                }
                else
                {
                    // Nếu không có file nhạc nào được chọn, dùng âm thanh mặc định của Windows
                    System.Media.SystemSounds.Exclamation.Play();
                }

                // Hiển thị thông báo
                notifyIcon.ShowBalloonTip(
                    3000, // Thời gian hiển thị (milliseconds)
                    "Task Master",
                    $"Đã đến giờ thực hiện nhiệm vụ: {taskTitle}",
                    Forms.ToolTipIcon.Info
                );

                // Hiện cửa sổ lên nếu đang ẩn
                if (!this.IsVisible)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                    this.Activate(); // Đảm bảo cửa sổ được focus
                }

                // Hiển thị MessageBox
                MessageBox.Show($"Báo thức {taskTitle} đến giờ!", "Task Master",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                StopSound();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi phát âm thanh: {ex.Message}", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                // Nếu có lỗi, vẫn hiển thị thông báo nhưng dùng âm thanh mặc định
                System.Media.SystemSounds.Exclamation.Play();
            }
        }

        private void PlaySound(string filePath)
        {
            try
            {
                if (player != null)
                {
                    player.Stop();
                    player.Dispose();
                }

                // Kiểm tra phần mở rộng của file
                string extension = Path.GetExtension(filePath).ToLower();

                if (extension == ".wav")
                {
                    // Sử dụng SoundPlayer cho file .wav
                    player = new SoundPlayer(filePath);
                    player.PlayLooping();
                }
                else if (extension == ".mp3" || extension == ".m4a" || extension == ".aac")
                {
                    // Sử dụng MediaPlayer cho các định dạng khác
                    mediaPlayer.Open(new Uri(filePath));
                    mediaPlayer.MediaEnded += (s, e) => mediaPlayer.Position = TimeSpan.Zero; // Loop
                    mediaPlayer.Play();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi phát âm thanh: {ex.Message}", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                System.Media.SystemSounds.Exclamation.Play();
            }
        }

        private void StopSound()
        {
            // Dừng SoundPlayer nếu đang chạy
            if (player != null)
            {
                player.Stop();
                player.Dispose();
                player = null;
            }

            // Dừng MediaPlayer nếu đang chạy
            if (mediaPlayer != null)
            {
                mediaPlayer.Stop();
                mediaPlayer.Close();
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
            // Load danh sách nhạc chuông khi mở ContextMenu
            LoadRingtoneList();

            // Nếu danh sách rỗng thì hiển thị thông báo
            if (ringtoneList.Count == 0)
            {
                RingtoneComboBox.ItemsSource = new List<string> { "Chưa có nhạc chuông" };
                RingtoneComboBox.IsEnabled = false;
                PreviewButton.IsEnabled = false;
                StopPreviewButton.IsEnabled = false;
            }
            else
            {
                RingtoneComboBox.ItemsSource = ringtoneList;
                RingtoneComboBox.IsEnabled = true;
                PreviewButton.IsEnabled = true;
                StopPreviewButton.IsEnabled = true;

                // Hiển thị tên file thay vì đường dẫn đầy đủ
                RingtoneComboBox.ItemTemplate = new DataTemplate();
                var textBlock = new FrameworkElementFactory(typeof(TextBlock));
                textBlock.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding
                {
                    Converter = new PathToFileNameConverter()
                });
                RingtoneComboBox.ItemTemplate.VisualTree = textBlock;
            }
        }
        public class PathToFileNameConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value is string path)
                {
                    return Path.GetFileName(path);
                }
                return value;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
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

                    // Cập nhật lại ItemsSource của ComboBox
                    RingtoneComboBox.ItemsSource = null;
                    RingtoneComboBox.ItemsSource = ringtoneList;
                    RingtoneComboBox.SelectedItem = alarmSoundFilePath; // Tự động chọn file vừa thêm
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

        private void StopPreviewButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

    }
}
