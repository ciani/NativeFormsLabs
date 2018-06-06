namespace NativeFormsLabs.UWP.Features.Login
{
    using NativeFormsLabs.Core.Features.Login;
    using NativeFormsLabs.Core.Services;
    using ReactiveUI;
    using Windows.UI.Xaml.Controls;
    using Xamarin.Forms.Platform.UWP;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginView : Page, IViewFor<LoginViewModel>
	{
		public LoginView()
		{
			InitializeComponent();

            var formPage = new Core.Features.Login.LoginView();
            formPage.BindingContext = ServiceLocator.Current.Resolve<LoginViewModel>();

            this.Content = formPage.CreateFrameworkElement();
   
		}

		public LoginViewModel ViewModel
		{
			get => (LoginViewModel)GetValue(DataContextProperty);
			set => SetValue(DataContextProperty, value);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (LoginViewModel)value;
		}
	}
}
