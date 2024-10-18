using System.Windows;
using System.Windows.Controls;
using TaskMasterAppBLL.Service.Implement;
using TaskMasterAppBLL.Service.Interface;
using TaskModel = TaskMasterAppDAL.Models.Task;
namespace TaskMasterAppUI.Windows.UserControls
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTaskUserControl : UserControl
    {
        public event EventHandler<TaskModel> TaskAdded;
        private ITaskCategoryService _taskCategoryService = new TaskCategoryService();
        private ITaskService _taskService = new TaskService();
        public AddTaskUserControl()
        {
            InitializeComponent();
            LoadCategories();
            StartDateTimePicker.Value = DateTime.Now;
            EndDateTimePicker.Value = DateTime.Now.AddHours(1);
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TitleTextBox.Text))
            {
                MessageBox.Show("Please enter a title");
                return;
            }

            if (string.IsNullOrEmpty(DescriptionTextBox.Text))
            {
                MessageBox.Show("Please enter a description");
                return;
            }

            if (CategoryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category");
                return;
            }

            if (EndDateTimePicker.Value < StartDateTimePicker.Value)
            {
                MessageBox.Show("End time must be greater than start time");
                return;
            }
            if (EndDateTimePicker.Value == null)
            {
                MessageBox.Show("Please select a due date");
                return;
            }
            TaskModel newTask = new TaskModel
            {
                Title = TitleTextBox.Text,
                Description = DescriptionTextBox.Text,
                DueDate = EndDateTimePicker.Value,
                CreatedDate = StartDateTimePicker.Value,
                CategoryId = (int)CategoryComboBox.SelectedValue,
                UserId = Application.Current.Properties["UserId"] as int?
            };

            _taskService.AddTask(newTask);
            MessageBox.Show("Task added successfully");

            TaskAdded?.Invoke(this, newTask);

            // Clear fields after adding
            TitleTextBox.Text = "Enter Task Title";
            DescriptionTextBox.Text = "Enter Description (optional)";
            CategoryComboBox.SelectedIndex = 0;
            StartDateTimePicker.Value = null;
            EndDateTimePicker.Value = null;
        }
        private void LoadCategories()
        {
            CategoryComboBox.ItemsSource = null;
            CategoryComboBox.ItemsSource = _taskCategoryService.GetCategory();
            CategoryComboBox.SelectedValuePath = "CategoryId";
            CategoryComboBox.DisplayMemberPath = "CategoryName";
            CategoryComboBox.SelectedIndex = 0;
        }
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var addCategoryModal = new AddCategoryModal();
            addCategoryModal.CategoryAdded += () =>
            {
                LoadCategories();
            };
            AddCategoryPopup.Child = addCategoryModal;
            AddCategoryPopup.IsOpen = true;
        }
    }
}
