using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using SqueakyCleanEnergy.Models;
using SqueakyCleanEnergy.Services;
using SqueakyCleanEnergy.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SqueakyCleanEnergy.ViewModels
{
    class DisplayTaskViewModel: BaseViewModel
    {
        #region Properties and Services

        private readonly INavigation _navigation;
        private readonly ApiService _apiService = new ApiService();

        public ICommand RefreshCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }

        private List<ProjectTask> ProjectTasksList { get; set; }

        private ObservableCollection<ProjectTask> _projectTasks;

        public ObservableCollection<ProjectTask> ProjectTasks
        {
            get => _projectTasks;
            set { _projectTasks = value; OnPropertyChanged(); }
        }


        private string _projectName;

        public string ProjectName
        {
            get => _projectName;
            set { _projectName = value; OnPropertyChanged(); }
        }

        public Guid ProjectId { get; set; }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set { _isRefreshing = value; OnPropertyChanged(); }
        }

        #endregion

        public DisplayTaskViewModel(INavigation navigation, Project project)
        {
            ProjectTasks = new ObservableCollection<ProjectTask>();

            RefreshCommand = new Command(LoadTasks);
            AddTaskCommand = new Command(AddTask);

            ProjectName = project.ProjectName;
            ProjectId = project.ProjectId;

            LoadTasks();

            // Suscribe to MessagingCenter to get notify when an event happens in a different view,
            // in this case when a task is created LoadTasks is called to apply the changes to the ListView in the view
            MessagingCenter.Subscribe<string>(this, "UpdateListProjectTask", (value) =>
            {
                LoadTasks();
            });

            IsRefreshing = false;
            _navigation = navigation;
        }

        private async void AddTask()
        {
            await _navigation.PushAsync(new AddTaskPage(ProjectId), true);
        }

        private async void LoadTasks()
        {
            IsRefreshing = true;

            var response = await _apiService.GetListAsync<ProjectTask>(
                "/api/project", $"/{ProjectId}/projecttask", "Bearer", Preferences.Get("Token", String.Empty)
            );

            IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            ProjectTasksList = (List<ProjectTask>)response.Result;
            ProjectTasks = new ObservableCollection<ProjectTask>(ProjectTasksList);
        }
    }
}
