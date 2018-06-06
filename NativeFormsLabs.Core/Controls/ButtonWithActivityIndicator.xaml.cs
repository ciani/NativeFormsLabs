namespace NativeFormsLabs.Core.Controls
{
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ButtonWithActivityIndicator : ContentView
	{

        public static readonly BindableProperty TextToShowProperty = BindableProperty.CreateAttached(
                                    nameof(TextToShow),
                                    typeof(string),
                                    typeof(ButtonWithActivityIndicator),
                                    string.Empty);

        public static readonly BindableProperty IsLoadingProperty = BindableProperty.CreateAttached(
                                    nameof(IsLoading),
                                    typeof(bool),
                                    typeof(ButtonWithActivityIndicator),
                                    false, propertyChanged:IsLoadingChanged);

        public static readonly BindableProperty ActionCommandProperty = BindableProperty.CreateAttached(
                                    nameof(ActionCommand),
                                    typeof(ICommand),
                                    typeof(ButtonWithActivityIndicator),
                                    null, BindingMode.TwoWay);

      

        public ButtonWithActivityIndicator ()
		{
			InitializeComponent ();
            BindingContext = this;
		}

        public string TextToShow
        {
            get { return (string)GetValue(TextToShowProperty); }
            set { SetValue(TextToShowProperty, value); }
        }

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public ICommand ActionCommand
        {
            get { return (ICommand)GetValue(ActionCommandProperty); }
            set { SetValue(ActionCommandProperty, value); }
        }

        private static void IsLoadingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as ButtonWithActivityIndicator;
            if ((bool)newValue)
            {
                instance.TextToShow = string.Empty;
            }
        }
    }
}