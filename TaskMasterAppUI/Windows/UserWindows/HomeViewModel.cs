using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskMasterAppBLL.Service.Interface;
using TaskModel = TaskMasterAppDAL.Models.Task;

namespace TaskMasterAppUI.Windows.UserWindows
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!(object.Equals(field, newValue)))
            {
                field = (newValue);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
        private DateTime currentDateTime = DateTime.Now;
        public DateTime StartTimePicker
        {
            get
            {
                return currentDateTime;
            }
            set
            {
                currentDateTime = value;
            }
        }
        private DateTime endDateTime = DateTime.Now;
        public DateTime EndTimePicker
        {
            get
            {
                return endDateTime;
            }
            set
            {
                endDateTime = value;
            }
        }
        private ObservableCollection<TaskModel> _tasks;
        public ObservableCollection<TaskModel> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }

        private readonly ITaskService _taskService;

        public HomeViewModel(ITaskService taskService)
        {
            _taskService = taskService;
            LoadTasks();
        }

        public void LoadTasks()
        {
            Tasks = new ObservableCollection<TaskModel>(_taskService.GetTasks());
        }
    }
}