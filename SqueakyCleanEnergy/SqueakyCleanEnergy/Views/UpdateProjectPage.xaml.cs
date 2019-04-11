using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqueakyCleanEnergy.Models;
using SqueakyCleanEnergy.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqueakyCleanEnergy.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdateProjectPage : ContentPage
	{
		public UpdateProjectPage (Project projectToUpdate)
		{
			InitializeComponent ();
            BindingContext = new UpdateProjectViewModel(Navigation, projectToUpdate);

        }
    }
}