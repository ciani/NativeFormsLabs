namespace NativeFormsLabs.Core.Behaviors
{
    using Xamarin.Forms;
    public class OnFocusBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.Focused += Entry_Focused;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.Focused -= Entry_Focused;
            base.OnDetachingFrom(entry);
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            entry.FadeTo(1, 1000, Easing.CubicOut);
        }
    }
}
