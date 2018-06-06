namespace NativeFormsLabs.Core.Features.MapDetail
{
	using ReactiveUI;
	using Core.Models;
	using System.Windows.Input;
	using System.Threading.Tasks;

	public class MapDetailViewModel : ReactiveObject
    {
		private IGpsService gpsService;

		private CoordinateModel currentCoordinates;
		private ObservableAsPropertyHelper<bool> isLoading;

		private ReactiveCommand getCurrentCoordinatesCommand;
		
		public MapDetailViewModel(IGpsService gpsService)
		{
			this.gpsService = gpsService;
			SetupCommands();
		}

		public CoordinateModel CurrentCoordinates
		{
			get => currentCoordinates;
			set => this.RaiseAndSetIfChanged(ref currentCoordinates, value);
		}

		public bool IsLoading => isLoading?.Value ?? false;

		public ICommand GetCurrentCoordinatesCommand => getCurrentCoordinatesCommand;

		private void SetupCommands()
		{
			getCurrentCoordinatesCommand = ReactiveCommand.CreateFromTask(GetCurrentCoordinates);
			getCurrentCoordinatesCommand.IsExecuting.ToProperty(this, vm => vm.IsLoading, out isLoading);
		}

		private async Task GetCurrentCoordinates()
		{
			CurrentCoordinates = await gpsService.GetCurrentPositionAsync();
		}
	}
}
