namespace NativeFormsLabs.UWP.Services
{
    using NativeFormsLabs.Core.Features.Main;
    using NativeFormsLabs.Core.Services;
    using UWP.Features.Main;
    using UWP.Features.MapDetail;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class NavigationService : INavigationService
	{
		private Frame RootFrame
		{
			get
			{
				return Window.Current.Content as Frame;
			}
		}

		public NavigationService()
		{
		}

		public void NavigateToMainScreenFromLogin()
		{
            RootFrame.Navigate(typeof(MainView));
		}

		public void NavigateToUserDetail()
		{
			RootFrame.Navigate(typeof(MapDetailView));
		}
	}
}
