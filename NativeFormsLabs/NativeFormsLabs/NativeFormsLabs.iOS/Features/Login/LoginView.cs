namespace NativeFormsLabs.iOS.Features.Login
{
	using ReactiveUI;
	using Core.Features.Login;
	using Core.Services;
	using System;
	using UIKit;

	public partial class LoginView : ReactiveViewController<LoginViewModel>
	{
		public LoginView() : base("LoginView", null)
		{
			this.WhenActivated(d =>
			{
				d(this.Bind(ViewModel, vm => vm.Username, v => v.TxtUsername.Text));
				d(this.Bind(ViewModel, vm => vm.Password, v => v.TxtPassword.Text));
				d(this.BindCommand(ViewModel, vm => vm.DoLoginCommand, v => v.BtnLogin));

				d(this.WhenAny(v => v.ViewModel.IsLoading, x => x.Value).Subscribe(loading =>
				{
					LoadingIndicator.Hidden = !loading;
					TxtPassword.Enabled = !loading;
					TxtUsername.Enabled = !loading;
				}));
				d(ViewModel.ErrorOnLogin.RegisterHandler(async (i) =>
				{
					var alert = new UIAlertController()
					{
						Title = "warning",
						Message = i.Input
					};
					alert.AddAction(UIAlertAction.Create("ok", UIAlertActionStyle.Default, null));
					await PresentViewControllerAsync(alert, true);
					i.SetOutput(true);
				}));
			});
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			ViewModel = ServiceLocator.Current.Resolve<LoginViewModel>();

			this.NavigationController.SetNavigationBarHidden(true, false);
		}
	}
}