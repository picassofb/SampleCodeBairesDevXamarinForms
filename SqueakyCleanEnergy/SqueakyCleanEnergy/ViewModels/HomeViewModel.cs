using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using SqueakyCleanEnergy.Models;
using SqueakyCleanEnergy.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SqueakyCleanEnergy.ViewModels
{
    class HomeViewModel: BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly ApiService _apiService = new ApiService();
        private List<Project> ProjectsList { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand CreateTaskCommand { get; set; }


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

     


        public HomeViewModel(INavigation navigation)
        {
            Projects = new ObservableCollection<Project>();
            SearchCommand = new Command(Search);
            RefreshCommand = new Command(LoadProjects);
            CreateTaskCommand = new Command(CreateTask);

            LoadProjects();

            IsEnabled = true;
            IsRefreshing = false;
            _navigation = navigation;
        }

        private void CreateTask()
        {
            throw new NotImplementedException();
        }

        private async void LoadProjects()
        {
            IsRefreshing = true;

            var response = await _apiService.GetListAsync<Project>(
                "/api", "/project", "Bearer", Preferences.Get("Token", String.Empty)
            );

            if (response.IsSuccess)
            {
                ProjectsList = (List<Project>)response.Result;
                Projects = new ObservableCollection<Project>(ProjectsList);
            }


            await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");

            IsRefreshing = false;
        }


        private void Search()
        {
            throw new NotImplementedException();
        }

    }
}
