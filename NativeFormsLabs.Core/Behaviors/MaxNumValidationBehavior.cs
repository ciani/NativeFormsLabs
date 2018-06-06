namespace NativeFormsLabs.Core.Behaviors
{
    using Xamarin.Forms;

    public class MaxNumValidationBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty MinValueProperty = BindableProperty.CreateAttached(
                                    nameof(MinValue),
                                    typeof(int),
                                    typeof(MaxNumValidationBehavior),
                                    0);

        public static readonly BindableProperty MaxValueProperty = BindableProperty.CreateAttached(
                                    nameof(MaxValue),
                                    typeof(int),
                                    typeof(MaxNumValidationBehavior),
                                    0);

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += Entry_TextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= Entry_TextChanged;
            base.OnDetachingFrom(entry);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            double result;
            bool isValid = double.TryParse(e.NewTextValue, out result);
            if (isValid)
            {
                if (result < MinValue || result > MaxValue)
                    isValid = false;
            }
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
