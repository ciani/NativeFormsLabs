namespace NativeFormsLabs.iOS.Features.MapDetail
{
    using Core.Features.MapDetail;
    using Core.Services;
    using CoreLocation;
    using MapKit;
    using ReactiveUI;
    using System;
    using System.Reactive.Linq;

    public partial class MapDetailView : ReactiveViewController<MapDetailViewModel>
	{
		public MapDetailView() : base("MapDetailView", null)
		{
			this.WhenActivated(d =>
			{
				d(this.WhenAny(v => v.ViewModel.IsLoading, x => x.Value).Subscribe(loading =>
				{
					LoadingRing.Hidden = !loading;
				}));

				d(this.WhenAny(v => v.ViewModel.CurrentCoordinates, x => x.Value).Where(v => v != null).Subscribe(center =>
				{
					var coordinate = new CLLocationCoordinate2D(center.Latitude, center.Longitude);
					MapPosition.AddAnnotations(new MKPointAnnotation()
					{
						Title = "Aqui estamos!",
						Coordinate = coordinate
					});
					MapPosition.SetCamera(new MKMapCamera()
					{
						CenterCoordinate = coordinate,
						Heading = center.Heading < 0 ? 0 : center.Heading,
						Altitude = 75,
						Pitch = 75
					}, true);
				}));

				ViewModel.GetCurrentCoordinatesCommand.Execute(null);
			});
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ViewModel = ServiceLocator.Current.Resolve<MapDetailViewModel>();

			this.NavigationController.SetNavigationBarHidden(false, false);
		}
	}
}