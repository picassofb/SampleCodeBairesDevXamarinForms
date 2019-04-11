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
	public partial class DisplayTaskPage : ContentPage
	{
		public DisplayTaskPage (Project project)
		{
			InitializeComponent ();
            BindingContext = new DisplayTaskViewModel(Navigation, project);
        }
	}
}