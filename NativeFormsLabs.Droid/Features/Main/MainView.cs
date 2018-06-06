namespace NativeFormsLabs.Droid.Features.Main
{
	using Android.App;
	using Android.OS;
	using Android.Widget;
	using System;
	using System.Reactive.Linq;
	using ReactiveUI;
	using Core.Features.Main;
	using Core.Models;
	using Core.Services;
    using Xamarin.Forms.Platform.Android;
    using Xamarin.Forms;


    [Activity(Label = "MainView")]
	public class MainView : ReactiveActivity<MainViewModel>
	{
		public MainView()
		{
			
		}

        


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(NativeFormsLabs.Droid.Resource.Layout.Main);

            var mainPage = new NativeFormsLabs.Core.Features.Main.MainXFView();
            mainPage.BindingContext = ServiceLocator.Current.Resolve<MainViewModel>();

            FragmentManager
                .BeginTransaction()
                .Replace(NativeFormsLabs.Droid.Resource.Id.fragment_main_frame_layout, mainPage.CreateFragment(this))
                .Commit();

            ViewModel = ServiceLocator.Current.Resolve<MainViewModel>();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			var storageService = ServiceLocator.Current.Resolve<IStorageService>();
			storageService.Shutdown();
		}
	}
}