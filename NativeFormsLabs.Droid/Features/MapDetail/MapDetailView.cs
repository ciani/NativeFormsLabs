namespace NativeFormsLabs.Droid.Features.MapDetail
{
	using Android.App;
	using Android.OS;
	using ReactiveUI;
	using Core.Features.MapDetail;
	using Core.Services;
	using Android.Gms.Maps;
	using System;
    using System.Linq;
    using Android.Gms.Maps.Model;
	using System.Reactive.Linq;

	[Activity(Label = "MapDetailView")]
	public class MapDetailView : ReactiveActivity<MapDetailViewModel>, IOnMapReadyCallback
	{
		private Bundle savedInstanceState;
		private GoogleMap map;

		public MapDetailView()
		{
			this.WhenActivated(d =>
			{
				this.WireUpControls();
				MapPosition.OnCreate(savedInstanceState);
				MapPosition.OnResume();
				MapsInitializer.Initialize(this);
				MapPosition.GetMapAsync(this);

				d(this.WhenAny(v => v.ViewModel.CurrentCoordinates, x => x.Value).Where(v => v != null).Subscribe(center =>
				{
					LatLng location = new LatLng(center.Latitude, center.Longitude);
					CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
					builder.Target(location);
					builder.Zoom(18);
					if (center.Heading > -1)
						builder.Bearing((float)center.Heading/360.0f);
					builder.Tilt(65);
					CameraPosition cameraPosition = builder.Build();
					CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
					map.MoveCamera(cameraUpdate);

					MarkerOptions myPosition = new MarkerOptions();
					myPosition.SetPosition(location);
					myPosition.SetTitle("Aqui estamos!");
					map.AddMarker(myPosition);
				}));
			});
		}

		public MapView MapPosition { get; set; }

		public void OnMapReady(GoogleMap googleMap)
		{
			map = googleMap;
			map.MapType = GoogleMap.MapTypeHybrid;
			ViewModel.GetCurrentCoordinatesCommand.Execute(null);
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			this.savedInstanceState = savedInstanceState;
			SetContentView(NativeFormsLabs.Droid.Resource.Layout.MapDetail);

			ViewModel = ServiceLocator.Current.Resolve<MapDetailViewModel>();
		}
	}
}