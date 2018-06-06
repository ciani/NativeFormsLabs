namespace NativeFormsLabs.iOS.Features.Main
{
	using ReactiveUI;
	using Core.Models;
	using System;
	using NativeFormsLabs.Core.Features.Main;
	using Foundation;
	using UIKit;

	public class ItemCell : ReactiveTableViewCell<ItemModel>
	{
		public ItemCell() : base()
		{
		}

		public ItemCell(IntPtr handle) : base(handle)
		{
		}

		public void Initialize()
		{
			this.Bind(ViewModel, vm => vm.Name, v => v.TextLabel.Text);
		}
	}
}
