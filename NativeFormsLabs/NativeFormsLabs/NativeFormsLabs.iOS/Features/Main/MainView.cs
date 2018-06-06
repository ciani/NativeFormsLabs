namespace NativeFormsLabs.iOS.Features.Main
{
	using ReactiveUI;
	using Core.Features.Main;
	using Core.Models;
	using Core.Services;
	using System;
	using UIKit;
	using Foundation;
	using UIKit.Rx;

	public partial class MainView : ReactiveViewController<MainViewModel>
	{
		public MainView() : base("MainView", null)
		{
			this.WhenActivated(d =>
			{
				var tvd = new UITableViewDelegateRx();

				d(ViewModel.WhenAnyValue(vm => vm.ItemsList).BindTo<ItemModel, ItemCell>(LstItems, 42, cell => cell.Initialize()));

				d(this.WhenAny(v => v.ViewModel.IsLoading, x => x.Value).Subscribe(loading =>
				{
					LoadingRing.Hidden = !loading;
				}));
				d(tvd.RowSelectedObs.Subscribe(c =>
				{
					if (c.Item2.Row > -1)
					{
						ViewModel.SelectedItem = ViewModel.ItemsList[c.Item2.Row];
						ViewModel.GoToDetailCommand.Execute(null);
					}
				}));
				LstItems.Delegate = tvd;

				d(ViewModel.ErrorLoadingData.RegisterHandler(async (i) =>
				{
					var alert = new UIAlertController()
					{
						Title = "warning",
						Message = i.Input
					};
					alert.AddAction(UIAlertAction.Create("ok", UIAlertActionStyle.Default, null));
					await PresentViewControllerAsync(alert, true);
					i.SetOutput(true);
				}));

				ViewModel.LoadItemsCommand.Execute(null);
			});
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			ViewModel = ServiceLocator.Current.Resolve<MainViewModel>();
			NavigationController.SetNavigationBarHidden(true, false);
		}

		public void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath != null)
			{
				ItemCell cell = (ItemCell)LstItems.DataSource.GetCell(LstItems, indexPath);
				ViewModel.SelectedItem = cell.ViewModel;
				ViewModel.GoToDetailCommand.Execute(null);
			}
		}
	}
}