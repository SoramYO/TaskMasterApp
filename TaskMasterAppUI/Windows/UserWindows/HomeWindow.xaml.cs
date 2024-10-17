﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskMasterAppBLL.Service.Implement;
using TaskMasterAppBLL.Service.Interface;
using TaskMasterAppUI.Windows.UserControls;
using TaskModel = TaskMasterAppDAL.Models.Task;
using System.Collections.ObjectModel;
namespace TaskMasterAppUI.Windows.UserWindows
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private ITaskCategoryService _taskCategoryService = new TaskCategoryService();
        private ITaskService _taskService = new TaskService();
        private HomeViewModel _viewModel = new HomeViewModel(new TaskService());

        public HomeWindow()
        {
            InitializeComponent();
            DataContext = new HomeViewModel(new TaskService());
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

                _viewModel.LoadTaskByDay(selectedDate);
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
            GetCategory();
            _viewModel.LoadTasks();
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
            if (string.IsNullOrEmpty(TitleTextBox.Text))
            {
                MessageBox.Show("Vui long dien title");

            }
            if (string.IsNullOrEmpty(DescriptionTextBox.Text))
            {
                MessageBox.Show("Vui long dien description");

            }
            if (CategoryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Vui long chon muc");
            }

            if (EndDateTimePicker.Value < StartDateTimePicker.Value)
            {
                MessageBox.Show("End time must be greater than start time");
                return;
            }
            TaskModel task = new TaskModel()
            {
                Title = TitleTextBox.Text,
                Description = DescriptionTextBox.Text,
                DueDate = EndDateTimePicker.Value,
                CreatedDate = StartDateTimePicker.Value,
                CategoryId = (int)CategoryComboBox.SelectedValue,
                UserId = Application.Current.Properties["UserId"] as int?
            };

            _taskService.AddTask(task);
            MessageBox.Show("Task Added Successfully");
            _viewModel.LoadTasks();

        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var addCategoryModal = new AddCategoryModal();
            addCategoryModal.CategoryAdded += () =>
              {
                  LoadData();
              };
            AddCategoryPopup.Child = addCategoryModal;
            AddCategoryPopup.IsOpen = true;
        }

        private void LoadTaskByDate()
        {
            TaskItemSouce.ItemsSource = null;
            TaskItemSouce.ItemsSource = _viewModel.Tasks;
        }

    }
}
