namespace NativeFormsLabs.UWP.Features.Main
{
    using NativeFormsLabs.Core.Features.Login;
    using NativeFormsLabs.Core.Features.Main;
    using NativeFormsLabs.Core.Services;
    using ReactiveUI;
    using Xamarin.Forms.Platform.UWP;

    public sealed partial class MainView : Windows.UI.Xaml.Controls.Page, IViewFor<MainViewModel>
	{
		public MainView()
		{            
            InitializeComponent();

            LoginView loginPage = new Core.Features.Login.LoginView();
            loginPage.BindingContext = ServiceLocator.Current.Resolve<LoginViewModel>();

            MainXFView formPage = new Core.Features.Main.MainXFView();
            formPage.BindingContext = ServiceLocator.Current.Resolve<MainViewModel>();


            LeftGrid.Children.Add(loginPage.CreateFrameworkElement());
            RightGrid.Children.Add(formPage.CreateFrameworkElement());
        }

		public MainViewModel ViewModel
		{
			get => (MainViewModel)GetValue(DataContextProperty);
			set => SetValue(DataContextProperty, value);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (MainViewModel)value;
		}
	}
}
