
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NativeFormsLabs.Core.Features.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainXFView : ContentPage
	{
		public MainXFView ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = (MainViewModel)BindingContext;
            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}