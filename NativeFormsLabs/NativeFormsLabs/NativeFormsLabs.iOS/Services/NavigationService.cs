namespace NativeFormsLabs.iOS.Services
{
	using Core.Services;
	using iOS.Features.Main;
	using iOS.Features.MapDetail;
    using NativeFormsLabs.Core.Features.Main;
    using UIKit;
    using Xamarin.Forms;

	public class NavigationService : INavigationService
	{
		private UINavigationController RootController
		{
			get
			{
				var window = UIApplication.SharedApplication.KeyWindow;
				return window.RootViewController as UINavigationController;
			}
		}

		public void NavigateToMainScreenFromLogin()
		{
            var page = new NativeFormsLabs.Core.Features.Main.MainXFView();
            page.BindingContext = ServiceLocator.Current.Resolve<MainViewModel>();

            RootController.PushViewController(page.CreateViewController(), true);
		}

		public void NavigateToUserDetail()
		{
			RootController.PushViewController(new MapDetailView(), true);
		}
	}
}
