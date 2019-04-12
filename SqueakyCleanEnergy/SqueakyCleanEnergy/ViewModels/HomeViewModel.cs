using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SqueakyCleanEnergy.Models;
using SqueakyCleanEnergy.Services;
using SqueakyCleanEnergy.Views;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace SqueakyCleanEnergy.ViewModels
{
    class HomeViewModel: BaseViewModel
    {
        #region Properties and Services

        private readonly INavigation _navigation;
        private readonly ApiService _apiService = new ApiService();
        private List<Project> ProjectsList { get; set; }

        public ICommand RefreshCommand { get; set; }
        public ICommand AddProjectCommand { get; set; }
        public ICommand UpdateProjectCommand { get; set; }
        public ICommand DeleteProjectCommand { get; set; }
        public ICommand DisplayTaskCommand { get; set; }


        private ObservableCollection<Project> _projects;

        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set { _projects = value; OnPropertyChanged(); }
        }

        private string _filter;

        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                Search();
            }
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(); }
        }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set { _isRefreshing = value; OnPropertyChanged(); }
        }

        #endregion

        public HomeViewModel(INavigation navigation)
        {
            Projects = new ObservableCollection<Project>();
            RefreshCommand = new Command(LoadProjects);
            AddProjectCommand = new Command(AddProject);
            DisplayTaskCommand = new Command(DisplayTask);
            UpdateProjectCommand = new Command(UpdateProject);
            DeleteProjectCommand = new Command(DeleteProject);

            LoadProjects();

            IsEnabled = true;
            IsRefreshing = false;
            _navigation = navigation;

            // Suscribe to MessagingCenter to get notify when an event happens in a different view,
            // in this case when a Project is created LoadTasks is called to apply the changes to the ListView in the view
            MessagingCenter.Subscribe<string>(this, "UpdateListProject", (value) =>
            {
                LoadProjects();
            });
        }

        private async void DeleteProject(object obj)
        {
            var projectToDelete = obj as Project;

            if (projectToDelete != null)
            {
                var action = await Application.Current.MainPage.DisplayAlert("Delete?",
                $"Are you sure to delete '{projectToDelete.ProjectName}' Project?",
                "Yes", "No");

                if (action)
                {
                    var response = await _apiService.DeleteAsync(
                        "/api",
                        "/project",
                        projectToDelete.ProjectId,
                        "Bearer",
                        Preferences.Get("Token", string.Empty));

                    if (response.IsSuccess)
                    {
                        await Application.Current.MainPage.DisplayAlert("Success", 
                            $"Project '{projectToDelete.ProjectName}' Has Been Deleted", 
                            "Ok");
                        LoadProjects();
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");
                }
            }
        }


        private async void UpdateProject(object obj)
        {
            var project = obj as Project;
            await _navigation.PushAsync(new UpdateProjectPage(project), true);
        }

        private async void DisplayTask(object obj)
        {
            var requerimiento = obj as Project;
            await _navigation.PushAsync(new DisplayTaskPage(requerimiento), true);
        }

        private async void AddProject(object obj)
        {
            await _navigation.PushAsync(new AddProjectPage(), true);
        }

        private async void LoadProjects()
        {
            IsRefreshing = true;

            var response = await _apiService.GetListAsync<Project>(
                "/api", "/project", "Bearer", Preferences.Get("Token", String.Empty)
            );

            IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Info", "Session Expired", "Ok");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                await _navigation.PopToRootAsync();
            }
            else
            {
                ProjectsList = (List<Project>)response.Result;
                Projects = new ObservableCollection<Project>(ProjectsList);
            }
        }


        private void Search()
        {
            if (string.IsNullOrEmpty(Filter))
                Projects = new ObservableCollection<Project>(ProjectsList);
            else
                Projects = new ObservableCollection<Project>(
                    ProjectsList.Where(l => l.ProjectName.ToLower().Contains(Filter.ToLower())));
        }

    }
}
