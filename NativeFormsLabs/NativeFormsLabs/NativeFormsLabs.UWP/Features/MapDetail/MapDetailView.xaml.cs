namespace NativeFormsLabs.UWP.Features.MapDetail
{
    using ReactiveUI;
    using NativeFormsLabs.Core.Features.MapDetail;
    using NativeFormsLabs.Core.Services;
    using System;
    using System.Reactive.Linq;
    using Windows.Devices.Geolocation;
    using Windows.Foundation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Maps;

    public sealed partial class MapDetailView : Page, IViewFor<MapDetailViewModel>
	{
		public MapDetailView()
		{
			InitializeComponent();

			DataContext = ServiceLocator.Current.Resolve<MapDetailViewModel>();

			this.WhenActivated(d =>
			{
				d(this.WhenAny(v => v.ViewModel.IsLoading, x => x.Value).Subscribe(loading =>
				{
					LoadingRing.Visibility = loading ? Visibility.Visible : Visibility.Collapsed;
				}));
				d(this.WhenAny(v => v.ViewModel.CurrentCoordinates, x => x.Value).Where(v => v != null).Subscribe(center =>
				{
					Geopoint point = new Geopoint(new BasicGeoposition()
					{
						Latitude = center.Latitude,
						Longitude = center.Longitude,
						Altitude = center.Altitude
					});
					MapPosition.Center = point;
					if (center.Heading > -1)
						MapPosition.Heading = center.Heading;
					MapPosition.ZoomLevel = 17;
					MapPosition.DesiredPitch = 70;

					MapIcon myPOI = new MapIcon
					{
						Location = point,
						NormalizedAnchorPoint = new Point(0.5, 1.0),
						Title = "Aqui estamos!",
						ZIndex = 0
					};
					MapPosition.MapElements.Add(myPOI);
				}));

				ViewModel.GetCurrentCoordinatesCommand.Execute(null);
			});
		}

		public MapDetailViewModel ViewModel
		{
			get => (MapDetailViewModel)GetValue(DataContextProperty);
			set => SetValue(DataContextProperty, value);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (MapDetailViewModel)value;
		}
	}
}
