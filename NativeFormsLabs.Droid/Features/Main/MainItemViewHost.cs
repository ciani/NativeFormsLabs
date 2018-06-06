namespace NativeFormsLabs.Droid.Features.Main
{
	using Android.Content;
	using Android.Views;
	using Android.Widget;
	using ReactiveUI;
	using Core.Models;

	public class MainItemViewHost : ReactiveViewHost<ItemModel>
	{
		public MainItemViewHost(ItemModel item, Context context, ViewGroup parent) 
			: base(context, NativeFormsLabs.Droid.Resource.Layout.MainItemView, parent)
		{
			ViewModel = item;
			BindViewsToViewModel();
		}

		public LinearLayout MainItemViewLayout { get; private set; }
		public TextView TxtName { get; private set; }
		public TextView TxtColor { get; private set; }

		private void BindViewsToViewModel()
		{
			this.OneWayBind(ViewModel, vm => vm.Name, view => view.TxtName.Text);
			this.OneWayBind(ViewModel, vm => vm.Color, view => view.TxtColor.Text);
		}
	}
}