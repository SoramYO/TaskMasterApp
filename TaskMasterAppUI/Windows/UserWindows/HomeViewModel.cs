using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskMasterAppBLL.Service.Interface;
using TaskModel = TaskMasterAppDAL.Models.Task;

namespace TaskMasterAppUI.Windows.UserWindows
{
    public class HomeViewModel : INotifyPropertyChanged
    {

        private readonly ITaskService _taskService;
        public HomeViewModel(ITaskService taskService)
        {
            _taskService = taskService;
            LoadTasks();
            StartDateTime = DateTime.Now;
            EndDateTime = DateTime.Now.AddHours(1);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!object.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        private DateTime _startDateTime;
        public DateTime StartDateTime
        {
            get => _startDateTime;
            set => SetProperty(ref _startDateTime, value);
        }

        private DateTime _endDateTime;
        public DateTime EndDateTime
        {
            get => _endDateTime;
            set => SetProperty(ref _endDateTime, value);
        }


        private ObservableCollection<TaskModel> _tasks;
        public ObservableCollection<TaskModel> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }




        public void LoadTasks()
        {
            Tasks = new ObservableCollection<TaskModel>(_taskService.GetTasks());
        }

        public void LoadTaskByDay(DateTime date)
        {
            if (Tasks == null)
            {
                Tasks = new ObservableCollection<TaskModel>();
            }
            else
            {
                Tasks.Clear();
            }
            Tasks = new ObservableCollection<TaskModel>(_taskService.GetTaskByDay(date));

        }

    }
}
