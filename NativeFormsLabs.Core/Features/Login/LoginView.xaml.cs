namespace NativeFormsLabs.Core.Features.Login
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
		public LoginView ()
		{
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent ();
		}
	}
}