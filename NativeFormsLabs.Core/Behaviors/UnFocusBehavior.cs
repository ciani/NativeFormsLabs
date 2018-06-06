namespace NativeFormsLabs.Core.Behaviors
{
    using Xamarin.Forms;

    public class UnFocusBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.Unfocused += Entry_Unfocused;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.Unfocused -= Entry_Unfocused;
            base.OnDetachingFrom(entry);
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            entry.FadeTo(0.5, 1000, Easing.CubicOut);
        }
    }
}
