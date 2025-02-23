using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;
using GestionTache.Models;
using GestionTache.Services;
using System.Linq;
using CommunityToolkit.Mvvm.Input;

namespace GestionTache.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        private string _newTaskTitle;
        private readonly TaskService _taskService;

        public ObservableCollection<TaskItem> Tasks { get; set; }

        public string NewTaskTitle
        {
            get { return _newTaskTitle; }
            set
            {
                _newTaskTitle = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        public TaskViewModel()
        {
            _taskService = new TaskService();
            Tasks = new ObservableCollection<TaskItem>();

            AddTaskCommand = new AsyncRelayCommand(AddTaskAsync);
            DeleteTaskCommand = new AsyncRelayCommand<int>(DeleteTaskAsync);

            LoadTasks();
        }

        // 🔹 Charger les tâches depuis l'API
        private async void LoadTasks()
        {
            try
            {
                var tasks = await _taskService.GetTasksAsync();
                Tasks.Clear();
                foreach (var task in tasks)
                {
                    Tasks.Add(task);
                }
            }
            catch (Exception ex)
            {
                // Gérer l'exception (par exemple, journaliser l'erreur)
            }
        }

        // 🔹 Ajouter une tâche via l'API
        private async Task AddTaskAsync()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                var newTask = new TaskItem { Title = NewTaskTitle };
                try
                {
                    if (await _taskService.AddTaskAsync(newTask))
                    {
                        Tasks.Add(newTask);
                        NewTaskTitle = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    // Gérer l'exception (par exemple, journaliser l'erreur)
                }
            }
        }

        // 🔹 Supprimer une tâche via l'API
        private async Task DeleteTaskAsync(int id)
        {
            try
            {
                if (await _taskService.DeleteTaskAsync(id))
                {
                    var taskToRemove = Tasks.FirstOrDefault(t => t.Id == id);
                    if (taskToRemove != null)
                    {
                        Tasks.Remove(taskToRemove);
                    }
                }
            }
            catch (Exception ex)
            {
                // Gérer l'exception (par exemple, journaliser l'erreur)
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
