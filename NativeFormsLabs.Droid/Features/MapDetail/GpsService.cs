namespace NativeFormsLabs.Droid.Features.MapDetail
{
	using System.Threading.Tasks;
	using Core.Features.MapDetail;
	using Core.Models;
	using Android.Content;
	using Android.Content.PM;
	using Android.Locations;
	using Android.Support.Design.Widget;
	using Android.Support.V4.App;
	using Android.Support.V4.Content;
	using System;
	using Android;
	using Android.App;

	public class GpsService : IGpsService, ActivityCompat.IOnRequestPermissionsResultCallback
	{
		private const int PERMISSION_ID = 2;
		private string[] permissions = { Manifest.Permission.AccessCoarseLocation };
		private LocationManager locationManager;

		public GpsService()
		{
			if (ActivityCompat.CheckSelfPermission(Application.Context, Manifest.Permission.AccessCoarseLocation) != (int)Android.Content.PM.Permission.Granted)
			{
				RequestPhonePermissions();
			}
		}

		public IntPtr Handle
		{
			get { return default(IntPtr); }
		}

		public Task<CoordinateModel> GetCurrentPositionAsync()
		{
			TaskCompletionSource<CoordinateModel> tcs = new TaskCompletionSource<CoordinateModel>();
			CoordinateModel currentPosition = null;

			Permission locationPermission = ContextCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.AccessCoarseLocation);
			if (locationPermission == Permission.Granted)
			{
				locationManager = Application.Context.GetSystemService(Context.LocationService) as LocationManager;
				string provider = GetBestLocationProvider();
				var result = locationManager.GetLastKnownLocation(provider);
				if (result != null)
				{
					currentPosition = new CoordinateModel()
					{
						Latitude = result.Latitude,
						Longitude = result.Longitude,
						Altitude = result.Altitude
					};

					tcs.SetResult(currentPosition);
				}
				else
					tcs.SetResult(default(CoordinateModel));
			}

			return tcs.Task;
		}

		public void OnRequestPermissionsResult(int requestCode, string[] permissions, [global::Android.Runtime.GeneratedEnum] global::Android.Content.PM.Permission[] grantResults)
		{
		}
		public void Dispose()
		{
		}

		private void RequestPhonePermissions()
		{
			var currentActivity = (Application.Context as Activity);
			if (ActivityCompat.ShouldShowRequestPermissionRationale(currentActivity, Manifest.Permission.AccessCoarseLocation))
			{
				Snackbar.Make(currentActivity.FindViewById((Resource.Id.Content)), "App need location.", Snackbar.LengthIndefinite).SetAction("Ok", v =>
				{
					(Application.Context as Activity).RequestPermissions(permissions, PERMISSION_ID);
				}).Show();
			}
			else
			{
				ActivityCompat.RequestPermissions((Application.Context as Activity), permissions, PERMISSION_ID);
			}
		}

		private string GetBestLocationProvider()
		{
			Criteria providerSearchCriteria = new Criteria() { Accuracy = Accuracy.Coarse };
			return locationManager.GetBestProvider(providerSearchCriteria, false);
		}
	}
}