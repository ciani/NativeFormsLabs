namespace NativeFormsLabs.Core.Features.Main
{
	using ReactiveUI;
	using Models;
	using Services;
	using System.Threading.Tasks;
	using System.Reactive.Linq;
	using System.Windows.Input;

	public class MainViewModel : ReactiveObject
    {
		private readonly IMainWebService mainService;
		private readonly INavigationService navService;
		private readonly IStorageService storageService;

		private ObservableAsPropertyHelper<bool> isLoading;
		private ReactiveList<ItemModel> itemsList;
		private ItemModel selectedItem;
		private ReactiveCommand loadItemsCommand;
		private ReactiveCommand goToDetailCommand;

		private readonly Interaction<string, bool> errorLoadingData;

		public MainViewModel(IMainWebService mainService, INavigationService navService, IStorageService storageService)
		{
			this.mainService = mainService;
			this.navService = navService;
			this.storageService = storageService;
			errorLoadingData = new Interaction<string, bool>();
			itemsList = new ReactiveList<ItemModel>();
			SetupCommands();
		}

		public ReactiveList<ItemModel> ItemsList
		{
			get => itemsList;
			set => this.RaiseAndSetIfChanged(ref itemsList, value);
		}

		public ItemModel SelectedItem
		{
			get => selectedItem;
			set => this.RaiseAndSetIfChanged(ref selectedItem, value);
		}

		public bool IsLoading => isLoading?.Value ?? false;

		public ICommand LoadItemsCommand => loadItemsCommand;

		public ICommand GoToDetailCommand => goToDetailCommand;

		public Interaction<string, bool> ErrorLoadingData => errorLoadingData;

		private void SetupCommands()
		{
			loadItemsCommand = ReactiveCommand.CreateFromTask(LoadItemsAsync);
			loadItemsCommand.IsExecuting.ToProperty(this, vm => vm.IsLoading, out isLoading);
			goToDetailCommand = ReactiveCommand.Create(GoToDetailExecute);
		}

		private async Task LoadItemsAsync()
		{
			var response = await storageService.GetOrFetchObjectAsync("Items", async () =>
			{
				var itemsResponse = await mainService.GetItemsAsync();
				await storageService.SaveAsync("Items", itemsResponse, new System.TimeSpan(0, 30, 0));
				return itemsResponse;
			});

			if (response.Data != null)
				ItemsList = new ReactiveList<ItemModel>(response.Data);
			else
				await errorLoadingData.Handle("can't connect to skynet right now...");
		}

		private void GoToDetailExecute()
		{
			navService.NavigateToUserDetail();
		}
	}
}
