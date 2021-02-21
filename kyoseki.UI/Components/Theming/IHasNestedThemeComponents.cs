namespace kyoseki.UI.Components.Theming
{
    public interface IHasNestedThemeComponents
    {
        void ApplyThemeToChildren(UITheme theme, bool fade);
    }
}
