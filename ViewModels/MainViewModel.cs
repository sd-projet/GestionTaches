using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GestionTache.Models;
using GestionTache.ViewModels;


namespace GestionTache.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<TaskItem> Tasks { get; set; } = new ObservableCollection<TaskItem>();
        public TaskViewModel myTaskViewModel { get; set; } = new TaskViewModel();

        private string _newTaskTitle;
        public string NewTaskTitle
        {
            get => _newTaskTitle;
            set { _newTaskTitle = value; OnPropertyChanged(); }
        }

        public ICommand AddTaskCommand { get; }

        public MainViewModel()
        {
            AddTaskCommand = new RelayCommand(AddTask);
        }

        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                Tasks.Add(new TaskItem { Title = NewTaskTitle });
                NewTaskTitle = "";
            }
        }
    }
}
