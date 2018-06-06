namespace NativeFormsLabs.Droid.Features.Login
{
    using Android.App;
    using Android.OS;
    using Android.Widget;
    using Core.Features.Login;
    using Core.Features.MapDetail;
    using Core.Services;
    using Droid.Features.MapDetail;
    using Droid.Services;
    using ReactiveUI;
    using System.Net.Http;
    using System;
    using Xamarin.Android.Net;
    using Xamarin.Forms.Platform.Android;
    using Xamarin.Forms;

    [Activity(Label = "LoginView", MainLauncher = true)]
	public class LoginView : ReactiveActivity<LoginViewModel>
	{
		public LoginView()
		{
			//this.WhenActivated(d =>
			//{
			//	this.WireUpControls();

			//	d(this.Bind(ViewModel, vm => vm.Username, v => v.TxtUsername.Text));
			//	d(this.Bind(ViewModel, vm => vm.Password, v => v.TxtPassword.Text));
			//	d(this.BindCommand(ViewModel, vm => vm.DoLoginCommand, v => v.BtnLogin));

			//	d(this.WhenAny(v => v.ViewModel.IsLoading, x => x.Value).Subscribe(loading =>
			//	{
			//		TxtPassword.Enabled = !loading;
			//		TxtUsername.Enabled = !loading;
			//	}));

			//	d(ViewModel.ErrorOnLogin.RegisterHandler((i) =>
			//	{
			//		AlertDialog.Builder builder = new AlertDialog.Builder(this);
			//		builder.SetTitle("warning")
			//			   .SetMessage(i.Input)
			//			   .SetCancelable(false);

			//		builder.SetPositiveButton("ok", (s, args) => {});
			//		i.SetOutput(true);
			//		builder.Create().Show();
			//	}));
			//});
		}
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            CustomRenderers.CustomButtonRenderer.Init();
            Forms.Init(this, savedInstanceState);
            

            ServiceLocator.Current.RegisterDependency<INavigationService, NavigationService>();
            ServiceLocator.Current.RegisterDependency<HttpMessageHandler, AndroidClientHandler>();
            ServiceLocator.Current.RegisterDependency<IGpsService, GpsService>();
            ServiceLocator.Current.BuildContainer();

            SetContentView(NativeFormsLabs.Droid.Resource.Layout.Login);
            //ViewModel = ServiceLocator.Current.Resolve<LoginViewModel>();

            var mainPage = new NativeFormsLabs.Core.Features.Login.LoginView();
            mainPage.BindingContext = ServiceLocator.Current.Resolve<LoginViewModel>();
            var mainPageFragment = mainPage.CreateFragment(this);

            FragmentManager
                .BeginTransaction()
                .Replace(NativeFormsLabs.Droid.Resource.Id.fragment_frame_layout, mainPageFragment)
                .Commit();
        }

		public EditText TxtUsername { get; set; }

		public EditText TxtPassword { get; set; }

		public Android.Widget.Button BtnLogin { get; set; }
	}
}