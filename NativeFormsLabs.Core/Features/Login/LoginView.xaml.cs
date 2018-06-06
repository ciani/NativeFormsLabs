namespace NativeFormsLabs.Core.Features.Login
{
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
		public LoginView ()
		{
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent ();
            
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            MainStack.TranslationY = 640;
            if (Device.RuntimePlatform == Device.Android)
                await Task.Delay(100);

            //Button.FadeTo(1, 3000, Easing.CubicIn);
            //LblEntry.RotateTo(180, 250, Easing.CubicIn);
            //Button.ScaleTo(10);
            MainStack.TranslateTo(0, 0,500, Easing.SpringOut);


            var parentAnimacion = new Animation();


            var fadetoAnimation = new Animation(v => Button.Opacity = v, 0, 1, Easing.BounceOut);
            var rotateToAnimation = new Animation(v => LblEntry.FontSize = v, 14, 50, Easing.SpringIn);

            parentAnimacion.Add(0,1,fadetoAnimation);
            parentAnimacion.Add(0.5,1, rotateToAnimation);


            parentAnimacion.Commit(this, "myAnimation", 16, 3000, Easing.CubicIn);






        }
    }
}