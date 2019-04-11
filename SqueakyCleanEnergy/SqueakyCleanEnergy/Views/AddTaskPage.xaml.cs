using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqueakyCleanEnergy.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqueakyCleanEnergy.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddTaskPage : ContentPage
	{
		public AddTaskPage (Guid projectId)
		{
			InitializeComponent ();
            BindingContext = new AddTaskViewModel(Navigation, projectId);
        }
	}
}