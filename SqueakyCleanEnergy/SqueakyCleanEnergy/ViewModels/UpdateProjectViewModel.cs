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
    class UpdateProjectViewModel: BaseViewModel
    {

        #region Properties Attributes and Services

        private readonly INavigation _navigation;
        private readonly ApiService _apiService = new ApiService();
        public ICommand UpdateCommand { get; set; }
        public Guid ProjectId { get; set; }

        private string _projectName;

        public string ProjectName
        {
            get => _projectName;
            set { _projectName = value; OnPropertyChanged(); }
        }

        private bool _isDone;

        public bool IsDone
        {
            get => _isDone;
            set { _isDone = value; OnPropertyChanged(); }
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

        #endregion


        public UpdateProjectViewModel(INavigation navigation, Project projectToUpdate)
        {
            UpdateCommand = new Command(UpdateProject);

            ProjectId = projectToUpdate.ProjectId;
            ProjectName = projectToUpdate.ProjectName;
            IsDone = projectToUpdate.IsDone;

            IsEnabled = true;
            _navigation = navigation;
        }

        private async void UpdateProject()
        {
            if (string.IsNullOrWhiteSpace(ProjectName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter a name for the project.", "Ok");
                return;
            }

            var newProject = new Project()
            {
                ProjectId = ProjectId,
                ProjectName = ProjectName,
                IsDone = IsDone
            };

            IsEnabled = false;
            IsRunning = true;

            var response = await _apiService.PutAsync(
                "/api",
                "/project",
                ProjectId, 
                newProject,
                "Bearer",
                Preferences.Get("Token", string.Empty));

            IsRunning = false;
            IsEnabled = true;

            if (response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Project Info Updated", "Ok");
                MessagingCenter.Send("Project", "UpdateListProject");
                await _navigation.PopAsync(true);
            }
            else
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");
        }
    }
}
