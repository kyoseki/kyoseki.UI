using kyoseki.UI.Components.Theming;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;

namespace kyoseki.UI.Components
{
    [Themeable(nameof(UITheme.ForegroundColour), nameof(Colour))]
    [Themeable(nameof(UITheme.DefaultFont), nameof(Font))]
    public class ThemedText : SpriteText
    {
        [BackgroundDependencyLoader(true)]
        private void load(ThemeContainer themeContainer)
        {
            if (themeContainer != null)
                themeContainer.Register(this);
            else
                this.ApplyTheme(new KyosekiTheme());
        }
    }
}
