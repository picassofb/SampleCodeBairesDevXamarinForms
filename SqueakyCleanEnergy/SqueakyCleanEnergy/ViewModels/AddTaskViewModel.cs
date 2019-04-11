using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SqueakyCleanEnergy.Models;
using SqueakyCleanEnergy.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SqueakyCleanEnergy.ViewModels
{
    class AddTaskViewModel:BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly ApiService _apiService = new ApiService();
        public ICommand AddCommand { get; set; }

        public Guid ProjectId { get; set; }

        private string _task;

        public string Task
        {
            get => _task;
            set { _task = value; OnPropertyChanged();}
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged();}
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get => _endDate;
            set { _endDate = value; OnPropertyChanged();}
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(); }
        }

        private bool _isRunning;

        public bool IsRunning
        {
            get => _isRunning;
            set { _isRunning = value; OnPropertyChanged(); }
        }

        public AddTaskViewModel(INavigation navigation, Guid projectId)
        {
            AddCommand = new Command(AddTask);

            StartDate = DateTime.Now;
            EndDate= DateTime.Now.AddDays(3);
            IsEnabled = true;
            ProjectId = projectId;
            _navigation = navigation;
        }

        private async void AddTask()
        {
            if (string.IsNullOrWhiteSpace(Task))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter a name for the Task.", "Ok");
                return;
            }

            var newTask = new ProjectTask()
            {
                Task = Task,
                StartDate = StartDate,
                EndDate = EndDate
            };

            IsEnabled = false;
            IsRunning = true;

            var response = await _apiService.PostAsync(
                $"/api/project/{ProjectId}",
                "/projecttask",
                newTask,
                "Bearer",
                Preferences.Get("Token", string.Empty));

            IsRunning = false;
            IsEnabled = true;

            if (response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Task Added!", "Ok");
                MessagingCenter.Send("Task", "UpdateListProjectTask");
                await _navigation.PopAsync(true);
            }
            else
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");
        }
    }
}
