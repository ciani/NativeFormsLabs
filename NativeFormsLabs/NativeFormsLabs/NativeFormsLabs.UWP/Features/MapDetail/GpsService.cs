namespace NativeFormsLabs.UWP.Features.MapDetail
{
	using System;
	using Core.Features.MapDetail;
	using System.Threading.Tasks;
	using Core.Models;
	using Windows.Devices.Geolocation;

	public class GpsService : IGpsService
	{
		public async Task<CoordinateModel> GetCurrentPositionAsync()
		{
			CoordinateModel currentPosition = new CoordinateModel();

			var accessResult = await Geolocator.RequestAccessAsync();
			if (accessResult == GeolocationAccessStatus.Allowed)
			{
				Geolocator geolocator = new Geolocator() { DesiredAccuracy = PositionAccuracy.Default };
				Geoposition geoPosition = await geolocator.GetGeopositionAsync();
				if (geoPosition?.Coordinate?.Point != null)
				{
					currentPosition.Latitude = geoPosition.Coordinate.Point.Position.Latitude;
					currentPosition.Longitude = geoPosition.Coordinate.Point.Position.Longitude;
					currentPosition.Altitude = geoPosition.Coordinate.Point.Position.Altitude;
					if (geoPosition.Coordinate.Heading.HasValue)
						currentPosition.Heading = geoPosition.Coordinate.Heading.Value;
				}
			}

			return currentPosition;
		}
	}
}
