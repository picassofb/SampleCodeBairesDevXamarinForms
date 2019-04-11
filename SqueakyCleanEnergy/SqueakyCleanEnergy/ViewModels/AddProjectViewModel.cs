using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SqueakyCleanEnergy.Models;
using SqueakyCleanEnergy.Services;
using SqueakyCleanEnergy.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SqueakyCleanEnergy.ViewModels
{
    class AddProjectViewModel:BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly ApiService _apiService = new ApiService();
        public ICommand AddCommand { get; set; }


        private string _projectName;

        public string ProjectName
        {
            get => _projectName;
            set { _projectName = value; OnPropertyChanged();}
        }

        private bool _isDone;

        public bool IsDone
        {
            get => _isDone;
            set { _isDone = value; OnPropertyChanged();}
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

        public AddProjectViewModel(INavigation navigation)
        {
            AddCommand = new Command(AddProject);

            IsEnabled = true;
            _navigation = navigation;
        }

        private async void AddProject()
        {
            if (string.IsNullOrWhiteSpace(ProjectName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter a name for the project.", "Aceptar");
                return;
            }

            var newProject = new Project()
            {
                ProjectName = ProjectName,
                IsDone = IsDone
            };

            IsEnabled = false;
            IsRunning = true;

            var response = await _apiService.PostAsync(
                "/api",
                "/project", 
                newProject,
                "Bearer", 
                Preferences.Get("Token", string.Empty));

            IsRunning = false;
            IsEnabled = true;

            //Si loguea entonces;
            if (response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Project Added", "Ok");
                MessagingCenter.Send("Project", "UpdateListProject");
                await _navigation.PopAsync(true);
            }
            else
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");


        }
    }
}
