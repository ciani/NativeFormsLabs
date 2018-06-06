namespace NativeFormsLabs.Droid.Services
{
    using Android.App;
    using Android.Content;
    using Core.Services;
    using NativeFormsLabs.Droid.Features.Main;
    using NativeFormsLabs.Droid.Features.MapDetail;

    public class NavigationService : INavigationService
	{
		public void NavigateToMainScreenFromLogin()
		{
			var mainIntent = new Intent(Application.Context, typeof(MainView));
			mainIntent.AddFlags(ActivityFlags.NewTask);
			Application.Context.StartActivity(mainIntent);
		}

		public void NavigateToUserDetail()
		{
			var mainIntent = new Intent(Application.Context, typeof(MapDetailView));
			mainIntent.AddFlags(ActivityFlags.NewTask);
			Application.Context.StartActivity(mainIntent);
		}
	}
}